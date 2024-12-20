using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.Ammo
{
	public class BioweaponBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bio-Weapon Bullet");
			base.Tooltip.SetDefault("Enemies explode when killed");
		}

		public override void SetDefaults()
		{
			base.item.damage = 12;
			base.item.ranged = true;
			base.item.width = 10;
			base.item.height = 16;
			base.item.maxStack = 999;
			base.item.consumable = true;
			base.item.knockBack = 4f;
			base.item.value = 7;
			base.item.rare = 7;
			base.item.shoot = base.mod.ProjectileType("BioweaponBulletPro");
			base.item.shootSpeed = 5f;
			base.item.ammo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(97, 150);
			modRecipe.AddIngredient(null, "Bioweapon", 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 150);
			modRecipe.AddRecipe();
		}
	}
}
