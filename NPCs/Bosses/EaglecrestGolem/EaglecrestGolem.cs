using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Materials.PreHM;
using Redemption.Items.Placeable.Tiles;
using Redemption.Items.Usable;
using Redemption.Items.Weapons.PreHM.Ranged;
using Redemption.Items.Weapons.PreHM.Summon;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.EaglecrestGolem
{
	[AutoloadBossHead]
	public class EaglecrestGolem : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Eaglecrest Golem");
			Main.npcFrameCount[base.npc.type] = 20;
		}

		public override void SetDefaults()
		{
			base.npc.lifeMax = 3200;
			base.npc.damage = 30;
			base.npc.defense = 18;
			base.npc.knockBackResist = 0f;
			base.npc.value = (float)Item.buyPrice(0, 2, 0, 0);
			base.npc.aiStyle = -1;
			base.npc.width = 80;
			base.npc.height = 80;
			base.npc.HitSound = SoundID.NPCHit41;
			base.npc.DeathSound = SoundID.NPCDeath43;
			base.npc.lavaImmune = true;
			base.npc.boss = true;
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			Player player3 = Main.player[base.npc.target];
			potionType = 188;
			if (!RedeWorld.downedEaglecrestGolem)
			{
				RedeWorld.redemptionPoints++;
				for (int i = 0; i < 255; i++)
				{
					Player player2 = Main.player[i];
					if (player2.active)
					{
						for (int j = 0; j < player2.inventory.Length; j++)
						{
							if (player2.inventory[j].type == ModContent.ItemType<RedemptionTeller>())
							{
								Main.NewText("<Chalice of Alignment> Living stones? Never seen that before.", Color.DarkGoldenrod, false);
							}
						}
						CombatText.NewText(player2.getRect(), Color.Gold, "+1", true, false);
					}
				}
			}
			RedeWorld.downedEaglecrestGolem = true;
			if (Main.netMode != 0)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override void NPCLoot()
		{
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<GolemEye>(), 1, false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<AncientPebble>(), 1, false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<AncientSlingShot>(), 1, false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<AncientStone2>(), Main.rand.Next(14, 34), false, 0, false, false);
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
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
			if (base.npc.velocity.Y == 0f && !this.roll)
			{
				this.hop = false;
				base.npc.frameCounter += (double)(base.npc.velocity.X * 0.5f);
				if (base.npc.frameCounter >= 3.0 || base.npc.frameCounter <= -3.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc = base.npc;
					npc.frame.Y = npc.frame.Y + 84;
					if (base.npc.frame.Y > 924)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 0;
					}
				}
				if (this.slash)
				{
					this.slashCounter++;
					if (this.slashCounter > 5)
					{
						this.slashFrame++;
						this.slashCounter = 0;
					}
					if (this.slashFrame >= 9)
					{
						this.slashFrame = 0;
					}
				}
				if (base.npc.Distance(Main.player[base.npc.target].Center) <= 300f && Main.rand.Next(150) == 0 && !this.slash && !this.roll)
				{
					this.slash = true;
					base.npc.netUpdate = true;
				}
				if (this.slash)
				{
					base.npc.ai[1] += 1f;
					base.npc.velocity.X = 0f;
					if (base.npc.ai[1] == 1f && !RedeConfigClient.Instance.NoCombatText)
					{
						CombatText.NewText(base.npc.getRect(), Colors.RarityTrash, "Slash!", true, true);
					}
					if (base.npc.ai[1] == 35f)
					{
						Main.PlaySound(SoundID.Item71, (int)base.npc.position.X, (int)base.npc.position.Y);
						float Speed = 7f;
						Vector2 vector8 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
						int damage = 11;
						int type = ModContent.ProjectileType<GolemSlashPro1>();
						float rotation = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
						int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
						Main.projectile[num54].netUpdate = true;
						base.npc.netUpdate = true;
					}
					if (base.npc.ai[1] >= 45f)
					{
						this.slash = false;
						base.npc.ai[1] = 0f;
						this.slashCounter = 0;
						this.slashFrame = 0;
						base.npc.netUpdate = true;
					}
				}
			}
			else
			{
				this.hop = true;
				base.npc.netUpdate = true;
			}
			if (this.roll)
			{
				this.hop = false;
				base.npc.frameCounter += (double)(base.npc.velocity.X * 0.5f);
				if (base.npc.frame.Y < 1008)
				{
					base.npc.frame.Y = 1008;
				}
				if (base.npc.frameCounter >= 3.0 || base.npc.frameCounter <= -3.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc2 = base.npc;
					npc2.frame.Y = npc2.frame.Y + 84;
					if (base.npc.frame.Y > 1596)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 1008;
					}
				}
			}
			if (!this.slash)
			{
				base.npc.ai[2] += 1f;
			}
			if (base.npc.ai[2] >= 500f && !this.roll && !this.slash)
			{
				this.roll = true;
				base.npc.netUpdate = true;
			}
			if (this.roll)
			{
				BaseAI.AIZombie(base.npc, ref base.npc.ai, false, false, -1, 0.07f, 10f, 26, 8, 60, true, 10, 60, false, null, false);
				base.npc.defense = 30;
				if (base.npc.ai[2] >= 900f)
				{
					this.roll = false;
					base.npc.ai[2] = 0f;
					base.npc.netUpdate = true;
				}
			}
			else
			{
				base.npc.defense = 18;
				base.npc.aiStyle = -1;
				this.aiType = 0;
				base.npc.netUpdate = true;
				if (base.npc.life < (int)((float)base.npc.lifeMax * 0.5f) && base.npc.life >= (int)((float)base.npc.lifeMax * 0.1f))
				{
					BaseAI.AIZombie(base.npc, ref base.npc.ai, false, false, -1, 0.06f, 4f, 20, 5, 60, true, 10, 60, false, null, false);
				}
				else if (base.npc.life < (int)((float)base.npc.lifeMax * 0.1f))
				{
					BaseAI.AIZombie(base.npc, ref base.npc.ai, false, false, -1, 0.07f, 6f, 26, 6, 60, true, 10, 60, false, null, false);
				}
				else if (base.npc.life >= (int)((float)base.npc.lifeMax * 0.5f))
				{
					BaseAI.AIZombie(base.npc, ref base.npc.ai, false, false, -1, 0.04f, 2f, 16, 4, 60, true, 10, 60, false, null, false);
				}
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.9f) && !this.summon1)
			{
				base.npc.SpawnNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), ModContent.NPCType<EaglecrestRockPile>(), 0f, 0f, 0f, 0f);
				base.npc.SpawnNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), ModContent.NPCType<EaglecrestRockPile>(), 0f, 0f, 0f, 0f);
				this.summon1 = true;
				base.npc.netUpdate = true;
				return;
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.5f) && !this.summon2)
			{
				base.npc.SpawnNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), ModContent.NPCType<EaglecrestRockPile>(), 0f, 0f, 0f, 0f);
				base.npc.SpawnNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), ModContent.NPCType<EaglecrestRockPile>(), 0f, 0f, 0f, 0f);
				base.npc.SpawnNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), ModContent.NPCType<EaglecrestRockPile>(), 0f, 0f, 0f, 0f);
				this.summon2 = true;
				base.npc.netUpdate = true;
				return;
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.25f) && !this.summon3)
			{
				base.npc.SpawnNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), ModContent.NPCType<EaglecrestRockPile>(), 0f, 0f, 0f, 0f);
				base.npc.SpawnNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), ModContent.NPCType<EaglecrestRockPile>(), 0f, 0f, 0f, 0f);
				base.npc.SpawnNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), ModContent.NPCType<EaglecrestRockPile>(), 0f, 0f, 0f, 0f);
				this.summon3 = true;
				base.npc.netUpdate = true;
				return;
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.1f) && !this.summon4)
			{
				base.npc.SpawnNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), ModContent.NPCType<EaglecrestRockPile>(), 0f, 0f, 0f, 0f);
				base.npc.SpawnNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), ModContent.NPCType<EaglecrestRockPile>(), 0f, 0f, 0f, 0f);
				this.summon4 = true;
				base.npc.netUpdate = true;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D hopAni = base.mod.GetTexture("NPCs/Bosses/EaglecrestGolem/EaglecrestGolemHop");
			Texture2D slashAni = base.mod.GetTexture("NPCs/Bosses/EaglecrestGolem/EaglecrestGolemSlash");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.hop && !this.slash)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.hop && !this.roll && !this.slash)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = hopAni.Height / 1;
				int y6 = 0;
				Main.spriteBatch.Draw(hopAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, hopAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)hopAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.slash && !this.roll)
			{
				Vector2 drawCenter2 = new Vector2(base.npc.Center.X, base.npc.Center.Y - 13f);
				int num215 = slashAni.Height / 9;
				int y7 = num215 * this.slashFrame;
				Main.spriteBatch.Draw(slashAni, drawCenter2 - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, slashAni.Width, num215)), drawColor, base.npc.rotation, new Vector2((float)slashAni.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 35; i++)
				{
					int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 1, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex2].velocity *= 4.6f;
				}
			}
			int dustIndex3 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 1, 0f, 0f, 100, default(Color), 1f);
			Main.dust[dustIndex3].velocity *= 4.6f;
		}

		private bool roll;

		private bool hop;

		private bool slash;

		private int slashFrame;

		private int slashCounter;

		private bool summon1;

		private bool summon2;

		private bool summon3;

		private bool summon4;
	}
}
