using System;
using Microsoft.Xna.Framework;
using Terraria;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Classes.Errors;


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

		public bool IsBlueInsignificant { get; set; } = false;
		public bool IsGreenInsignificant { get; set; } = false;
		public bool IsYellowInsignificant { get; set; } = false;
		public bool IsRedInsignificant { get; set; } = false;



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
			this.IsBlueInsignificant = msg.IsBlueInsignificant;
			this.IsGreenInsignificant = msg.IsGreenInsignificant;
			this.IsYellowInsignificant = msg.IsYellowInsignificant;
			this.IsRedInsignificant = msg.IsRedInsignificant;
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
				&& myobj.RedSeenPercent == this.RedSeenPercent
				&& myobj.IsBlueInsignificant == this.IsBlueInsignificant
				&& myobj.IsGreenInsignificant == this.IsGreenInsignificant
				&& myobj.IsYellowInsignificant == this.IsYellowInsignificant
				&& myobj.IsRedInsignificant == this.IsRedInsignificant;
		}

		public override int GetHashCode() {
			return this.BlueRealPercent.GetHashCode()
				+ this.GreenRealPercent.GetHashCode()
				+ this.YellowRealPercent.GetHashCode()
				+ this.RedRealPercent.GetHashCode()
				+ this.BlueSeenPercent.GetHashCode()
				+ this.GreenSeenPercent.GetHashCode()
				+ this.YellowSeenPercent.GetHashCode()
				+ this.RedSeenPercent.GetHashCode()
				+ (this.IsBlueInsignificant ? 1 : 0)
				+ (this.IsGreenInsignificant ? 2 : 0)
				+ (this.IsYellowInsignificant ? 4 : 0)
				+ (this.IsRedInsignificant ? 8 : 0);
		}


		////////////////

		public PKEGaugeType GetSignificantGauge( bool checkIfInsignificant ) {
			float b = (checkIfInsignificant && this.IsBlueInsignificant) ? 0f : this.BlueRealPercent;
			float g = (checkIfInsignificant && this.IsGreenInsignificant) ? 0f : this.GreenRealPercent;
			float y = (checkIfInsignificant && this.IsYellowInsignificant) ? 0f : this.YellowRealPercent;
			float r = (checkIfInsignificant && this.IsRedInsignificant) ? 0f : this.RedRealPercent;

			if( b >= g ) {
				if( b >= y ) {
					if( b >= r ) {
						return PKEGaugeType.Blue;
					}
				} else {
					if( y >= r ) {
						return PKEGaugeType.Yellow;
					}
				}
			} else {
				if( g >= y ) {
					if( g >= r ) {
						return PKEGaugeType.Green;
					}
				} else {
					if( y >= r ) {
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
