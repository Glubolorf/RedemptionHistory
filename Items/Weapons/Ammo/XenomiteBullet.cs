using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.Ammo
{
	public class XenomiteBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenomite Bullet");
			base.Tooltip.SetDefault("Inflicts mildly infected\nHas a chance to inflict stronger infection");
		}

		public override void SetDefaults()
		{
			base.item.damage = 5;
			base.item.ranged = true;
			base.item.width = 12;
			base.item.height = 12;
			base.item.maxStack = 999;
			base.item.consumable = true;
			base.item.knockBack = 2f;
			base.item.value = 1;
			base.item.rare = 1;
			base.item.shoot = ModContent.ProjectileType<XenomiteBulletPro>();
			base.item.shootSpeed = 4f;
			base.item.ammo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(97, 33);
			modRecipe.AddIngredient(null, "XenomiteShard", 1);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 33);
			modRecipe.AddRecipe();
		}
	}
}
