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

		private void DrawHUDScanLightsCurrentRow( SpriteBatch sb, Vector2 pos, Color color, float intensityPercent ) {
			float invPercent = 1f - intensityPercent;
			int tickRate = 5 + (int)(invPercent * 10f);

			//

			if( this.ScanLightsRowTimer++ > tickRate ) {
				this.ScanLightsRowTimer = 0;

				this.ScanLightsRow = (this.ScanLightsRow + 1) >= 3
					? 0
					: this.ScanLightsRow + 1;
			}

			//

			this.DrawHUDScanLightsRow( sb, pos, this.ScanLightsRow, color );
		}

		private void DrawHUDScanLightsRow( SpriteBatch sb, Vector2 pos, int row, Color color ) {
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
				color: Color.Lerp( color, Color.White, 0.6f )
			);

			//

			int lGlowX = posX + 2;
			int rGlowX = posX + 64;
			int glowY = posY + 10 + rowOffY;
			
			Color glowColor = color == Color.White
				? new Color( 255, 240, 192 )
				: color;
			glowColor *= 0.15f;

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
