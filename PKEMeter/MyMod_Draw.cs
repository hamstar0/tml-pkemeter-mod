using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.ModLoader;
using PKEMeter.Items;


namespace PKEMeter {
	public partial class PKEMeterMod : Mod {
		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			int layerIdx = layers.FindIndex( layer => layer.Name.Equals("Vanilla: Mouse Over") );
			if( layerIdx == -1 ) {
				return;
			}

			//

			var binocsLayer = new LegacyGameInterfaceLayer(
				"PKE: Mouse Icon",
				() => {
					this.DrawMousePKE_If( Main.spriteBatch );
					return true;
				},
				InterfaceScaleType.UI
			);

			//

			layers.Insert( layerIdx + 1, binocsLayer );
		}


		////////////////

		private bool DrawMousePKE_If( SpriteBatch sb ) {
			//Item heldItem = Main.LocalPlayer.HeldItem;
			//if( heldItem?.active != true || heldItem.type != ModContent.ItemType<PKEMeterItem>() ) {
			//	return false;
			//}
			if( !PKEMeterItem.CanScanAt(Main.mouseX, Main.mouseY, out _) ) {
				return false;
			}

			//

			Vector2 pos = Main.MouseScreen + new Vector2(-32f, -16f);

			sb.Draw(
				texture: ModContent.GetTexture("PKEMeter/Items/PKEMeterItem"),
				position: pos,
				color: Color.White * 0.5f
			);

			//

			return true;
		}
	}
}