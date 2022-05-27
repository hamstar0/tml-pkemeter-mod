using System;
using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Services.Timers;
using PKEMeter.Logic;


namespace PKEMeter.Items {
	public partial class PKEMeterItem : ModItem {
		public void UpdateForHeldPKE_Local( bool canScan, bool scannableInInventory ) {
			if( !canScan ) {
				return;
			}

			if( scannableInInventory ) {
				PKEMeterItem.RunScanAt_If( Main.mouseX, Main.mouseY );
			} else {
				var config = PKEMeterConfig.Instance;
				int maxDist = config.Get<int>( nameof(config.PKEScanRange) );
				int maxDistSqr = maxDist * maxDist;
				float distSqr = (Main.MouseWorld - Main.LocalPlayer.MountedCenter).LengthSquared();

				if( distSqr < maxDistSqr ) {
					PKEMeterItem.RunScanAt_If( Main.mouseX, Main.mouseY );
				}
			}
		}


		////////////////

		public void UpdateForInventoryPKE_Local( bool isHeld, bool canScan ) {
			this.UpdateForScanState_Local( isHeld, canScan );

			this.UpdateForNearbyReadings_Local();
		}


		////

		 private bool _CanScanSinceLastCheck = false;

		public void UpdateForScanState_Local( bool isHeld, bool canScan ) {
			if( isHeld ) {
				return;
			}

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


		////////////////

		 private PKEGaugeType? _CurrentSignificantGauge = null;

		private void UpdateForNearbyReadings_Local() {
			var mymod = PKEMeterMod.Instance;

			float significantGuageIntenisty = PKEMeterLogic.GetSignificantGaugeIntensityPercent_Local(
				out PKEGaugeType significantGauge
			);

			//

			if( PKEMeterItem.DisplayHUDMeter ) {
				bool isNewSignal = !this._CurrentSignificantGauge.HasValue
								|| this._CurrentSignificantGauge.Value != significantGauge;
//if( isNewSignal ) {
//	Main.NewText( $"old significant gauge {this._CurrentSignificantGauge}, new sig gauge {significantGauge}" );
//}

				this.ApplyProxmityFx(
					significantGauge: significantGauge,
					signalPercent: significantGuageIntenisty,
					isNewSignal: isNewSignal
				);
			}

			mymod.MeterWidget.SetProximityLights( significantGauge, significantGuageIntenisty );

			//

			this._CurrentSignificantGauge = significantGauge;
		}
	}
}