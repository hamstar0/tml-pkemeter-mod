using System.Linq;
using Microsoft.Xna.Framework.Audio;
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
			int pkeType = ModContent.ItemType<PKEMeterItem>();
			Item heldItem = this.player.HeldItem;

			bool isHoldingPKE = heldItem?.active == true && heldItem.type == pkeType;
			isHoldingPKE |= Main.mouseItem?.active == true && Main.mouseItem.type == pkeType;

			//

			this.UpdateForPKE( isHoldingPKE );
		}


		////////////////

		 private bool _CanScanSinceLastCheck = false;

		private void UpdateForPKE( bool isHoldingPKE ) {
			int pkeType = ModContent.ItemType<PKEMeterItem>();

			this.HasInventoryPKE = isHoldingPKE || Main.LocalPlayer.inventory
				.Any( i => i?.active == true && i.type == pkeType );

			//this.IsHoldingPKE = isHoldingPKE;

			//

			bool canScan = PKEMeterItem.CanScanAt( Main.mouseX, Main.mouseY, out bool foundInInventory );

			//

			if( isHoldingPKE && canScan ) {
				if( foundInInventory ) {
					PKEMeterItem.RunScanAt( Main.mouseX, Main.mouseY );
				} else {
					var config = PKEMeterConfig.Instance;
					int maxDist = config.Get<int>( nameof(config.PKEScanRange) );
					int maxDistSqr = maxDist * maxDist;
					float distSqr = (Main.MouseWorld - this.player.MountedCenter).LengthSquared();

					if( distSqr < maxDistSqr ) {
						PKEMeterItem.RunScanAt( Main.mouseX, Main.mouseY );
					}
				}
			}

			//

			if( this.HasInventoryPKE && canScan != this._CanScanSinceLastCheck ) {
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