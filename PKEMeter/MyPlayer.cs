using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;


namespace PKEMeter {
	public class PKEMeterPlayer : ModPlayer {
		public Color MyColor { get; private set; } = default(Color);



		////////////////

		public override void ModifyDrawInfo( ref PlayerDrawInfo drawInfo ) {
			this.MyColor = drawInfo.bodyColor;
		}
	}
}