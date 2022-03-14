using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ModLoader;
using PKEMeter.Items;


namespace PKEMeter {
	public partial class PKEMeterPlayer : ModPlayer {
		public override void PreUpdate() {
			if( this.player.whoAmI == Main.myPlayer ) {
				this.UpdateLocal();
			}
		}

		private void UpdateLocal() {
			Item heldItem = this.player.HeldItem;
			bool isHoldingPKE = heldItem?.active == true && heldItem.type == ModContent.ItemType<PKEMeterItem>();

			this.UpdateForPKE( isHoldingPKE );
		}


		////

		 private bool _CanScanSinceLastCheck = false;

		private void UpdateForPKE( bool isHoldingPKE ) {
			bool canScan = PKEMeterItem.CanScanAt( Main.mouseX, Main.mouseY, out bool foundInInventory );

			//

			if( isHoldingPKE && canScan ) {
				var config = PKEMeterConfig.Instance;
				int maxDist = config.Get<int>( nameof(config.PKEScanRange) );
				int maxDistSqr = maxDist * maxDist;
				float distSqr = (Main.MouseWorld - this.player.MountedCenter).LengthSquared();

				if( distSqr < maxDistSqr ) {
					PKEMeterItem.RunScanAt( Main.mouseX, Main.mouseY );
				}
			}

			//

			if( canScan != this._CanScanSinceLastCheck ) {
				this._CanScanSinceLastCheck = canScan;

				if( canScan ) {
					if( PKEMeterMod.Instance.PKEScanAlert.State != SoundState.Playing ) {
						PKEMeterMod.Instance.PKEScanAlert.Play();
					}
				}
			}
		}
	}
}