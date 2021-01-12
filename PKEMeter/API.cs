using System;
using System.Linq;
using PKEMeter.Logic;


namespace PKEMeter {
	public static class PKEMeterAPI {
		public static PKEGauge GetGauge() {
			return PKEMeterLogic.Instance.CurrentGauge;
		}

		public static void SetGauge( PKEGauge gauge ) {
			PKEMeterLogic.Instance.CurrentGauge = gauge;
		}

		////

		public static PKEText[] GetMeterTexts() {
			return PKEMeterLogic.Instance.TextSources.Values.ToArray();
		}

		public static void SetMeterText( string id, PKEText text ) {
			PKEMeterLogic.Instance.TextSources[ id ] = text;
		}

		public static bool RemoveMeterText( string id ) {
			return PKEMeterLogic.Instance.TextSources.Remove( id );
		}
	}
}
