using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using ModLibsCore.Classes.Loadable;


namespace PKEMeter.Logic {
	public partial class PKEScannable : ILoadable {
		public Func<Rectangle> ScreenAreaGetter { get; private set; }

		public object Data { get; private set; }

		////

		public event Action OnScanComplete;


		////////////////

		public float ScanPercent = 0f;



		////////////////
		
		private PKEScannable() { }
		
		public PKEScannable( Func<Rectangle> screenAreaGetter, Action onScanComplete, object data = null ) {
			this.ScreenAreaGetter = screenAreaGetter;
			this.OnScanComplete = onScanComplete;
			this.Data = data;
		}



		////////////////

		public void RunScanComplete() {
			this.OnScanComplete?.Invoke();
		}
	}
}
