using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class PlutoniumNukeRadio : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("P-2-Warhead Receiver");
			base.Tooltip.SetDefault("'TACTICAL NUKE INCOMING!'\nCalls a Plutonium Nuke from the sky\nDoesn't destroy tiles");
		}

		public override void SetDefaults()
		{
			base.item.damage = 1000;
			base.item.magic = true;
			base.item.mana = 100;
			base.item.width = 18;
			base.item.height = 40;
			base.item.useTime = 120;
			base.item.useAnimation = 120;
			base.item.useStyle = 4;
			base.item.knockBack = 11f;
			base.item.rare = 9;
			base.item.value = Item.sellPrice(2, 50, 0, 0);
			base.item.UseSound = base.mod.GetLegacySoundSlot(2, "Sounds/Item/AlarmItem");
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = ModContent.ProjectileType<PlutoniumNukePro>();
			base.item.shootSpeed = 15f;
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
			int num78 = 1;
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
				Projectile.NewProjectile(vector2.X, vector2.Y, speedX2, speedY2, ModContent.ProjectileType<PlutoniumNukePro>(), num73, num74, i, 0f, (float)Main.rand.Next(10));
			}
			return false;
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Texture2D texture = base.mod.GetTexture("Items/Weapons/v08/" + base.GetType().Name + "_Glow");
			spriteBatch.Draw(texture, new Vector2(base.item.position.X - Main.screenPosition.X + (float)base.item.width * 0.5f, base.item.position.Y - Main.screenPosition.Y + (float)base.item.height - (float)texture.Height * 0.5f + 2f), new Rectangle?(new Rectangle(0, 0, texture.Width, texture.Height)), Color.White, rotation, Utils.Size(texture) * 0.5f, scale, SpriteEffects.None, 0f);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Plutonium", 18);
			modRecipe.AddRecipeGroup("Redemption:Plating", 6);
			modRecipe.AddRecipeGroup("Redemption:Capacitators", 1);
			modRecipe.AddIngredient(null, "CarbonMyofibre", 4);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
