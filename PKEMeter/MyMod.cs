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

		public PKEMeterHUD MeterWidget { get; private set; }

		////

		public SoundEffectInstance PKEScanAlert { get; private set; } = null;

		public SoundEffectInstance PKEScanAlertNear { get; private set; } = null;

		public SoundEffectInstance PKEScanLoop { get; private set; } = null;

		public SoundEffectInstance PKEScanDone { get; private set; } = null;

		public SoundEffectInstance PKEScanPing { get; private set; } = null;



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
				SoundEffect alertNearSfx = this.GetSound( "Sounds/Custom/ScanAlertNear" );
				SoundEffect scanSfx = this.GetSound( "Sounds/Custom/Scan" );
				SoundEffect scanDoneSfx = this.GetSound( "Sounds/Custom/ScanDone" );
				SoundEffect scanPingSfx = this.GetSound( "Sounds/Custom/ScanPing" );

				this.PKEScanAlert = alertSfx.CreateInstance();
				this.PKEScanAlertNear = alertNearSfx.CreateInstance();
				this.PKEScanLoop = scanSfx.CreateInstance();
				this.PKEScanDone = scanDoneSfx.CreateInstance();
				this.PKEScanPing = scanPingSfx.CreateInstance();

				this.PKEScanAlert.Volume = 0.1f;
				this.PKEScanAlertNear.Volume = 0.05f;
				this.PKEScanLoop.Volume = 0.2f;
				this.PKEScanLoop.IsLooped = true;
				this.PKEScanDone.Volume = 0.2f;
				this.PKEScanPing.Volume = 0.015f;

				//

				this.MeterWidget = PKEMeterHUD.CreateDefault(); //"Vanilla: Info Accessories Bar"

				HUDElementsLibAPI.AddWidget( this.MeterWidget );
			}
		}
	}
}