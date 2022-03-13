using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using PKEMeter.Items;


namespace PKEMeter {
	public class PKEMeterPlayer : ModPlayer {
		public Color MyColor { get; private set; } = default;



		////////////////

		public override void Load( TagCompound tag ) {
			if( tag.ContainsKey("pke_hud") ) {
				PKEMeterItem.DisplayHUDMeter = tag.GetBool( "pke_hud" );
			}
		}

		public override TagCompound Save() {
			return new TagCompound {
				{ "pke_hud", PKEMeterItem.DisplayHUDMeter }
			};
		}


		////////////////

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

			if( isHoldingPKE && canScan /*&& foundInInventory*/ ) {
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


		////////////////

		public override void ModifyDrawInfo( ref PlayerDrawInfo drawInfo ) {
			this.MyColor = drawInfo.bodyColor;
		}
	}
}