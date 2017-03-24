using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace Projekt
{
	public class Okno:Form
	{
		ListViewHitTestInfo hitinfo;
		public TextBox editbox=new TextBox();
		public static StatusBar sb = new StatusBar ();
		public static ListView lv = new ListView();
		MenuStrip ms = new MenuStrip ();

		public Okno (){
			Text = "Projekt";
			Size = new Size(480,360);
			//this.BackgroundImage = new Bitmap ();
			ToolStripMenuItem plik = new ToolStripMenuItem ("Plik");				ms.Items.Add (plik);
			ToolStripMenuItem obliczenia = new ToolStripMenuItem ("Obliczenia");	ms.Items.Add (obliczenia);

			ToolStripMenuItem import = new ToolStripMenuItem ("Import"); 		import.ShortcutKeys = Keys.Control | Keys.I;		plik.DropDownItems.Add (import);
			ToolStripMenuItem odczyt = new ToolStripMenuItem ("Odczyt"); 		odczyt.ShortcutKeys = Keys.Control | Keys.O;		plik.DropDownItems.Add (odczyt);
			ToolStripMenuItem zapis = new ToolStripMenuItem ("Zapis"); 			zapis.ShortcutKeys = Keys.Control | Keys.S;			plik.DropDownItems.Add (zapis);
			ToolStripMenuItem zamknij = new ToolStripMenuItem ("Zamknij plik"); zamknij.ShortcutKeys = Keys.Control | Keys.C;		plik.DropDownItems.Add (zamknij);
			ToolStripMenuItem wyjscie = new ToolStripMenuItem ("Wyjście"); 		wyjscie.ShortcutKeys = Keys.Control | Keys.X;		plik.DropDownItems.Add (wyjscie);

			ToolStripMenuItem srednia = new ToolStripMenuItem ("Średnia");		obliczenia.DropDownItems.Add (srednia);
			ToolStripMenuItem suma = new ToolStripMenuItem ("Suma");			obliczenia.DropDownItems.Add (suma);
			ToolStripMenuItem mediana = new ToolStripMenuItem ("Mediana");		obliczenia.DropDownItems.Add (mediana);

			ToolStripMenuItem temp;
			temp = new ToolStripMenuItem ("NO2"); 			temp.Click += (object sender, EventArgs e) => {Metody.Srednia("NO2");};		srednia.DropDownItems.Add (temp);
			temp = new ToolStripMenuItem ("NO2");			temp.Click += (object sender, EventArgs e) => {Metody.Suma ("NO2");};		suma.DropDownItems.Add (temp); 
			temp = new ToolStripMenuItem ("NO2");			temp.Click += (object sender, EventArgs e) => {Metody.Mediana ("NO2");};	mediana.DropDownItems.Add (temp);

			temp = new ToolStripMenuItem ("SO2"); 			temp.Click += (object sender, EventArgs e) => {Metody.Srednia("SO2");};		srednia.DropDownItems.Add (temp); 
			temp = new ToolStripMenuItem ("SO2");			temp.Click += (object sender, EventArgs e) => {Metody.Suma ("SO2");};		suma.DropDownItems.Add (temp); 
			temp = new ToolStripMenuItem ("SO2");			temp.Click += (object sender, EventArgs e) => {Metody.Mediana ("NO2");};	mediana.DropDownItems.Add (temp);

			temp = new ToolStripMenuItem ("CO"); 			temp.Click += (object sender, EventArgs e) => {Metody.Srednia("CO");};		srednia.DropDownItems.Add (temp);
			temp = new ToolStripMenuItem ("CO");			temp.Click += (object sender, EventArgs e) => {Metody.Suma ("CO");};		suma.DropDownItems.Add (temp); 
			temp = new ToolStripMenuItem ("CO");			temp.Click += (object sender, EventArgs e) => {Metody.Mediana ("CO");};		mediana.DropDownItems.Add (temp);

			temp = new ToolStripMenuItem ("PM2,5");			temp.Click += (object sender, EventArgs e) => {Metody.Srednia("PM25");};	srednia.DropDownItems.Add (temp); 
			temp = new ToolStripMenuItem ("PM2,5");			temp.Click += (object sender, EventArgs e) => {Metody.Suma ("PM25");};		suma.DropDownItems.Add (temp); 
			temp = new ToolStripMenuItem ("PM2,5");			temp.Click += (object sender, EventArgs e) => {Metody.Mediana ("PM25");};	mediana.DropDownItems.Add (temp);

			temp = new ToolStripMenuItem ("PM10");			temp.Click += (object sender, EventArgs e) => {Metody.Srednia("PM10");};	srednia.DropDownItems.Add (temp); 
			temp = new ToolStripMenuItem ("PM10");			temp.Click += (object sender, EventArgs e) => {Metody.Suma ("PM10");};		suma.DropDownItems.Add (temp); 
			temp = new ToolStripMenuItem ("PM10");			temp.Click += (object sender, EventArgs e) => {Metody.Mediana ("PM10");};	mediana.DropDownItems.Add (temp);

			import.Click += Import;
			odczyt.Click += Odczyt;
			zapis.Click += Zapis;
			zamknij.Click += Zamknij;
			wyjscie.Click += OnExit;

			Controls.Add (ms);
			MainMenuStrip = ms;
			ms.Dock = DockStyle.Top;

			ColumnHeader godzina = new ColumnHeader ();
			godzina.Text = "Godzina";
			ColumnHeader no2 = new ColumnHeader ();
			no2.Text = "NO2";
			ColumnHeader so2 = new ColumnHeader ();
			so2.Text = "SO2";
			ColumnHeader co = new ColumnHeader ();
			co.Text = "CO";
			ColumnHeader pm25 = new ColumnHeader ();
			pm25.Text = "PM2,5";
			ColumnHeader pm10 = new ColumnHeader ();
			pm10.Text = "PM10";

			SuspendLayout ();
			lv.Hide ();
			lv.Parent = this;
			lv.FullRowSelect = true;
			lv.GridLines = true;
			lv.BringToFront ();
			lv.Columns.AddRange (new ColumnHeader[] {godzina, no2, so2, co, pm25, pm10 });
			lv.Dock = DockStyle.Fill;
			lv.View = View.Details;

			ResumeLayout ();

			sb.Parent = this;
			sb.Text = "Gotowy";

			editbox.Parent = lv;
			editbox.Hide ();
			editbox.LostFocus += lvLostFocus;
			lv.MouseDoubleClick += lvDoubleClick;
			lv.MouseClick += lvClick;

			CenterToScreen();

		}

		void OnExit(object sender, EventArgs e) { Close ();}

		void Import(object sender, EventArgs e) {
			lv.Items.Clear ();
			Metody.d.Clear ();
			OpenFileDialog dialog = new OpenFileDialog ();
			dialog.Filter = "Pliki tekstowe (*.txt)|*.txt";
			if(dialog.ShowDialog(this)==DialogResult.OK)
			{
				Metody.Import (dialog.FileName);
				rys_tab ();
				sb.Text = "Zaimportowano";
			}
			lv.Show ();
			Controls.Add (lv);
			lv.BringToFront ();
		}

		void Zapis(object sender, EventArgs e) {
			SaveFileDialog dialog = new SaveFileDialog ();
			dialog.Filter = "Pliki binarne (*.bin)|*.bin";
			if (dialog.ShowDialog (this) == DialogResult.OK) 
			{
				Metody.Zapis (dialog.FileName);
				sb.Text = "Zapisano dane";
			}
		}

		void Odczyt(object sender, EventArgs e){
			lv.Items.Clear ();
			Metody.d.Clear ();
			OpenFileDialog dialog = new OpenFileDialog ();
			dialog.Filter = "Pliki binarne (*.bin)|*.bin";
			if (dialog.ShowDialog (this) == DialogResult.OK) 
			{
				Metody.Odczyt (dialog.FileName);
				rys_tab ();
				sb.Text = "Wczytano dane";
			}
			Controls.Add (lv);
			lv.Show ();
			lv.BringToFront ();
		}

		void Zamknij(object sender, EventArgs e){
			lv.Hide ();
			Controls.Remove (lv);
			lv.Items.Clear ();
			Metody.d.Clear();
			sb.Text = "Zamknięto plik";
		}

		void rys_tab(){
			foreach (Dane d in Metody.d) {
				ListViewItem item = new ListViewItem();
				item.Text = d.godzina;
				item.SubItems.Add (d.NO2.ToString ()+" μm"); //norma 200 μm
				item.SubItems.Add (d.SO2.ToString ()+" μm");//norma 350 μg
				item.SubItems.Add (d.CO.ToString ()+" μm");//norma 10000 μg
				item.SubItems.Add (d.PM25.ToString ()+" μm");//norma 25 μg
				item.SubItems.Add (d.PM10.ToString ()+" μm");//norma 50 μg
				lv.Items.Add (item);
			}
		}

		void lvDoubleClick(object sender, MouseEventArgs e){
			editbox.Parent = lv;
			hitinfo = lv.HitTest(e.X, e.Y);
			editbox.Bounds = hitinfo.SubItem.Bounds;
			editbox.Text = hitinfo.SubItem.Text;
			Controls.Add (editbox);
			editbox.BringToFront ();
			editbox.Show ();
			editbox.Focus();
		}

		void lvLostFocus(object sender, EventArgs e){
			float l;
			if(float.TryParse(editbox.Text,out l))
			{
				hitinfo.SubItem.Text = editbox.Text;
				Metody.Aktuaizuj(hitinfo.Item.Index);
			}
			Controls.Remove (editbox);
			editbox.Hide ();
			editbox.SendToBack ();
		}
		void lvClick(object sender, MouseEventArgs e)
		{
			hitinfo = lv.HitTest (e.X, e.Y);
			wypiszDane (hitinfo.Item.Index);
		}
		void wypiszDane (int wsk)
		{
			sb.Text = "NO2: "+Math.Round(100*Metody.d[wsk].NO2/200*100)/100+"%, SO2: "
				+Math.Round(100*Metody.d[wsk].SO2/350*100)/100+"%, CO: "
				+Math.Round(100*Metody.d[wsk].CO/10000*100)/100+"%, PM2,5: "
				+Math.Round(100*Metody.d[wsk].PM25/25*100)/100+"%, PM10: "
				+Math.Round(100*Metody.d[wsk].PM10/50*100)/100+"%";
		}
	}
}

