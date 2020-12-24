﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Loadable;
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
		private Texture2D MeterDisplayB;
		private Texture2D MeterDisplayG;
		private Texture2D MeterDisplayY;
		private Texture2D MeterDisplayR;



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
			if( Main.playerInventory ) {
				return;
			}

			int meterType = ModContent.ItemType<PKEMeterItem>();

			Item heldItem = Main.LocalPlayer.HeldItem;
			heldItem = heldItem?.active == true ? heldItem : null;

			Item mouseItem = Main.mouseItem;
			mouseItem = mouseItem?.active == true ? mouseItem : null;

			if( heldItem?.type == meterType || mouseItem?.type == meterType ) {
				this.DrawHUD( sb );
			}
		}

		public void DrawHUD( SpriteBatch sb ) {
			var logic = PKEMeterLogic.Instance;
			var config = PKEMeterConfig.Instance;
			int posX = config.Get<int>( nameof(config.PKEMeterHUDPositionX) );
			int posY = config.Get<int>( nameof(config.PKEMeterHUDPositionY) );
			var pos = new Vector2(
				posX < 0 ? Main.screenWidth + posX : posX,
				posY < 0 ? Main.screenHeight + posY : posY
			);

			Player plr = Main.LocalPlayer;
			var myplayer = plr.GetModPlayer<PKEMeterPlayer>();
			Color plrColor = myplayer.MyColor;

			sb.Draw(
				texture: this.MeterDisplay,
				position: pos,
				color: Color.White
			);

			(float b, float g, float y, float r) gauge = logic.GetGauges( plr, plr.Center );
			this.DrawHUDGauges( sb, pos, gauge.b, gauge.g, gauge.y, gauge.r );

			(string text, Color color, int offset) msg = logic.GetText( plr, plr.Center );
			this.DrawHUDText( sb, pos, msg.text, msg.color, msg.offset );

			sb.Draw(
				texture: this.MeterBody,
				position: pos,
				color: plrColor
			);

			sb.Draw(
				texture: this.MeterWires,
				position: pos,
				color: plrColor
			);
		}
	}
}
