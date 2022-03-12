using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.ModLoader;
using PKEMeter.Items;
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
			//Item heldItem = Main.LocalPlayer.HeldItem;

			//if( heldItem?.active == true && heldItem.type == ModContent.ItemType<PKEMeterItem>() ) {
			if( PKEScannable.ScannableItems.ContainsKey(item.type) ) {
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