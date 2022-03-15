using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ModLibsCore.Services.Hooks.LoadHooks;
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

		public static void SetScannable( string name, PKEScannable scannable, bool allowRepeat, bool runIfComplete ) {
			LoadHooks.AddWorldInPlayOnceHook( () => {
				PKEScannable.SetScannable( name, scannable, allowRepeat, runIfComplete );
			} );
		}
	}
}
