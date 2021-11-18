using System;
using System.Linq;
using PKEMeter.Items;
using PKEMeter.Logic;


namespace PKEMeter {
	public static class PKEMeterAPI {
		public static void SetPKEBlueTooltip( Func<string> blueLabelGetter ) {
			PKEMeterItem.BlueTooltipGetter = blueLabelGetter;
		}

		public static void SetPKEGreenTooltip( Func<string> greenLabelGetter ) {
			PKEMeterItem.GreenLabelGetter = greenLabelGetter;
		}

		public static void SetPKEYellowTooltip( Func<string> yellowLabelGetter ) {
			PKEMeterItem.YellowLabelGetter = yellowLabelGetter;
		}

		public static void SetPKERedTooltip( Func<string> redLabelGetter ) {
			PKEMeterItem.RedLabelGetter = redLabelGetter;
		}

		public static void SetMiscTooltip( Func<string> miscLabelGetter ) {
			PKEMeterItem.MiscLabelGetter = miscLabelGetter;
		}


		////

		public static PKEGaugesGetter GetGauge() {
			return PKEMeterLogic.Instance.CurrentGauge;
		}

		public static void SetGauge( PKEGaugesGetter gauge ) {
			PKEMeterLogic.Instance.CurrentGauge = gauge;
		}

		////
		
		public static PKEMiscLightsGetter GetMiscLights() {
			return PKEMeterLogic.Instance.CurrentMiscLights;
		}

		public static void SetMiscLights( PKEMiscLightsGetter lights ) {
			PKEMeterLogic.Instance.CurrentMiscLights = lights;
		}

		////

		public static PKETextGetter[] GetMeterTexts() {
			return PKEMeterLogic.Instance.TextSources.Values.ToArray();
		}

		public static void SetMeterText( string id, PKETextGetter text ) {
			PKEMeterLogic.Instance.TextSources[ id ] = text;
		}

		public static bool RemoveMeterText( string id ) {
			return PKEMeterLogic.Instance.TextSources.Remove( id );
		}
	}
}
