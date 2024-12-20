using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.Ammo
{
	public class KaniteBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Kanite Bullet");
			base.Tooltip.SetDefault("Breaks into fragments on hit");
		}

		public override void SetDefaults()
		{
			base.item.damage = 5;
			base.item.ranged = true;
			base.item.width = 14;
			base.item.height = 14;
			base.item.maxStack = 999;
			base.item.consumable = true;
			base.item.knockBack = 2f;
			base.item.value = 1;
			base.item.rare = 1;
			base.item.shoot = ModContent.ProjectileType<KaniteBulletPro>();
			base.item.shootSpeed = 4.25f;
			base.item.ammo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "KaniteBar", 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 100);
			modRecipe.AddRecipe();
		}
	}
}
