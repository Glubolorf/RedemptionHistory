using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.DruidProjectiles.Stave;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class WallsClaw : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Wall's Claw");
			base.Tooltip.SetDefault("Shoots Night Spirits\nThe Night Spirit is stronger in the Corruption/Crimson");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 34;
			base.item.width = 54;
			base.item.height = 78;
			base.item.useTime = 32;
			base.item.useAnimation = 32;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 1, 50, 0);
			base.item.rare = 3;
			base.item.UseSound = SoundID.Item43;
			base.item.autoReuse = false;
			base.item.useTurn = true;
			base.item.shoot = ModContent.ProjectileType<NightSpirit>();
			base.item.shootSpeed = 15f;
			this.defaultShoot = ModContent.ProjectileType<NightSpirit>();
			this.singleShotStave = false;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 78.2f;
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

		protected override bool SpecialShootPattern(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
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
				vector2.X = (vector2.X + player.Center.X) / 2f + (float)Main.rand.Next(-300, 301);
				vector2.Y += (float)(200 * num79);
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
				int projectile = Projectile.NewProjectile(vector2.X, vector2.Y, speedX2, speedY2, ModContent.ProjectileType<NightSpirit>(), num73, num74, i, 0f, (float)Main.rand.Next(10));
				Main.projectile[projectile].tileCollide = true;
				Main.projectile[projectile].ranged = false;
				Main.projectile[projectile].magic = false;
				Main.projectile[projectile].timeLeft = 200;
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
