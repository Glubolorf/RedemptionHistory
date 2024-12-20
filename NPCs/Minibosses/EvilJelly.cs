using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Quest;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Minibosses
{
	[AutoloadBossHead]
	public class EvilJelly : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dark Slime");
			Main.npcFrameCount[base.npc.type] = 2;
		}

		public override void SetDefaults()
		{
			base.npc.width = 142;
			base.npc.height = 100;
			base.npc.friendly = false;
			base.npc.damage = 100;
			base.npc.defense = 0;
			base.npc.lifeMax = 9500;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.value = 4000f;
			base.npc.alpha = 80;
			base.npc.buffImmune[20] = true;
			base.npc.buffImmune[70] = true;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
			base.npc.boss = true;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 40; i++)
				{
					Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				}
			}
			for (int j = 0; j < 4; j++)
			{
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
			}
			if (Main.netMode != 1 && base.npc.life <= 0)
			{
				if (RedeQuests.daerelQuests == 9 && !NPC.AnyNPCs(base.mod.NPCType("Zephos3")) && !NPC.AnyNPCs(base.mod.NPCType("Zephos2")) && !NPC.AnyNPCs(base.mod.NPCType("Zephos1")) && !NPC.AnyNPCs(base.mod.NPCType("ZepUnconscious")) && !NPC.AnyNPCs(base.mod.NPCType("Zep2Unconscious")) && !NPC.AnyNPCs(base.mod.NPCType("Zep3Unconscious")) && (NPC.AnyNPCs(base.mod.NPCType("Daerel3")) || NPC.AnyNPCs(base.mod.NPCType("Daerel2")) || NPC.AnyNPCs(base.mod.NPCType("Daerel1")) || NPC.AnyNPCs(base.mod.NPCType("DaerelUnconscious")) || NPC.AnyNPCs(base.mod.NPCType("Daerel2Unconscious")) || NPC.AnyNPCs(base.mod.NPCType("Daerel3Unconscious"))))
				{
					NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("Zephos3"), 0, 0f, 0f, 0f, 0f, 255);
					return;
				}
				if (RedeQuests.zephosQuests == 9 && !NPC.AnyNPCs(base.mod.NPCType("Daerel3")) && !NPC.AnyNPCs(base.mod.NPCType("Daerel2")) && !NPC.AnyNPCs(base.mod.NPCType("Daerel1")) && !NPC.AnyNPCs(base.mod.NPCType("DaerelUnconscious")) && !NPC.AnyNPCs(base.mod.NPCType("Daerel2Unconscious")) && !NPC.AnyNPCs(base.mod.NPCType("Daerel3Unconscious")) && (NPC.AnyNPCs(base.mod.NPCType("Zephos3")) || NPC.AnyNPCs(base.mod.NPCType("Zephos2")) || NPC.AnyNPCs(base.mod.NPCType("Zephos1")) || NPC.AnyNPCs(base.mod.NPCType("ZepUnconscious")) || NPC.AnyNPCs(base.mod.NPCType("Zep2Unconscious")) || NPC.AnyNPCs(base.mod.NPCType("Zep3Unconscious"))))
				{
					NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("Daerel3"), 0, 0f, 0f, 0f, 0f, 255);
				}
			}
		}

		public override void AI()
		{
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 30.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 106;
				if (base.npc.frame.Y > 106)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			this.frameCounters += 1f;
			if (this.frameCounters > 10f)
			{
				this.bubbleFrame++;
				this.frameCounters = 0f;
			}
			if (this.bubbleFrame >= 6)
			{
				this.bubbleFrame = 0;
			}
			this.Target();
			this.DespawnHandler();
			if (base.npc.ai[0] == 0f)
			{
				string text = "A Dark Slime slumbers...";
				Color rarityPurple = Colors.RarityPurple;
				byte r = rarityPurple.R;
				rarityPurple = Colors.RarityPurple;
				byte g = rarityPurple.G;
				rarityPurple = Colors.RarityPurple;
				Main.NewText(text, r, g, rarityPurple.B, false);
				base.npc.ai[0] = 1f;
			}
			if (base.npc.life < base.npc.lifeMax && base.npc.ai[0] == 1f)
			{
				string text2 = "The Dark Slime has awoken...";
				Color rarityPurple = Colors.RarityPurple;
				byte r2 = rarityPurple.R;
				rarityPurple = Colors.RarityPurple;
				byte g2 = rarityPurple.G;
				rarityPurple = Colors.RarityPurple;
				Main.NewText(text2, r2, g2, rarityPurple.B, false);
				base.npc.ai[0] = 2f;
			}
			if (base.npc.ai[0] == 2f)
			{
				base.npc.ai[0] = 3f;
				base.npc.ai[2] = 0f;
				base.npc.ai[1] = (float)Main.rand.Next(2);
				base.npc.netUpdate = true;
			}
			if (base.npc.ai[0] == 3f)
			{
				int num = (int)base.npc.ai[1];
				if (num != 0)
				{
					if (num != 1)
					{
						return;
					}
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] % 3f == 0f && base.npc.ai[2] <= 80f)
					{
						Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.position.Y + 12f), new Vector2(Utils.NextFloat(Main.rand, -3f, 3f), Utils.NextFloat(Main.rand, -14f, -6f)), base.mod.ProjectileType("DarkSludge"), 20, 3f, 255, 0f, 0f);
					}
					if (base.npc.ai[2] >= 150f)
					{
						base.npc.ai[0] = 2f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
				}
				else
				{
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] % 25f == 0f && NPC.CountNPCS(121) <= 9)
					{
						Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.position.Y + 12f), new Vector2(Utils.NextFloat(Main.rand, -3f, 3f), Utils.NextFloat(Main.rand, -14f, -6f)), base.mod.ProjectileType("SlimerBall"), 16, 3f, 255, 0f, 0f);
					}
					if (base.npc.ai[2] >= 100f)
					{
						base.npc.ai[0] = 2f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
						return;
					}
				}
			}
		}

		private void Target()
		{
			this.player = Main.player[base.npc.target];
		}

		private void DespawnHandler()
		{
			if (!this.player.active || this.player.dead)
			{
				base.npc.TargetClosest(false);
				this.player = Main.player[base.npc.target];
				if (!this.player.active || this.player.dead)
				{
					base.npc.velocity = new Vector2(0f, 100f);
					if (base.npc.timeLeft > 10)
					{
						base.npc.timeLeft = 10;
					}
					return;
				}
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			name = "A Dark Slime";
			potionType = 154;
			if (!RedeWorld.downedDarkSlime)
			{
				for (int i = 0; i < 255; i++)
				{
					Player player2 = Main.player[i];
					if (player2.active)
					{
						for (int j = 0; j < player2.inventory.Length; j++)
						{
							if (player2.inventory[j].type == base.mod.ItemType("RedemptionTeller"))
							{
								Main.NewText("<Chalice of Alignment> Someone emerged from the slime!", Color.DarkGoldenrod, false);
							}
						}
						CombatText.NewText(player2.getRect(), Color.Gray, "+0", true, false);
					}
				}
			}
			RedeWorld.downedDarkSlime = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override void NPCLoot()
		{
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("DarkShard"), Main.rand.Next(7, 12), false, 0, false, false);
			if (RedeQuests.daerelQuests == 9)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("DarkSteelBow"), 1, false, 0, false, false);
				return;
			}
			if (RedeQuests.zephosQuests == 9)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("MythrilsBane"), 1, false, 0, false, false);
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D bubblesAni = base.mod.GetTexture("NPCs/Minibosses/Bubbles");
			Texture2D teethAni = base.mod.GetTexture("NPCs/Minibosses/EvilJelly_Teeth");
			Texture2D daeAni = base.mod.GetTexture("NPCs/Minibosses/EvilJelly_Daerel");
			Texture2D bowAni = base.mod.GetTexture("NPCs/Minibosses/EvilJelly_Bow");
			Texture2D zepAni = base.mod.GetTexture("NPCs/Minibosses/EvilJelly_Zephos");
			Texture2D swordAni = base.mod.GetTexture("NPCs/Minibosses/EvilJelly_Sword");
			int spriteDirection = base.npc.spriteDirection;
			Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
			int num214 = bubblesAni.Height / 6;
			int y6 = num214 * this.bubbleFrame;
			if (!NPC.AnyNPCs(base.mod.NPCType("Zephos3")) && !NPC.AnyNPCs(base.mod.NPCType("Zephos2")) && !NPC.AnyNPCs(base.mod.NPCType("Zephos1")) && !NPC.AnyNPCs(base.mod.NPCType("ZepUnconscious")) && !NPC.AnyNPCs(base.mod.NPCType("Zep2Unconscious")) && !NPC.AnyNPCs(base.mod.NPCType("Zep3Unconscious")) && (NPC.AnyNPCs(base.mod.NPCType("Daerel3")) || NPC.AnyNPCs(base.mod.NPCType("Daerel2")) || NPC.AnyNPCs(base.mod.NPCType("Daerel1")) || NPC.AnyNPCs(base.mod.NPCType("DaerelUnconscious")) || NPC.AnyNPCs(base.mod.NPCType("Daerel2Unconscious")) || NPC.AnyNPCs(base.mod.NPCType("Daerel3Unconscious"))))
			{
				if (RedeQuests.daerelQuests == 9)
				{
					spriteBatch.Draw(zepAni, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
					spriteBatch.Draw(bowAni, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				}
			}
			else if (!NPC.AnyNPCs(base.mod.NPCType("Daerel3")) && !NPC.AnyNPCs(base.mod.NPCType("Daerel2")) && !NPC.AnyNPCs(base.mod.NPCType("Daerel1")) && !NPC.AnyNPCs(base.mod.NPCType("DaerelUnconscious")) && !NPC.AnyNPCs(base.mod.NPCType("Daerel2Unconscious")) && !NPC.AnyNPCs(base.mod.NPCType("Daerel3Unconscious")) && (NPC.AnyNPCs(base.mod.NPCType("Zephos3")) || NPC.AnyNPCs(base.mod.NPCType("Zephos2")) || NPC.AnyNPCs(base.mod.NPCType("Zephos1")) || NPC.AnyNPCs(base.mod.NPCType("ZepUnconscious")) || NPC.AnyNPCs(base.mod.NPCType("Zep2Unconscious")) || NPC.AnyNPCs(base.mod.NPCType("Zep3Unconscious"))) && RedeQuests.zephosQuests == 9)
			{
				spriteBatch.Draw(daeAni, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				spriteBatch.Draw(swordAni, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			spriteBatch.Draw(teethAni, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			Main.spriteBatch.Draw(bubblesAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, bubblesAni.Width, num214)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)bubblesAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			return false;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			for (int i = 0; i < Main.npc.Length; i++)
			{
				if (Main.npc[i].boss)
				{
					return 0f;
				}
			}
			if (RedeWorld.darkSlimeLure)
			{
				if (WorldGen.crimson)
				{
					return SpawnCondition.Crimson.Chance * ((!RedeQuests.DslimeQuest && (RedeQuests.zephosQuests == 9 || RedeQuests.daerelQuests == 9) && Main.hardMode && !RedeWorld.downedDarkSlime && !NPC.AnyNPCs(base.mod.NPCType("DarkSlime"))) ? 3f : 0f);
				}
				return SpawnCondition.Corruption.Chance * ((!RedeQuests.DslimeQuest && (RedeQuests.zephosQuests == 9 || RedeQuests.daerelQuests == 9) && Main.hardMode && !RedeWorld.downedDarkSlime && !NPC.AnyNPCs(base.mod.NPCType("DarkSlime"))) ? 3f : 0f);
			}
			else
			{
				if (WorldGen.crimson)
				{
					return SpawnCondition.Crimson.Chance * ((!RedeQuests.DslimeQuest && (RedeQuests.zephosQuests == 9 || RedeQuests.daerelQuests == 9) && Main.hardMode && !RedeWorld.downedDarkSlime && !NPC.AnyNPCs(base.mod.NPCType("DarkSlime"))) ? 0.5f : 0f);
				}
				return SpawnCondition.Corruption.Chance * ((!RedeQuests.DslimeQuest && (RedeQuests.zephosQuests == 9 || RedeQuests.daerelQuests == 9) && Main.hardMode && !RedeWorld.downedDarkSlime && !NPC.AnyNPCs(base.mod.NPCType("DarkSlime"))) ? 0.5f : 0f);
			}
		}

		private Player player;

		public int bubbleFrame;

		public float frameCounters;
	}
}
