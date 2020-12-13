using System;
using Terraria;
using HamstarHelpers.Classes.Loadable;


namespace PKEMeter.Logic {
	partial class PKEMeterLogic : ILoadable {
		public static PKEMeterLogic Instance { get; private set; }



		////////////////

		private (float b, float g, float y, float r) GaugeSnapshot = (0, 0, 0, 0);



		////////////////

		void ILoadable.OnModsLoad() {
			PKEMeterLogic.Instance = this;
		}

		void ILoadable.OnPostModsLoad() {
			this.InitializeDefaultGauge();
			this.InitializeDefaultText();
		}

		void ILoadable.OnModsUnload() {
			PKEMeterLogic.Instance = null;
		}
	}
}
