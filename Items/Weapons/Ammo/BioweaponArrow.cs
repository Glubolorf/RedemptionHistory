using System;
using Redemption.Projectiles.v08;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.Ammo
{
	public class BioweaponArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bio-Weapon Arrow");
			base.Tooltip.SetDefault("Enemies explode when killed");
		}

		public override void SetDefaults()
		{
			base.item.damage = 17;
			base.item.ranged = true;
			base.item.width = 18;
			base.item.height = 40;
			base.item.maxStack = 999;
			base.item.consumable = true;
			base.item.knockBack = 3f;
			base.item.value = 17;
			base.item.rare = 7;
			base.item.shoot = ModContent.ProjectileType<BioweaponArrowPro>();
			base.item.shootSpeed = 4f;
			base.item.ammo = AmmoID.Arrow;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(40, 150);
			modRecipe.AddIngredient(null, "Bioweapon", 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 150);
			modRecipe.AddRecipe();
		}
	}
}
