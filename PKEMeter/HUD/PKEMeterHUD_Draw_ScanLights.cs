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

		private int ReactivationBufferTimer = 0;



		////////////////

		private void DrawHUDScanLights_If( SpriteBatch sb, Vector2 pos, Color color, float signalPercent ) {
			// Grace period before scan begins
			if( this.ReactivationBufferTimer < 60 ) {
				this.ReactivationBufferTimer++;

				return;
			}

			Timers.SetTimer( "PKE Scan Lights Off", 60, true, () => {	// <- Timer (re)starts from scratch
				this.ReactivationBufferTimer = 0;	// If scan lights stop for a full second, reactivation time needed again
				return false;
			} );

			//

			if( signalPercent > 0f ) {
				float fxIntensityPecent = signalPercent;

				// Cap intensity to ambiguate results
				if( fxIntensityPecent > 0.8f ) {
					fxIntensityPecent = 0.8f;
				}

				//

				this.UpdateHUDScanLightCurrentRow( fxIntensityPecent );

				//

				this.DrawHUDScanLightsRow( sb, pos, this.ScanLightsRow, color );
			}
		}


		////////////////

		private void DrawHUDScanLightsRow( SpriteBatch sb, Vector2 scannerPos, int row, Color color ) {
			int rowOffY = row * 8;

			Vector2 rowPos = scannerPos + new Vector2(0, 12 + rowOffY);

			//

			sb.Draw(
				texture: this.MeterScanLightsRow,
				position: rowPos,
				color: Color.Lerp( color, Color.White, 0.6f )
			);

			//

			int lGlowX = (int)rowPos.X - 2;
			int rGlowX = (int)rowPos.X + this.MeterScanLightsRow.Width - 8;
			int glowY = (int)rowPos.Y - 2;
			
			Color glowColor = color == Color.White
				? new Color( 255, 240, 192 )
				: color;
			glowColor *= 0.15f;

			//

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
			int tickRate = 4 + (int)( invPercent * 14f );

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
