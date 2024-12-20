using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class Dandelion : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Giant Dandelion");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nCasts down Giant Dandelion Seeds from the sky");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 40;
			base.item.width = 22;
			base.item.height = 22;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.mana = 10;
			base.item.useStyle = 4;
			base.item.knockBack = 3f;
			base.item.crit = 4;
			base.item.value = Item.sellPrice(0, 1, 50, 0);
			base.item.rare = 4;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.noMelee = true;
			base.item.shoot = base.mod.ProjectileType("GiantDandelionSeed");
			base.item.shootSpeed = 10f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int myPlayer = Main.myPlayer;
			float shootSpeed = base.item.shootSpeed;
			int num = damage;
			float num2 = knockBack;
			num2 = player.GetWeaponKnockback(base.item, num2);
			player.itemTime = base.item.useTime;
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			Utils.RotatedBy(Vector2.UnitX, (double)player.fullRotation, default(Vector2));
			Main.MouseWorld - vector;
			float num3 = (float)Main.mouseX + Main.screenPosition.X - vector.X;
			float num4 = (float)Main.mouseY + Main.screenPosition.Y - vector.Y;
			if (player.gravDir == -1f)
			{
				num4 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector.Y;
			}
			float num5 = (float)Math.Sqrt((double)(num3 * num3 + num4 * num4));
			if ((float.IsNaN(num3) && float.IsNaN(num4)) || (num3 == 0f && num4 == 0f))
			{
				num3 = (float)player.direction;
				num4 = 0f;
				num5 = shootSpeed;
			}
			else
			{
				num5 = shootSpeed / num5;
			}
			num3 *= num5;
			num4 *= num5;
			int num6 = 3;
			for (int i = 0; i < num6; i++)
			{
				vector..ctor(player.position.X + (float)player.width * 0.5f + (float)Main.rand.Next(201) * -(float)player.direction + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
				vector.X = (vector.X + player.Center.X) / 2f + (float)Main.rand.Next(-200, 201);
				vector.Y -= (float)(100 * i);
				num3 = (float)Main.mouseX + Main.screenPosition.X - vector.X;
				num4 = (float)Main.mouseY + Main.screenPosition.Y - vector.Y;
				if (num4 < 0f)
				{
					num4 *= -1f;
				}
				if (num4 < 20f)
				{
					num4 = 20f;
				}
				num5 = (float)Math.Sqrt((double)(num3 * num3 + num4 * num4));
				num5 = shootSpeed / num5;
				num3 *= num5;
				num4 *= num5;
				float num7 = num3 + (float)Main.rand.Next(-50, 51) * 0.02f;
				float num8 = num4 + (float)Main.rand.Next(-50, 51) * 0.02f;
				int num9 = Projectile.NewProjectile(vector.X, vector.Y, num7, num8, base.mod.ProjectileType("GiantDandelionSeed"), num, num2, myPlayer, 0f, (float)Main.rand.Next(10));
				Main.projectile[num9].tileCollide = false;
				Main.projectile[num9].ranged = false;
				Main.projectile[num9].magic = true;
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "ForestCore", 5);
			modRecipe.AddRecipeGroup("Redemption:Plant", 5);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
