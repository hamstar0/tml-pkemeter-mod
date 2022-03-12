using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HUDElementsLib;


namespace PKEMeter.HUD {
	public partial class PKEMeterHUD : HUDElement {
		private int ScanLightsRow = 0;
		private int ScanLightsRowTimer = 0;



		////////////////

		private void DrawHUDScanLightsCurrentRow( SpriteBatch sb, Vector2 pos ) {
			if( this.ScanLightsRowTimer++ > 5 ) {
				this.ScanLightsRowTimer = 0;

				this.ScanLightsRow = this.ScanLightsRow + 1 >= 3
					? 0
					: this.ScanLightsRow + 1;
			}

			//

			this.DrawHUDScanLightsRow( sb, pos, this.ScanLightsRow );
		}

		private void DrawHUDScanLightsRow( SpriteBatch sb, Vector2 pos, int row ) {
			int posX = (int)pos.X;
			int posY = (int)pos.Y;
			int rowOffY = row * 8;
			int x = posX + 4;
			int y = posY + 12 + rowOffY;
			int wid = this.MeterScanLightsRow.Width;
			int hei = this.MeterScanLightsRow.Height;

			sb.Draw(
				texture: this.MeterScanLightsRow,
				destinationRectangle: new Rectangle(x, y, wid, hei),
				//position: pos + new Vector2(4f, 12f),
				color: Color.White
			);

			//

			int lGlowX = posX + 2;
			int rGlowX = posX + 64;
			int glowY = posY + 10 + rowOffY;
			Color glowColor = new Color(255, 240, 192) * 0.1f;

			sb.Draw(
				texture: Main.magicPixel,
				destinationRectangle: new Rectangle(lGlowX, glowY, 8, 10),
				color: glowColor
			);

			sb.Draw(
				texture: Main.magicPixel,
				destinationRectangle: new Rectangle(rGlowX, glowY, 8, 10),
				color: glowColor
			);
		}
	}
}
