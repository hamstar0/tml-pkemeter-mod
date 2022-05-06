using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Services.Timers;


namespace PKEMeter.Items {
	public partial class PKEMeterItem : ModItem {
		public static bool DisplayHUDMeter { get; internal set; } = true;

		internal static Func<string> BlueTooltipGetter;
		internal static Func<string> GreenLabelGetter;
		internal static Func<string> YellowLabelGetter;
		internal static Func<string> RedLabelGetter;
		internal static Func<string> MiscLabelGetter;



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "PKE Analysis Device" );
			this.Tooltip.SetDefault(
				"Analyzes psycho-kinetic (spiritual) energy signals."
				+"\nGauges indicate signal intensity (usually due to proximity)"
				//+"\nLeft-click to scan scannable objects"
				+"\nRight-click to toggle permanent HUD display"
			);
		}

		public override void SetDefaults() {
			this.item.width = 4;
			this.item.height = 4;

			this.item.holdStyle = 1;
			//this.item.useTime = 20;
			//this.item.useAnimation = 20;
			//this.item.useStyle = ItemUseStyleID.HoldingUp;
			//this.item.noMelee = true;
			//this.item.autoReuse = true;

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


		////////////////

		public override bool CanRightClick() {
			Timers.SetTimer( "PKEMeterToggleBlocker", 3, true, () => {
				PKEMeterItem.DisplayHUDMeter = !PKEMeterItem.DisplayHUDMeter;

				return false;
			} );
			return false;
		}


		////////////////

		/*public override bool CanUseItem( Player player ) {
			return PKEMeterItem.CanScanAt( Main.mouseX, Main.mouseY, out bool foundInInventory )
				&& !foundInInventory;
		}

		public override bool UseItem( Player player ) {
			PKEMeterItem.RunScanAt( Main.mouseX, Main.mouseY );

			return base.UseItem( player );
		}*/
	}
}