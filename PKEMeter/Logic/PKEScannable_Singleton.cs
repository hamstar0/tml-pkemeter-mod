using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Libraries.DotNET.Extensions;


namespace PKEMeter.Logic {
	public partial class PKEScannable : ILoadable {
		public static PKEScannable Instance => ModContent.GetInstance<PKEScannable>();

		////

		internal static IDictionary<string, PKEScannable> Scannables
			=> PKEScannable.Instance.SingletonScannables;



		////////////////
		
		public static PKEScannable GetScannable( string name ) {
			return PKEScannable.Scannables.GetOrDefault( name );
		}

		public static IDictionary<string, PKEScannable> GetScannables() {
			return new Dictionary<string, PKEScannable>(
				PKEScannable.Scannables
			);
		}

		////

		public static bool SetScannable( string name, PKEScannable scannable ) {
			IDictionary<string, PKEScannable> scannables = PKEScannable.Instance.SingletonScannables;

			if( scannables.ContainsKey(name) ) {
				return false;
			}

			//

			scannables[ name ] = scannable;

			return true;
		}


		////////////////

		public static bool CompleteScan( string name ) {
			IDictionary<string, PKEScannable> scannables = PKEScannable.Instance.SingletonScannables;

			if( !scannables.ContainsKey(name) ) {
				return false;
			}

			//

			scannables[ name ].RunScanComplete();

			return scannables.Remove( name );
		}



		////////////////

		void ILoadable.OnModsLoad() {
			this.SingletonScannables = new Dictionary<string, PKEScannable>();
		}

		void ILoadable.OnPostModsLoad() { }

		void ILoadable.OnModsUnload() { }
	}
}
