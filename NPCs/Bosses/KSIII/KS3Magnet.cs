using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.KSIII
{
	public class KS3Magnet : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Energy Magnet Drone Mk.I");
			Main.npcFrameCount[base.npc.type] = 12;
		}

		public override void SetDefaults()
		{
			base.npc.width = 14;
			base.npc.height = 46;
			base.npc.friendly = false;
			base.npc.damage = 0;
			base.npc.defense = 12;
			base.npc.lifeMax = 750;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath56;
			base.npc.noTileCollide = true;
			base.npc.noGravity = true;
			base.npc.value = 0f;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 5; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[dustIndex].velocity *= 1.8f;
				}
				for (int j = 0; j < 5; j++)
				{
					int dustIndex2 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[dustIndex2].velocity *= 1.8f;
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void AI()
		{
			Player player = Main.player[base.npc.target];
			if (player.Center.X > base.npc.Center.X)
			{
				base.npc.spriteDirection = 1;
			}
			else
			{
				base.npc.spriteDirection = -1;
			}
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 5.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 78;
				if (base.npc.frame.Y > 858)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 624;
				}
			}
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			float soundVolume = base.npc.velocity.Length() / 50f;
			if (soundVolume > 2f)
			{
				soundVolume = 2f;
			}
			if (base.npc.soundDelay == 0)
			{
				Main.PlaySound(SoundID.Item24.WithVolume(soundVolume), base.npc.position);
				base.npc.soundDelay = 10;
			}
			NPC host = Main.npc[(int)base.npc.ai[0]];
			if (host.life <= 0 || !host.active || host.type != ModContent.NPCType<KS3_Body>())
			{
				base.npc.active = false;
				base.npc.life = 0;
			}
			float[] ai = base.npc.ai;
			int num = 1;
			float num2 = ai[num] + 1f;
			ai[num] = num2;
			if (num2 % 80f == 0f)
			{
				double angle = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
				this.vector.X = (float)(Math.Sin(angle) * 200.0);
				this.vector.Y = (float)(Math.Cos(angle) * 200.0);
				base.npc.ai[1] = 0f;
			}
			base.npc.ai[2] += 1f;
			if (base.npc.ai[2] == 1f)
			{
				for (int i = 0; i < 16; i++)
				{
					int dustID = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, 92, 0f, 0f, 100, Color.White, 2f);
					Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(4f, 0f), (float)i / 16f * 6.28f);
					Main.dust[dustID].noLight = false;
					Main.dust[dustID].noGravity = true;
				}
			}
			if (base.npc.ai[2] < 180f)
			{
				for (int j = 0; j < 3; j++)
				{
					double angle2 = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
					this.vector2.X = (float)(Math.Sin(angle2) * 200.0);
					this.vector2.Y = (float)(Math.Cos(angle2) * 200.0);
					Dust dust3 = Main.dust[Dust.NewDust(base.npc.Center + this.vector2, 2, 2, 92, 0f, 0f, 100, default(Color), 1f)];
					dust3.noGravity = true;
					dust3.velocity *= 0f;
				}
				for (int p = 0; p < 1000; p++)
				{
					Projectile proj = Main.projectile[p];
					if (proj.active && proj.width < 40 && proj.height < 40 && base.npc.Distance(proj.Center) < 200f && proj.friendly && proj.damage > 0 && proj.minionSlots == 0f)
					{
						base.npc.Shoot(proj.Center, ModContent.ProjectileType<KSMagnetPulse>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
						base.npc.ai[3] += (float)proj.damage;
						proj.Kill();
					}
				}
			}
			if (base.npc.ai[2] >= 180f && base.npc.ai[2] < 240f)
			{
				for (int k = 0; k < 4; k++)
				{
					double angle3 = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
					this.vector2.X = (float)(Math.Sin(angle3) * 90.0);
					this.vector2.Y = (float)(Math.Cos(angle3) * 90.0);
					Dust dust2 = Main.dust[Dust.NewDust(base.npc.Center + this.vector2, 2, 2, 92, 0f, 0f, 100, default(Color), 2f)];
					dust2.noGravity = true;
					dust2.velocity = -base.npc.DirectionTo(dust2.position) * 8f;
				}
			}
			if (base.npc.ai[2] == 240f && base.npc.ai[3] > 10f)
			{
				if (base.npc.ai[3] < 500f)
				{
					base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSMagnetBeam1>(), (int)base.npc.ai[3] / 4, RedeHelper.PolarVector(10f, Utils.ToRotation(player.Center - base.npc.Center)), false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
				}
				else if (base.npc.ai[3] >= 500f && base.npc.ai[3] < 1000f)
				{
					base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSMagnetBeam2>(), (int)base.npc.ai[3] / 4, RedeHelper.PolarVector(10f, Utils.ToRotation(player.Center - base.npc.Center)), false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
				}
				else
				{
					base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KSMagnetBeam3>(), (int)base.npc.ai[3] / 4, RedeHelper.PolarVector(10f, Utils.ToRotation(player.Center - base.npc.Center)), false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
				}
			}
			if (base.npc.ai[2] >= 400f)
			{
				NPC npc2 = base.npc;
				npc2.velocity.Y = npc2.velocity.Y - 0.5f;
				if (Vector2.Distance(base.npc.Center, host.Center) > 1500f)
				{
					base.npc.life = 0;
					base.npc.active = false;
					return;
				}
			}
			else
			{
				if (base.npc.ai[2] <= 200f)
				{
					base.npc.MoveToNPC(host, new Vector2(this.vector.X, this.vector.Y), 11f, 15f);
					return;
				}
				base.npc.velocity *= 0.96f;
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D glowMask = base.mod.GetTexture("NPCs/Bosses/KSIII/KS3Magnet");
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			spriteBatch.Draw(glowMask, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			return false;
		}

		private Vector2 vector;

		private Vector2 vector2;
	}
}
