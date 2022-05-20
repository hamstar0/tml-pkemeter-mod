using System;
using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Services.Timers;
using PKEMeter.Logic;


namespace PKEMeter.Items {
	public partial class PKEMeterItem : ModItem {
		private void ApplyProxmityFx( PKEGaugeType significantGauge, float signalPercent, bool isNewSignal ) {
			var mymod = PKEMeterMod.Instance;

			//

			// Alert to readings nearby (any)
			if( signalPercent > 0f ) {
				if( isNewSignal ) {
					if( mymod.PKEScanAlertNear.State != SoundState.Playing ) {
						mymod.PKEScanAlertNear.Play();
					}
				}
			}

			// Apply repeating alert sounds
			if( signalPercent > 0f ) {
				// Cap intensity to ambiguate results
				float fxIntensityPecent = signalPercent;
				if( fxIntensityPecent > 0.7f ) {
					fxIntensityPecent = 0.7f;
				}

				int fxTickRate = 15 + (int)((1f - fxIntensityPecent) * 105f);

				if( Timers.GetTimerTickDuration("PKEPingLoop") <= 0 ) {
					Timers.SetTimer( "PKEPingLoop", fxTickRate, false, () => {
						return false;
					} );

					mymod.PKEScanPing.Play();
				}
			}

			// Display scanner lights corresponding to nearby readings
			mymod.MeterWidget.SetProximityLights( significantGauge, signalPercent );
		}
	}
}