using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Services.Timers;


namespace PKEMeter.Items {
	public partial class PKEMeterItem : ModItem {
		public static bool DisplayHUDMeter { get; internal set; } = false;

		internal static string BlueTooltip;
		internal static string GreenLabel;
		internal static string YellowLabel;
		internal static string RedLabel;



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "PKE Meter" );
			this.Tooltip.SetDefault(
				"Detects spiritual energies."
				+"\nRight-click to toggle permanent HUD display"
			);
		}

		public override void SetDefaults() {
			this.item.width = 4;
			this.item.height = 4;
			this.item.holdStyle = 1;
			this.item.value = Item.buyPrice( 0, 5, 0, 0 );
			this.item.rare = ItemRarityID.Lime;
		}

		////

		public override void HoldStyle( Player player ) {
			if( player.itemLocation.X > player.Center.X ) {
				player.itemLocation.X -= 10;
			} else {
				player.itemLocation.X += 10;
			}
		}

		////
		
		public override bool CanRightClick() {
			Timers.SetTimer( "PKEMeterToggleBlocker", 2, true, () => {
				PKEMeterItem.DisplayHUDMeter = !PKEMeterItem.DisplayHUDMeter;
				return false;
			} );
			return false;
		}


		////////////////

		public override void ModifyTooltips( List<TooltipLine> tooltips ) {
			tooltips.Add( new TooltipLine(
				this.mod,
				"PKEHUDState",
				"HUD display status: "+(PKEMeterItem.DisplayHUDMeter ? "[c/00FF00:On]" : "[c/FF0000:Off]")
			) );

			if( !string.IsNullOrEmpty(PKEMeterItem.BlueTooltip) ) {
				tooltips.Add( new TooltipLine( this.mod, "PKEGaugeBlue", PKEMeterItem.BlueTooltip ) );
			}
			if( !string.IsNullOrEmpty(PKEMeterItem.GreenLabel) ) {
				tooltips.Add( new TooltipLine( this.mod, "PKEGaugeGreen", PKEMeterItem.GreenLabel ) );
			}
			if( !string.IsNullOrEmpty(PKEMeterItem.YellowLabel) ) {
				tooltips.Add( new TooltipLine( this.mod, "PKEGaugeYellow", PKEMeterItem.YellowLabel ) );
			}
			if( !string.IsNullOrEmpty(PKEMeterItem.RedLabel) ) {
				tooltips.Add( new TooltipLine( this.mod, "PKEGaugeRed", PKEMeterItem.RedLabel ) );
			}
		}
	}
}