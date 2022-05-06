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
			var myplayer = Main.LocalPlayer.GetModPlayer<PKEMeterPlayer>();
			if( !myplayer.HasPKESinceLastCheck ) {
				return false;
			}

			if( !PKEMeterItem.CanScanAt(Main.mouseX, Main.mouseY, out _) ) {
				return false;
			}

			//

			Vector2 pos = Main.MouseScreen + new Vector2(-16f, -24f);   //new Vector2(-32f, -16f);
			float pulse = (float)Main.mouseTextColor / 255f;

			sb.Draw(
				texture: ModContent.GetTexture("PKEMeter/Items/PKEMeterItem"),
				position: pos,
				color: Color.White * 0.5f * pulse
			);

			//

			return true;
		}
	}
}