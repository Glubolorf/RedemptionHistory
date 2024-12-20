using System;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class Chernobyl : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Chernobyl");
		}

		public override void SetDefaults()
		{
			base.item.damage = 40;
			base.item.melee = true;
			base.item.useTime = 20;
			base.item.useAnimation = 25;
			base.item.useStyle = 5;
			base.item.channel = true;
			base.item.knockBack = 5f;
			base.item.value = Item.buyPrice(0, 4, 0, 0);
			base.item.rare = 7;
			base.item.autoReuse = false;
			base.item.shoot = ModContent.ProjectileType<ChernobylPro>();
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.UseSound = SoundID.Item1;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(3278, 1);
			modRecipe.AddIngredient(null, "Xenomite", 15);
			modRecipe.AddIngredient(null, "Biohazard", 10);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
