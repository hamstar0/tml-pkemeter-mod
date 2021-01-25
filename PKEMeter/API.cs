using System;
using System.Linq;
using PKEMeter.Items;
using PKEMeter.Logic;


namespace PKEMeter {
	public static class PKEMeterAPI {
		public static void SetPKETooltips( string blueLabel, string greenLabel, string yellowLabel, string redLabel ) {
			PKEMeterItem.BlueTooltip = blueLabel;
			PKEMeterItem.GreenLabel = greenLabel;
			PKEMeterItem.YellowLabel = yellowLabel;
			PKEMeterItem.RedLabel = redLabel;
		}

		////

		public static PKEGaugesGetter GetGauge() {
			return PKEMeterLogic.Instance.CurrentGauge;
		}

		public static void SetGauge( PKEGaugesGetter gauge ) {
			PKEMeterLogic.Instance.CurrentGauge = gauge;
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
