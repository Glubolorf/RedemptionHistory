using System;
using Redemption.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Ranged
{
	public class BonfireDagger : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bonfire Dagger");
			base.Tooltip.SetDefault("Daggers split into flames after a small distance");
		}

		public override void SetDefaults()
		{
			base.item.thrown = true;
			base.item.shootSpeed = 21f;
			base.item.damage = 12;
			base.item.knockBack = 5f;
			base.item.useStyle = 1;
			base.item.useAnimation = 25;
			base.item.useTime = 25;
			base.item.width = 22;
			base.item.height = 52;
			base.item.maxStack = 999;
			base.item.rare = 3;
			base.item.consumable = true;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.UseSound = SoundID.Item1;
			base.item.value = Item.sellPrice(0, 0, 1, 0);
			base.item.shoot = ModContent.ProjectileType<BonfireDaggerPro>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DruidDagger", 100);
			modRecipe.AddIngredient(174, 5);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 100);
			modRecipe.AddRecipe();
		}
	}
}
