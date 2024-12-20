using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Usable
{
	public class PlutoniumBomb : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Plutonium Bomb");
			base.Tooltip.SetDefault("Falls from the sky, turns the small blast radius into plutonium");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 14;
			base.item.maxStack = 99;
			base.item.consumable = true;
			base.item.useStyle = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.useAnimation = 25;
			base.item.useTime = 25;
			base.item.rare = 9;
			base.item.value = Item.buyPrice(2, 0, 0, 0);
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.shoot = ModContent.ProjectileType<PlutoniumBombPro>();
			base.item.shootSpeed = 0f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(3467, 8);
			modRecipe.AddRecipeGroup("Redemption:Capacitators", 1);
			modRecipe.AddRecipeGroup("Redemption:Plating", 3);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(new Vector2(player.position.X, player.position.Y + -1300f), new Vector2(speedX, speedY), type, 500, 5f, player.whoAmI, 0f, 0f);
			return false;
		}
	}
}
