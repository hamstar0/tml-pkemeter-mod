using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HUDElementsLib;


namespace PKEMeter.HUD {
	public partial class PKEMeterHUD : HUDElement {
		private void DrawHUDGaugeLights( SpriteBatch sb, Vector2 pos, bool bLit, bool gLit, bool yLit, bool rLit ) {
			if( bLit ) {
				this.DrawHUDGaugeLight2( sb, pos, 0, new Color(16, 32, 255) );
			}
			if( gLit ) {
				this.DrawHUDGaugeLight2( sb, pos, 1, Color.Lime );
			}
			if( yLit ) {
				this.DrawHUDGaugeLight2( sb, pos, 2, Color.Yellow );
			}
			if( rLit ) {
				this.DrawHUDGaugeLight2( sb, pos, 3, Color.Red );
			}

			if( bLit ) {
				this.DrawHUDGaugeLight1( sb, pos + new Vector2(), 0 );
			}
			if( gLit ) {
				this.DrawHUDGaugeLight1( sb, pos + new Vector2( 8, 0 ), 1 );
			}
			if( yLit ) {
				this.DrawHUDGaugeLight1( sb, pos + new Vector2( 16, 0 ), 2 );
			}
			if( rLit ) {
				this.DrawHUDGaugeLight1( sb, pos + new Vector2( 24, 0 ), 3 );
			}
		}


		////////////////

		private void DrawHUDGaugeLight1( SpriteBatch sb, Vector2 pos, int frameX ) {
			var destRect = new Rectangle( (int)pos.X + 28, (int)pos.Y, 6, 6 );
			var srcRect = new Rectangle( frameX * 8, 0, 6, 6 );

			sb.Draw(
				texture: this.MeterLights,
				destinationRectangle: destRect,
				sourceRectangle: srcRect,
				color: Color.White
			);
		}

		private void DrawHUDGaugeLight2( SpriteBatch sb, Vector2 pos, int lightSlot, Color color ) {
			pos.X += 28 + (lightSlot * 8);
			var backDestRect = new Rectangle( (int)pos.X - 4, (int)pos.Y + 6, 14, 4 );
			var airDestRect = new Rectangle( (int)pos.X - 2, (int)pos.Y - 2, 10, 10 );

			sb.Draw(
				texture: Main.magicPixel,
				destinationRectangle: backDestRect,
				color: color * 0.1f
			);

			sb.Draw(
				texture: Main.magicPixel,
				destinationRectangle: airDestRect,
				color: color * 0.05f
			);
		}
	}
}
