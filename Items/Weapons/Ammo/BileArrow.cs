using System;
using Redemption.Projectiles.v08;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.Ammo
{
	public class BileArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bile Arrow");
			base.Tooltip.SetDefault("Decreases target's defense and drains life");
		}

		public override void SetDefaults()
		{
			base.item.damage = 16;
			base.item.ranged = true;
			base.item.width = 14;
			base.item.height = 38;
			base.item.maxStack = 999;
			base.item.consumable = true;
			base.item.knockBack = 3f;
			base.item.value = 17;
			base.item.rare = 7;
			base.item.shoot = ModContent.ProjectileType<BileArrowPro>();
			base.item.shootSpeed = 4.25f;
			base.item.ammo = AmmoID.Arrow;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(40, 150);
			modRecipe.AddIngredient(null, "Bile", 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 150);
			modRecipe.AddRecipe();
		}
	}
}
