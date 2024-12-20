using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class TeslaCoilStaffPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Tesla Coil Staff");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 28;
			base.projectile.height = 84;
			base.projectile.friendly = true;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = false;
			base.projectile.magic = true;
			base.projectile.ignoreWater = true;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			float num = 1.5707964f;
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			float num2 = 30f;
			if (base.projectile.ai[0] > 90f)
			{
				num2 = 15f;
			}
			if (base.projectile.ai[0] > 120f)
			{
				num2 = 5f;
			}
			base.projectile.damage = (int)((float)player.inventory[player.selectedItem].damage * player.magicDamage);
			base.projectile.ai[0] += 1f;
			base.projectile.ai[1] += 1f;
			bool flag = false;
			if (base.projectile.ai[0] % num2 == 0f)
			{
				flag = true;
			}
			int soundDelay = 10;
			bool flag2 = false;
			if (base.projectile.ai[0] % num2 == 0f)
			{
				flag2 = true;
			}
			if (base.projectile.ai[1] >= 1f)
			{
				base.projectile.ai[1] = 0f;
				flag2 = true;
				if (Main.myPlayer == base.projectile.owner)
				{
					float num3 = player.inventory[player.selectedItem].shootSpeed * base.projectile.scale;
					Vector2 vector2 = vector;
					Vector2 vector3 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - vector2;
					if (player.gravDir == -1f)
					{
						vector3.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - vector2.Y;
					}
					Vector2 vector4 = Vector2.Normalize(vector3);
					if (float.IsNaN(vector4.X) || float.IsNaN(vector4.Y))
					{
						vector4 = -Vector2.UnitY;
					}
					vector4 = Vector2.Normalize(Vector2.Lerp(vector4, Vector2.Normalize(base.projectile.velocity), 0.92f));
					vector4 *= num3;
					if (vector4.X != base.projectile.velocity.X || vector4.Y != base.projectile.velocity.Y)
					{
						base.projectile.netUpdate = true;
					}
					base.projectile.velocity = vector4;
				}
			}
			if (base.projectile.soundDelay <= 0)
			{
				base.projectile.soundDelay = soundDelay;
				base.projectile.soundDelay *= 2;
				if (base.projectile.ai[0] != 1f)
				{
					Main.PlaySound(SoundID.Item15, base.projectile.position);
				}
			}
			if (flag2 && Main.myPlayer == base.projectile.owner)
			{
				bool flag3 = !flag || player.CheckMana(player.inventory[player.selectedItem].mana, true, false);
				bool flag4 = player.channel && flag3 && !player.noItems && !player.CCed;
				if (flag4)
				{
					if (base.projectile.ai[0] == 1f)
					{
						Vector2 center = base.projectile.Center;
						Vector2 vector5 = Vector2.Normalize(base.projectile.velocity);
						if (float.IsNaN(vector5.X) || float.IsNaN(vector5.Y))
						{
							vector5 = -Vector2.UnitY;
						}
						int damage = base.projectile.damage;
						for (int i = 0; i < 7; i++)
						{
							Projectile.NewProjectile(center.X, center.Y, vector5.X, vector5.Y, base.mod.ProjectileType("TeslaLaser"), damage, base.projectile.knockBack, base.projectile.owner, (float)i, (float)base.projectile.whoAmI);
						}
						base.projectile.netUpdate = true;
					}
				}
				else
				{
					base.projectile.Kill();
				}
			}
			base.projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - base.projectile.Size / 2f;
			base.projectile.rotation = Utils.ToRotation(base.projectile.velocity) + num;
			base.projectile.spriteDirection = base.projectile.direction;
			base.projectile.timeLeft = 2;
			player.ChangeDir(base.projectile.direction);
			player.heldProj = base.projectile.whoAmI;
			player.itemTime = 2;
			player.itemAnimation = 2;
			player.itemRotation = (float)Math.Atan2((double)(base.projectile.velocity.Y * (float)base.projectile.direction), (double)(base.projectile.velocity.X * (float)base.projectile.direction));
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 mountedCenter = Main.player[base.projectile.owner].MountedCenter;
			Color color = Lighting.GetColor((int)((double)base.projectile.position.X + (double)base.projectile.width * 0.5) / 16, (int)(((double)base.projectile.position.Y + (double)base.projectile.height * 0.5) / 16.0));
			if (base.projectile.hide && !ProjectileID.Sets.DontAttachHideToAlpha[base.projectile.type])
			{
				color = Lighting.GetColor((int)mountedCenter.X / 16, (int)(mountedCenter.Y / 16f));
			}
			SpriteEffects spriteEffects = 0;
			if (base.projectile.spriteDirection == -1)
			{
				spriteEffects = 1;
			}
			Texture2D texture2D = Main.projectileTexture[base.projectile.type];
			int num = Main.projectileTexture[base.projectile.type].Height / Main.projFrames[base.projectile.type];
			int num2 = num * base.projectile.frame;
			Vector2 vector = Utils.Floor(base.projectile.position + new Vector2((float)base.projectile.width, (float)base.projectile.height) / 2f + Vector2.UnitY * base.projectile.gfxOffY - Main.screenPosition);
			if (Main.player[base.projectile.owner].shroomiteStealth && Main.player[base.projectile.owner].inventory[Main.player[base.projectile.owner].selectedItem].ranged)
			{
				float num3 = Main.player[base.projectile.owner].stealth;
				if ((double)num3 < 0.03)
				{
					num3 = 0.03f;
				}
				float num4 = (1f + num3 * 10f) / 11f;
				color *= num3;
			}
			if (Main.player[base.projectile.owner].setVortex && Main.player[base.projectile.owner].inventory[Main.player[base.projectile.owner].selectedItem].ranged)
			{
				float num5 = Main.player[base.projectile.owner].stealth;
				if ((double)num5 < 0.03)
				{
					num5 = 0.03f;
				}
				float num6 = (1f + num5 * 10f) / 11f;
				color = Utils.MultiplyRGBA(color, new Color(Vector4.Lerp(Vector4.One, new Vector4(0.16f, 0.12f, 0f, 0f), 1f - num5)));
			}
			Main.spriteBatch.Draw(texture2D, vector, new Rectangle?(new Rectangle(0, num2, texture2D.Width, num)), base.projectile.GetAlpha(color), base.projectile.rotation, new Vector2((float)texture2D.Width / 2f, (float)num / 2f), base.projectile.scale, spriteEffects, 0f);
			float num7 = (float)Math.Cos((double)(6.2831855f * (base.projectile.ai[0] / 30f))) * 2f + 2f;
			if (base.projectile.ai[0] > 120f)
			{
				num7 = 4f;
			}
			for (float num8 = 0f; num8 < 4f; num8 += 1f)
			{
				Main.spriteBatch.Draw(texture2D, vector + Utils.RotatedBy(Vector2.UnitY, (double)(num8 * 6.2831855f / 4f), default(Vector2)) * num7, new Rectangle?(new Rectangle(0, num2, texture2D.Width, num)), Utils.MultiplyRGBA(base.projectile.GetAlpha(color), new Color(255, 255, 255, 0)) * 0.03f, base.projectile.rotation, new Vector2((float)texture2D.Width / 2f, (float)num / 2f), base.projectile.scale, spriteEffects, 0f);
			}
			return false;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(Color.White);
		}
	}
}
