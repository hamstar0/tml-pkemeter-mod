using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using PKEMeter.Items;


namespace PKEMeter {
	public partial class PKEMeterPlayer : ModPlayer {
		public static Item GetPreferredPKE( Player player, bool checkMouseItem, out bool isInventoryOnly ) {
			int pkeType = ModContent.ItemType<PKEMeterItem>();

			if( player.HeldItem?.active == true && player.HeldItem.type == pkeType ) {
				isInventoryOnly = false;
				return player.HeldItem;
			}

			if( checkMouseItem ) {
				if( Main.mouseItem?.active == true && Main.mouseItem.type == pkeType ) {
					isInventoryOnly = false;
					return Main.mouseItem;
				}
			}

			Item invPKE = player.inventory.FirstOrDefault( i => i?.active == true && i.type == pkeType );
			isInventoryOnly = invPKE != null;

			return invPKE;
		}



		////////////////

		public Color MyColor { get; private set; } = default;
		
		public bool HasPKESinceLastCheck { get; private set; } = false;


		////////////////

		internal ISet<string> AlreadyScanned = new HashSet<string>();

		

		////////////////

		public override void Load( TagCompound tag ) {
			this.AlreadyScanned.Clear();

			//

			if( tag.ContainsKey("pke_hud") ) {
				PKEMeterItem.DisplayHUDMeter = tag.GetBool( "pke_hud" );
			}

			if( tag.ContainsKey("scan_count") ) {
				int scans = tag.GetInt( "scan_count" );

				for( int i=0; i<scans; i++ ) {
					this.AlreadyScanned.Add( tag.GetString("scan_"+i ) );
				}
			}
		}

		public override TagCompound Save() {
			var tag = new TagCompound {
				{ "pke_hud", PKEMeterItem.DisplayHUDMeter },
				{ "scan_count", this.AlreadyScanned.Count }
			};

			int i = 0;
			foreach( string name in this.AlreadyScanned ) {
				tag[ "scan_"+i ] = name;
			}

			return tag;
		}


		////////////////

		public override void ModifyDrawInfo( ref PlayerDrawInfo drawInfo ) {
			this.MyColor = drawInfo.bodyColor;
		}
	}
}