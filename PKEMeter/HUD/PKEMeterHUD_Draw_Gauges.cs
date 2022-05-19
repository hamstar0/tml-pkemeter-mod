using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HUDElementsLib;


namespace PKEMeter.HUD {
	public partial class PKEMeterHUD : HUDElement {
		public const int GaugesOffsetX = 30;
		public const int GaugesOffsetY = 28;//16;



		public (Rectangle bRect, Rectangle gRect, Rectangle yRect, Rectangle rRect) DrawHUDGauges(
					SpriteBatch sb,
					Vector2 pos,
					float opacity,
					float b,
					float g,
					float y,
					float r ) {
			pos.X += PKEMeterHUD.GaugesOffsetX;
			pos.Y += PKEMeterHUD.GaugesOffsetY;

			var destRect = new Rectangle( (int)pos.X, (int)pos.Y + 42, 6, 4 );
			var bDestRect = destRect;
			var gDestRect = destRect;
			var yDestRect = destRect;
			var rDestRect = destRect;

			//bDestRect.X += 0;
			gDestRect.X += 8;
			yDestRect.X += 16;
			rDestRect.X += 24;

			float bTicks = 7f * b;
			float gTicks = 7f * g;
			float yTicks = 7f * y;
			float rTicks = 7f * r;

			this.DrawHUDGaugeTicks( sb, this.MeterDisplayB, opacity, ref bDestRect, bTicks );
			this.DrawHUDGaugeTicks( sb, this.MeterDisplayG, opacity, ref gDestRect, gTicks );
			this.DrawHUDGaugeTicks( sb, this.MeterDisplayY, opacity, ref yDestRect, yTicks );
			this.DrawHUDGaugeTicks( sb, this.MeterDisplayR, opacity, ref rDestRect, rTicks );

			return (bDestRect, gDestRect, yDestRect, rDestRect);
		}


		////

		private void DrawHUDGaugeTicks(
					SpriteBatch sb,
					Texture2D tickTex,
					float opacity,
					ref Rectangle bottomRect,
					float ticks ) {
			for( int i=0; i<(int)ticks; i++ ) {
				bottomRect.Y -= 6;

				sb.Draw(
					texture: tickTex,
					destinationRectangle: bottomRect,
					color: Color.White * opacity
				);
			}

			float tickFrac = ticks - (float)(int)ticks;

			// Set the fractional amount to flicker according to its significance
			if( Main.rand.NextFloat() < tickFrac ) {
				bottomRect.Y -= 6;

				sb.Draw(
					texture: tickTex,
					destinationRectangle: bottomRect,
					color: Color.White * opacity
				);
			}
		}
	}
}
