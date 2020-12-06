using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;


namespace PKEMeter {
	public delegate (float bluePercent, float greenPercent, float yellowPercent, float redPercent)
		PKEGauge( Player player, Vector2 position );




	public partial class PKEMeterMod : Mod {
		private void InitializeDefaultGauge() {
			float b = 0, g = 0, y = 0, r = 0;

			this.CurrentGauge = (_, __) => {
				if( Main.rand.NextFloat() < (1f / 60f) ) {
					b = Main.rand.NextFloat();
					g = Main.rand.NextFloat();
					y = Main.rand.NextFloat();
					r = Main.rand.NextFloat();
				}
				return (b, g, y, r);
			};
		}
	}
}