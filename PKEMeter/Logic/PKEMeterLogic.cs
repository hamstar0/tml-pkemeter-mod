using System;
using System.Collections.Generic;
using Terraria;
using ModLibsCore.Classes.Loadable;


namespace PKEMeter.Logic {
	partial class PKEMeterLogic : ILoadable {
		public static PKEMeterLogic Instance { get; private set; }



		////////////////

		public PKEGaugesGetter CurrentGauge { get; internal set; }

		public string CurrentMessageId { get; internal set; } = PKETextMessage.EmptyMessage.Key;

		public int CurrentTextTickDuration { get; private set; } = 0;

		////

		public IDictionary<string, PKETextGetter> TextSources { get; internal set; } = new Dictionary<string, PKETextGetter>();


		////////////////


		private PKEGaugeValues GaugeSnapshot = new PKEGaugeValues( 0, 0, 0, 0);


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
