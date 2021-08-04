using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
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

		public override void Load() {
			PKEMeterMod.Instance = this;
		}

		public override void Unload() {
			PKEMeterMod.Instance = null;
		}


		////////////////

		public override void PostSetupContent() {
			if( !Main.dedServ && Main.netMode != NetmodeID.Server ) {
				this.Meter = PKEMeterHUD.CreateDefault(); //"Vanilla: Info Accessories Bar"

				HUDElementsLibAPI.AddWidget( this.Meter );
			}
			//Vanilla: Info Accessories Bar
		}
	}
}