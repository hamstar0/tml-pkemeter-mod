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
		
		private IDictionary<string, PKEScannable> SingletonScannables = null;



		////////////////

		public void RunScanComplete() {
			this.OnScanComplete?.Invoke();
		}
	}
}
