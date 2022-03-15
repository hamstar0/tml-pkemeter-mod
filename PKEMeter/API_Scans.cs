using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PKEMeter.Logic;


namespace PKEMeter {
	public static partial class PKEMeterAPI {
		public static PKEScannable GetScannable( string name ) {
			return PKEScannable.GetScannable( name );
		}

		public static IDictionary<string, PKEScannable> GetScannables() {
			return PKEScannable.GetScannables();
		}

		////

		public static bool SetScannable( string name, PKEScannable scannable, bool allowRepeat, bool runIfComplete ) {
			return PKEScannable.SetScannable( name, scannable, allowRepeat, runIfComplete );
		}
	}
}
