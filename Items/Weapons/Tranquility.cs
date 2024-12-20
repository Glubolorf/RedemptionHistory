using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class Tranquility : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Tranquility");
			base.Tooltip.SetDefault("Occasionally fires a shattering Shooting Star Arrow");
		}

		public override void SetDefaults()
		{
			base.item.damage = 13;
			base.item.ranged = true;
			base.item.width = 26;
			base.item.height = 56;
			base.item.useTime = 22;
			base.item.useAnimation = 22;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 0f;
			base.item.value = Item.sellPrice(0, 0, 50, 0);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item5;
			base.item.autoReuse = false;
			base.item.shoot = 10;
			base.item.shootSpeed = 6.6f;
			base.item.useAmmo = AmmoID.Arrow;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-2f, 0f));
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (Main.rand.Next(4) == 0)
			{
				Vector2 vector = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(2f));
				speedX = vector.X;
				speedY = vector.Y;
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, base.mod.ProjectileType("ShootingStarArrow"), 10, 2f, player.whoAmI, 0f, 0f);
				return true;
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(3516, 1);
			modRecipe.AddIngredient(null, "Nightshade", 5);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(3480, 1);
			modRecipe.AddIngredient(null, "Nightshade", 5);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
