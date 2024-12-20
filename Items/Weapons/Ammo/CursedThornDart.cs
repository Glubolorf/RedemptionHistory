using System;
using Redemption.Projectiles.Ranged;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.Ammo
{
	public class CursedThornDart : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Thorn Dart");
			base.Tooltip.SetDefault("Hitting an enemy will cause a burst of more Cursed Thorn Dart");
		}

		public override void SetDefaults()
		{
			base.item.damage = 35;
			base.item.ranged = true;
			base.item.width = 10;
			base.item.height = 16;
			base.item.maxStack = 999;
			base.item.consumable = true;
			base.item.knockBack = 2.3f;
			base.item.value = 70;
			base.item.shoot = ModContent.ProjectileType<CursedThornDartPro>();
			base.item.shootSpeed = 4f;
			base.item.ammo = AmmoID.Dart;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CursedThorns", 1);
			modRecipe.SetResult(this, 100);
			modRecipe.AddRecipe();
		}
	}
}
