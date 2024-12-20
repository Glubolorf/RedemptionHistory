using System;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class CrystalShuriken : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Crystal Shuriken");
			base.Tooltip.SetDefault("Shurikens split in two after a small distance\nNot consumable, but deals less damage to compensate");
		}

		public override void SetDefaults()
		{
			base.item.thrown = true;
			base.item.shootSpeed = 21f;
			base.item.crit = 4;
			base.item.damage = 43;
			base.item.knockBack = 5f;
			base.item.useStyle = 1;
			base.item.useAnimation = 30;
			base.item.useTime = 30;
			base.item.width = 14;
			base.item.height = 56;
			base.item.maxStack = 1;
			base.item.rare = 4;
			base.item.consumable = false;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.UseSound = SoundID.Item9;
			base.item.value = Item.sellPrice(0, 0, 1, 5);
			base.item.shoot = ModContent.ProjectileType<CrystalShurikenPro>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CrystalDagger", 999);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "DruidShuriken", 1);
			modRecipe2.AddIngredient(502, 10);
			modRecipe2.AddTile(16);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
