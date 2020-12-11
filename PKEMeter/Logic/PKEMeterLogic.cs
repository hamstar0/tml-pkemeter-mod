using System;
using Terraria;
using HamstarHelpers.Classes.Loadable;


namespace PKEMeter.Logic {
	partial class PKEMeterLogic : ILoadable {
		public static PKEMeterLogic Instance { get; private set; }



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
