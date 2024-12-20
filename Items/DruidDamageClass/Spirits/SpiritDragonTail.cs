using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.Spirits
{
	public class SpiritDragonTail : ModProjectile
	{
		public override void SetDefaults()
		{
			base.projectile.width = 28;
			base.projectile.height = 28;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.netImportant = true;
			base.projectile.tileCollide = false;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 2400;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromStave = false;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(Color.White);
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Spirit Dragon");
		}

		public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
		{
			drawCacheProjsBehindProjectiles.Add(index);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture2D13 = Main.projectileTexture[base.projectile.type];
			int num214 = Main.projectileTexture[base.projectile.type].Height / Main.projFrames[base.projectile.type];
			int y6 = num214 * base.projectile.frame;
			Main.spriteBatch.Draw(texture2D13, base.projectile.Center - Main.screenPosition + new Vector2(0f, base.projectile.gfxOffY), new Rectangle?(new Rectangle(0, y6, texture2D13.Width, num214)), base.projectile.GetAlpha(Color.White), base.projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)num214 / 2f), base.projectile.scale, (base.projectile.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			return false;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if ((int)Main.time % 120 == 0)
			{
				base.projectile.netUpdate = true;
			}
			if (!player.active)
			{
				base.projectile.active = false;
				return;
			}
			if (player.dead)
			{
				modPlayer.spiritWyvern2 = false;
			}
			int num1038 = 30;
			if (!player.HasBuff(base.mod.BuffType("SpiritDragonBuff")))
			{
				base.projectile.Kill();
			}
			bool flag67 = false;
			Vector2 value67 = Vector2.Zero;
			Vector2 zero = Vector2.Zero;
			float num1039 = 0f;
			float scaleFactor16 = 0f;
			float scaleFactor17 = 1f;
			if (base.projectile.ai[1] == 1f)
			{
				base.projectile.ai[1] = 0f;
				base.projectile.netUpdate = true;
			}
			int byUUID = Projectile.GetByUUID(base.projectile.owner, (int)base.projectile.ai[0]);
			if (byUUID >= 0 && Main.projectile[byUUID].active)
			{
				flag67 = true;
				value67 = Main.projectile[byUUID].Center;
				Vector2 velocity = Main.projectile[byUUID].velocity;
				num1039 = Main.projectile[byUUID].rotation;
				scaleFactor17 = MathHelper.Clamp(Main.projectile[byUUID].scale, 0f, 50f);
				scaleFactor16 = 16f;
				int alpha = Main.projectile[byUUID].alpha;
				Main.projectile[byUUID].localAI[0] = base.projectile.localAI[0] + 1f;
				if (Main.projectile[byUUID].type != base.mod.ProjectileType("SpiritDragonHead"))
				{
					Main.projectile[byUUID].localAI[1] = (float)base.projectile.whoAmI;
				}
				if (base.projectile.owner == player.whoAmI && Main.projectile[byUUID].type == base.mod.ProjectileType("SpiritDragonHead"))
				{
					Main.projectile[byUUID].Kill();
					base.projectile.Kill();
					return;
				}
			}
			if (!flag67)
			{
				return;
			}
			if (base.projectile.alpha > 0)
			{
				for (int num1040 = 0; num1040 < 2; num1040++)
				{
					int num1041 = Dust.NewDust(base.projectile.position, base.projectile.width, base.projectile.height, 68, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num1041].noGravity = true;
					Main.dust[num1041].noLight = true;
				}
			}
			base.projectile.alpha -= 42;
			if (base.projectile.alpha < 0)
			{
				base.projectile.alpha = 0;
			}
			base.projectile.velocity = Vector2.Zero;
			Vector2 vector134 = value67 - base.projectile.Center;
			if (num1039 != base.projectile.rotation)
			{
				float num1042 = MathHelper.WrapAngle(num1039 - base.projectile.rotation);
				vector134 = Utils.RotatedBy(vector134, (double)(num1042 * 0.1f), default(Vector2));
			}
			base.projectile.rotation = Utils.ToRotation(vector134) + 1.5707964f;
			base.projectile.position = base.projectile.Center;
			base.projectile.scale = scaleFactor17;
			base.projectile.width = (base.projectile.height = (int)((float)num1038 * base.projectile.scale));
			base.projectile.Center = base.projectile.position;
			if (vector134 != Vector2.Zero)
			{
				base.projectile.Center = value67 - Vector2.Normalize(vector134) * scaleFactor16 * scaleFactor17;
			}
			base.projectile.spriteDirection = ((vector134.X > 0f) ? 1 : -1);
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.NPCDeath6.WithVolume(0.4f), base.projectile.position);
			Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 68, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 1f);
			Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 68, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 1f);
			Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 68, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 1f);
			Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 68, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 1f);
			Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 68, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 1f);
			Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 68, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 1f);
			Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 68, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 1f);
			Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 68, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 1f);
		}
	}
}
