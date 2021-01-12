using System;
using System.Collections.Generic;
using Terraria;
using HamstarHelpers.Classes.Loadable;


namespace PKEMeter.Logic {
	partial class PKEMeterLogic : ILoadable {
		public static PKEMeterLogic Instance { get; private set; }



		////////////////

		public PKEGauge CurrentGauge { get; internal set; }

		public string CurrentMessageId { get; internal set; } = PKETextMessage.EmptyMessage.Key;

		public int CurrentTextTickDuration { get; private set; } = 0;

		////

		public IDictionary<string, PKEText> TextSources { get; internal set; } = new Dictionary<string, PKEText>();


		////////////////


		private (float b, float g, float y, float r) GaugeSnapshot = (0, 0, 0, 0);


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
