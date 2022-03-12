using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsGeneral.Libraries.Draw;
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
				SoundEffect scanSfx = this.GetSound( "Sounds/Custom/Scan" );
				SoundEffect scanDoneSfx = this.GetSound( "Sounds/Custom/ScanDone" );

				this.PKEScanLoop = scanSfx.CreateInstance();
				this.PKEScanDone = scanDoneSfx.CreateInstance();

				//
				
				this.Meter = PKEMeterHUD.CreateDefault(); //"Vanilla: Info Accessories Bar"

				HUDElementsLibAPI.AddWidget( this.Meter );
			}
		}


public override void PostDrawInterface( SpriteBatch spriteBatch ) {
	DrawLibraries.DrawBorderedRect(
		spriteBatch,
		Color.Transparent,
		Color.Red,
		new Rectangle(128, 128, 64, 96),
		4
	);
}
	}
}