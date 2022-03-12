using System;
using Terraria;
using ModLibsCore.Classes.Loadable;


namespace PKEMeter.Logic {
	public partial class PKEScannable : ILoadable {
		public delegate bool CanScanDef( int screenX, int screenY );



		////////////////
		
		private CanScanDef CanScanFunc;

		private int ItemType;

		private Action OnScanCompleteAction;


		////

		public float ScanPercent = 0f;



		////////////////
		
		private PKEScannable() { }
		
		public PKEScannable(
					CanScanDef canScan,
					Action onScanCompleteAction = null,
					int itemType = 0 ) {
			this.CanScanFunc = canScan;
			this.ItemType = itemType;

			this.OnScanCompleteAction = onScanCompleteAction;
		}


		////////////////
		
		public bool CanScan( int screenX, int screenY, out bool foundInInventory ) {
			if( Main.HoverItem?.active == true && Main.HoverItem.type == this.ItemType ) {
				foundInInventory = true;
				return true;
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
