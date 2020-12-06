using System;


namespace PKEMeter {
	public static class PKEMeterAPI {
		public static PKEGauge GetGauge() {
			return PKEMeterMod.Instance.CurrentGauge;
		}

		public static void SetGauge( PKEGauge gauge ) {
			PKEMeterMod.Instance.CurrentGauge = gauge;
		}
	}
}
