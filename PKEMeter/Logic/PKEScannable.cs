using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using ModLibsCore.Classes.Loadable;


namespace PKEMeter.Logic {
	public partial class PKEScannable : ILoadable {
		public delegate bool CanScanDef( int screenX, int screenY );



		////////////////
		
		private CanScanDef CanScanFunc;

		private Action OnScanCompleteAction;

		private ISet<int> AnyOfItemTypes;


		////

		public float ScanPercent = 0f;



		////////////////
		
		private PKEScannable() { }
		
		public PKEScannable(
					CanScanDef canScan,
					Action onScanCompleteAction = null,
					IEnumerable<int> itemType = null ) {
			this.CanScanFunc = canScan;
			this.AnyOfItemTypes = itemType != null
				? new HashSet<int>( itemType )
				: new HashSet<int>();

			this.OnScanCompleteAction = onScanCompleteAction;
		}


		////////////////
		
		public bool CanScan( int screenX, int screenY, out bool foundInInventory ) {
			if( Main.HoverItem?.active == true ) {
				if( this.AnyOfItemTypes.Any(iType => iType == Main.HoverItem.type) ) {
					foundInInventory = true;
					return true;
				}
			}

			if( this.CanScanFunc?.Invoke(screenX, screenY) ?? false ) {
				foundInInventory = false;
				return true;
			}

			foundInInventory = false;
			return false;
		}


		////////////////

		public void RunScanComplete() {
			this.OnScanCompleteAction?.Invoke();
		}
	}
}
