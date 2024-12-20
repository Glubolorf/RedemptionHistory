using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.EaglecrestGolem
{
	public class EaglecrestRockPilePZ : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Eaglecrest Rock Pile");
			Main.npcFrameCount[base.npc.type] = 8;
		}

		public override void SetDefaults()
		{
			base.npc.lifeMax = 12000;
			base.npc.damage = 95;
			base.npc.defense = 30;
			base.npc.knockBackResist = 0f;
			base.npc.value = (float)Item.buyPrice(0, 0, 0, 0);
			base.npc.aiStyle = -1;
			base.npc.width = 42;
			base.npc.height = 54;
			base.npc.HitSound = SoundID.NPCHit41;
			base.npc.DeathSound = SoundID.NPCDeath43;
			base.npc.lavaImmune = true;
		}

		public override void AI()
		{
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
				base.npc.frameCounter += (double)(base.npc.velocity.X * 0.5f);
				if (base.npc.frameCounter >= 3.0 || base.npc.frameCounter <= -3.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc = base.npc;
					npc.frame.Y = npc.frame.Y + 58;
					if (base.npc.frame.Y > 406)
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
			BaseAI.AIZombie(base.npc, ref base.npc.ai, false, false, -1, 0.1f, 10f, 40, 40, 60, true, 10, 60, false, null, false);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D hopAni = base.mod.GetTexture("NPCs/Bosses/EaglecrestGolem/EaglecrestRockPilePZHop");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.hop)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.hop)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = hopAni.Height / 1;
				int y6 = 0;
				Main.spriteBatch.Draw(hopAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, hopAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)hopAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 25; i++)
				{
					int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 1, 0f, 0f, 100, default(Color), 1f);
					Main.dust[dustIndex2].velocity *= 4.6f;
				}
			}
			int dustIndex3 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 1, 0f, 0f, 100, default(Color), 1f);
			Main.dust[dustIndex3].velocity *= 4.6f;
		}

		private bool hop;
	}
}
