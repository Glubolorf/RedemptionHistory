using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class Orion : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Orion");
			base.Tooltip.SetDefault("Right-clicking fires a homing bullet, at the cost of 15 mana");
		}

		public override void SetDefaults()
		{
			base.item.damage = 28;
			base.item.ranged = true;
			base.item.width = 46;
			base.item.height = 22;
			base.item.useTime = 33;
			base.item.useAnimation = 33;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 5f;
			base.item.value = Item.sellPrice(0, 3, 0, 0);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item11;
			base.item.autoReuse = true;
			base.item.shoot = 10;
			base.item.shootSpeed = 80f;
			base.item.useAmmo = AmmoID.Bullet;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				base.item.mana = 15;
				base.item.shoot = base.mod.ProjectileType("OrionBulletPro");
				base.item.shootSpeed = 16f;
				base.item.useAmmo = 0;
			}
			else
			{
				base.item.mana = 0;
				base.item.shoot = 10;
				base.item.shootSpeed = 80f;
				base.item.useAmmo = AmmoID.Bullet;
			}
			return base.CanUseItem(player);
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-4f, 0f));
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(96, 1);
			modRecipe.AddIngredient(null, "Nightshade", 10);
			modRecipe.AddRecipeGroup("Redemption:EvilWood", 15);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(800, 1);
			modRecipe.AddIngredient(null, "Nightshade", 10);
			modRecipe.AddRecipeGroup("Redemption:EvilWood", 15);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
