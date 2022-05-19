using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.Players;
using HUDElementsLib;
using PKEMeter.Items;
using PKEMeter.Logic;


namespace PKEMeter.HUD {
	public partial class PKEMeterHUD : HUDElement {
		public static bool CanDrawPKE() {
			if( /*Main.playerInventory ||*/ Main.LocalPlayer.dead ) {
				return false;
			}

			int meterType = ModContent.ItemType<PKEMeterItem>();

			if( PKEMeterItem.DisplayHUDMeter ) {
				return PlayerItemFinderLibraries
					.CountTotalOfEach(
						Main.LocalPlayer,
						new HashSet<int> { meterType },
						false
					) > 0;
			}

			//

			Item heldItem = Main.LocalPlayer.HeldItem;
			heldItem = heldItem?.active == true ? heldItem : null;

			Item mouseItem = Main.mouseItem;
			mouseItem = mouseItem?.active == true ? mouseItem : null;

			return heldItem?.type == meterType || mouseItem?.type == meterType;
		}



		////////////////

		private void DrawHUDComponents(
					SpriteBatch sb,
					Vector2 scrPos,
					Player plr,
					Color lightColor,
					out (Rectangle b, Rectangle g, Rectangle y, Rectangle r) gaugeRects,
					out Rectangle marqueeRect ) {
			var logic = PKEMeterLogic.Instance;

			float opacity = 1f;//Main.playerInventory ? 0.5f : 1f;

			sb.Draw(
				texture: this.MeterDisplay,
				position: scrPos,
				color: Color.White * opacity
			);

			//

			PKEGaugeValues gauge = logic.GetGaugesDynamically( plr, plr.Center );
			PKEMiscLightsValues lights = logic.GetMiscLightsDynamically( plr, plr.Center );
			Color scanLightColor = this.GetProximityLightColor_Local( out float scanLightPercent );

			//

			gaugeRects = this.DrawHUDGauges(
				sb: sb,
				pos: scrPos,
				opacity: opacity,
				b: gauge.BlueSeenPercent,
				g: gauge.GreenSeenPercent,
				y: gauge.YellowSeenPercent,
				r: gauge.RedSeenPercent
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
				bLit: gauge.BlueSeenPercent > 0.99f,
				gLit: gauge.GreenSeenPercent > 0.99f,
				yLit: gauge.YellowSeenPercent > 0.99f,
				rLit: gauge.RedSeenPercent > 0.99f
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

			(PKETextMessage msg, int textOffset) = logic.GetText( plr, plr.Center );

			marqueeRect = this.DrawHUDText_If( sb, scrPos, msg.Message, msg.Color * opacity, textOffset );

			//

			sb.Draw(
				texture: this.MeterWires,
				position: scrPos,
				color: lightColor * opacity
			);
		}
	}
}
