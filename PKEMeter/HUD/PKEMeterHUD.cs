using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using HUDElementsLib;


namespace PKEMeter.HUD {
	public partial class PKEMeterHUD : HUDElement {
		public static PKEMeterHUD CreateDefault() {
			Vector2 pos = PKEMeterHUD.GetBaseHUDPosition();
			Texture2D bodyTex = PKEMeterMod.Instance.GetTexture( "HUD/MeterBody" );

			return new PKEMeterHUD( "PKEMeter", pos, new Vector2(bodyTex.Width, bodyTex.Height) );
		}


		////////////////

		public static Vector2 GetBaseHUDPosition() {
			var config = PKEMeterConfig.Instance;
			int posX = config.Get<int>( nameof( config.PKEMeterHUDBasePositionX ) );
			int posY = config.Get<int>( nameof( config.PKEMeterHUDBasePositionY ) );
			var pos = new Vector2(
				posX < 0 ? Main.screenWidth + posX : posX,
				posY < 0 ? Main.screenHeight + posY : posY
			);

			return pos;
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



		////////////////
		
		private PKEMeterHUD( string name, Vector2 position, Vector2 dim ) : base( name, position, dim ) {
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


		////////////////

		public override bool IsInteractive() {
			return PKEMeterHUD.CanDrawPKE();
		}
	}
}
