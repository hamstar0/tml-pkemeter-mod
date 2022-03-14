using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using PKEMeter.Items;


namespace PKEMeter {
	public partial class PKEMeterPlayer : ModPlayer {
		public Color MyColor { get; private set; } = default;
		
		public bool HasInventoryPKE { get; private set; } = false;


		////////////////

		internal ISet<string> Scans = new HashSet<string>();



		////////////////

		public override void Load( TagCompound tag ) {
			this.Scans.Clear();

			//

			if( tag.ContainsKey("pke_hud") ) {
				PKEMeterItem.DisplayHUDMeter = tag.GetBool( "pke_hud" );
			}

			if( tag.ContainsKey("scan_count") ) {
				int scans = tag.GetInt( "scan_count" );

				for( int i=0; i<scans; i++ ) {
					this.Scans.Add( tag.GetString("scan_"+i ) );
				}
			}
		}

		public override TagCompound Save() {
			var tag = new TagCompound {
				{ "pke_hud", PKEMeterItem.DisplayHUDMeter },
				{ "scan_count", this.Scans.Count }
			};

			int i = 0;
			foreach( string name in this.Scans ) {
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