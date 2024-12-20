using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class Sirius : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sirius");
			base.Tooltip.SetDefault("Fragments into poisoning shards upon hitting an enemy");
		}

		public override void SetDefaults()
		{
			base.item.width = 32;
			base.item.height = 32;
			base.item.value = Item.sellPrice(0, 0, 74, 0);
			base.item.rare = 1;
			base.item.noMelee = true;
			base.item.useStyle = 5;
			base.item.useAnimation = 42;
			base.item.useTime = 42;
			base.item.knockBack = 9.5f;
			base.item.damage = 17;
			base.item.scale = 1f;
			base.item.noUseGraphic = true;
			base.item.shoot = base.mod.ProjectileType("SiriusPro");
			base.item.shootSpeed = 14f;
			base.item.UseSound = SoundID.Item1;
			base.item.melee = true;
			base.item.channel = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(162, 1);
			modRecipe.AddIngredient(null, "Nightshade", 10);
			modRecipe.AddRecipeGroup("Redemption:EvilWood", 15);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
