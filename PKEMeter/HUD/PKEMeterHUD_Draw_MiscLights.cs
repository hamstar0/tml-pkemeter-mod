using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HUDElementsLib;


namespace PKEMeter.HUD {
	public partial class PKEMeterHUD : HUDElement {
		private void DrawHUDMiscLights(
					SpriteBatch sb,
					Vector2 pos,
					Color c1,
					Color c2,
					Color c3,
					Color c4,
					Color c5,
					Color c6,
					Color c7,
					Color c8,
					Color c9 ) {
			this.DrawHUDMiscLight1( sb, pos, 0, c1 );
			this.DrawHUDMiscLight1( sb, pos, 4, c2 );
			this.DrawHUDMiscLight1( sb, pos, 8, c3 );
			this.DrawHUDMiscLight1( sb, pos, 12, c4 );
			this.DrawHUDMiscLight1( sb, pos, 16, c5 );
			this.DrawHUDMiscLight1( sb, pos, 20, c6 );
			this.DrawHUDMiscLight1( sb, pos, 24, c7 );
			this.DrawHUDMiscLight1( sb, pos, 28, c8 );
			this.DrawHUDMiscLight1( sb, pos, 30, c9 );

			this.DrawHUDMiscLight2( sb, pos, 0, c1 );
			this.DrawHUDMiscLight2( sb, pos, 4, c2 );
			this.DrawHUDMiscLight2( sb, pos, 8, c3 );
			this.DrawHUDMiscLight2( sb, pos, 12, c4 );
			this.DrawHUDMiscLight2( sb, pos, 16, c5 );
			this.DrawHUDMiscLight2( sb, pos, 20, c6 );
			this.DrawHUDMiscLight2( sb, pos, 24, c7 );
			this.DrawHUDMiscLight2( sb, pos, 28, c8 );
			this.DrawHUDMiscLight2( sb, pos, 30, c9 );
		}


		////////////////

		private void DrawHUDMiscLight1( SpriteBatch sb, Vector2 pos, int offset, Color color ) {
			var destRect = new Rectangle(
				(int)pos.X + 22 + offset,
				(int)pos.Y + 70,
				2,
				2
			);

			sb.Draw(
				texture: Main.magicPixel,
				destinationRectangle: destRect,
				sourceRectangle: null,
				color: color
			);
		}

		private void DrawHUDMiscLight2( SpriteBatch sb, Vector2 pos, int offset, Color color ) {
			var destRect = new Rectangle(
				(int)pos.X + 20 + offset,
				(int)pos.Y + 68,
				6,
				6
			);

			sb.Draw(
				texture: Main.magicPixel,
				destinationRectangle: destRect,
				sourceRectangle: null,
				color: color * 0.25f
			);
		}
	}
}
