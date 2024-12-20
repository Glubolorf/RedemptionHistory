using System;
using Redemption.Projectiles.v08;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.Ammo
{
	public class BioweaponDart : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bio-Weapon Dart");
			base.Tooltip.SetDefault("Enemies explode when killed");
		}

		public override void SetDefaults()
		{
			base.item.damage = 11;
			base.item.ranged = true;
			base.item.width = 10;
			base.item.height = 28;
			base.item.maxStack = 999;
			base.item.consumable = true;
			base.item.rare = 7;
			base.item.knockBack = 2.2f;
			base.item.value = 7;
			base.item.shoot = ModContent.ProjectileType<BioweaponDartPro>();
			base.item.shootSpeed = 3f;
			base.item.ammo = AmmoID.Dart;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Bioweapon", 1);
			modRecipe.SetResult(this, 100);
			modRecipe.AddRecipe();
		}
	}
}
