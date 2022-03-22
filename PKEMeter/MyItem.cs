using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.TModLoader;
using PKEMeter.Logic;


namespace PKEMeter {
	public class PKEMeterGlobalItem : GlobalItem {
		public override void PostDrawInInventory(
					Item item,
					SpriteBatch sb,
					Vector2 pos,
					Rectangle frame,
					Color drawColor,
					Color itemColor,
					Vector2 origin,
					float scale ) {
			var myplayer = TmlLibraries.SafelyGetModPlayer<PKEMeterPlayer>( Main.LocalPlayer );

			if( myplayer.HasInventoryPKE ) {
				var scanItems = PKEScannable.ScannableItems;

				if( scanItems.ContainsKey(item.type) && scanItems[item.type].Count > 0 ) {
					float pulse = (float)Main.mouseTextColor / 255f;

					Utils.DrawBorderStringFourWay(
						sb: sb,
						font: Main.fontMouseText,
						text: "!",
						x: pos.X - 8f,
						y: pos.Y - 8f,
						textColor: Color.Yellow * pulse,
						borderColor: Color.Black,
						origin: default
					);
				}
			}
		}
	}
}