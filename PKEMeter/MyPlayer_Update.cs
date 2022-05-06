using System.Linq;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using PKEMeter.Items;


namespace PKEMeter {
	public partial class PKEMeterPlayer : ModPlayer {
		public override void PreUpdate() {
			if( !this.player.dead && this.player.whoAmI == Main.myPlayer ) {
				this.Update_Local();
			}
		}

		public override void UpdateAutopause() {
			if( !this.player.dead && this.player.whoAmI == Main.myPlayer ) {
				this.Update_Local();
			}
		}


		////////////////

		private void Update_Local() {
			Item pke = PKEMeterPlayer.GetPreferredPKE( this.player, true, out bool isPKEInventoryOnly );
			var mypke = pke.modItem as PKEMeterItem;

			//

			this.HasPKESinceLastCheck = pke != null;

			//this.IsHoldingPKE = isHoldingPKE;

			//

			bool canScan = PKEMeterItem.CanScanAt( Main.mouseX, Main.mouseY, out bool scannableInInventory );

			//

			if( this.HasPKESinceLastCheck && !isPKEInventoryOnly ) {
				mypke.UpdateForHeldPKE_Local( canScan, scannableInInventory );
			}

			if( this.HasPKESinceLastCheck ) {
				mypke.UpdateForInventoryPKE( this.player, !isPKEInventoryOnly, canScan );
			}
		}
	}
}