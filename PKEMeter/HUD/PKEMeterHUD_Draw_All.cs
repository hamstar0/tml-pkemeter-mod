using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using HUDElementsLib;
using PKEMeter.Logic;


namespace PKEMeter.HUD {
	public partial class PKEMeterHUD : HUDElement {
		private void DrawHUDComponents(
					SpriteBatch sb,
					Vector2 scrPos,
					Player plr,
					Color lightColor,
					PKEGaugeValues gauges,
					PKEMiscLightsValues lights,
					Color scanLightColor,
					float scanLightPercent,
					PKETextMessage displayText,
					int displayTextOffset,
					out (Rectangle b, Rectangle g, Rectangle y, Rectangle r) gaugeRects,
					out Rectangle marqueeRect ) {
			float opacity = 1f;//Main.playerInventory ? 0.5f : 1f;

			sb.Draw(
				texture: this.MeterDisplay,
				position: scrPos,
				color: Color.White * opacity
			);

			//

			gaugeRects = this.DrawHUDGauges(
				sb: sb,
				pos: scrPos,
				opacity: opacity,
				b: gauges.BlueSeenPercent,
				g: gauges.GreenSeenPercent,
				y: gauges.YellowSeenPercent,
				r: gauges.RedSeenPercent
			);

			//

			sb.Draw(
				texture: scanLightPercent > 0f
					? this.MeterBodyScan
					: this.MeterBody,
				position: scrPos,
				color: lightColor * opacity
			);

			//

			this.DrawHUDGaugeLights(
				sb: sb,
				pos: scrPos,
				bLit: gauges.BlueSeenPercent > 0.99f,
				gLit: gauges.GreenSeenPercent > 0.99f,
				yLit: gauges.YellowSeenPercent > 0.99f,
				rLit: gauges.RedSeenPercent > 0.99f
			);

			//

			if( scanLightPercent > 0f ) {
				this.DrawHUDScanLights_If( sb, scrPos, scanLightColor, scanLightPercent );
			}

			//
			
			if( lights != null ) {
				this.DrawHUDMiscLights(
					sb: sb,
					pos: scrPos,
					c1: lights.Light1,
					c2: lights.Light2,
					c3: lights.Light3,
					c4: lights.Light4,
					c5: lights.Light5,
					c6: lights.Light6,
					c7: lights.Light7,
					c8: lights.Light8,
					c9: lights.Light9
				);
			}

			//

			marqueeRect = this.DrawHUDText_If(
				sb,
				scrPos,
				displayText.Message,
				displayText.Color * opacity,
				displayTextOffset
			);

			//

			sb.Draw(
				texture: this.MeterWires,
				position: scrPos,
				color: lightColor * opacity
			);
		}
	}
}
