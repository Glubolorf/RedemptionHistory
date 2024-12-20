using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.DruidProjectiles;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class Dandelion : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Giant Dandelion");
			base.Tooltip.SetDefault("Casts down Giant Dandelion Seeds from the sky");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 45;
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
			base.item.shoot = ModContent.ProjectileType<GiantDandelionSeed>();
			base.item.shootSpeed = 10f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int i = Main.myPlayer;
			float num72 = base.item.shootSpeed;
			int num73 = damage;
			float num74 = knockBack;
			num74 = player.GetWeaponKnockback(base.item, num74);
			player.itemTime = base.item.useTime;
			Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
			Utils.RotatedBy(Vector2.UnitX, (double)player.fullRotation, default(Vector2));
			Main.MouseWorld - vector2;
			float num75 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
			float num76 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
			if (player.gravDir == -1f)
			{
				num76 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector2.Y;
			}
			float num77 = (float)Math.Sqrt((double)(num75 * num75 + num76 * num76));
			if ((float.IsNaN(num75) && float.IsNaN(num76)) || (num75 == 0f && num76 == 0f))
			{
				num75 = (float)player.direction;
				num76 = 0f;
				num77 = num72;
			}
			else
			{
				num77 = num72 / num77;
			}
			num75 *= num77;
			num76 *= num77;
			int num78 = 3;
			for (int num79 = 0; num79 < num78; num79++)
			{
				vector2 = new Vector2(player.position.X + (float)player.width * 0.5f + (float)Main.rand.Next(201) * -(float)player.direction + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
				vector2.X = (vector2.X + player.Center.X) / 2f + (float)Main.rand.Next(-200, 201);
				vector2.Y -= (float)(100 * num79);
				num75 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
				num76 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
				if (num76 < 0f)
				{
					num76 *= -1f;
				}
				if (num76 < 20f)
				{
					num76 = 20f;
				}
				num77 = (float)Math.Sqrt((double)(num75 * num75 + num76 * num76));
				num77 = num72 / num77;
				num75 *= num77;
				num76 *= num77;
				float speedX2 = num75 + (float)Main.rand.Next(-50, 51) * 0.02f;
				float speedY2 = num76 + (float)Main.rand.Next(-50, 51) * 0.02f;
				int projectile = Projectile.NewProjectile(vector2.X, vector2.Y, speedX2, speedY2, ModContent.ProjectileType<GiantDandelionSeed>(), num73, num74, i, 0f, (float)Main.rand.Next(10));
				Main.projectile[projectile].tileCollide = false;
				Main.projectile[projectile].ranged = false;
				Main.projectile[projectile].magic = true;
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
