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
			bool flag9 = false;
			if (base.projectile.ai[0] % num2 == 0f)
			{
				flag9 = true;
			}
			int num3 = 10;
			bool flag10 = false;
			if (base.projectile.ai[0] % num2 == 0f)
			{
				flag10 = true;
			}
			if (base.projectile.ai[1] >= 1f)
			{
				base.projectile.ai[1] = 0f;
				flag10 = true;
				if (Main.myPlayer == base.projectile.owner)
				{
					float scaleFactor5 = player.inventory[player.selectedItem].shootSpeed * base.projectile.scale;
					Vector2 value12 = vector;
					Vector2 value13 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - value12;
					if (player.gravDir == -1f)
					{
						value13.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - value12.Y;
					}
					Vector2 vector2 = Vector2.Normalize(value13);
					if (float.IsNaN(vector2.X) || float.IsNaN(vector2.Y))
					{
						vector2 = -Vector2.UnitY;
					}
					vector2 = Vector2.Normalize(Vector2.Lerp(vector2, Vector2.Normalize(base.projectile.velocity), 0.92f));
					vector2 *= scaleFactor5;
					if (vector2.X != base.projectile.velocity.X || vector2.Y != base.projectile.velocity.Y)
					{
						base.projectile.netUpdate = true;
					}
					base.projectile.velocity = vector2;
				}
			}
			if (base.projectile.soundDelay <= 0)
			{
				base.projectile.soundDelay = num3;
				base.projectile.soundDelay *= 2;
				if (base.projectile.ai[0] != 1f)
				{
					Main.PlaySound(SoundID.Item15, base.projectile.position);
				}
			}
			if (flag10 && Main.myPlayer == base.projectile.owner)
			{
				bool flag11 = !flag9 || player.CheckMana(player.inventory[player.selectedItem].mana, true, false);
				if (player.channel && flag11 && !player.noItems && !player.CCed)
				{
					if (base.projectile.ai[0] == 1f)
					{
						Vector2 center3 = base.projectile.Center;
						Vector2 vector3 = Vector2.Normalize(base.projectile.velocity);
						if (float.IsNaN(vector3.X) || float.IsNaN(vector3.Y))
						{
							vector3 = -Vector2.UnitY;
						}
						int num4 = base.projectile.damage;
						for (int i = 0; i < 7; i++)
						{
							Projectile.NewProjectile(center3.X, center3.Y, vector3.X, vector3.Y, base.mod.ProjectileType("TeslaLaser"), num4, base.projectile.knockBack, base.projectile.owner, (float)i, (float)base.projectile.whoAmI);
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
			Color color25 = Lighting.GetColor((int)((double)base.projectile.position.X + (double)base.projectile.width * 0.5) / 16, (int)(((double)base.projectile.position.Y + (double)base.projectile.height * 0.5) / 16.0));
			if (base.projectile.hide && !ProjectileID.Sets.DontAttachHideToAlpha[base.projectile.type])
			{
				color25 = Lighting.GetColor((int)mountedCenter.X / 16, (int)(mountedCenter.Y / 16f));
			}
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (base.projectile.spriteDirection == -1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
			Texture2D texture2D14 = Main.projectileTexture[base.projectile.type];
			int num215 = Main.projectileTexture[base.projectile.type].Height / Main.projFrames[base.projectile.type];
			int y7 = num215 * base.projectile.frame;
			Vector2 vector27 = Utils.Floor(base.projectile.position + new Vector2((float)base.projectile.width, (float)base.projectile.height) / 2f + Vector2.UnitY * base.projectile.gfxOffY - Main.screenPosition);
			if (Main.player[base.projectile.owner].shroomiteStealth && Main.player[base.projectile.owner].inventory[Main.player[base.projectile.owner].selectedItem].ranged)
			{
				float num216 = Main.player[base.projectile.owner].stealth;
				if ((double)num216 < 0.03)
				{
					num216 = 0.03f;
				}
				float num219 = (1f + num216 * 10f) / 11f;
				color25 *= num216;
			}
			if (Main.player[base.projectile.owner].setVortex && Main.player[base.projectile.owner].inventory[Main.player[base.projectile.owner].selectedItem].ranged)
			{
				float num217 = Main.player[base.projectile.owner].stealth;
				if ((double)num217 < 0.03)
				{
					num217 = 0.03f;
				}
				float num220 = (1f + num217 * 10f) / 11f;
				color25 = Utils.MultiplyRGBA(color25, new Color(Vector4.Lerp(Vector4.One, new Vector4(0.16f, 0.12f, 0f, 0f), 1f - num217)));
			}
			Main.spriteBatch.Draw(texture2D14, vector27, new Rectangle?(new Rectangle(0, y7, texture2D14.Width, num215)), base.projectile.GetAlpha(color25), base.projectile.rotation, new Vector2((float)texture2D14.Width / 2f, (float)num215 / 2f), base.projectile.scale, spriteEffects, 0f);
			float scaleFactor2 = (float)Math.Cos((double)(6.2831855f * (base.projectile.ai[0] / 30f))) * 2f + 2f;
			if (base.projectile.ai[0] > 120f)
			{
				scaleFactor2 = 4f;
			}
			for (float num218 = 0f; num218 < 4f; num218 += 1f)
			{
				Main.spriteBatch.Draw(texture2D14, vector27 + Utils.RotatedBy(Vector2.UnitY, (double)(num218 * 6.2831855f / 4f), default(Vector2)) * scaleFactor2, new Rectangle?(new Rectangle(0, y7, texture2D14.Width, num215)), Utils.MultiplyRGBA(base.projectile.GetAlpha(color25), new Color(255, 255, 255, 0)) * 0.03f, base.projectile.rotation, new Vector2((float)texture2D14.Width / 2f, (float)num215 / 2f), base.projectile.scale, spriteEffects, 0f);
			}
			return false;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(Color.White);
		}
	}
}
