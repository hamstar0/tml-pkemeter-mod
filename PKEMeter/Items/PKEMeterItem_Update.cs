using System;
using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using PKEMeter.Logic;


namespace PKEMeter.Items {
	public partial class PKEMeterItem : ModItem {
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
			this.UpdateForScanState( isHeld, canScan );

			this.UpdateForNearbyReadings( player );
		}


		////

		 private bool _CanScanSinceLastCheck = false;

		public void UpdateForScanState( bool isHeld, bool canScan ) {
			if( isHeld ) {
				return;
			}

			//

			if( canScan == this._CanScanSinceLastCheck ) {
				return;
			}

			this._CanScanSinceLastCheck = canScan;

			//

			if( canScan ) {
				var mymod = PKEMeterMod.Instance;

				if( mymod.PKEScanAlert.State != SoundState.Playing ) {
					mymod.PKEScanAlert.Play();
				}
			}
		}


		////

		 private PKEGaugeType _CurrentSignificantGauge = 0;

		private void UpdateForNearbyReadings( Player player ) {
			float minGaugeAlertPercent = 0.65f;

			var mymod = PKEMeterMod.Instance;

			//

			PKEGaugesGetter gaugesGetter = PKEMeterAPI.GetGauge();
			PKEGaugeValues gaugesValues = gaugesGetter.Invoke( player, player.MountedCenter );

			PKEGaugeType significantGauge = gaugesValues.GetSignificantGauge();
			float gaugeValue = gaugesValues.GetGaugeValue( significantGauge, true );

			// Alert to readings nearby (any)
			if( gaugeValue > minGaugeAlertPercent ) {
				if( this._CurrentSignificantGauge == 0 ) {

					if( mymod.PKEScanAlertNear.State != SoundState.Playing ) {
						mymod.PKEScanAlertNear.Play();
					}
				}

				this._CurrentSignificantGauge = significantGauge;
			} else {
				this._CurrentSignificantGauge = 0;
			}

			// Display scanner lights corresponding to nearby readings
			if( this._CurrentSignificantGauge != 0 ) {
				float scanAlertPercent = gaugeValue - minGaugeAlertPercent;
				scanAlertPercent /= (1f - minGaugeAlertPercent);

				mymod.Meter.SetProximityLights_If( this._CurrentSignificantGauge, scanAlertPercent );
			}
		}
	}
}