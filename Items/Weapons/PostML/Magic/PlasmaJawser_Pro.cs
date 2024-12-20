using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Projectiles.Magic;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Magic
{
	public class PlasmaJawser_Pro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Plasma Jawser");
			Main.projFrames[base.projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 62;
			base.projectile.height = 60;
			base.projectile.friendly = false;
			base.projectile.hostile = false;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = false;
			base.projectile.magic = true;
			base.projectile.ownerHitCheck = true;
			base.projectile.ignoreWater = true;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			float num = MathHelper.ToRadians(0f);
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			if (base.projectile.spriteDirection == -1)
			{
				num = MathHelper.ToRadians(180f);
			}
			if (Main.myPlayer == base.projectile.owner)
			{
				float scaleFactor6 = 1f;
				if (player.inventory[player.selectedItem].shoot == base.projectile.type)
				{
					scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * base.projectile.scale;
				}
				Vector2 vector2 = Main.MouseWorld - vector;
				vector2.Normalize();
				if (Utils.HasNaNs(vector2))
				{
					vector2 = Vector2.UnitX * (float)player.direction;
				}
				vector2 *= scaleFactor6;
				if (vector2.X != base.projectile.velocity.X || vector2.Y != base.projectile.velocity.Y)
				{
					base.projectile.netUpdate = true;
				}
				base.projectile.velocity = vector2;
				if (player.noItems || player.CCed || player.dead || !player.active)
				{
					base.projectile.Kill();
				}
				base.projectile.netUpdate = true;
			}
			base.projectile.Center + base.projectile.velocity * 3f;
			base.projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - base.projectile.Size / 2f;
			base.projectile.rotation = Utils.ToRotation(base.projectile.velocity) + num;
			base.projectile.spriteDirection = base.projectile.direction;
			base.projectile.timeLeft = 2;
			player.ChangeDir(base.projectile.direction);
			player.heldProj = base.projectile.whoAmI;
			player.itemTime = 2;
			player.itemAnimation = 2;
			player.itemRotation = (float)Math.Atan2((double)(base.projectile.velocity.Y * (float)base.projectile.direction), (double)(base.projectile.velocity.X * (float)base.projectile.direction));
			Vector2 ShootPos = base.projectile.Center + RedeHelper.PolarVector(12f, base.projectile.rotation);
			float obj = base.projectile.localAI[0];
			if (!0f.Equals(obj))
			{
				if (1f.Equals(obj))
				{
					Projectile projectile = base.projectile;
					int num2 = projectile.frameCounter + 1;
					projectile.frameCounter = num2;
					if (num2 >= 5)
					{
						base.projectile.frameCounter = 0;
						Projectile projectile2 = base.projectile;
						num2 = projectile2.frame + 1;
						projectile2.frame = num2;
						if (num2 >= 3)
						{
							base.projectile.frame = 1;
						}
					}
				}
			}
			else
			{
				base.projectile.localAI[1] += 1f;
				if (base.projectile.localAI[1] > 30f)
				{
					base.projectile.frame = 1;
					base.projectile.localAI[1] = 0f;
					base.projectile.localAI[0] = 1f;
					if (Main.myPlayer == base.projectile.owner)
					{
						Projectile.NewProjectile(ShootPos, RedeHelper.PolarVector(10f, base.projectile.rotation), ModContent.ProjectileType<PlasmaLaser>(), base.projectile.damage, base.projectile.knockBack, Main.myPlayer, (float)base.projectile.whoAmI, 0f);
					}
				}
			}
			if (!player.channel)
			{
				base.projectile.Kill();
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.projectileTexture[base.projectile.type];
			Texture2D glow = base.mod.GetTexture("Items/Weapons/PostML/Magic/PlasmaJawser_Pro_Glow");
			SpriteEffects effects = (base.projectile.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			int num215 = texture.Height / 3;
			int y7 = num215 * base.projectile.frame;
			Vector2 position = base.projectile.Center - Main.screenPosition;
			Rectangle rect = new Rectangle(0, y7, texture.Width, num215);
			Vector2 origin = new Vector2((float)texture.Width / 2f, (float)num215 / 2f);
			spriteBatch.Draw(texture, position, new Rectangle?(rect), drawColor, base.projectile.rotation, origin, base.projectile.scale, effects, 0f);
			spriteBatch.Draw(glow, position, new Rectangle?(rect), base.projectile.GetAlpha(Color.White), base.projectile.rotation, origin, base.projectile.scale, effects, 0f);
			return false;
		}
	}
}
