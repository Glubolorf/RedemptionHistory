using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class Betelguise : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Betelguise");
		}

		public override void SetDefaults()
		{
			base.item.damage = 7;
			base.item.melee = true;
			base.item.width = 40;
			base.item.height = 14;
			base.item.useTime = 12;
			base.item.useAnimation = 15;
			base.item.channel = true;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.pick = 60;
			base.item.useStyle = 5;
			base.item.knockBack = 2f;
			base.item.value = 5000;
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item23;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("BetelguiseDrillPro");
			base.item.shootSpeed = 40f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(3521, 1);
			modRecipe.AddIngredient(null, "Nightshade", 10);
			modRecipe.AddRecipeGroup("Redemption:EvilWood", 10);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(3485, 1);
			modRecipe2.AddIngredient(null, "Nightshade", 10);
			modRecipe2.AddRecipeGroup("Redemption:EvilWood", 10);
			modRecipe2.AddTile(16);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
