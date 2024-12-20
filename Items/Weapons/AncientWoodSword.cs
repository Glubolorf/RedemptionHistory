using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class AncientWoodSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Wood Sword");
		}

		public override void SetDefaults()
		{
			base.item.damage = 22;
			base.item.melee = true;
			base.item.width = 36;
			base.item.height = 42;
			base.item.useTime = 17;
			base.item.useAnimation = 17;
			base.item.useStyle = 1;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 0, 0, 50);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientWood", 7);
			modRecipe.AddTile(null, "AncientWoodWorkbenchTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
