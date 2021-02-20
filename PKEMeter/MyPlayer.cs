using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using PKEMeter.Items;


namespace PKEMeter {
	public class PKEMeterPlayer : ModPlayer {
		public Color MyColor { get; private set; } = default;

		public Vector2 PKEDisplayOffset { get; internal set; } = default;



		////////////////

		public override void Initialize() {
			this.PKEDisplayOffset = default;
		}

		public override void Load( TagCompound tag ) {
			this.PKEDisplayOffset = default;

			if( tag.ContainsKey("pke_hud") ) {
				PKEMeterItem.DisplayHUDMeter = tag.GetBool( "pke_hud" );
			}

			if( tag.ContainsKey("pke_offset_x") ) {
				this.PKEDisplayOffset = new Vector2(
					tag.GetInt( "pke_offset_x" ),
					tag.GetInt( "pke_offset_y" )
				);
			}
		}

		public override TagCompound Save() {
			return new TagCompound {
				{ "pke_hud", PKEMeterItem.DisplayHUDMeter },
				{ "pke_offset_x", (int)this.PKEDisplayOffset.X },
				{ "pke_offset_y", (int)this.PKEDisplayOffset.Y }
			};
		}



		////////////////

		public override void ModifyDrawInfo( ref PlayerDrawInfo drawInfo ) {
			this.MyColor = drawInfo.bodyColor;
		}
	}
}