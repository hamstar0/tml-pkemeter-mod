using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using PKEMeter.Items;
using PKEMeter.Logic;


namespace PKEMeter {
	public static partial class PKEMeterAPI {
		public static void SetPKEBlueTooltip( Func<string> blueLabelGetter ) {
			PKEMeterItem.BlueTooltipGetter = blueLabelGetter;
		}

		public static void SetPKEGreenTooltip( Func<string> greenLabelGetter ) {
			PKEMeterItem.GreenLabelGetter = greenLabelGetter;
		}

		public static void SetPKEYellowTooltip( Func<string> yellowLabelGetter ) {
			PKEMeterItem.YellowLabelGetter = yellowLabelGetter;
		}

		public static void SetPKERedTooltip( Func<string> redLabelGetter ) {
			PKEMeterItem.RedLabelGetter = redLabelGetter;
		}

		public static void SetMiscTooltip( Func<string> miscLabelGetter ) {
			PKEMeterItem.MiscLabelGetter = miscLabelGetter;
		}


		////

		public static PKEGaugesGetter GetGauge() {
			return PKEMeterLogic.Instance.CurrentGauge;
		}

		public static void SetGauge( PKEGaugesGetter gauge ) {
			PKEMeterLogic.Instance.CurrentGauge = gauge;
		}

		////
		
		public static PKEMiscLightsGetter GetMiscLights() {
			return PKEMeterLogic.Instance.CurrentMiscLights;
		}

		public static void SetMiscLights( PKEMiscLightsGetter lights ) {
			PKEMeterLogic.Instance.CurrentMiscLights = lights;
		}

		////

		public static (string text, Color color, int offset) GetCurrentMeterText() {
			var logic = PKEMeterLogic.Instance;
			return logic.GetText( Main.LocalPlayer, Main.LocalPlayer.Center );
		}

		public static IDictionary<PKEGaugeType, PKETextGetter> GetMeterTexts() {
			return PKEMeterLogic.Instance.TextSources.ToDictionary(
				kv => kv.Key,
				kv => kv.Value
			);
		}
		
		public static void SetMeterText( PKEGaugeType gauge, PKETextGetter text ) {
			PKEMeterLogic.Instance.TextSources[ gauge ] = text;
		}

		public static bool RemoveMeterText( PKEGaugeType gauge ) {
			return PKEMeterLogic.Instance.TextSources.Remove( gauge );
		}
	}
}
