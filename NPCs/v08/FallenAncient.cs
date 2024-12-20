using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.v08
{
	public class FallenAncient : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Fallen Ancient");
			Main.npcFrameCount[base.npc.type] = 8;
		}

		public override void SetDefaults()
		{
			base.npc.lifeMax = 30000;
			base.npc.damage = 70;
			base.npc.defense = 80;
			base.npc.knockBackResist = 0f;
			base.npc.value = (float)Item.buyPrice(0, 3, 0, 0);
			base.npc.aiStyle = -1;
			base.npc.width = 80;
			base.npc.height = 92;
			base.npc.HitSound = SoundID.NPCHit41;
			base.npc.DeathSound = SoundID.NPCDeath43;
			base.npc.lavaImmune = true;
		}

		public override void NPCLoot()
		{
			if (RedeWorld.downedEaglecrestGolemPZ)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<AncientPowerCore>(), Main.rand.Next(3, 7), false, 0, false, false);
				return;
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<AncientCoreF>(), Main.rand.Next(3, 7), false, 0, false, false);
		}

		public override void AI()
		{
			Entity entity = Main.player[base.npc.target];
			float distance = base.npc.Distance(Main.player[base.npc.target].Center);
			if (entity.Center.X > base.npc.Center.X)
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
					npc.frame.Y = npc.frame.Y + 102;
					if (base.npc.frame.Y > 714)
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
					if (this.slashFrame >= 8)
					{
						this.slashFrame = 0;
					}
				}
				if (this.shield)
				{
					this.shieldCounter++;
					if (this.shieldCounter > 5)
					{
						this.shieldFrame++;
						this.shieldCounter = 0;
					}
					if (this.shieldFrame >= 7)
					{
						this.shieldFrame = 5;
					}
				}
				if (distance <= 140f && Main.rand.Next(25) == 0 && !this.slash && !this.shield)
				{
					this.slash = true;
				}
				if (distance <= 400f && Main.rand.Next(300) == 0 && !this.slash && !this.shield)
				{
					this.shield = true;
				}
				if (this.slash)
				{
					this.slashTimer++;
					base.npc.velocity.X = 0f;
					if (this.slashTimer == 1 && !RedeConfigClient.Instance.NoCombatText)
					{
						CombatText.NewText(base.npc.getRect(), Color.DarkGoldenrod, "Hyaar!", true, true);
					}
					if (this.slashTimer == 25)
					{
						Main.PlaySound(SoundID.Item71, (int)base.npc.position.X, (int)base.npc.position.Y);
						if (base.npc.direction == -1)
						{
							int p = Projectile.NewProjectile(base.npc.Center.X + -90f, base.npc.Center.Y + 18f, 0f, 0f, ModContent.ProjectileType<DamagePro4>(), 140, 3f, 255, 0f, 0f);
							Main.projectile[p].netUpdate = true;
						}
						else
						{
							int p2 = Projectile.NewProjectile(base.npc.Center.X + 62f, base.npc.Center.Y + 18f, 0f, 0f, ModContent.ProjectileType<DamagePro4>(), 140, 3f, 255, 0f, 0f);
							Main.projectile[p2].netUpdate = true;
						}
					}
					if (this.slashTimer >= 40)
					{
						this.slash = false;
						this.slashTimer = 0;
						this.slashCounter = 0;
						this.slashFrame = 0;
					}
				}
				if (this.shield)
				{
					this.shieldTimer++;
					base.npc.velocity.X = 0f;
					if (this.shieldTimer >= 120)
					{
						this.shield = false;
						this.shieldTimer = 0;
						this.shieldCounter = 0;
						this.shieldFrame = 0;
					}
				}
			}
			else
			{
				this.hop = true;
			}
			if (!this.slash && !this.shield)
			{
				if (distance <= 500f)
				{
					BaseAI.AIZombie(base.npc, ref base.npc.ai, false, false, -1, 0.1f, 4f, 10, 10, 60, true, 10, 60, false, null, false);
					return;
				}
				BaseAI.AIZombie(base.npc, ref base.npc.ai, false, false, -1, 0.08f, 1f, 7, 7, 60, true, 10, 60, false, null, false);
			}
		}

		public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			if (this.shield)
			{
				damage *= 0.1;
			}
			return true;
		}

		public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (this.shield && !projectile.minion)
			{
				if (projectile.penetrate == 1)
				{
					projectile.penetrate = 2;
				}
				if (damage > 200)
				{
					damage = 200;
				}
				projectile.damage = damage / 8;
				projectile.velocity.X = -projectile.velocity.X;
				projectile.velocity.Y = -projectile.velocity.Y;
				projectile.friendly = false;
				projectile.hostile = true;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D hopAni = base.mod.GetTexture("NPCs/v08/FallenAncientHop");
			Texture2D slashAni = base.mod.GetTexture("NPCs/v08/FallenAncientSlash");
			Texture2D shieldAni = base.mod.GetTexture("NPCs/v08/FallenAncientShield1");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.hop && !this.slash && !this.shield)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.hop && !this.slash && !this.shield)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = hopAni.Height / 1;
				int y6 = 0;
				Main.spriteBatch.Draw(hopAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, hopAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)hopAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.slash)
			{
				Vector2 drawCenter2 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num215 = slashAni.Height / 8;
				int y7 = num215 * this.slashFrame;
				Main.spriteBatch.Draw(slashAni, drawCenter2 - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, slashAni.Width, num215)), drawColor, base.npc.rotation, new Vector2((float)slashAni.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.shield)
			{
				Vector2 drawCenter3 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num216 = shieldAni.Height / 7;
				int y8 = num216 * this.shieldFrame;
				Main.spriteBatch.Draw(shieldAni, drawCenter3 - Main.screenPosition, new Rectangle?(new Rectangle(0, y8, shieldAni.Width, num216)), drawColor, base.npc.rotation, new Vector2((float)shieldAni.Width / 2f, (float)num216 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.Cavern.Chance * ((RedeWorld.downedPatientZero && !NPC.AnyNPCs(ModContent.NPCType<FallenAncient>())) ? 0.04f : 0f);
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				if (Main.netMode != 1 && base.npc.life <= 0)
				{
					NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<LostSoul3>(), 0, 0f, 0f, 0f, 0f, 255);
				}
				for (int i = 0; i < 40; i++)
				{
					int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 1, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex2].velocity *= 4.6f;
				}
				for (int j = 0; j < 20; j++)
				{
					int dustIndex3 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex3].velocity *= 4.6f;
				}
			}
			int dustIndex4 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 1, 0f, 0f, 100, default(Color), 1f);
			Main.dust[dustIndex4].velocity *= 4.6f;
		}

		private bool hop;

		private bool slash;

		private int slashFrame;

		private bool shield;

		private int shieldFrame;

		private int shieldCounter;

		private int slashCounter;

		private int slashTimer;

		private int shieldTimer;
	}
}
