using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs.New
{
	public class JanitorBot : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Janitor");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 46;
			base.npc.height = 40;
			base.npc.friendly = false;
			base.npc.damage = 55;
			base.npc.defense = 120;
			base.npc.lifeMax = 12500;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath3;
			base.npc.noGravity = false;
			base.npc.noTileCollide = false;
			base.npc.value = 5200f;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
			base.npc.boss = true;
			base.npc.netAlways = true;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 25; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 226, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[dustIndex].velocity *= 1.9f;
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 58;
			RedeWorld.downedJanitor = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			int defeated = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y + 8, base.mod.NPCType("JanitorBotDefeated"), 0, 0f, 0f, 0f, 0f, 255);
			Main.npc[defeated].netUpdate = true;
		}

		public override void NPCLoot()
		{
			if (!RedeWorld.labAccess1)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("ZoneAccessPanel1A"), 1, false, 0, false, false);
			}
		}

		public override void AI()
		{
			this.Target();
			this.DespawnHandler();
			if (this.player.Center.X > base.npc.Center.X)
			{
				base.npc.spriteDirection = 1;
			}
			else
			{
				base.npc.spriteDirection = -1;
			}
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 4.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 44;
				if (base.npc.frame.Y > 132)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			if (this.yeetMop)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] > 5f)
				{
					this.yeetFrame++;
					base.npc.ai[3] = 0f;
				}
				if (this.yeetFrame >= 6)
				{
					this.yeetFrame = 0;
				}
				base.npc.ai[1] += 1f;
				if (Main.rand.Next(3) == 0)
				{
					if (base.npc.ai[1] == 20f)
					{
						Main.PlaySound(SoundID.Item1, (int)base.npc.position.X, (int)base.npc.position.Y);
						float Speed = 8f;
						Vector2 vector8 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
						int damage = 30;
						int type = base.mod.ProjectileType("JanitorMopPro2");
						float rotation = (float)Math.Atan2((double)(vector8.Y - (this.player.position.Y + (float)this.player.height * 0.5f)), (double)(vector8.X - (this.player.position.X + (float)this.player.width * 0.5f)));
						int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
						Main.projectile[num54].netUpdate = true;
					}
				}
				else if (base.npc.ai[1] == 20f)
				{
					Main.PlaySound(SoundID.Item1, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed2 = 12f;
					Vector2 vector9 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage2 = 30;
					int type2 = base.mod.ProjectileType("JanitorMopPro1");
					float rotation2 = (float)Math.Atan2((double)(vector9.Y - (this.player.position.Y + (float)this.player.height * 0.5f)), (double)(vector9.X - (this.player.position.X + (float)this.player.width * 0.5f)));
					int num55 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0), type2, damage2, 0f, 0, 0f, 0f);
					Main.projectile[num55].netUpdate = true;
				}
				if (base.npc.ai[1] >= 30f)
				{
					this.yeetMop = false;
					base.npc.ai[1] = 0f;
					base.npc.ai[3] = 0f;
					this.yeetFrame = 0;
				}
			}
			if (this.yeetBucket)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] > 5f)
				{
					this.bucketFrame++;
					base.npc.ai[3] = 0f;
				}
				if (this.bucketFrame >= 6)
				{
					this.bucketFrame = 0;
				}
				base.npc.ai[1] += 1f;
				if (base.npc.ai[1] == 20f)
				{
					Main.PlaySound(SoundID.Item1, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed3 = 12f;
					Vector2 vector10 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage3 = 30;
					int type3 = base.mod.ProjectileType("JanitorBucketPro");
					float rotation3 = (float)Math.Atan2((double)(vector10.Y - (this.player.position.Y + (float)this.player.height * 0.5f)), (double)(vector10.X - (this.player.position.X + (float)this.player.width * 0.5f)));
					int num56 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)rotation3) * (double)Speed3 * -1.0), (float)(Math.Sin((double)rotation3) * (double)Speed3 * -1.0), type3, damage3, 0f, 0, 0f, 0f);
					Main.projectile[num56].netUpdate = true;
				}
				if (base.npc.ai[1] >= 30f)
				{
					this.yeetBucket = false;
					base.npc.ai[1] = 0f;
					base.npc.ai[3] = 0f;
					this.bucketFrame = 0;
				}
			}
			if (this.hitMop)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] > 10f)
				{
					this.hitMopFrame++;
					base.npc.ai[3] = 0f;
				}
				if (base.npc.ai[1] < 240f)
				{
					if (this.hitMopFrame >= 7)
					{
						this.hitMopFrame = 5;
					}
				}
				else if (this.hitMopFrame >= 8)
				{
					this.hitMop = false;
					base.npc.ai[1] = 0f;
					base.npc.ai[3] = 0f;
					this.hitMopFrame = 0;
				}
				base.npc.ai[1] += 1f;
				if (base.npc.ai[1] == 2f && !RedeConfigClient.Instance.NoCombatText)
				{
					switch (Main.rand.Next(6))
					{
					case 0:
						CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "Oof!", false, false);
						break;
					case 1:
						CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "Owch!", false, false);
						break;
					case 2:
						CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "Yowch!", false, false);
						break;
					case 3:
						CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "Ow!", false, false);
						break;
					case 4:
						CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "Arg!", false, false);
						break;
					case 5:
						CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "Damn it!", false, false);
						break;
					}
				}
			}
			if (this.slip)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] > 10f)
				{
					this.slipFrame++;
					base.npc.ai[3] = 0f;
				}
				if (base.npc.ai[1] < 140f)
				{
					if (this.slipFrame >= 6)
					{
						this.slipFrame = 4;
					}
				}
				else if (this.slipFrame >= 7)
				{
					this.slip = false;
					base.npc.ai[1] = 0f;
					base.npc.ai[3] = 0f;
					this.slipFrame = 0;
				}
				base.npc.ai[1] += 1f;
				if (base.npc.ai[1] == 2f && !RedeConfigClient.Instance.NoCombatText)
				{
					switch (Main.rand.Next(6))
					{
					case 0:
						CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "Ah!", false, false);
						break;
					case 1:
						CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "D'oh!", false, false);
						break;
					case 2:
						CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "Oops!", false, false);
						break;
					case 3:
						CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "Whoops!", false, false);
						break;
					case 4:
						CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "Not again!", false, false);
						break;
					case 5:
						CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "Damn it!", false, false);
						break;
					}
				}
			}
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			if (!this.hitMop && !this.slip)
			{
				base.npc.ai[0] += 1f;
				base.npc.defense = 120;
			}
			else
			{
				base.npc.defense = 0;
			}
			if (base.npc.HasBuff(base.mod.BuffType("JanitorStun")) && this.landed && !this.yeetBucket && !this.yeetMop && !this.slip && base.npc.ai[0] != 10f && base.npc.ai[0] != 90f && base.npc.ai[0] != 190f && base.npc.ai[0] != 270f)
			{
				this.hitMop = true;
			}
			if (base.npc.HasBuff(103) && this.landed && !this.yeetBucket && !this.yeetMop && !this.hitMop && Main.rand.Next(200) == 0 && base.npc.ai[0] != 10f && base.npc.ai[0] != 90f && base.npc.ai[0] != 190f && base.npc.ai[0] != 270f)
			{
				this.slip = true;
			}
			if (base.npc.ai[0] == 10f && this.landed)
			{
				if (base.npc.life < (int)((float)base.npc.lifeMax * 0.65f))
				{
					base.npc.velocity.X = -5f;
					base.npc.velocity.Y = -7f;
				}
				else
				{
					base.npc.velocity.X = -5f;
					base.npc.velocity.Y = -5f;
				}
				this.landed = false;
				base.npc.netUpdate = true;
			}
			if (base.npc.ai[0] == 90f && this.landed)
			{
				switch (Main.rand.Next(3))
				{
				case 0:
					this.yeetMop = true;
					break;
				case 1:
					this.yeetMop = true;
					break;
				case 2:
					this.yeetBucket = true;
					break;
				}
				base.npc.netUpdate = true;
			}
			if (base.npc.ai[0] == 190f)
			{
				if (base.npc.life < (int)((float)base.npc.lifeMax * 0.65f))
				{
					base.npc.velocity.X = 5f;
					base.npc.velocity.Y = -7f;
				}
				else
				{
					base.npc.velocity.X = 5f;
					base.npc.velocity.Y = -5f;
				}
				this.landed = false;
				base.npc.netUpdate = true;
			}
			if (base.npc.ai[0] == 270f && this.landed)
			{
				switch (Main.rand.Next(3))
				{
				case 0:
					this.yeetMop = true;
					break;
				case 1:
					this.yeetMop = true;
					break;
				case 2:
					this.yeetBucket = true;
					break;
				}
				base.npc.netUpdate = true;
			}
			if (base.npc.ai[0] >= 370f)
			{
				base.npc.ai[0] = 0f;
				base.npc.netUpdate = true;
			}
			if (base.npc.collideY && base.npc.velocity.Y > 0f)
			{
				if (!this.landed)
				{
					for (int i = 0; i < 6; i++)
					{
						Dust.NewDust(base.npc.BottomLeft, Main.rand.Next(base.npc.width), 1, 31, 0f, 0f, 0, default(Color), 1f);
					}
					this.landed = true;
					base.npc.netUpdate = true;
				}
				base.npc.velocity.X = 0f;
			}
			base.npc.ai[2] += 1f;
			if (base.npc.ai[2] == 800f && !RedeConfigClient.Instance.NoCombatText)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "You may be wondering...", true, false);
			}
			if (base.npc.ai[2] == 950f && !RedeConfigClient.Instance.NoCombatText)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "... Where I'm getting all these mops from...", true, false);
			}
			if (base.npc.ai[2] == 1200f && !RedeConfigClient.Instance.NoCombatText)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "... Well, it's a secret! Haha!", true, false);
			}
			if (base.npc.ai[2] == 2000f && !RedeConfigClient.Instance.NoCombatText)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "All this jumping about is making me tired.", true, false);
			}
			if (base.npc.ai[2] == 2200f && !RedeConfigClient.Instance.NoCombatText)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "Oh well! It's exercise!", true, false);
			}
			if (base.npc.ai[2] == 2350f && !RedeConfigClient.Instance.NoCombatText)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "Oh right, robots don't need exercise...", true, false);
			}
			if (base.npc.ai[2] == 5000f && !RedeConfigClient.Instance.NoCombatText)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "My joints are a little stiff...", true, false);
			}
			if (base.npc.ai[2] == 5200f && !RedeConfigClient.Instance.NoCombatText)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "When was the last time I was oiled?", true, false);
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
					base.npc.noTileCollide = true;
					base.npc.velocity = new Vector2(0f, -500f);
					if (base.npc.timeLeft > 10)
					{
						base.npc.timeLeft = 10;
					}
					return;
				}
			}
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.5f);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D hopAni = base.mod.GetTexture("NPCs/LabNPCs/New/JanitorBotHop");
			Texture2D YeetAni = base.mod.GetTexture("NPCs/LabNPCs/New/JanitorBotYeet");
			Texture2D BucketAni = base.mod.GetTexture("NPCs/LabNPCs/New/JanitorBotBucketYeet");
			Texture2D HitMopAni = base.mod.GetTexture("NPCs/LabNPCs/New/JanitorBotHitMop");
			Texture2D SlipAni = base.mod.GetTexture("NPCs/LabNPCs/New/JanitorBotSlip");
			int spriteDirection = base.npc.spriteDirection;
			if (base.npc.velocity.Y == 0f && !this.yeetMop && !this.yeetBucket && !this.hitMop && !this.slip)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (base.npc.velocity.Y != 0f && !this.yeetMop && !this.yeetBucket && !this.hitMop && !this.slip)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = hopAni.Height / 1;
				int y6 = num214 * this.singleFrame;
				Main.spriteBatch.Draw(hopAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, hopAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)hopAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.yeetMop)
			{
				Vector2 drawCenter2 = new Vector2(base.npc.Center.X, base.npc.Center.Y - 6f);
				int num215 = YeetAni.Height / 6;
				int y7 = num215 * this.yeetFrame;
				Main.spriteBatch.Draw(YeetAni, drawCenter2 - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, YeetAni.Width, num215)), drawColor, base.npc.rotation, new Vector2((float)YeetAni.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.yeetBucket)
			{
				Vector2 drawCenter3 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num216 = BucketAni.Height / 6;
				int y8 = num216 * this.bucketFrame;
				Main.spriteBatch.Draw(BucketAni, drawCenter3 - Main.screenPosition, new Rectangle?(new Rectangle(0, y8, BucketAni.Width, num216)), drawColor, base.npc.rotation, new Vector2((float)BucketAni.Width / 2f, (float)num216 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.hitMop)
			{
				Vector2 drawCenter4 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num217 = HitMopAni.Height / 8;
				int y9 = num217 * this.hitMopFrame;
				Main.spriteBatch.Draw(HitMopAni, drawCenter4 - Main.screenPosition, new Rectangle?(new Rectangle(0, y9, HitMopAni.Width, num217)), drawColor, base.npc.rotation, new Vector2((float)HitMopAni.Width / 2f, (float)num217 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.slip)
			{
				Vector2 drawCenter5 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num218 = SlipAni.Height / 7;
				int y10 = num218 * this.slipFrame;
				Main.spriteBatch.Draw(SlipAni, drawCenter5 - Main.screenPosition, new Rectangle?(new Rectangle(0, y10, SlipAni.Width, num218)), drawColor, base.npc.rotation, new Vector2((float)SlipAni.Width / 2f, (float)num218 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		private Player player;

		public bool landed;

		public bool yeetMop;

		public int yeetFrame;

		public bool yeetBucket;

		public int bucketFrame;

		public bool hitMop;

		public int hitMopFrame;

		public bool slip;

		public int slipFrame;

		private int singleFrame;
	}
}
