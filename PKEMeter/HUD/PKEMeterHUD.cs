using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.Debug;


namespace PKEMeter.HUD {
	partial class PKEMeterHUD : ILoadable {
		public static PKEMeterHUD Instance { get; private set; }



		////////////////

		public static Vector2 GetHUDPosition() {
			var config = PKEMeterConfig.Instance;
			int posX = config.Get<int>( nameof( config.PKEMeterHUDBasePositionX ) );
			int posY = config.Get<int>( nameof( config.PKEMeterHUDBasePositionY ) );
			var pos = new Vector2(
				posX < 0 ? Main.screenWidth + posX : posX,
				posY < 0 ? Main.screenHeight + posY : posY
			);

			var myplayer = Main.LocalPlayer.GetModPlayer<PKEMeterPlayer>();
			return pos + myplayer.PKEDisplayOffset;
		}



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

		private bool IsHovering = false;



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

		public bool Update() {
			bool isInterfacing = false;

			if( Main.playerInventory ) {
				if( this.CanDrawPKE() ) {
					isInterfacing = this.RunHUDEditorIf( out this.IsHovering );
				}
			} else {
				this.IsHovering = false;
			}

			return isInterfacing;
		}


		////////////////

		public bool ConsumesCursor() {
			return this.BaseDragOffset.HasValue;
		}
	}
}
