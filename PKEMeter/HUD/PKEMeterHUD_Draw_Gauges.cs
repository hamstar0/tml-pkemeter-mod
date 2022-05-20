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

			var baseRect = new Rectangle( (int)pos.X, (int)pos.Y + 42, 6, 4 );
			var bRect = baseRect;
			var gRect = baseRect;
			var yRect = baseRect;
			var rRect = baseRect;

			//bDestRect.X += 0;
			gRect.X += 8;
			yRect.X += 16;
			rRect.X += 24;

			float bTicks = 7f * b;
			float gTicks = 7f * g;
			float yTicks = 7f * y;
			float rTicks = 7f * r;

			bRect = this.DrawHUDGaugeTicks( sb, this.MeterDisplayB, opacity, bRect, bTicks );
			gRect = this.DrawHUDGaugeTicks( sb, this.MeterDisplayG, opacity, gRect, gTicks );
			yRect = this.DrawHUDGaugeTicks( sb, this.MeterDisplayY, opacity, yRect, yTicks );
			rRect = this.DrawHUDGaugeTicks( sb, this.MeterDisplayR, opacity, rRect, rTicks );

			return (bRect, gRect, yRect, rRect);
		}


		////

		private Rectangle DrawHUDGaugeTicks(
					SpriteBatch sb,
					Texture2D tickTex,
					float opacity,
					Rectangle bottomRect,
					float ticks ) {
			Rectangle totalArea = bottomRect;

			for( int i=0; i<(int)ticks; i++ ) {
				bottomRect.Y -= 6;

				totalArea.Y -= 6;
				totalArea.Height += 6;

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

				totalArea.Y -= 6;
				totalArea.Height += 6;

				sb.Draw(
					texture: tickTex,
					destinationRectangle: bottomRect,
					color: Color.White * opacity
				);
			}

			return totalArea;
		}
	}
}
