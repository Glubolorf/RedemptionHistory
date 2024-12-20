using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class LastRedemption : ModItem
	{
		public override void SetStaticDefaults()
		{
			if (Main.netMode != 2)
			{
				Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					glowMasks[i] = Main.glowMaskTexture[i];
				}
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Weapons/" + base.GetType().Name + "_Glow");
				LastRedemption.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.item.glowMask = LastRedemption.customGlowMask;
			base.DisplayName.SetDefault("Last Redemption");
			base.Tooltip.SetDefault("[c/ffea9b:'The darkened Night will lose its Sight,]\n[c/ffea9b:Leaving the Light command the Fright,]\n[c/ffea9b:And the Might to the Holy Knight.']\nRight-clicking will cast down Holy Comets\nOnly the worthy may wield this\n[c/ffc300:Legendary]");
		}

		public override void SetDefaults()
		{
			base.item.damage = 1000;
			base.item.melee = true;
			base.item.width = 82;
			base.item.height = 82;
			base.item.useTime = 18;
			base.item.useAnimation = 18;
			base.item.useStyle = 1;
			base.item.knockBack = 8.5f;
			base.item.value = Item.sellPrice(20, 0, 0, 0);
			base.item.rare = 8;
			base.item.UseSound = SoundID.Item71;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = base.mod.ProjectileType("LastRPro1");
			base.item.shootSpeed = 10f;
			base.item.glowMask = LastRedemption.customGlowMask;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 7;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				base.item.UseSound = SoundID.Item71;
				base.item.shoot = base.mod.ProjectileType("LastRPro3");
				base.item.shootSpeed = 18f;
			}
			else
			{
				base.item.UseSound = SoundID.Item122;
				base.item.shoot = base.mod.ProjectileType("LastRPro1");
				base.item.shootSpeed = 10f;
			}
			return NPC.downedMoonlord && RedeWorld.downedVlitch1 && RedeWorld.downedVlitch2 && RedeWorld.downedVlitch3;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.altFunctionUse == 2)
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
				int num78 = 4;
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
					int projectile = Projectile.NewProjectile(vector2.X, vector2.Y, speedX2, speedY2, base.mod.ProjectileType("LastRPro3"), num73, num74, i, 0f, (float)Main.rand.Next(10));
					Main.projectile[projectile].tileCollide = false;
					Main.projectile[projectile].ranged = false;
					Main.projectile[projectile].magic = true;
					Main.projectile[projectile].timeLeft = 200;
				}
				return false;
			}
			return true;
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(base.mod.BuffType("RedemptiveEmbraceBuff"), 4, true);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("LastRPro4")] == 0)
			{
				Projectile.NewProjectile(player.position, Vector2.Zero, base.mod.ProjectileType("LastRPro4"), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}

		public static short customGlowMask;
	}
}
