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
			return PKEMeterLogic.Instance.TextSources.ToArray();
		}

		public static void AddMeterText( PKEText text ) {
			PKEMeterLogic.Instance.TextSources.Add( text );
		}

		public static bool RemoveMeterText( PKEText text ) {
			return PKEMeterLogic.Instance.TextSources.Remove( text );
		}
	}
}
