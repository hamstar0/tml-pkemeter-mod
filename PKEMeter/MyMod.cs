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


			Mod msgMod = ModLoader.GetMod( "Messages" );
			if( msgMod != null ) {
				msgMod.Call(
					"AddMessage",
					"How to use Nihilism mod",	//title
					"Drag custom HUD elements around with shift+left click.", //description
					HUDElementsLibMod.Instance,	//modOfOrigin
					"DraggableHUDItem", //id
					0,	//weight
					msgMod.Call( "GetMessage", "Messages - Mod Info" ), //parentMessage
					true	//alertPlayer
				);
			}
			//Vanilla: Info Accessories Bar
		}
	}
}