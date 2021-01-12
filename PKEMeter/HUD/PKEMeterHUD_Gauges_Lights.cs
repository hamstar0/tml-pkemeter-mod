using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HamstarHelpers.Classes.Loadable;


namespace PKEMeter.HUD {
	partial class PKEMeterHUD : ILoadable {
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
