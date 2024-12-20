using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Materials.PostML;
using Redemption.Items.Materials.PreHM;
using Redemption.Items.Weapons.PreHM.Melee;
using Redemption.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Melee
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
			base.item.width = 76;
			base.item.height = 76;
			base.item.useTime = 22;
			base.item.useAnimation = 22;
			base.item.useStyle = 1;
			base.item.knockBack = 6f;
			base.item.value = Item.sellPrice(2, 50, 0, 0);
			base.item.UseSound = SoundID.Item71;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = ModContent.ProjectileType<CursedMoonPro1>();
			base.item.shootSpeed = 15f;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
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
			int num78;
			if (Main.bloodMoon)
			{
				num78 = 5;
			}
			else
			{
				num78 = 3;
			}
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
				int projectile = Projectile.NewProjectile(vector2.X, vector2.Y, speedX2, speedY2, ModContent.ProjectileType<CursedMoonPro1>(), num73, num74, i, 0f, (float)Main.rand.Next(10));
				Main.projectile[projectile].tileCollide = false;
				Main.projectile[projectile].melee = true;
				Main.projectile[projectile].timeLeft = 200;
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(ModContent.ItemType<Firebreak>(), 1);
			modRecipe.AddIngredient(ModContent.ItemType<ForgottenGreatsword>(), 1);
			modRecipe.AddIngredient(ModContent.ItemType<ForgottenSword>(), 1);
			modRecipe.AddIngredient(null, "ShinkiteCursed", 20);
			modRecipe.AddIngredient(3467, 20);
			modRecipe.AddIngredient(ModContent.ItemType<DarkShard>(), 5);
			modRecipe.AddIngredient(ModContent.ItemType<LargeLostSoul>(), 2);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
