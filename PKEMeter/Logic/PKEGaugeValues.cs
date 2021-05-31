using System;
using Microsoft.Xna.Framework;
using Terraria;
using ModLibsCore.Libraries.Debug;


namespace PKEMeter.Logic {
	public delegate PKEGaugeValues PKEGaugesGetter( Player player, Vector2 position );




	////////////////

	public class PKEGaugeValues {
		public float BluePercent { get; set; }
		public float GreenPercent { get; set; }
		public float YellowPercent { get; set; }
		public float RedPercent { get; set; }



		////////////////

		public PKEGaugeValues( float b, float g, float y, float r ) {
			this.BluePercent = b;
			this.GreenPercent = g;
			this.YellowPercent = y;
			this.RedPercent = r;
		}

		public PKEGaugeValues( PKEGaugeValues msg ) {
			this.BluePercent = msg.BluePercent;
			this.GreenPercent = msg.GreenPercent;
			this.YellowPercent = msg.YellowPercent;
			this.RedPercent = msg.RedPercent;
		}

		////

		public override bool Equals( object obj ) {
			var myobj = obj as PKEGaugeValues;
			return myobj?.BluePercent == this.BluePercent
				&& myobj.GreenPercent == this.GreenPercent
				&& myobj.YellowPercent == this.YellowPercent
				&& myobj.RedPercent == this.RedPercent;
		}

		public override int GetHashCode() {
			return this.BluePercent.GetHashCode()
				+ this.GreenPercent.GetHashCode()
				+ this.YellowPercent.GetHashCode()
				+ this.RedPercent.GetHashCode();
		}
	}
}
