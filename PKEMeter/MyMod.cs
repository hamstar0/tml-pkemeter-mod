using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using HamstarHelpers.Services.Messages.Inbox;
using PKEMeter.HUD;


namespace PKEMeter {
	public partial class PKEMeterMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-pkemeter-mod";


		////////////////

		public static PKEMeterMod Instance { get; private set; }



		////////////////

		public PKEMeterMod() {
			PKEMeterMod.Instance = this;
		}

		////

		public override void Load() {
			PKEMeterConfig.Instance = ModContent.GetInstance<PKEMeterConfig>();
		}

		public override void Unload() {
			PKEMeterConfig.Instance = null;
			PKEMeterMod.Instance = null;
		}


		////////////////

		public override void PostSetupContent() {
			InboxMessages.SetMessage( "DraggableHUDItem", "Drag custom HUD elements around with shift+left click.", false );
		}


		////////////////

		public override void UpdateUI( GameTime gameTime ) {
			PKEMeterHUD.Instance.Update();
		}


		////////////////

		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			GameInterfaceDrawMethod drawHUDMeter = () => {
				var hud = PKEMeterHUD.Instance;
				if( hud.CanDrawPKE() ) {
					hud.DrawHUD( Main.spriteBatch );
				}
				return true;
			};

			//

			int infoAccBarIdx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Info Accessories Bar" ) );	//"Vanilla: Mouse Text"
			if( infoAccBarIdx == -1 ) { return; }

			var hudLayer = new LegacyGameInterfaceLayer( "PKE Meter: HUD Display",
				drawHUDMeter,
				InterfaceScaleType.UI );
			layers.Insert( infoAccBarIdx, hudLayer );

			//

			int cursorIdx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Cursor" ) );
			if( cursorIdx == -1 ) { return; }

			if( PKEMeterHUD.Instance.ConsumesCursor() ) {
				layers.RemoveAt( cursorIdx );
			}
		}
	}
}