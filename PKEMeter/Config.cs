using System;
using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using ModLibsCore.Classes.UI.ModConfig;


namespace PKEMeter {
	class MyFloatInputElement : FloatInputElement { }




	public partial class PKEMeterConfig : ModConfig {
		public static PKEMeterConfig Instance => ModContent.GetInstance<PKEMeterConfig>();



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;


		////////////////

		//public bool DebugModeFakeSignals { get; set; } = false;


		////////////////

		[DefaultValue( true )]
		public bool PKEMeterRecipeEnabled { get; set; } = true;


		[DefaultValue( -128 )]
		public int PKEMeterHUDBasePositionX { get; set; } = -128;

		[DefaultValue( -144 )]
		public int PKEMeterHUDBasePositionY { get; set; } = -96;
	}
}
