using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses
{
	[AutoloadBossHead]
	public class TheKeeper : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Keeper");
			Main.npcFrameCount[base.npc.type] = 8;
		}

		public override void SetDefaults()
		{
			base.npc.aiStyle = -1;
			base.npc.lifeMax = 2250;
			base.npc.damage = 30;
			base.npc.defense = 0;
			base.npc.knockBackResist = 0f;
			base.npc.width = 150;
			base.npc.height = 182;
			base.npc.value = (float)Item.buyPrice(0, 1, 50, 0);
			base.npc.npcSlots = 1f;
			base.npc.boss = true;
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.HitSound = SoundID.NPCHit13;
			base.npc.DeathSound = SoundID.NPCDeath19;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/BossKeeper");
			this.bossBag = base.mod.ItemType("TheKeeperBag");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 100; i++)
				{
					int num = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, 0f, 0f, 100, default(Color), 2.5f);
					Main.dust[num].velocity *= 2.6f;
				}
			}
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.2f);
			base.npc.defense = base.npc.defense + numPlayers;
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 188;
			RedeWorld.downedTheKeeper = true;
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("TheKeeperTrophy"), 1, false, 0, false, false);
			}
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
				return;
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("OldGathicWaraxe"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(7) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("TheKeeperMask"), 1, false, 0, false, false);
			}
			int num = Main.rand.Next(5);
			if (num == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("KeepersBow"), 1, false, 0, false, false);
			}
			if (num == 1)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("KeepersStaff"), 1, false, 0, false, false);
			}
			if (num == 2)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("KeepersClaw"), 1, false, 0, false, false);
			}
			if (num == 3)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("KeepersKnife"), 1, false, 0, false, false);
			}
			if (num == 4)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("KeepersSummon"), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("DarkShard"), Main.rand.Next(2, 3), false, 0, false, false);
		}

		public override void AI()
		{
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 5.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 184;
				if (base.npc.frame.Y > 1288)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			if (this.shriekStart)
			{
				this.shriekCounter++;
				if (this.shriekCounter > 10)
				{
					this.shriekFrame++;
					this.shriekCounter = 0;
				}
				if (this.shriekFrame >= 3)
				{
					this.shriekFrame = 1;
				}
			}
			if (Main.dayTime)
			{
				NPC npc2 = base.npc;
				npc2.position.Y = npc2.position.Y - 300f;
			}
			this.Target();
			this.DespawnHandler();
			this.Move(new Vector2(240f, 0f));
			base.npc.ai[1] -= 1f;
			if (base.npc.ai[1] <= 0f)
			{
				this.Shoot();
			}
			if (Main.rand.Next(1) == 0)
			{
				Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 5, 0f, 0f, 0, default(Color), 1f);
			}
			if (base.npc.life > 1200 && Main.rand.Next(600) == 0)
			{
				NPC.NewNPC((int)base.npc.position.X + 70, (int)base.npc.position.Y + 70, base.mod.NPCType("DarkSoul2"), 0, 0f, 0f, 0f, 0f, 255);
			}
			if (!Main.expertMode && base.npc.life < 1200)
			{
				this.timer++;
				if (this.timer == 20)
				{
					this.shriekStart = true;
					string text = "*Shrieks of pain echo through the night*";
					Color rarityPurple = Colors.RarityPurple;
					byte r = rarityPurple.R;
					Color rarityPurple2 = Colors.RarityPurple;
					byte g = rarityPurple2.G;
					Color rarityPurple3 = Colors.RarityPurple;
					Main.NewText(text, r, g, rarityPurple3.B, false);
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Shriek").WithVolume(0.7f).WithPitchVariance(0.1f), -1, -1);
					}
				}
				if (this.timer == 40)
				{
					NPC.NewNPC((int)base.npc.position.X + 70, (int)base.npc.position.Y + 70, base.mod.NPCType("DarkSoul2"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (this.timer == 45)
				{
					NPC.NewNPC((int)base.npc.position.X + 60, (int)base.npc.position.Y + 80, base.mod.NPCType("DarkSoul2"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (this.timer == 50)
				{
					NPC.NewNPC((int)base.npc.position.X + 80, (int)base.npc.position.Y + 75, base.mod.NPCType("DarkSoul2"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (this.timer == 55)
				{
					NPC.NewNPC((int)base.npc.position.X + 50, (int)base.npc.position.Y + 60, base.mod.NPCType("DarkSoul2"), 0, 0f, 0f, 0f, 0f, 255);
					NPC.NewNPC((int)base.npc.position.X + 85, (int)base.npc.position.Y + 50, base.mod.NPCType("DarkSoul2"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (this.timer == 60)
				{
					NPC.NewNPC((int)base.npc.position.X + 65, (int)base.npc.position.Y + 65, base.mod.NPCType("DarkSoul2"), 0, 0f, 0f, 0f, 0f, 255);
					NPC.NewNPC((int)base.npc.position.X + 45, (int)base.npc.position.Y + 70, base.mod.NPCType("DarkSoul2"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (this.timer >= 220)
				{
					this.shriekStart = false;
				}
				if (Main.rand.Next(250) == 0)
				{
					NPC.NewNPC((int)base.npc.position.X + 70, (int)base.npc.position.Y + 160, base.mod.NPCType("SkeletonMinion"), 0, 0f, 0f, 0f, 0f, 255);
				}
			}
			if (Main.expertMode && base.npc.life < 1200)
			{
				this.timer++;
				if (this.timer == 20)
				{
					this.shriekStart = true;
					string text2 = "*Shrieks of pain echo through the night*";
					Color rarityPurple4 = Colors.RarityPurple;
					byte r2 = rarityPurple4.R;
					Color rarityPurple5 = Colors.RarityPurple;
					byte g2 = rarityPurple5.G;
					Color rarityPurple6 = Colors.RarityPurple;
					Main.NewText(text2, r2, g2, rarityPurple6.B, false);
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Shriek").WithVolume(0.7f).WithPitchVariance(0.1f), -1, -1);
					}
				}
				if (this.timer == 40)
				{
					NPC.NewNPC((int)base.npc.position.X + 70, (int)base.npc.position.Y + 70, base.mod.NPCType("DarkSoul2"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (this.timer == 45)
				{
					NPC.NewNPC((int)base.npc.position.X + 60, (int)base.npc.position.Y + 80, base.mod.NPCType("DarkSoul2"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (this.timer == 50)
				{
					NPC.NewNPC((int)base.npc.position.X + 80, (int)base.npc.position.Y + 75, base.mod.NPCType("DarkSoul2"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (this.timer == 55)
				{
					NPC.NewNPC((int)base.npc.position.X + 50, (int)base.npc.position.Y + 60, base.mod.NPCType("DarkSoul2"), 0, 0f, 0f, 0f, 0f, 255);
					NPC.NewNPC((int)base.npc.position.X + 85, (int)base.npc.position.Y + 50, base.mod.NPCType("DarkSoul2"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (this.timer == 60)
				{
					NPC.NewNPC((int)base.npc.position.X + 65, (int)base.npc.position.Y + 65, base.mod.NPCType("DarkSoul2"), 0, 0f, 0f, 0f, 0f, 255);
					NPC.NewNPC((int)base.npc.position.X + 45, (int)base.npc.position.Y + 70, base.mod.NPCType("DarkSoul2"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (this.timer >= 220)
				{
					this.shriekStart = false;
				}
				if (Main.rand.Next(400) == 0)
				{
					NPC.NewNPC((int)base.npc.position.X + 70, (int)base.npc.position.Y + 160, base.mod.NPCType("BoneWormHead"), 0, 0f, 0f, 0f, 0f, 255);
				}
			}
		}

		private void Target()
		{
			this.player = Main.player[base.npc.target];
		}

		private void Move(Vector2 offset)
		{
			if (Main.expertMode)
			{
				this.speed = 20f;
			}
			else
			{
				this.speed = 12f;
			}
			Vector2 vector = this.player.Center + offset;
			Vector2 vector2 = vector - base.npc.Center;
			float num = this.Magnitude(vector2);
			if (num > this.speed)
			{
				vector2 *= this.speed / num;
			}
			float num2 = 25f;
			vector2 = (base.npc.velocity * num2 + vector2) / (num2 + 1f);
			num = this.Magnitude(vector2);
			if (num > this.speed)
			{
				vector2 *= this.speed / num;
			}
			base.npc.velocity = vector2;
		}

		private void DespawnHandler()
		{
			if (!this.player.active || this.player.dead)
			{
				base.npc.TargetClosest(false);
				this.player = Main.player[base.npc.target];
				if (!this.player.active || this.player.dead)
				{
					base.npc.velocity = new Vector2(0f, -10f);
					if (base.npc.timeLeft > 10)
					{
						base.npc.timeLeft = 10;
					}
				}
			}
		}

		private void Shoot()
		{
			int num = base.mod.ProjectileType("TheKeeperPro");
			Vector2 vector = this.player.Center - base.npc.Center;
			float num2 = this.Magnitude(vector);
			if (num2 > 0f)
			{
				vector *= 10f / num2;
			}
			else
			{
				vector..ctor(0f, 5f);
			}
			Projectile.NewProjectile(base.npc.Center, vector, num, base.npc.damage, 2f, 255, 0f, 0f);
			base.npc.ai[1] = 80f;
		}

		private float Magnitude(Vector2 mag)
		{
			return (float)Math.Sqrt((double)(mag.X * mag.X + mag.Y * mag.Y));
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 1.5f;
			return null;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/Bosses/TheKeeper_Glow");
			Texture2D texture2 = base.mod.GetTexture("NPCs/Bosses/TheKeeperShriek");
			Texture2D texture3 = base.mod.GetTexture("NPCs/Bosses/TheKeeperShriek_Glow");
			SpriteEffects spriteEffects = (base.npc.spriteDirection == -1) ? 0 : 1;
			if (!this.shriekStart)
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, spriteEffects, 0f);
			}
			if (this.shriekStart)
			{
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num = texture2.Height / 3;
				int num2 = num * this.shriekFrame;
				Main.spriteBatch.Draw(texture2, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture2.Width, num)), drawColor, base.npc.rotation, new Vector2((float)texture2.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
				Main.spriteBatch.Draw(texture3, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture2.Width, num)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)texture2.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? 0 : 1, 0f);
			}
			return false;
		}

		private Player player;

		private float speed;

		public int timer;

		private bool shriekStart;

		private int shriekFrame;

		private int shriekCounter;
	}
}
