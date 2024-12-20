using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class SlayerFlamethrower : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ultra-Heated Flamethrower");
			base.Tooltip.SetDefault("'Light 'em up!'\nMelts all enemies in the flame's path");
		}

		public override void SetDefaults()
		{
			base.item.damage = 70;
			base.item.ranged = true;
			base.item.width = 76;
			base.item.height = 34;
			base.item.useTime = 5;
			base.item.useAnimation = 15;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 2f;
			base.item.UseSound = SoundID.Item34;
			base.item.value = Item.sellPrice(0, 15, 0, 0);
			base.item.rare = 9;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("BlueFlames");
			base.item.shootSpeed = 7.5f;
			base.item.useAmmo = 23;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-5f, 0f));
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CyberPlating", 8);
			modRecipe.AddIngredient(null, "KingCore", 1);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
