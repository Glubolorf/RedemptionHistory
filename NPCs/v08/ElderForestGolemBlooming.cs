using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.v08
{
	public class ElderForestGolemBlooming : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Elder Forest Golem");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.lifeMax = 15000;
			base.npc.damage = 70;
			base.npc.defense = 60;
			base.npc.knockBackResist = 0f;
			base.npc.value = (float)Item.buyPrice(0, 1, 0, 0);
			base.npc.aiStyle = -1;
			base.npc.width = 86;
			base.npc.height = 102;
			base.npc.HitSound = base.mod.GetLegacySoundSlot(3, "Sounds/NPCHit/WoodHit");
			base.npc.DeathSound = SoundID.NPCDeath3;
			base.npc.lavaImmune = true;
		}

		public override void NPCLoot()
		{
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("LivingTwig"), Main.rand.Next(42, 52), false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("CursedThornsF"), Main.rand.Next(3, 7), false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 27, Main.rand.Next(7, 12), false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 313, 1, false, 0, false, false);
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
			if (base.npc.velocity.Y == 0f)
			{
				this.hop = false;
				base.npc.frameCounter += (double)(base.npc.velocity.X * 0.5f);
				if (base.npc.frameCounter >= 3.0 || base.npc.frameCounter <= -3.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc = base.npc;
					npc.frame.Y = npc.frame.Y + 108;
					if (base.npc.frame.Y > 324)
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
			BaseAI.AIZombie(base.npc, ref base.npc.ai, false, false, -1, 0.02f, 1.2f, 2, 2, 60, true, 10, 60, false, null, false);
			if (Main.raining)
			{
				this.regenTimer++;
				if (this.regenTimer >= 30 && base.npc.life < base.npc.lifeMax - 5)
				{
					base.npc.life += 5;
					base.npc.HealEffect(5, true);
					this.regenTimer = 0;
				}
			}
			if (base.npc.wet && !base.npc.lavaWet)
			{
				this.regenTimer++;
				if (this.regenTimer >= 20 && base.npc.life < base.npc.lifeMax - 5)
				{
					base.npc.life += 5;
					base.npc.HealEffect(5, true);
					this.regenTimer = 0;
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/v08/ElderForestGolemBloomingHop");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.hop)
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.hop)
			{
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num = texture.Height / 1;
				int num2 = num * this.hopFrame;
				Main.spriteBatch.Draw(texture, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture.Width, num)), drawColor, base.npc.rotation, new Vector2((float)texture.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			return false;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 40; i++)
				{
					int num = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 78, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num].velocity *= 4.6f;
				}
			}
			int num2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 78, 0f, 0f, 100, default(Color), 1f);
			Main.dust[num2].velocity *= 4.6f;
		}

		private bool hop;

		private int hopFrame;

		private int regenTimer;
	}
}
