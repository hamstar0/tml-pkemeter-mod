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
			return PKEMeterLogic.Instance.CurrentGaugesGetter;
		}

		public static void SetGauge( PKEGaugesGetter gauge ) {
			PKEMeterLogic.Instance.CurrentGaugesGetter = gauge;
		}

		////
		
		public static PKEMiscLightsGetter GetMiscLights() {
			return PKEMeterLogic.Instance.CurrentMiscLightsGetter;
		}

		public static void SetMiscLights( PKEMiscLightsGetter lights ) {
			PKEMeterLogic.Instance.CurrentMiscLightsGetter = lights;
		}

		////

		public static (PKETextMessage message, int offset) GetCurrentMeterText() {
			var logic = PKEMeterLogic.Instance;
			return logic.GetText( Main.LocalPlayer, Main.LocalPlayer.Center );
		}

		public static IDictionary<PKEGaugeType, PKETextGetter> GetMeterTexts() {
			return PKEMeterLogic.Instance.GaugeTextsGetter.ToDictionary(
				kv => kv.Key,
				kv => kv.Value
			);
		}
		
		public static void SetMeterText( PKEGaugeType gauge, PKETextGetter text ) {
			PKEMeterLogic.Instance.GaugeTextsGetter[ gauge ] = text;
		}

		public static bool RemoveMeterText( PKEGaugeType gauge ) {
			return PKEMeterLogic.Instance.GaugeTextsGetter.Remove( gauge );
		}
	}
}
