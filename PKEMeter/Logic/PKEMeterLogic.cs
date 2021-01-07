using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Classes.Loadable;


namespace PKEMeter.Logic {
	partial class PKEMeterLogic : ILoadable {
		public static PKEMeterLogic Instance { get; private set; }



		////////////////

		public PKEGauge CurrentGauge { get; internal set; }

		public (string text, Color color, float priority) CurrentText { get; internal set; } = ("", Color.White, 0f);

		public int CurrentTextTickDuration { get; private set; } = 0;

		////

		public IList<PKEText> TextSources { get; internal set; } = new List<PKEText>();


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
