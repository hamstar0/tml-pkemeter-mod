using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Players;
using PKEMeter.Items;
using PKEMeter.Logic;


namespace PKEMeter.HUD {
	partial class PKEMeterHUD : ILoadable {
		public static PKEMeterHUD Instance { get; private set; }



		////////////////

		private Texture2D MinFont;
		private Texture2D MeterBody;
		private Texture2D MeterDisplay;
		private Texture2D MeterWires;
		private Texture2D MeterLights;
		private Texture2D MeterDisplayB;
		private Texture2D MeterDisplayG;
		private Texture2D MeterDisplayY;
		private Texture2D MeterDisplayR;

		private Color LastVisiblePlayerColor;



		////////////////

		void ILoadable.OnModsLoad() {
			PKEMeterHUD.Instance = this;
		}

		void ILoadable.OnPostModsLoad() {
			if( Main.netMode != NetmodeID.Server && !Main.dedServ ) {
				this.MinFont = PKEMeterMod.Instance.GetTexture( "HUD/MinFont" );
				this.MeterBody = PKEMeterMod.Instance.GetTexture( "HUD/MeterBody" );
				this.MeterDisplay = PKEMeterMod.Instance.GetTexture( "HUD/MeterDisplay" );
				this.MeterWires = PKEMeterMod.Instance.GetTexture( "HUD/MeterWires" );
				this.MeterLights = PKEMeterMod.Instance.GetTexture( "HUD/MeterLights" );
				this.MeterDisplayB = PKEMeterMod.Instance.GetTexture( "HUD/MeterDisplayB" );
				this.MeterDisplayG = PKEMeterMod.Instance.GetTexture( "HUD/MeterDisplayG" );
				this.MeterDisplayY = PKEMeterMod.Instance.GetTexture( "HUD/MeterDisplayY" );
				this.MeterDisplayR = PKEMeterMod.Instance.GetTexture( "HUD/MeterDisplayR" );
			}
		}

		void ILoadable.OnModsUnload() {
			PKEMeterHUD.Instance = null;
		}


		////////////////

		public void DrawHUDIf( SpriteBatch sb ) {
			if( Main.playerInventory || Main.LocalPlayer.dead ) {
				return;
			}

			int meterType = ModContent.ItemType<PKEMeterItem>();

			if( PKEMeterItem.DisplayHUDMeter ) {
				if( PlayerItemFinderHelpers.CountTotalOfEach( Main.LocalPlayer, new HashSet<int> { meterType }, false ) > 0 ) {
					this.DrawHUD( sb );
				}

				return;
			}

			Item heldItem = Main.LocalPlayer.HeldItem;
			heldItem = heldItem?.active == true ? heldItem : null;

			Item mouseItem = Main.mouseItem;
			mouseItem = mouseItem?.active == true ? mouseItem : null;

			if( heldItem?.type == meterType || mouseItem?.type == meterType ) {
				this.DrawHUD( sb );
			}
		}

		public void DrawHUD( SpriteBatch sb ) {
			var config = PKEMeterConfig.Instance;
			int posX = config.Get<int>( nameof( config.PKEMeterHUDPositionX ) );
			int posY = config.Get<int>( nameof( config.PKEMeterHUDPositionY ) );
			var pos = new Vector2(
				posX < 0 ? Main.screenWidth + posX : posX,
				posY < 0 ? Main.screenHeight + posY : posY
			);

			Player plr = Main.LocalPlayer;
			var myplayer = plr.GetModPlayer<PKEMeterPlayer>();

			Color plrColor = myplayer.MyColor;
			if( plrColor.A < 255 ) {
				plrColor = this.LastVisiblePlayerColor;
			} else {
				this.LastVisiblePlayerColor = plrColor;
			}

			//

			this.DrawHUDComponents( sb, pos, plr, plrColor );

			//

			var meterArea = new Rectangle( (int)pos.X, (int)pos.Y, this.MeterBody.Width, this.MeterBody.Height );

			if( meterArea.Contains( Main.MouseScreen.ToPoint() ) ) {
				PKEText[] texts = PKEMeterAPI.GetMeterTexts();
				this.DrawHUDHoverText( sb, pos, plr, texts );
			}
		}


		////

		private void DrawHUDComponents( SpriteBatch sb, Vector2 pos, Player plr, Color plrColor ) {
			var logic = PKEMeterLogic.Instance;

			sb.Draw(
				texture: this.MeterDisplay,
				position: pos,
				color: Color.White
			);

			(float b, float g, float y, float r) gauge = logic.GetGauges( plr, plr.Center );
			this.DrawHUDGauges( sb, pos, gauge.b, gauge.g, gauge.y, gauge.r );

			(string title, string text, Color color, int offset) msg = logic.GetText( plr, plr.Center );
			this.DrawHUDText( sb, pos, msg.text, msg.color, msg.offset );

			sb.Draw(
				texture: this.MeterBody,
				position: pos,
				color: plrColor
			);

			this.DrawHUDGaugeLights(
				sb: sb,
				pos: pos,
				bLit: gauge.b > 0.99f,
				gLit: gauge.g > 0.99f,
				yLit: gauge.y > 0.99f,
				rLit: gauge.r > 0.99f
			);

			sb.Draw(
				texture: this.MeterWires,
				position: pos,
				color: plrColor
			);
		}


		////

		private void DrawHUDHoverText( SpriteBatch sb, Vector2 pos, Player plr, PKEText[] texts ) {
			Vector2 textPos = pos;
			textPos.X -= 128;

			for( int i=0; i<texts.Length; i++ ) {
				PKEText text = texts[i];
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

				textPos.Y += 12;
			}
		}
	}
}
