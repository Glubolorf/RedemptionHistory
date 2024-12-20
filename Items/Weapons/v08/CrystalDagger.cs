using System;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class CrystalDagger : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Crystal Dagger");
			base.Tooltip.SetDefault("Daggers split in two after a small distance");
		}

		public override void SetDefaults()
		{
			base.item.thrown = true;
			base.item.shootSpeed = 19f;
			base.item.damage = 44;
			base.item.knockBack = 5f;
			base.item.useStyle = 1;
			base.item.useAnimation = 27;
			base.item.useTime = 27;
			base.item.width = 14;
			base.item.height = 56;
			base.item.maxStack = 999;
			base.item.rare = 4;
			base.item.consumable = true;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.UseSound = SoundID.Item9;
			base.item.value = Item.sellPrice(0, 0, 1, 5);
			base.item.shoot = ModContent.ProjectileType<CrystalDaggerPro>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DruidDagger", 100);
			modRecipe.AddIngredient(502, 1);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 100);
			modRecipe.AddRecipe();
		}
	}
}
