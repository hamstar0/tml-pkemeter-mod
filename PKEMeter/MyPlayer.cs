using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using PKEMeter.Items;


namespace PKEMeter {
	public class PKEMeterPlayer : ModPlayer {
		public Color MyColor { get; private set; } = default;



		////////////////

		public override void Load( TagCompound tag ) {
			if( tag.ContainsKey("pke_hud") ) {
				PKEMeterItem.DisplayHUDMeter = tag.GetBool( "pke_hud" );
			}
		}

		public override TagCompound Save() {
			return new TagCompound {
				{ "pke_hud", PKEMeterItem.DisplayHUDMeter }
			};
		}



		////////////////

		public override void ModifyDrawInfo( ref PlayerDrawInfo drawInfo ) {
			this.MyColor = drawInfo.bodyColor;
		}
	}
}