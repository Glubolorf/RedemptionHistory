using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.v08
{
	public class BloodBoiledSkeleton : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Blood-Boiled Skeleton");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 90;
			base.npc.height = 82;
			base.npc.friendly = false;
			base.npc.damage = 140;
			base.npc.defense = 30;
			base.npc.lifeMax = 10000;
			base.npc.HitSound = SoundID.NPCHit8;
			base.npc.DeathSound = SoundID.NPCDeath10;
			base.npc.value = 1000f;
			base.npc.knockBackResist = 0.03f;
			base.npc.aiStyle = 3;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (Main.netMode != 1 && base.npc.life <= 0)
			{
				NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("LostSoul3"), 0, 0f, 0f, 0f, 0f, 255);
				for (int i = 0; i < 35; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 5, 0f, 0f, 100, default(Color), 4f);
					Main.dust[dustIndex].velocity *= 4f;
				}
				for (int j = 0; j < 10; j++)
				{
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-3 + Main.rand.Next(-11, 0)), base.mod.ProjectileType("Blood"), 40, 3f, 255, 0f, 0f);
				}
				for (int k = 0; k < 4; k++)
				{
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-3 + Main.rand.Next(-11, 0)), base.mod.ProjectileType("BBBone1"), 40, 3f, 255, 0f, 0f);
				}
				for (int l = 0; l < 4; l++)
				{
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-3 + Main.rand.Next(-11, 0)), base.mod.ProjectileType("BBBone2"), 40, 3f, 255, 0f, 0f);
				}
				Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-3 + Main.rand.Next(-11, 0)), base.mod.ProjectileType("BBBone3"), 40, 3f, 255, 0f, 0f);
			}
			if (Main.netMode != 1)
			{
				if (Main.rand.Next(5) == 0)
				{
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-3 + Main.rand.Next(-11, 0)), base.mod.ProjectileType("BBBone1"), 40, 3f, 255, 0f, 0f);
				}
				if (Main.rand.Next(5) == 0)
				{
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-3 + Main.rand.Next(-11, 0)), base.mod.ProjectileType("BBBone2"), 40, 3f, 255, 0f, 0f);
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void AI()
		{
			base.npc.TargetClosest(true);
			if (Main.player[base.npc.target].Center.X > base.npc.Center.X)
			{
				base.npc.spriteDirection = 1;
			}
			else
			{
				base.npc.spriteDirection = -1;
			}
			if (base.npc.velocity.Y == 0f)
			{
				this.hop = false;
				base.npc.frameCounter += 1.0;
				if (base.npc.frameCounter >= 10.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc = base.npc;
					npc.frame.Y = npc.frame.Y + 86;
					if (base.npc.frame.Y > 258)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 0;
					}
				}
			}
			else
			{
				this.hop = true;
			}
			if (Main.rand.Next(300) == 0 && NPC.CountNPCS(base.mod.NPCType("GrandLarva")) <= 1)
			{
				int minion = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("GrandLarva"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[minion].netUpdate = true;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D hopAni = base.mod.GetTexture("NPCs/v08/BloodBoiledSkeletonHop");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.hop)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.hop)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = hopAni.Height / 1;
				int y6 = num214 * this.hopFrame;
				Main.spriteBatch.Draw(hopAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, hopAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)hopAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if ((double)spawnInfo.spawnTileY >= Main.rockLayer || !Main.bloodMoon || NPC.AnyNPCs(base.mod.NPCType("BloodBoiledSkeleton")) || !NPC.downedMoonlord)
			{
				return 0f;
			}
			return 0.02f;
		}

		private bool hop;

		private int hopFrame;

		private int hopCounter;
	}
}
