using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Redemption.Items.DruidDamageClass
{
	public class WallsClaw : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Wall's Claw");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nShoots Night Spirits\nThe Night Spirit is stronger in the Corruption/Crimson");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 39;
			base.item.width = 54;
			base.item.height = 78;
			base.item.useTime = 32;
			base.item.useAnimation = 32;
			base.item.useStyle = 1;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 1, 50, 0);
			base.item.rare = 3;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = false;
			base.item.useTurn = true;
			base.item.shoot = base.mod.ProjectileType("NightSpirit");
			base.item.shootSpeed = 15f;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).burnStaves)
			{
				target.AddBuff(24, 180, false);
			}
		}

		public override bool CanUseItem(Player player)
		{
			if (player.ZoneCorrupt || player.ZoneCrimson)
			{
				base.item.damage = 43;
				base.item.shootSpeed = 19f;
			}
			else
			{
				base.item.damage = 39;
				base.item.shootSpeed = 15f;
			}
			return true;
		}

		public override float UseTimeMultiplier(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).fasterStaves)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).rapidStave)
				{
					return 1.45f;
				}
				return 1.15f;
			}
			else
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).rapidStave)
				{
					return 1.35f;
				}
				return 1f;
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
			int num6 = 4;
			for (int i = 0; i < num6; i++)
			{
				vector..ctor(player.position.X + (float)player.width * 0.5f + (float)Main.rand.Next(201) * -(float)player.direction + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
				vector.X = (vector.X + player.Center.X) / 2f + (float)Main.rand.Next(-300, 301);
				vector.Y += (float)(200 * i);
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
				int num9 = Projectile.NewProjectile(vector.X, vector.Y, num7, num8, base.mod.ProjectileType("NightSpirit"), num, num2, myPlayer, 0f, (float)Main.rand.Next(10));
				Main.projectile[num9].tileCollide = true;
				Main.projectile[num9].ranged = false;
				Main.projectile[num9].magic = false;
				Main.projectile[num9].timeLeft = 200;
			}
			return false;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 5, 0f, 0f, 0, default(Color), 1f);
			}
		}
	}
}
