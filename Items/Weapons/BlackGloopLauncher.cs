using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class BlackGloopLauncher : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Gloop Launcher");
			base.Tooltip.SetDefault("Shoots gloop that explodes into smaller pieces of gloop");
		}

		public override void SetDefaults()
		{
			base.item.damage = 140;
			base.item.ranged = true;
			base.item.width = 58;
			base.item.height = 58;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.useStyle = 5;
			base.item.knockBack = 9f;
			base.item.UseSound = SoundID.Item11;
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.rare = 10;
			base.item.shoot = base.mod.ProjectileType("BlackGloopBall");
			base.item.shootSpeed = 20f;
			base.item.autoReuse = true;
			base.item.noMelee = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "BlackGloop", 12);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-12f, 0f));
		}
	}
}
