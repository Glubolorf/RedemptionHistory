using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.NPCs.Bosses.KSIII;
using Redemption.NPCs.Bosses.KSIII.Clone;
using Redemption.NPCs.HM;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Minions
{
	public class SlayerMissilePro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Drone Missile");
			Main.projFrames[base.projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 14;
			base.projectile.height = 14;
			base.projectile.aiStyle = -1;
			base.projectile.minion = true;
			base.projectile.friendly = true;
			base.projectile.hostile = false;
			base.projectile.penetrate = 1;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 180;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 2)
				{
					base.projectile.frame = 0;
				}
			}
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			if (base.projectile.localAI[0] == 0f)
			{
				this.AdjustMagnitude(ref base.projectile.velocity);
				Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Alarm2").WithVolume(0.2f).WithPitchVariance(0.1f), -1, -1);
				base.projectile.localAI[0] = 1f;
			}
			Vector2 move = Vector2.Zero;
			float distance = 800f;
			bool target = false;
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && !Main.npc[i].friendly && Main.npc[i].lifeMax > 5 && !Main.npc[i].immortal && Main.npc[i].type != ModContent.NPCType<Android>() && Main.npc[i].type != ModContent.NPCType<Apidroid>() && Main.npc[i].type != ModContent.NPCType<PrototypeSilver>() && Main.npc[i].type != ModContent.NPCType<SpaceKeeper>() && Main.npc[i].type != ModContent.NPCType<SpacePaladin>() && Main.npc[i].type != ModContent.NPCType<KS3_Body>() && Main.npc[i].type != ModContent.NPCType<KS3_Body_Clone>())
				{
					Vector2 newMove = Main.npc[i].Center - base.projectile.Center;
					float distanceTo = (float)Math.Sqrt((double)(newMove.X * newMove.X + newMove.Y * newMove.Y));
					if (distanceTo < distance)
					{
						move = newMove;
						distance = distanceTo;
						target = true;
					}
				}
			}
			if (target)
			{
				this.AdjustMagnitude(ref move);
				base.projectile.velocity = (10f * base.projectile.velocity + move) / 11f;
				this.AdjustMagnitude(ref base.projectile.velocity);
			}
		}

		private void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt((double)(vector.X * vector.X + vector.Y * vector.Y));
			if (magnitude > 20f)
			{
				vector *= 19f / magnitude;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.projectileTexture[base.projectile.type];
			Texture2D glow = base.mod.GetTexture("Projectiles/Minions/SlayerMissilePro_Glow");
			int spriteDirection = base.projectile.spriteDirection;
			Vector2 drawCenter2 = new Vector2(base.projectile.Center.X, base.projectile.Center.Y);
			int num215 = texture.Height / 2;
			int y7 = num215 * base.projectile.frame;
			Main.spriteBatch.Draw(texture, drawCenter2 - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, texture.Width, num215)), drawColor, base.projectile.rotation, new Vector2((float)texture.Width / 2f, (float)num215 / 2f), base.projectile.scale, (base.projectile.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			Main.spriteBatch.Draw(glow, drawCenter2 - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, texture.Width, num215)), base.projectile.GetAlpha(Color.White), base.projectile.rotation, new Vector2((float)texture.Width / 2f, (float)num215 / 2f), base.projectile.scale, (base.projectile.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			return false;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item14, base.projectile.position);
			for (int i = 0; i < 25; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 6, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			for (int j = 0; j < 15; j++)
			{
				int dustIndex2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 31, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex2].velocity *= 1.4f;
			}
			Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), new Vector2(0f, 0f), ModContent.ProjectileType<MissileBlast>(), base.projectile.damage, base.projectile.knockBack, Main.myPlayer, 0f, 0f);
		}
	}
}
