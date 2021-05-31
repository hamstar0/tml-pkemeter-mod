using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsInterMod.Libraries.Mods.APIMirrors.ModHelpersAPIMirrors;
using HUDElementsLib;
using PKEMeter.HUD;


namespace PKEMeter {
	public partial class PKEMeterMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-pkemeter-mod";


		////////////////

		public static PKEMeterMod Instance { get; private set; }



		////////////////

		public PKEMeterHUD Meter { get; private set; }



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
			if( !Main.dedServ && Main.netMode != NetmodeID.Server ) {
				this.Meter = PKEMeterHUD.CreateDefault(); //"Vanilla: Info Accessories Bar"

				HUDElementsLibAPI.AddWidget( this.Meter );
			}

			InboxAPIMirrorsLibraries.SetMessage( "DraggableHUDItem", "Drag custom HUD elements around with shift+left click.", false );
			//Vanilla: Info Accessories Bar
		}
	}
}