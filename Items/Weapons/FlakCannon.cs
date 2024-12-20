using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class FlakCannon : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Flak Cannon");
			base.Tooltip.SetDefault("'Quite the unreal bang bang and boom boom'\nReplaces normal bullets with bouncy Meteor Bullets\nRight-clicking fires a flak grenade");
		}

		public override void SetDefaults()
		{
			base.item.damage = 24;
			base.item.ranged = true;
			base.item.width = 74;
			base.item.height = 46;
			base.item.crit = -4;
			base.item.useTime = 33;
			base.item.useAnimation = 33;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 6f;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.rare = 6;
			base.item.UseSound = SoundID.Item36;
			base.item.autoReuse = true;
			base.item.shoot = 10;
			base.item.shootSpeed = 90f;
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
				base.item.damage = 45;
				base.item.useTime = 70;
				base.item.useAnimation = 70;
				base.item.shootSpeed = 10f;
				base.item.UseSound = SoundID.Item61;
				base.item.shoot = 10;
				base.item.useAmmo = AmmoID.Bullet;
			}
			else
			{
				base.item.damage = 26;
				base.item.useTime = 20;
				base.item.UseSound = SoundID.Item36;
				base.item.useAnimation = 20;
				base.item.shoot = 10;
				base.item.shootSpeed = 40f;
				base.item.useAmmo = AmmoID.Bullet;
			}
			return base.CanUseItem(player);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == 14)
			{
				type = 36;
			}
			if (player.altFunctionUse == 2)
			{
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<FlakPro>(), 200, 9f, player.whoAmI, 0f, 0f);
				int numberProjectiles = 2;
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(20f));
					float scale = 1f - Utils.NextFloat(Main.rand) * 0.1f;
					perturbedSpeed *= scale;
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				}
			}
			else
			{
				int numberProjectiles2 = 4;
				for (int j = 0; j < numberProjectiles2; j++)
				{
					Vector2 perturbedSpeed2 = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(10f));
					float scale2 = 1f - Utils.NextFloat(Main.rand) * 0.1f;
					perturbedSpeed2 *= scale2;
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed2.X, perturbedSpeed2.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				}
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(534, 1);
			modRecipe.AddIngredient(219, 1);
			modRecipe.AddIngredient(175, 25);
			modRecipe.AddIngredient(1225, 10);
			modRecipe.AddIngredient(1347, 15);
			modRecipe.AddIngredient(324, 1);
			modRecipe.AddTile(77);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-8f, 0f));
		}
	}
}
