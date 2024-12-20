using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.KSIII
{
	public class ScannerDrone : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Scanner Drone Mk.I");
			Main.npcFrameCount[base.npc.type] = 8;
		}

		public override void SetDefaults()
		{
			base.npc.width = 30;
			base.npc.height = 14;
			base.npc.friendly = false;
			base.npc.damage = 0;
			base.npc.defense = 20;
			base.npc.lifeMax = 260;
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
			base.npc.LookAtPlayer();
			base.npc.frameCounter += 1.0;
			int num = this.frameType;
			if (num != 0)
			{
				if (num == 1)
				{
					if (base.npc.frameCounter >= 5.0)
					{
						base.npc.frameCounter = 0.0;
						NPC npc = base.npc;
						npc.frame.Y = npc.frame.Y + 40;
						if (base.npc.frame.Y > 280)
						{
							base.npc.frameCounter = 0.0;
							base.npc.frame.Y = 240;
						}
					}
				}
			}
			else if (base.npc.frameCounter >= 5.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc2 = base.npc;
				npc2.frame.Y = npc2.frame.Y + 40;
				if (base.npc.frame.Y > 120)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
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
			float[] ai = base.npc.ai;
			int num2 = 1;
			float num3 = ai[num2] + 1f;
			ai[num2] = num3;
			if (num3 % 50f == 0f)
			{
				this.DefaultPos = new Vector2((float)((player.Center.X > base.npc.Center.X) ? Main.rand.Next(-280, -180) : Main.rand.Next(180, 280)), (float)Main.rand.Next(-60, -40));
			}
			num3 = base.npc.ai[0];
			if (!0f.Equals(num3))
			{
				if (!1f.Equals(num3))
				{
					if (!2f.Equals(num3))
					{
						return;
					}
					float[] ai2 = base.npc.ai;
					int num4 = 2;
					float num5 = ai2[num4] + 1f;
					ai2[num4] = num5;
					if (num5 > 10f)
					{
						NPC npc3 = base.npc;
						npc3.velocity.Y = npc3.velocity.Y - 0.3f;
						if (base.npc.Distance(player.Center) > 1500f)
						{
							base.npc.active = false;
						}
					}
				}
				else
				{
					base.npc.velocity *= 0.96f;
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 30f)
					{
						base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<ScanPro>(), 0, Vector2.Zero, true, SoundID.Item1, "Sounds/Custom/BallFire", (float)base.npc.whoAmI, 0f);
					}
					if (base.npc.ai[2] > 240f)
					{
						base.npc.ai[2] = 0f;
						this.frameType = 1;
						base.npc.ai[0] = 2f;
						if (RedeWorld.slayerDeath == 0 && RedeWorld.redemptionPoints > 0)
						{
							CombatText.NewText(base.npc.getRect(), Colors.RarityCyan, "TARGET UNIMPORTANT...", true, true);
							return;
						}
					}
				}
				return;
			}
			base.npc.ai[2] += 1f;
			if (base.npc.Distance(this.DefaultPos) < 40f || base.npc.ai[2] > 120f)
			{
				base.npc.ai[0] = 1f;
				base.npc.ai[2] = 0f;
				return;
			}
			base.npc.Move(this.DefaultPos, 11f, 15f, true);
		}

		public override bool CheckDead()
		{
			Main.npc[(int)base.npc.ai[3]].ai[1] += 1f;
			return true;
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D glowMask = base.mod.GetTexture("NPCs/Bosses/KSIII/ScannerDrone_Glow");
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			spriteBatch.Draw(glowMask, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			return false;
		}

		private Vector2 DefaultPos;

		public int frameType;
	}
}
