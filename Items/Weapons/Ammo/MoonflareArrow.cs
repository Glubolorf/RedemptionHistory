using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.Ammo
{
	public class MoonflareArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Moonflare Arrow");
			base.Tooltip.SetDefault("Ignores gravity");
		}

		public override void SetDefaults()
		{
			base.item.damage = 5;
			base.item.ranged = true;
			base.item.width = 14;
			base.item.height = 34;
			base.item.maxStack = 999;
			base.item.consumable = true;
			base.item.knockBack = 2.5f;
			base.item.value = 2;
			base.item.rare = 0;
			base.item.shoot = base.mod.ProjectileType("MoonflareArrowPro");
			base.item.shootSpeed = 7f;
			base.item.ammo = AmmoID.Arrow;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(40, 20);
			modRecipe.AddIngredient(null, "MoonflareFragment", 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 20);
			modRecipe.AddRecipe();
		}
	}
}
