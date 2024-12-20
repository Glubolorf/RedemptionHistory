using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class SeedNade : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Seed Grenade");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nExplodes into random seeds\nLarge Seed Pouch increases the number of seeds");
		}

		public override void SetDefaults()
		{
			base.item.damage = 65;
			base.item.width = 14;
			base.item.height = 20;
			base.item.maxStack = 99;
			base.item.consumable = true;
			base.item.useStyle = 1;
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item1;
			base.item.useAnimation = 44;
			base.item.useTime = 44;
			base.item.value = Item.buyPrice(0, 1, 75, 0);
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.shoot = base.mod.ProjectileType("SeedNade");
			base.item.shootSpeed = 5.5f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddRecipeGroup("Redemption:Seedbag", 5);
			modRecipe.AddIngredient(1347, 30);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
