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
				return PlayerItemFinderLibraries.CountTotalOfEach( Main.LocalPlayer, new HashSet<int> { meterType }, false ) > 0;
			}

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

			Vector2 pos = this.GetHUDComputedPosition( true );

			//

			this.DrawHUDComponents( sb, pos, plr, plrColor );

			//

			/*var meterArea = new Rectangle( (int)pos.X, (int)pos.Y, this.MeterBody.Width, this.MeterBody.Height );

			if( meterArea.Contains( Main.MouseScreen.ToPoint() ) ) {
				PKETextGetter[] texts = PKEMeterAPI.GetMeterTexts();
				this.DrawHUDHoverText( sb, pos, plr, texts );
			}*/
		}


		////

		private void DrawHUDComponents( SpriteBatch sb, Vector2 pos, Player plr, Color plrColor ) {
			var logic = PKEMeterLogic.Instance;

			float opacity = 1f;//Main.playerInventory ? 0.5f : 1f;

			sb.Draw(
				texture: this.MeterDisplay,
				position: pos,
				color: Color.White * opacity
			);

			//

			PKEGaugeValues gauge = logic.GetGaugesDynamically( plr, plr.Center );
			PKEMiscLightsValues lights = logic.GetMiscLightsDynamically( plr, plr.Center );

			this.DrawHUDGauges(
				sb,
				pos,
				opacity,
				gauge.BluePercent,
				gauge.GreenPercent,
				gauge.YellowPercent,
				gauge.RedPercent
			);

			//

			(string text, Color color, int offset) msg = logic.GetText( plr, plr.Center );
			this.DrawHUDText( sb, pos, msg.text, msg.color * opacity, msg.offset );

			//

			sb.Draw(
				texture: this.MeterBody,
				position: pos,
				color: plrColor * opacity
			);

			//

			this.DrawHUDGaugeLights(
				sb: sb,
				pos: pos,
				bLit: gauge.BluePercent > 0.99f,
				gLit: gauge.GreenPercent > 0.99f,
				yLit: gauge.YellowPercent > 0.99f,
				rLit: gauge.RedPercent > 0.99f
			);

			//

			if( PKEMeterItem.CanScanAt(Main.mouseX, Main.mouseY) ) {
				this.DrawHUDScanLightsCurrentRow( sb, pos );
			}

			//
			
			if( lights != null ) {
				this.DrawHUDMiscLights(
					sb: sb,
					pos: pos,
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

			sb.Draw(
				texture: this.MeterWires,
				position: pos,
				color: plrColor * opacity
			);
		}


		////

		/*private void DrawHUDHoverText( SpriteBatch sb, Vector2 pos, Player plr, PKETextGetter[] texts ) {
			Vector2 textPos = pos;
			textPos.X -= 168;

			for( int i=0; i<texts.Length; i++ ) {
				PKETextGetter text = texts[i];
				PKETextMessage msg = text.Invoke( plr, textPos, (0f, 0f, 0f, 0f) );
				if( string.IsNullOrEmpty(msg.Title) ) {
					continue;
				}

				Utils.DrawBorderStringFourWay(
					sb: sb,
					font: Main.fontMouseText,
					text: msg.Title,
					x: textPos.X,
					y: textPos.Y,
					textColor: msg.Color,
					borderColor: Color.Black,
					origin: Vector2.Zero
				);

				textPos.Y += 18;
			}
		}*/
	}
}
