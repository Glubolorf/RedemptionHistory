using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Melee
{
	public class AncientWoodHamaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Wood Hamaxe");
		}

		public override void SetDefaults()
		{
			base.item.damage = 9;
			base.item.melee = true;
			base.item.width = 42;
			base.item.height = 38;
			base.item.useTime = 21;
			base.item.useAnimation = 27;
			base.item.hammer = 25;
			base.item.axe = 7;
			base.item.useStyle = 1;
			base.item.knockBack = 5.8f;
			base.item.value = 50;
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientWood", 12);
			modRecipe.AddTile(null, "AncientWoodWorkbenchTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
