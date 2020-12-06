using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace PKEMeter.Items {
	public partial class PKEMeterItem : ModItem {
		public override void AddRecipes() {
			var recipe = new PKEMeterItemRecipe( this );
			recipe.AddRecipe();
		}
	}




	class PKEMeterItemRecipe : ModRecipe {
		public PKEMeterItemRecipe( PKEMeterItem myitem ) : base( PKEMeterMod.Instance ) {
			this.AddIngredient( ItemID.MetalDetector, 1 );
			this.AddIngredient( ItemID.MechanicalLens, 1 );
			this.AddIngredient( ItemID.ManaCrystal, 1 );
			this.AddTile( TileID.Anvils );
			this.SetResult( myitem );
		}

		public override bool RecipeAvailable() {
			var config = PKEMeterConfig.Instance;
			return config.Get<bool>( nameof(config.PKEMeterRecipeEnabled) );
		}
	}
}