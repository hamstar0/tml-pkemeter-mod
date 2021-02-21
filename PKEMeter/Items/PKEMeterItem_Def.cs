using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Services.Timers;


namespace PKEMeter.Items {
	public partial class PKEMeterItem : ModItem {
		public static bool DisplayHUDMeter { get; internal set; } = false;

		internal static Func<string> BlueTooltipGetter;
		internal static Func<string> GreenLabelGetter;
		internal static Func<string> YellowLabelGetter;
		internal static Func<string> RedLabelGetter;



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "PKE Meter" );
			this.Tooltip.SetDefault(
				"Detects spiritual energy signals."
				+"\nGauges indicate signal intensity (usually due to proximity)"
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
	}
}