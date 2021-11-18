using System;
using Microsoft.Xna.Framework;
using Terraria;
using ModLibsCore.Libraries.Debug;


namespace PKEMeter.Logic {
	public delegate PKEMiscLightsValues PKEMiscLightsGetter( Player player, Vector2 position );




	////////////////

	public class PKEMiscLightsValues {
		public Color Light1 { get; set; }
		public Color Light2 { get; set; }
		public Color Light3 { get; set; }
		public Color Light4 { get; set; }
		public Color Light5 { get; set; }
		public Color Light6 { get; set; }
		public Color Light7 { get; set; }
		public Color Light8 { get; set; }
		public Color Light9 { get; set; }



		////////////////

		public PKEMiscLightsValues(
					Color c1=default,
					Color c2=default,
					Color c3=default,
					Color c4=default,
					Color c5=default,
					Color c6=default,
					Color c7=default,
					Color c8=default,
					Color c9=default ) {
			this.Light1 = c1;
			this.Light2 = c2;
			this.Light3 = c3;
			this.Light4 = c4;
			this.Light5 = c5;
			this.Light6 = c6;
			this.Light7 = c7;
			this.Light8 = c8;
			this.Light9 = c9;
		}

		////

		public override bool Equals( object obj ) {
			var myobj = obj as PKEMiscLightsValues;
			return myobj?.Light1 == this.Light1
				&& myobj.Light2 == this.Light2
				&& myobj.Light3 == this.Light3
				&& myobj.Light4 == this.Light4
				&& myobj.Light5 == this.Light5
				&& myobj.Light6 == this.Light6
				&& myobj.Light7 == this.Light7
				&& myobj.Light8 == this.Light8
				&& myobj.Light9 == this.Light9;
		}

		public override int GetHashCode() {
			return this.Light1.GetHashCode()
				+ this.Light2.GetHashCode()
				+ this.Light3.GetHashCode()
				+ this.Light4.GetHashCode()
				+ this.Light5.GetHashCode()
				+ this.Light6.GetHashCode()
				+ this.Light7.GetHashCode()
				+ this.Light8.GetHashCode()
				+ this.Light9.GetHashCode();
		}
	}
}
