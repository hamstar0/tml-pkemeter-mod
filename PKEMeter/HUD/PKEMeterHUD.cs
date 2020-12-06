using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Loadable;
using PKEMeter.Items;


namespace PKEMeter.HUD {
	class PKEMeterHUD : ILoadable {
		public static PKEMeterHUD Instance { get; private set; }



		////////////////

		private Texture2D MeterBody;
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
				this.MeterBody = PKEMeterMod.Instance.GetTexture( "HUD/MeterBody" );
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
			if( Main.LocalPlayer.HeldItem?.type != ModContent.ItemType<PKEMeterItem>() ) {
				return;
			}
			this.DrawHUD( sb );
		}

		public void DrawHUD( SpriteBatch sb ) {
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
				texture: this.MeterBody,
				position: pos,
				color: plrColor
			);

			(float b, float g, float y, float r) gauge = PKEMeterMod.Instance.CurrentGauge?.Invoke( plr, plr.Center )
				?? (0, 0, 0, 0);
			this.DrawHUDGauges( sb, pos, gauge.b, gauge.g, gauge.y, gauge.r );

			sb.Draw(
				texture: this.MeterWires,
				position: pos,
				color: plrColor
			);
		}


		public void DrawHUDGauges( SpriteBatch sb, Vector2 pos, float b, float g, float y, float r ) {
			pos.X += 22;
			pos.Y += 16;

			var destRect = new Rectangle( (int)pos.X, (int)pos.Y + 42, 6, 4 );
			var bDestRect = destRect;
			var gDestRect = destRect;
			var yDestRect = destRect;
			var rDestRect = destRect;

			//bDestRect.X += 0;
			gDestRect.X += 8;
			yDestRect.X += 16;
			rDestRect.X += 24;

			float bTicks = 7f * b;
			float gTicks = 7f * g;
			float yTicks = 7f * y;
			float rTicks = 7f * r;

			this.DrawHUDGaugeTicks( sb, this.MeterDisplayB, bDestRect, bTicks );
			this.DrawHUDGaugeTicks( sb, this.MeterDisplayG, gDestRect, gTicks );
			this.DrawHUDGaugeTicks( sb, this.MeterDisplayY, yDestRect, yTicks );
			this.DrawHUDGaugeTicks( sb, this.MeterDisplayR, rDestRect, rTicks );
		}


		private void DrawHUDGaugeTicks( SpriteBatch sb, Texture2D tickTex, Rectangle rect, float ticks ) {
			for( int i=0; i<(int)ticks; i++ ) {
				rect.Y -= 6;

				sb.Draw(
					texture: tickTex,
					destinationRectangle: rect,
					color: Color.White
				);
			}

			float tickFrac = ticks - (float)(int)ticks;

			if( Main.rand.NextFloat() < tickFrac ) {
				rect.Y -= 6;

				sb.Draw(
					texture: tickTex,
					destinationRectangle: rect,
					color: Color.White
				);
			}
		}
	}
}
