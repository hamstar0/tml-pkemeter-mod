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
		
		protected override void PostDrawSelf( bool isSelfDrawn, SpriteBatch sb ) {
			Player plr = Main.LocalPlayer;
			var myplayer = plr.GetModPlayer<PKEMeterPlayer>();

			Color plrColor = myplayer.MyColor;
			if( plrColor.A < 255 ) {
				plrColor = this.LastVisiblePlayerColor;
			} else {
				this.LastVisiblePlayerColor = plrColor;
			}

			Vector2 widgetPos = this.GetHUDComputedPosition( true );

			//

			var logic = PKEMeterLogic.Instance;

			PKEGaugeValues gauges = logic.GetGaugesDynamically( plr, plr.Center );
			PKEMiscLightsValues lights = logic.GetMiscLightsDynamically( plr, plr.Center );
			Color scanLightColor = this.GetProximityLightColor_Local( out float scanLightPercent );
			(PKETextMessage displayText, int displayTextOffset) = logic.GetText( plr, plr.Center );

			//
			
			this.DrawHUDComponents(
				sb: sb,
				scrPos: widgetPos,
				plr: plr,
				lightColor: plrColor,
				gauges: gauges,
				lights: lights,
				scanLightColor: scanLightColor,
				scanLightPercent: scanLightPercent,
				displayText: displayText,
				displayTextOffset: displayTextOffset,
				out (Rectangle, Rectangle, Rectangle, Rectangle) gaugeRects,
				out Rectangle marqueeRect
			);

			//

			var meterArea = new Rectangle( (int)widgetPos.X, (int)widgetPos.Y, this.MeterBody.Width, this.MeterBody.Height );

			if( meterArea.Contains(Main.MouseScreen.ToPoint()) ) {
				PKEGaugeValues values = PKEMeterAPI.GetGauge().Invoke( plr, plr.MountedCenter );

				this.DrawHUDHoverTextAt_If( sb, widgetPos, plr, gaugeRects, marqueeRect, values );
			}
		}
	}
}
