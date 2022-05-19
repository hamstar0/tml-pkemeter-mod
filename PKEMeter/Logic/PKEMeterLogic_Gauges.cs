using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using ModLibsCore.Classes.Loadable;


namespace PKEMeter.Logic {
	partial class PKEMeterLogic : ILoadable {
		private static PKEGaugeValues DefaultGaugeGet(
					ref int proxCheckTimer,
					float b,
					float g,
					float y,
					float r ) {
			/*if( Main.rand.NextFloat() < (1f / 60f) ) {
				b = Main.rand.NextFloat();
				g = Main.rand.NextFloat();
				y = Main.rand.NextFloat();
				r = Main.rand.NextFloat();
			}*/

			// Report proximity to first found boss
			if( proxCheckTimer++ > 5 ) {
				proxCheckTimer = 0;
				NPC npc = Main.npc.FirstOrDefault( n =>
					n?.active == true
					&& ( n.boss || n.type == NPCID.EaterofWorldsHead )
				);

				if( npc != null ) {
					r = ( npc.Center - Main.LocalPlayer.Center ).Length() / ( 160f * 16f );
					r = 1f - r;
					r = Math.Max( r, 0f );
				} else {
					r = 0f;
				}
			}

			return new PKEGaugeValues( b, g, y, r );
		}

		public static float GetSignificantGaugeIntensityPercent_Local( out PKEGaugeType significantGauge ) {
			float minGaugeAlertPercent = 0.65f;

			PKEGaugesGetter gaugesGetter = PKEMeterAPI.GetGauge();
			PKEGaugeValues gaugesValues = gaugesGetter.Invoke( Main.LocalPlayer, Main.LocalPlayer.MountedCenter );

			significantGauge = gaugesValues.GetSignificantGauge();

			float gaugeValue = gaugesValues.GetGaugeValue( significantGauge, true );
			float gaugeIntensity = gaugeValue - minGaugeAlertPercent;
			gaugeIntensity /= 1f - minGaugeAlertPercent;

			return gaugeIntensity;
		}



		////////////////

		private void InitializeDefaultGauge() {
			float b = 0, g = 0, y = 0, r = 0;
			int proxCheckTimer = 0;

			if( this.CurrentGaugesGetter == null ) {
				this.CurrentGaugesGetter = ( _, __ ) => PKEMeterLogic.DefaultGaugeGet( ref proxCheckTimer, b, g, y, r );
			}
		}


		////

		public PKEGaugeValues GetGaugesDynamically( Player player, Vector2 position ) {
			this.GaugeSnapshot = this.CurrentGaugesGetter?.Invoke( player, position )
				?? new PKEGaugeValues( 0f, 0f, 0f, 0f);
			return this.GaugeSnapshot;
		}

		public PKEMiscLightsValues GetMiscLightsDynamically( Player player, Vector2 position ) {
			this.MiscLightsSnapshot = this.CurrentMiscLightsGetter?.Invoke( player, position )
				?? null;
//new PKEMiscLightsValues( Color.Blue, Color.Cyan, Color.Lime, Color.HotPink, Color.DarkMagenta, Color.Red, Color.Peru, Color.White, Color.Yellow );
			return this.MiscLightsSnapshot;
		}
	}
}
