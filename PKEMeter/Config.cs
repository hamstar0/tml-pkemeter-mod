using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;
using HamstarHelpers.Classes.UI.ModConfig;


namespace PKEMeter {
	class MyFloatInputElement : FloatInputElement { }




	public partial class PKEMeterConfig : ModConfig {
		public static PKEMeterConfig Instance { get; internal set; }



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;


		////////////////

		public bool DebugModeFakeSignals { get; set; } = false;


		////////////////

		[DefaultValue( true )]
		public bool PKEMeterRecipeEnabled { get; set; } = true;


		[DefaultValue( -128 )]
		public int PKEMeterHUDPositionX { get; set; } = -128;

		[DefaultValue( -144 )]
		public int PKEMeterHUDPositionY { get; set; } = -96;
	}
}
