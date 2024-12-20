using System;
using Microsoft.Xna.Framework;
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
			base.item.damage = 59;
			base.item.ranged = true;
			base.item.width = 74;
			base.item.height = 46;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
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
				base.item.damage = 59;
				base.item.useTime = 70;
				base.item.useAnimation = 70;
				base.item.shootSpeed = 10f;
				base.item.UseSound = SoundID.Item61;
				base.item.shoot = 10;
				base.item.useAmmo = AmmoID.Bullet;
			}
			else
			{
				base.item.damage = 59;
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
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, base.mod.ProjectileType("FlakPro"), 300, 9f, player.whoAmI, 0f, 0f);
				int num = 5;
				for (int i = 0; i < num; i++)
				{
					Vector2 vector = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(20f));
					float num2 = 1f - Utils.NextFloat(Main.rand) * 0.1f;
					vector *= num2;
					Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				}
			}
			else
			{
				int num3 = 8;
				for (int j = 0; j < num3; j++)
				{
					Vector2 vector2 = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(10f));
					float num4 = 1f - Utils.NextFloat(Main.rand) * 0.1f;
					vector2 *= num4;
					Projectile.NewProjectile(position.X, position.Y, vector2.X, vector2.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
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
