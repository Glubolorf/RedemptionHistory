using System;
using Redemption.Projectiles.v08;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.Ammo
{
	public class BileBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bile Bullet");
			base.Tooltip.SetDefault("Decreases target's defense and drains life");
		}

		public override void SetDefaults()
		{
			base.item.damage = 13;
			base.item.ranged = true;
			base.item.width = 10;
			base.item.height = 16;
			base.item.maxStack = 999;
			base.item.consumable = true;
			base.item.knockBack = 4f;
			base.item.value = 7;
			base.item.rare = 7;
			base.item.shoot = ModContent.ProjectileType<BileBulletPro>();
			base.item.shootSpeed = 5.25f;
			base.item.ammo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(97, 150);
			modRecipe.AddIngredient(null, "Bile", 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 150);
			modRecipe.AddRecipe();
		}
	}
}
