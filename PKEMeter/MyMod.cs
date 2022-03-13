using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ID;
using Terraria.UI;
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

		////

		public SoundEffectInstance PKEScanAlert { get; private set; } = null;

		public SoundEffectInstance PKEScanLoop { get; private set; } = null;

		public SoundEffectInstance PKEScanDone { get; private set; } = null;



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
				SoundEffect alertSfx = this.GetSound( "Sounds/Custom/ScanAlert" );
				SoundEffect scanSfx = this.GetSound( "Sounds/Custom/Scan" );
				SoundEffect scanDoneSfx = this.GetSound( "Sounds/Custom/ScanDone" );

				this.PKEScanAlert = alertSfx.CreateInstance();
				this.PKEScanLoop = scanSfx.CreateInstance();
				this.PKEScanDone = scanDoneSfx.CreateInstance();

				this.PKEScanAlert.Volume = 0.2f;
				this.PKEScanLoop.Volume = 0.2f;
				this.PKEScanLoop.IsLooped = true;
				this.PKEScanDone.Volume = 0.2f;

				//

				this.Meter = PKEMeterHUD.CreateDefault(); //"Vanilla: Info Accessories Bar"

				HUDElementsLibAPI.AddWidget( this.Meter );
			}
		}
	}
}