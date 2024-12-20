using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class BanefulMoonlight : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Baneful Moonlight");
			base.Tooltip.SetDefault("'Moonlight will carry you...'\nRains down Cursed Moons from the skies\nGains more power while its a blood moon");
		}

		public override void SetDefaults()
		{
			base.item.damage = 333;
			base.item.melee = true;
			base.item.width = 88;
			base.item.height = 88;
			base.item.useTime = 22;
			base.item.useAnimation = 22;
			base.item.useStyle = 1;
			base.item.knockBack = 6f;
			base.item.value = Item.sellPrice(2, 50, 0, 0);
			base.item.UseSound = SoundID.Item71;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = base.mod.ProjectileType("CursedMoonPro1");
			base.item.shootSpeed = 15f;
		}

		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.overrideColor = new Color?(new Color(0, 255, 200));
				}
			}
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
			int num6;
			if (Main.bloodMoon)
			{
				num6 = 5;
			}
			else
			{
				num6 = 3;
			}
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
				int num9 = Projectile.NewProjectile(vector.X, vector.Y, num7, num8, base.mod.ProjectileType("CursedMoonPro1"), num, num2, myPlayer, 0f, (float)Main.rand.Next(10));
				Main.projectile[num9].tileCollide = false;
				Main.projectile[num9].melee = true;
				Main.projectile[num9].timeLeft = 200;
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(base.mod.ItemType("Firebreak"), 1);
			modRecipe.AddIngredient(base.mod.ItemType("ForgottenGreatsword"), 1);
			modRecipe.AddIngredient(base.mod.ItemType("ForgottenSword"), 1);
			modRecipe.AddIngredient(null, "ShinkiteCursed", 20);
			modRecipe.AddIngredient(3467, 20);
			modRecipe.AddIngredient(base.mod.ItemType("DarkShard"), 5);
			modRecipe.AddIngredient(base.mod.ItemType("LargeLostSoul"), 2);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
