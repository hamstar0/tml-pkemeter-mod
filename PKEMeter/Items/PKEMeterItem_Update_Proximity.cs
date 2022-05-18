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

			// Apply repeating alert sounds
			if( gaugeValue > minGaugeAlertPercent ) {
				float gaugeIntensity = gaugeValue - minGaugeAlertPercent;
				gaugeIntensity /= 1f - minGaugeAlertPercent;

				int fxTickRate = 15 + (int)((1f - gaugeIntensity) * 105f);

				if( Timers.GetTimerTickDuration( "PKEPingLoop" ) <= 0 ) {
					Timers.SetTimer( "PKEPingLoop", fxTickRate, false, () => {
						return false;
					} );

					mymod.PKEScanPing.Play();
				}
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