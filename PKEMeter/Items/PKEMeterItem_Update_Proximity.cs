using System;
using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Services.Timers;
using PKEMeter.Logic;


namespace PKEMeter.Items {
	public partial class PKEMeterItem : ModItem {
		private PKEGaugeType _CurrentSignificantGauge = 0;



		////////////////

		private void UpdateForNearbyReadings_Local() {
			var mymod = PKEMeterMod.Instance;

			//

			float significantGuageIntenisty = PKEMeterLogic.GetSignificantGaugeIntensityPercent_Local(
				out PKEGaugeType significantGauge
			);

			// Alert to readings nearby (any)
			if( significantGuageIntenisty > 0f ) {
				if( this._CurrentSignificantGauge == 0 ) {

					if( mymod.PKEScanAlertNear.State != SoundState.Playing ) {
						mymod.PKEScanAlertNear.Play();
					}
				}

				this._CurrentSignificantGauge = significantGauge;
			} else {
				this._CurrentSignificantGauge = 0;
			}

			// Apply repeating alert sounds
			if( significantGuageIntenisty > 0f ) {
				int fxTickRate = 15 + (int)((1f - significantGuageIntenisty) * 105f);

				if( Timers.GetTimerTickDuration( "PKEPingLoop" ) <= 0 ) {
					Timers.SetTimer( "PKEPingLoop", fxTickRate, false, () => {
						return false;
					} );

					mymod.PKEScanPing.Play();
				}
			}

			// Display scanner lights corresponding to nearby readings
			if( this._CurrentSignificantGauge != 0 ) {
				mymod.Meter.SetProximityLights_If( this._CurrentSignificantGauge, significantGuageIntenisty );
			}
		}
	}
}