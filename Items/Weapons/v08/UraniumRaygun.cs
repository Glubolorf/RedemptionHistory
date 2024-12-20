using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class UraniumRaygun : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Uranium Raygun");
			base.Tooltip.SetDefault("Fires rings of uranium\nCan pierce through tiles and enemies");
		}

		public override void SetDefaults()
		{
			base.item.damage = 86;
			base.item.useTime = 5;
			base.item.useAnimation = 20;
			base.item.reuseDelay = 25;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("UraniumRaygunRingPro");
			base.item.shootSpeed = 11f;
			base.item.UseSound = SoundID.Item92;
			base.item.ranged = true;
			base.item.width = 48;
			base.item.height = 32;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 0f;
			base.item.value = Item.buyPrice(0, 25, 50, 0);
			base.item.rare = 7;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Uranium", 18);
			modRecipe.AddRecipeGroup("Redemption:Plating", 3);
			modRecipe.AddRecipeGroup("Redemption:Capacitators", 1);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-2f, 0f));
		}
	}
}
