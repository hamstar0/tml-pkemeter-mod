using System;
using Microsoft.Xna.Framework;
using Terraria;
using ModLibsCore.Libraries.Debug;


namespace PKEMeter.Logic {
	public enum PKEGaugeType {
		Blue = 1,
		Green = 2,
		Yellow = 4,
		Red = 8
	}



	////////////////
	
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

		////////////////

		public PKEGaugeType GetSignificantGauge() {
			if( this.BluePercent >= this.GreenPercent ) {
				if( this.BluePercent >= this.YellowPercent ) {
					if( this.BluePercent >= this.RedPercent ) {
						return PKEGaugeType.Blue;
					}
				} else {
					if( this.YellowPercent >= this.RedPercent ) {
						return PKEGaugeType.Yellow;
					}
				}
			} else {
				if( this.GreenPercent >= this.YellowPercent ) {
					if( this.GreenPercent >= this.RedPercent ) {
						return PKEGaugeType.Green;
					}
				} else {
					if( this.YellowPercent >= this.RedPercent ) {
						return PKEGaugeType.Yellow;
					}
				}
			}
			return PKEGaugeType.Red;
		}

		public float GetGaugeValue( PKEGaugeType gauge ) {
			switch( gauge ) {
			case PKEGaugeType.Blue:
				return this.BluePercent;
			case PKEGaugeType.Green:
				return this.GreenPercent;
			case PKEGaugeType.Yellow:
				return this.YellowPercent;
			case PKEGaugeType.Red:
				return this.RedPercent;
			default:
				throw new NotImplementedException( "Unspecified gauge type." );
			}
		}
	}
}
