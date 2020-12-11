using System;
using PKEMeter.Logic;


namespace PKEMeter {
	public static class PKEMeterAPI {
		public static PKEGauge GetGauge() {
			return PKEMeterLogic.Instance.CurrentGauge;
		}

		public static void SetGauge( PKEGauge gauge ) {
			PKEMeterLogic.Instance.CurrentGauge = gauge;
		}

		public static PKEText GetMeterText() {
			return PKEMeterLogic.Instance.CurrentText;
		}

		public static void SetMeterText( PKEText text ) {
			PKEMeterLogic.Instance.CurrentText = gauge;
		}
	}
}
