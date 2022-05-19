using System;
using System.Collections.Generic;
using Terraria;
using ModLibsCore.Classes.Loadable;


namespace PKEMeter.Logic {
	partial class PKEMeterLogic : ILoadable {
		public static PKEMeterLogic Instance { get; private set; }



		////////////////

		public PKEGaugesGetter CurrentGaugesGetter { get; internal set; }

		public PKEMiscLightsGetter CurrentMiscLightsGetter { get; internal set; }

		public PKEGaugeType CurrentMessageGauge { get; internal set; } = PKEGaugeType.Red;

		public int CurrentTextTickDuration { get; private set; } = 0;

		////

		public IDictionary<PKEGaugeType, PKETextGetter> GaugeTextsGetter { get; internal set; }
			= new Dictionary<PKEGaugeType, PKETextGetter>();


		////////////////


		private PKEGaugeValues GaugeSnapshot = new PKEGaugeValues( 0, 0, 0, 0 );

		private PKEMiscLightsValues MiscLightsSnapshot = null;

		////////////////

		private int TextScrollPos = -6;



		////////////////

		void ILoadable.OnModsLoad() {
			PKEMeterLogic.Instance = this;

			this.InitializeDefaultGauge();
			this.InitializeDefaultText();
		}

		void ILoadable.OnPostModsLoad() {
			this.PostInitializeDefaultText();
		}

		void ILoadable.OnModsUnload() {
			PKEMeterLogic.Instance = null;
		}
	}
}
