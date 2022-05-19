using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.XNA;
using HUDElementsLib;
using PKEMeter.Logic;
using PKEMeter.Items;


namespace PKEMeter.HUD {
	public partial class PKEMeterHUD : HUDElement {
		public static PKEMeterHUD CreateDefault() {
			(Vector2 posOffset, Vector2 posPerc) = PKEMeterHUD.GetBaseHUDPosition();
			Texture2D bodyTex = PKEMeterMod.Instance.GetTexture( "HUD/MeterBody" );

			return new PKEMeterHUD( "PKEMeter", posOffset, posPerc, new Vector2(bodyTex.Width, bodyTex.Height) );
		}


		////////////////

		public static (Vector2 posOffset, Vector2 posPerc) GetBaseHUDPosition() {
			var config = PKEMeterConfig.Instance;
			int posX = config.Get<int>( nameof(config.PKEMeterHUDBasePositionX) );
			int posY = config.Get<int>( nameof(config.PKEMeterHUDBasePositionY) );

			var posOffset = new Vector2( posX, posY );
			var posPerc = new Vector2(
				posOffset.X < 0 ? 1f : 0f,
				posOffset.Y < 0 ? 1f : 0f
			);

			return (posOffset, posPerc);
		}



		////////////////

		private Texture2D MinFont;
		private Texture2D MeterBody;
		private Texture2D MeterBodyScan;
		private Texture2D MeterDisplay;
		private Texture2D MeterWires;
		private Texture2D MeterLights;
		private Texture2D MeterScanLightsRow;
		private Texture2D MeterDisplayB;
		private Texture2D MeterDisplayG;
		private Texture2D MeterDisplayY;
		private Texture2D MeterDisplayR;

		private Color LastVisiblePlayerColor;

		private PKEGaugeType LastSignificantGaugeNearby;
		private float LastSignificantGaugeNearbyPercent;



		////////////////

		private PKEMeterHUD( string name, Vector2 positionOffset, Vector2 positionPercent, Vector2 dim )
					: base( name, positionOffset, positionPercent, dim, () => PKEMeterHUD.CanDrawPKE() ) {
			this.MinFont = PKEMeterMod.Instance.GetTexture( "HUD/MinFont" );
			this.MeterBody = PKEMeterMod.Instance.GetTexture( "HUD/MeterBody" );
			this.MeterBodyScan = PKEMeterMod.Instance.GetTexture( "HUD/MeterBodyScan" );
			this.MeterDisplay = PKEMeterMod.Instance.GetTexture( "HUD/MeterDisplay" );
			this.MeterWires = PKEMeterMod.Instance.GetTexture( "HUD/MeterWires" );
			this.MeterLights = PKEMeterMod.Instance.GetTexture( "HUD/MeterLights" );
			this.MeterScanLightsRow = PKEMeterMod.Instance.GetTexture( "HUD/MeterScanLightsRow" );
			this.MeterDisplayB = PKEMeterMod.Instance.GetTexture( "HUD/MeterDisplayB" );
			this.MeterDisplayG = PKEMeterMod.Instance.GetTexture( "HUD/MeterDisplayG" );
			this.MeterDisplayY = PKEMeterMod.Instance.GetTexture( "HUD/MeterDisplayY" );
			this.MeterDisplayR = PKEMeterMod.Instance.GetTexture( "HUD/MeterDisplayR" );

			XNALibraries.PremultiplyTexture( this.MeterScanLightsRow );
		}


		////////////////
		
		public void SetProximityLights( PKEGaugeType gauge, float percent ) {
			this.LastSignificantGaugeNearby = gauge;
			this.LastSignificantGaugeNearbyPercent = percent;
		}


		public Color GetProximityLightColor_Local( out float scanLightPercent ) {
			bool canScan = PKEMeterItem.CanScanAt( Main.mouseX, Main.mouseY, out bool foundInInventory );

			// Manual scannables always override custom "nearby readings" lights
			if( canScan && foundInInventory ) {
				scanLightPercent = 1f;
				return Color.White;
			}

			//

			scanLightPercent = this.LastSignificantGaugeNearbyPercent;

			return PKEGaugeValues.GetColor( this.LastSignificantGaugeNearby )
				?? Color.White;
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
