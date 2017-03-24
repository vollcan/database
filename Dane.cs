using System;
using System.Collections.Generic;

namespace Projekt
{
	[Serializable]
	public class Dane
	{
		public string godzina { get; set; }
		public float NO2 { get; set; }
		public float SO2 { get; set; }
		public float CO { get; set; }
		public float PM25{ get; set; }
		public float PM10{ get; set; }

 		public Dane ()
		{
			NO2 = SO2 = CO = PM25 = PM10 = 0;
		}
		public Dane (string h, float n,float s,float c,float p2,float p1)
		{
			godzina = h;
			NO2 = n;
			SO2 = s;
			CO = c;
			PM25 = p2;
			PM10 = p1;
		}

	}
}

