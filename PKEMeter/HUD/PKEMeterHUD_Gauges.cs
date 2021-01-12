using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HamstarHelpers.Classes.Loadable;


namespace PKEMeter.HUD {
	partial class PKEMeterHUD : ILoadable {
		public void DrawHUDGauges( SpriteBatch sb, Vector2 pos, float b, float g, float y, float r ) {
			pos.X += 22;
			pos.Y += 28;//16;

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

			this.DrawHUDGaugeTicks( sb, this.MeterDisplayB, bDestRect, bTicks );
			this.DrawHUDGaugeTicks( sb, this.MeterDisplayG, gDestRect, gTicks );
			this.DrawHUDGaugeTicks( sb, this.MeterDisplayY, yDestRect, yTicks );
			this.DrawHUDGaugeTicks( sb, this.MeterDisplayR, rDestRect, rTicks );
			
			if( b >= 0.99f ) {
				this.DrawHUDGaugeLight1( sb, pos + new Vector2(), 0 );
			}
			if( g >= 0.99f ) {
				this.DrawHUDGaugeLight1( sb, pos + new Vector2(8, 0), 1 );
			}
			if( y >= 0.99f ) {
				this.DrawHUDGaugeLight1( sb, pos + new Vector2(16, 0), 2 );
			}
			if( r >= 0.99f ) {
				this.DrawHUDGaugeLight1( sb, pos + new Vector2(24, 0), 3 );
			}

			if( b >= 0.99f ) {
				this.DrawHUDGaugeLight2( sb, pos + new Vector2(), Color.Blue );
			}
			if( g >= 0.99f ) {
				this.DrawHUDGaugeLight2( sb, pos + new Vector2(8, 0), Color.Lime );
			}
			if( y >= 0.99f ) {
				this.DrawHUDGaugeLight2( sb, pos + new Vector2(16, 0), Color.Yellow );
			}
			if( r >= 0.99f ) {
				this.DrawHUDGaugeLight2( sb, pos + new Vector2(16, 0), Color.Red );
			}
		}


		////

		private void DrawHUDGaugeTicks( SpriteBatch sb, Texture2D tickTex, Rectangle rect, float ticks ) {
			for( int i=0; i<(int)ticks; i++ ) {
				rect.Y -= 6;

				sb.Draw(
					texture: tickTex,
					destinationRectangle: rect,
					color: Color.White
				);
			}

			float tickFrac = ticks - (float)(int)ticks;

			if( Main.rand.NextFloat() < tickFrac ) {
				rect.Y -= 6;

				sb.Draw(
					texture: tickTex,
					destinationRectangle: rect,
					color: Color.White
				);
			}
		}


		////

		private void DrawHUDGaugeLight1( SpriteBatch sb, Vector2 pos, int frameX ) {
			var destRect = new Rectangle( (int)pos.X, (int)pos.Y, 6, 6 );
			var srcRect = new Rectangle( frameX * 6, 0, 6, 6 );

			sb.Draw(
				texture: this.MeterLights,
				destinationRectangle: destRect,
				sourceRectangle: srcRect,
				color: Color.White
			);
		}

		private void DrawHUDGaugeLight2( SpriteBatch sb, Vector2 pos, Color color ) {
			var airDestRect = new Rectangle( (int)pos.X - 4, (int)pos.Y - 4, 14, 14 );
			var backDestRect = new Rectangle( (int)pos.X - 2, (int)pos.Y + 6, 10, 4 );

			sb.Draw(
				texture: Main.magicPixel,
				destinationRectangle: airDestRect,
				color: color * 0.1f
			);

			sb.Draw(
				texture: Main.magicPixel,
				destinationRectangle: backDestRect,
				color: color * 0.2f
			);
		}
	}
}
