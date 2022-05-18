using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using ModLibsCore.Services.Timers;
using HUDElementsLib;


namespace PKEMeter.HUD {
	public partial class PKEMeterHUD : HUDElement {
		private int ScanLightsRow = 0;
		private int ScanLightsRowTimer = 0;

		private int StartTimerTicks = 0;



		////////////////

		private void DrawHUDScanLights_If( SpriteBatch sb, Vector2 pos, Color color, float intensityPercent ) {
			// Grace period before 
			if( this.StartTimerTicks < 60 ) {
				this.StartTimerTicks++;

				return;
			}

			//

			Timers.SetTimer( "PKE Scan Lights Off", 60, true, () => {
				this.StartTimerTicks = 0;
				return false;
			} );

			//

			this.UpdateHUDScanLightCurrentRow( intensityPercent );

			//

			this.DrawHUDScanLightsRow( sb, pos, this.ScanLightsRow, color );
		}


		////////////////

		private void DrawHUDScanLightsRow( SpriteBatch sb, Vector2 pos, int row, Color color ) {
			int posX = (int)pos.X;
			int posY = (int)pos.Y;
			int rowOffY = row * 8;
			int x = posX + 4;
			int y = posY + 12 + rowOffY;
			int wid = this.MeterScanLightsRow1.Width;
			int hei = this.MeterScanLightsRow1.Height;

			sb.Draw(
				texture: this.MeterScanLightsRow1,
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


		////////////////

		private void UpdateHUDScanLightCurrentRow( float intensityPercent ) {
			float invPercent = 1f - intensityPercent;
			int tickRate = 4 + (int)( invPercent * 16f );

			//

			if( this.ScanLightsRowTimer++ > tickRate ) {
				this.ScanLightsRowTimer = 0;

				this.ScanLightsRow = ( this.ScanLightsRow + 1 ) >= 3
					? 0
					: this.ScanLightsRow + 1;
			}
		}
	}
}
