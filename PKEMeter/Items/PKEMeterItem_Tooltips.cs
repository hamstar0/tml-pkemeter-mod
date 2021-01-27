using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;


namespace PKEMeter.Items {
	public partial class PKEMeterItem : ModItem {
		public override void ModifyTooltips( List<TooltipLine> tooltips ) {
			tooltips.Add( new TooltipLine(
				this.mod,
				"PKEHUDState",
				"HUD display status: "+(PKEMeterItem.DisplayHUDMeter ? "[c/00FF00:On]" : "[c/FF0000:Off]")
			) );

			if( PKEMeterItem.BlueTooltipGetter != null ) {
				string text = "Blue gauge bar's label: \"[c/8888FF:"+PKEMeterItem.BlueTooltipGetter.Invoke()+"]\"";
				tooltips.Add( new TooltipLine( this.mod, "PKEGaugeBlue", text ) );
			}
			if( PKEMeterItem.GreenLabelGetter != null ) {
				string text = "Green gauge bar's label: \"[c/88FF88:"+PKEMeterItem.GreenLabelGetter.Invoke()+"]\"";
				tooltips.Add( new TooltipLine( this.mod, "PKEGaugeGreen", text ) );
			}
			if( PKEMeterItem.YellowLabelGetter != null ) {
				string text = "Yellow gauge bar's label: \"[c/DDDD88:"+PKEMeterItem.YellowLabelGetter.Invoke()+"]\"";
				tooltips.Add( new TooltipLine( this.mod, "PKEGaugeYellow", text ) );
			}
			if( PKEMeterItem.RedLabelGetter != null ) {
				string text = "Red gauge bar's label: \"[c/FF8888:"+PKEMeterItem.RedLabelGetter.Invoke()+"]\"";
				tooltips.Add( new TooltipLine( this.mod, "PKEGaugeRed", text ) );
			}
		}
	}
}