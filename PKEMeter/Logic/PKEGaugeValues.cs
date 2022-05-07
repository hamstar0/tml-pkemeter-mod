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
		public static Color? GetColor( PKEGaugeType gauge ) {
			switch( gauge ) {
			case PKEGaugeType.Blue:
				return new Color(0, 0, 255);
			case PKEGaugeType.Green:
				return new Color( 0, 255, 0 );
			case PKEGaugeType.Yellow:
				return new Color( 255, 255, 0 );
			case PKEGaugeType.Red:
				return new Color( 255, 0, 0 );
			default:
				return null;
			}
		}



		////////////////

		public float BlueRealPercent { get; set; }
		public float GreenRealPercent { get; set; }
		public float YellowRealPercent { get; set; }
		public float RedRealPercent { get; set; }

		public float BlueSeenPercent { get; set; }
		public float GreenSeenPercent { get; set; }
		public float YellowSeenPercent { get; set; }
		public float RedSeenPercent { get; set; }



		////////////////
		
		public PKEGaugeValues( float b, float g, float y, float r ) {
			this.BlueRealPercent = b;
			this.GreenRealPercent = g;
			this.YellowRealPercent = y;
			this.RedRealPercent = r;
			this.BlueSeenPercent = b;
			this.GreenSeenPercent = g;
			this.YellowSeenPercent = y;
			this.RedSeenPercent = r;
		}

		public PKEGaugeValues( PKEGaugeValues msg ) {
			this.BlueRealPercent = msg.BlueRealPercent;
			this.GreenRealPercent = msg.GreenRealPercent;
			this.YellowRealPercent = msg.YellowRealPercent;
			this.RedRealPercent = msg.RedRealPercent;
			this.BlueSeenPercent = msg.BlueSeenPercent;
			this.GreenSeenPercent = msg.GreenSeenPercent;
			this.YellowSeenPercent = msg.YellowSeenPercent;
			this.RedSeenPercent = msg.RedSeenPercent;
		}

		////

		public override bool Equals( object obj ) {
			var myobj = obj as PKEGaugeValues;
			return myobj?.BlueRealPercent == this.BlueRealPercent
				&& myobj.GreenRealPercent == this.GreenRealPercent
				&& myobj.YellowRealPercent == this.YellowRealPercent
				&& myobj.RedRealPercent == this.RedRealPercent
				&& myobj.BlueSeenPercent == this.BlueSeenPercent
				&& myobj.GreenSeenPercent == this.GreenSeenPercent
				&& myobj.YellowSeenPercent == this.YellowSeenPercent
				&& myobj.RedSeenPercent == this.RedSeenPercent;
		}

		public override int GetHashCode() {
			return this.BlueRealPercent.GetHashCode()
				+ this.GreenRealPercent.GetHashCode()
				+ this.YellowRealPercent.GetHashCode()
				+ this.RedRealPercent.GetHashCode()
				+ this.BlueSeenPercent.GetHashCode()
				+ this.GreenSeenPercent.GetHashCode()
				+ this.YellowSeenPercent.GetHashCode()
				+ this.RedSeenPercent.GetHashCode();
		}

		////////////////

		public PKEGaugeType GetSignificantGauge() {
			if( this.BlueRealPercent >= this.GreenRealPercent ) {
				if( this.BlueRealPercent >= this.YellowRealPercent ) {
					if( this.BlueRealPercent >= this.RedRealPercent ) {
						return PKEGaugeType.Blue;
					}
				} else {
					if( this.YellowRealPercent >= this.RedRealPercent ) {
						return PKEGaugeType.Yellow;
					}
				}
			} else {
				if( this.GreenRealPercent >= this.YellowRealPercent ) {
					if( this.GreenRealPercent >= this.RedRealPercent ) {
						return PKEGaugeType.Green;
					}
				} else {
					if( this.YellowRealPercent >= this.RedRealPercent ) {
						return PKEGaugeType.Yellow;
					}
				}
			}
			return PKEGaugeType.Red;
		}

		public float GetGaugeValue( PKEGaugeType gauge, bool actualValue ) {
			switch( gauge ) {
			case PKEGaugeType.Blue:
				return actualValue ? this.BlueRealPercent : this.BlueSeenPercent;
			case PKEGaugeType.Green:
				return actualValue ? this.GreenRealPercent : this.GreenSeenPercent;
			case PKEGaugeType.Yellow:
				return actualValue ? this.YellowRealPercent : this.YellowSeenPercent;
			case PKEGaugeType.Red:
				return actualValue ? this.RedRealPercent : this.RedSeenPercent;
			default:
				throw new NotImplementedException( "Unspecified gauge type." );
			}
		}
	}
}
