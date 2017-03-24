using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

namespace Projekt
{
	public static class Metody
	{
		public static List<Dane> d=new List<Dane>();
		public static void Import (string sciezka="dane.txt"){
			string dane="";
			string[] lista=new string[6];
			int licznik = 0;

			FileStream fs = new FileStream (sciezka, FileMode.Open, FileAccess.Read);
			try
			{
				StreamReader sr=new StreamReader(fs);
				while(!sr.EndOfStream)
				{
					licznik++;
					dane=sr.ReadLine();
					lista=dane.Split(new char[] {'	'});
					d.Add(new Dane(lista[0].ToString(),float.Parse(lista[1]),float.Parse(lista[2]),float.Parse(lista[3]),float.Parse(lista[4]),float.Parse(lista[5])));
				}
				fs.Close();
			}
			catch(Exception e) {Okno.sb.Text = "Wystąpił błąd przy importowaniu danych: "+e.ToString();}
		}

		public static void Srednia (string opcja) {
			float suma=0f;
			for (int i=0; i < d.Count; i++) {
				switch (opcja) {
				case "NO2":
					suma = suma + d [i].NO2;
					break;
				case "SO2":
					suma = suma + d [i].SO2;
					break;
				case "CO":
					suma = suma + d [i].CO;
					break;
				case "PM25":
					suma = suma + d [i].PM25;
					break;
				case "PM10":
					suma = suma + d [i].PM10;
					break;
				}
			}
			Okno.sb.Text="Średnia "+opcja.ToString()+"= "+(suma/d.Count).ToString();
		}

		public static void Mediana(string opcja){
			float[] pom=new float[d.Count];
			float mediana=0f;
			int srodek =(int) Math.Floor ((double)d.Count/2.0)-1;
			switch (opcja){
			case "NO2":
				for (int i = 0; i < d.Count; i++) pom [i] = d [i].NO2;
				Array.Sort (pom);
				mediana = pom.Length % 2 != 0 ? pom [srodek] : (pom [srodek] + pom [srodek + 1]) / 2;
				break;
			case "SO2":
				for (int i = 0; i < d.Count; i++) pom [i] = d [i].SO2;
				Array.Sort (pom);
				mediana = pom.Length % 2 != 0 ? pom [srodek] : (pom [srodek] + pom [srodek + 1]) / 2;
				break;
			case "CO":
				for (int i = 0; i < d.Count; i++) pom [i] = d [i].CO;
				Array.Sort (pom);
				mediana = pom.Length % 2 != 0 ? pom [srodek] : (pom [srodek] + pom [srodek + 1]) / 2;
				break;
			case "PM25":
				for (int i = 0; i < d.Count; i++) pom [i] = d [i].PM25;
				Array.Sort (pom);
				mediana = pom.Length % 2 != 0 ? pom [srodek] : (pom [srodek] + pom [srodek + 1]) / 2;
				break;
			case "PM10":
				for (int i = 0; i < d.Count; i++) pom [i] = d [i].PM10;
				Array.Sort (pom);
				mediana = pom.Length % 2 != 0 ? pom [srodek] : (pom [srodek] + pom [srodek + 1]) / 2;
				break;
			}
			Okno.sb.Text = "Mediana " + opcja.ToString () + "= " + mediana.ToString ();
		}
		public static void Suma(string opcja){
			float suma=0f;
			for (int i=0; i < d.Count; i++) {
				switch (opcja) {
				case "NO2":
					suma = suma + d [i].NO2;
					break;
				case "SO2":
					suma = suma + d [i].SO2;
					break;
				case "CO":
					suma = suma + d [i].CO;
					break;
				case "PM25":
					suma = suma + d [i].PM25;
					break;
				case "PM10":
					suma = suma + d [i].PM10;
					break;
				}
			}
			Okno.sb.Text="Suma "+opcja.ToString()+"= "+suma.ToString();
		}
		public static void Zapis(string nazwa="dane.bin"){
			try
			{
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream fs = new FileStream (nazwa, FileMode.Create);
				bf.Serialize (fs, d);
				fs.Close ();
			}
			catch (Exception e) {Okno.sb.Text = "Wystąpił błąd przy zapisie danych: "+e.ToString();}
		}

		public static void Odczyt(string nazwa="dane.bin"){
			try
			{
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream fs = new FileStream (nazwa, FileMode.Open);
				d.AddRange ((List<Dane>)bf.Deserialize (fs));
				fs.Close();
			}
			catch (Exception e) {Okno.sb.Text = "Wystąpił błąd przy odczycie danych: "+e.ToString();}
		}
		public static void Aktuaizuj(int w){
			d [w].NO2 = float.Parse(Okno.lv.Items [w].SubItems [1].Text);
			d [w].SO2 = float.Parse(Okno.lv.Items [w].SubItems [2].Text);
			d [w].CO = float.Parse(Okno.lv.Items [w].SubItems [3].Text);
			d [w].PM25 = float.Parse(Okno.lv.Items [w].SubItems [4].Text);
			d [w].PM10 = float.Parse(Okno.lv.Items [w].SubItems [5].Text);
		}
	}
}

