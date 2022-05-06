using System;
using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using PKEMeter.Logic;


namespace PKEMeter.Items {
	public partial class PKEMeterItem : ModItem {
		private bool _CanScanSinceLastCheck = false;
		private PKEGaugeType _PreviousAlertedGauge = 0;



		////////////////

		public void UpdateForHeldPKE_Local( bool canScan, bool scannableInInventory ) {
			if( !canScan ) {
				return;
			}

			if( scannableInInventory ) {
				PKEMeterItem.RunScanAt( Main.mouseX, Main.mouseY );
			} else {
				var config = PKEMeterConfig.Instance;
				int maxDist = config.Get<int>( nameof( config.PKEScanRange ) );
				int maxDistSqr = maxDist * maxDist;
				float distSqr = (Main.MouseWorld - Main.LocalPlayer.MountedCenter).LengthSquared();

				if( distSqr < maxDistSqr ) {
					PKEMeterItem.RunScanAt( Main.mouseX, Main.mouseY );
				}
			}
		}


		////////////////

		public void UpdateForInventoryPKE( Player player, bool isHeld, bool canScan ) {
			var mymod = PKEMeterMod.Instance;

			if( !isHeld ) {
				if( canScan != this._CanScanSinceLastCheck ) {
					this._CanScanSinceLastCheck = canScan;

					if( canScan ) {
						if( mymod.PKEScanAlert.State != SoundState.Playing ) {
							mymod.PKEScanAlert.Play();
						}
					}
				}
			}

			//

			PKEGaugesGetter gaugesGetter = PKEMeterAPI.GetGauge();
			PKEGaugeValues gaugesValues = gaugesGetter.Invoke( player, player.MountedCenter );

			PKEGaugeType significantGauge = gaugesValues.GetSignificantGauge();

			if( gaugesValues.GetGaugeValue(significantGauge) > 0.65f ) {
				if( this._PreviousAlertedGauge != significantGauge ) {
					this._PreviousAlertedGauge = significantGauge;

					if( mymod.PKEScanAlertNear.State != SoundState.Playing ) {
						mymod.PKEScanAlertNear.Play();
					}
				}
			}
		}
	}
}