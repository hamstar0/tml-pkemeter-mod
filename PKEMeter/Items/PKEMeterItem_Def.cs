using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace PKEMeter.Items {
	public partial class PKEMeterItem : ModItem {
		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "PKE Meter" );
			this.Tooltip.SetDefault( "Detects spiritual energies." );
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
	}
}