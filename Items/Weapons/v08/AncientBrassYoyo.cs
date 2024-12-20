using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class AncientBrassYoyo : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Brass Yoyo");
		}

		public override void SetDefaults()
		{
			base.item.damage = 17;
			base.item.melee = true;
			base.item.useTime = 28;
			base.item.useAnimation = 28;
			base.item.useStyle = 5;
			base.item.channel = true;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 2, 5, 0);
			base.item.rare = 1;
			base.item.autoReuse = false;
			base.item.shoot = base.mod.ProjectileType("AncientBrassYoyoPro");
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.UseSound = SoundID.Item1;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientBrassIngot", 8);
			modRecipe.AddIngredient(null, "Archcloth", 2);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
