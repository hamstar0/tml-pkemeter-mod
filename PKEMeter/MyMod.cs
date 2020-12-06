using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using PKEMeter.HUD;


namespace PKEMeter {
	public partial class PKEMeterMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-pkemeter-mod";


		////////////////

		public static PKEMeterMod Instance { get; private set; }



		////////////////

		public PKEGauge CurrentGauge { get; internal set; }



		////////////////

		public PKEMeterMod() {
			PKEMeterMod.Instance = this;

			this.InitializeDefaultGauge();
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

		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			int idx = layers.FindIndex( layer => layer.Name.Equals("Vanilla: Mouse Text") );
			if( idx == -1 ) { return; }

			//

			GameInterfaceDrawMethod drawHUDMeter = () => {
				PKEMeterHUD.Instance.DrawHUDIf( Main.spriteBatch );
				return true;
			};

			//

			var debugLayer = new LegacyGameInterfaceLayer( "PKE Meter: HUD Display",
				drawHUDMeter,
				InterfaceScaleType.UI );
			layers.Insert( idx, debugLayer );
		}
	}
}