using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.v08
{
	public class SneezyInfectedFlinx : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Infected Snow Flinx");
			Main.npcFrameCount[base.npc.type] = 10;
		}

		public override void SetDefaults()
		{
			base.npc.width = 44;
			base.npc.height = 40;
			base.npc.damage = 75;
			base.npc.friendly = false;
			base.npc.defense = 0;
			base.npc.lifeMax = 740;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.value = 400f;
			base.npc.knockBackResist = 1.1f;
			base.npc.aiStyle = 3;
			this.aiType = 185;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 25; i++)
				{
					int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, 0f, 0f, 100, default(Color), 3f);
					Main.dust[dustIndex2].velocity *= 2.6f;
				}
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
			}
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			if (Main.rand.Next(2) == 0 || (Main.expertMode && Main.rand.Next(0) == 0))
			{
				target.AddBuff(base.mod.BuffType("XenomiteDebuff"), Main.rand.Next(500, 1000), true);
			}
			if (Main.rand.Next(9) == 0 || (Main.expertMode && Main.rand.Next(7) == 0))
			{
				target.AddBuff(base.mod.BuffType("XenomiteDebuff2"), Main.rand.Next(250, 500), true);
			}
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
				if (base.npc.frameCounter >= 5.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc = base.npc;
					npc.frame.Y = npc.frame.Y + 46;
					if (base.npc.frame.Y > 414)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 0;
					}
				}
				if (this.buildup)
				{
					this.buildupCounter++;
					if (this.buildupCounter > 10)
					{
						this.buildupFrame++;
						this.buildupCounter = 0;
					}
					if (this.buildupFrame >= 5)
					{
						this.buildupFrame = 0;
					}
				}
				if (base.npc.Distance(Main.player[base.npc.target].Center) <= 200f && Main.rand.Next(120) == 0 && !this.buildup && !this.sneeze)
				{
					this.buildup = true;
				}
				if (!this.buildup && !this.sneeze)
				{
					base.npc.aiStyle = 3;
				}
				if (this.buildup)
				{
					this.buildupTimer++;
					base.npc.aiStyle = 0;
					base.npc.velocity.X = 0f;
					if (this.buildupTimer >= 25)
					{
						Main.PlaySound(SoundID.Item50, base.npc.position);
						for (int i = 0; i < 25; i++)
						{
							int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 1f);
							Main.dust[dustIndex2].velocity *= 4.6f;
						}
						for (int j = 0; j < 10; j++)
						{
							int dustIndex3 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 1f);
							Main.dust[dustIndex3].velocity *= 4.6f;
						}
						this.sneeze = true;
						this.buildup = false;
						this.buildupTimer = 0;
						this.buildupCounter = 0;
						this.buildupFrame = 0;
					}
				}
				if (!this.sneeze)
				{
					base.npc.HitSound = SoundID.NPCHit1;
					return;
				}
				base.npc.HitSound = SoundID.DD2_WitherBeastCrystalImpact;
				base.npc.knockBackResist = 0f;
				this.sneezeTimer++;
				base.npc.aiStyle = 0;
				base.npc.velocity.X = 0f;
				this.sneezeTimer++;
				if (this.sneezeTimer == 2 && !RedeConfigClient.Instance.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Color.LightBlue, "ACHOO!", true, true);
				}
				if (this.sneezeTimer >= 180)
				{
					Main.PlaySound(SoundID.Item50, base.npc.position);
					for (int k = 0; k < 25; k++)
					{
						int dustIndex4 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 1f);
						Main.dust[dustIndex4].velocity *= 4.6f;
					}
					for (int l = 0; l < 10; l++)
					{
						int dustIndex5 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 1f);
						Main.dust[dustIndex5].velocity *= 4.6f;
					}
					this.sneeze = false;
					this.buildup = false;
					this.sneezeTimer = 0;
					this.sneezeCounter = 0;
					this.sneezeFrame = 0;
					return;
				}
			}
			else
			{
				this.hop = true;
			}
		}

		public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
		{
			if (this.sneeze && item.pick > 0)
			{
				damage = item.damage + item.pick * 2;
			}
		}

		public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			if (this.sneeze)
			{
				damage *= 0.1;
			}
			return true;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D hopAni = base.mod.GetTexture("NPCs/v08/SneezyInfectedFlinxHop");
			Texture2D buildupAni = base.mod.GetTexture("NPCs/v08/SneezyInfectedFlinxBuildup");
			Texture2D sneezeAni = base.mod.GetTexture("NPCs/v08/SneezyInfectedFlinxSneeze");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.hop && !this.buildup && !this.sneeze)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.hop && !this.sneeze && !this.buildup)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = hopAni.Height / 1;
				int y6 = num214 * this.hopFrame;
				Main.spriteBatch.Draw(hopAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, hopAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)hopAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.buildup)
			{
				Vector2 drawCenter2 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num215 = buildupAni.Height / 5;
				int y7 = num215 * this.buildupFrame;
				Main.spriteBatch.Draw(buildupAni, drawCenter2 - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, buildupAni.Width, num215)), drawColor, base.npc.rotation, new Vector2((float)buildupAni.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.sneeze)
			{
				Vector2 drawCenter3 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num216 = sneezeAni.Height / 1;
				int y8 = num216 * this.sneezeFrame;
				Main.spriteBatch.Draw(sneezeAni, drawCenter3 - Main.screenPosition, new Rectangle?(new Rectangle(0, y8, sneezeAni.Width, num216)), drawColor, base.npc.rotation, new Vector2((float)sneezeAni.Width / 2f, (float)num216 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		private bool buildup;

		private bool sneeze;

		private int buildupFrame;

		private int buildupCounter;

		private int buildupTimer;

		private int sneezeFrame;

		private int sneezeCounter;

		private int sneezeTimer;

		private bool hop;

		private int hopFrame;

		private int hopCounter;
	}
}
