using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.NPCs.Bosses.EaglecrestGolem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Thorn
{
	[AutoloadBossHead]
	public class Akka : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Akka");
			Main.npcFrameCount[base.npc.type] = 6;
		}

		public override void SetDefaults()
		{
			base.npc.lifeMax = 210000;
			base.npc.damage = 110;
			base.npc.defense = 140;
			base.npc.knockBackResist = 0f;
			base.npc.value = (float)Item.buyPrice(0, 8, 0, 0);
			base.npc.aiStyle = -1;
			base.npc.width = 46;
			base.npc.height = 108;
			base.npc.HitSound = base.mod.GetLegacySoundSlot(3, "Sounds/NPCHit/WoodHit");
			base.npc.DeathSound = SoundID.NPCDeath3;
			base.npc.noTileCollide = true;
			base.npc.noGravity = true;
			base.npc.lavaImmune = true;
			base.npc.boss = true;
			this.bossBag = base.mod.ItemType("AkkaBag");
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 3544;
			RedeWorld.downedThornPZ = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("AkanKirvesTrophy"), 1, false, 0, false, false);
			}
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
				return;
			}
			if (Main.rand.Next(7) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("AkkaMask"), 1, false, 0, false, false);
			}
			int num = Main.rand.Next(2);
			if (num == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("CursedThornBow2"), 1, false, 0, false, false);
			}
			if (num == 1)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("CursedThornFlail"), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("TuhonAura"), 1, false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("Verenhimo"), 1, false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("CursedThorns"), Main.rand.Next(9, 18), false, 0, false, false);
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override void AI()
		{
			RedeUkkoAkka.begin = false;
			this.Target();
			this.DespawnHandler();
			Player player = Main.player[base.npc.target];
			for (int i = this.oldPos.Length - 1; i > 0; i--)
			{
				this.oldPos[i] = this.oldPos[i - 1];
				this.oldrot[i] = this.oldrot[i - 1];
			}
			this.oldPos[0] = base.npc.Center;
			this.oldrot[0] = base.npc.rotation;
			base.npc.rotation = 0f;
			if (player.Center.X > base.npc.Center.X)
			{
				base.npc.spriteDirection = 1;
			}
			else
			{
				base.npc.spriteDirection = -1;
			}
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 6.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 114;
				if (base.npc.frame.Y > 570)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			if (base.npc.ai[3] == 1f)
			{
				this.frameCounters++;
				if (this.frameCounters > 6)
				{
					this.magicFrame++;
					this.frameCounters = 0;
				}
				if (this.magicFrame >= 6)
				{
					this.magicFrame = 0;
				}
			}
			Vector2 EarthProtectPos = new Vector2((player.Center.X > base.npc.Center.X) ? (player.Center.X - 500f) : (player.Center.X + 500f), player.Center.Y - 100f);
			if (RedeUkkoAkka.TAbubbles && NPC.AnyNPCs(base.mod.NPCType("Ukko")))
			{
				if (base.npc.ai[0] == 0f)
				{
					this.MoveVector2 = this.Pos();
					base.npc.ai[0] += 1f;
					return;
				}
				if (base.npc.ai[0] == 1f)
				{
					if (Vector2.Distance(base.npc.Center, this.MoveVector2) < 10f)
					{
						base.npc.velocity *= 0f;
						base.npc.ai[0] += 1f;
						base.npc.netUpdate = true;
						return;
					}
					this.MoveToVector2(this.MoveVector2);
					return;
				}
				else if (base.npc.ai[0] == 2f)
				{
					if (!this.teamAttackCheck)
					{
						base.npc.ai[1] = 0f;
						base.npc.ai[2] = 0f;
						this.teamAttackCheck = true;
						base.npc.netUpdate = true;
						return;
					}
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] % 3f == 0f && base.npc.ai[2] < 70f)
					{
						for (int j = 0; j < 2; j++)
						{
							int p = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, Utils.NextFloat(Main.rand, -16f, 16f), Utils.NextFloat(Main.rand, -16f, 16f), base.mod.ProjectileType("AkkaBubble"), 26, 1f, 255, 0f, 0f);
							Main.projectile[p].netUpdate = true;
						}
					}
					if (base.npc.ai[2] >= 100f)
					{
						this.teamAttackCheck = false;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
						return;
					}
				}
			}
			else
			{
				if (RedeUkkoAkka.TAearthProtection && NPC.AnyNPCs(base.mod.NPCType("Ukko")))
				{
					int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 163, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[dustIndex].velocity *= 1.4f;
					base.npc.ai[3] = 1f;
					base.npc.ai[1] = 0f;
					base.npc.ai[2] = 0f;
					base.npc.ai[0] = 0f;
					this.teamAttackCheck = true;
					this.MoveToVector2(EarthProtectPos);
					base.npc.netUpdate = true;
					return;
				}
				base.npc.ai[3] = 0f;
				this.teamAttackCheck = false;
				if (base.npc.ai[0] == 0f)
				{
					if (this.islandCooldown > 0)
					{
						this.islandCooldown--;
					}
					if (this.barkskinCooldown > 0)
					{
						this.barkskinCooldown--;
					}
					if (this.healingCooldown > 0)
					{
						this.healingCooldown--;
					}
					this.MoveVector2 = this.Pos();
					base.npc.ai[0] += 1f;
					return;
				}
				if (base.npc.ai[0] == 1f)
				{
					if (Vector2.Distance(base.npc.Center, this.MoveVector2) < 10f)
					{
						base.npc.velocity *= 0f;
						base.npc.ai[0] += 1f;
						base.npc.ai[1] = (float)Main.rand.Next(9);
						base.npc.netUpdate = true;
						return;
					}
					this.MoveToVector2(this.MoveVector2);
					return;
				}
				else if (base.npc.ai[0] == 2f)
				{
					switch ((int)base.npc.ai[1])
					{
					case 0:
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] % 6f == 0f && base.npc.ai[2] < 60f)
						{
							int p2 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, Utils.NextFloat(Main.rand, -8f, 8f), Utils.NextFloat(Main.rand, -8f, 8f), base.mod.ProjectileType("AkkaPoisonBubble"), 20, 1f, 255, 0f, 0f);
							Main.projectile[p2].netUpdate = true;
						}
						if (base.npc.ai[2] >= 65f)
						{
							base.npc.ai[0] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						break;
					case 1:
						if (this.islandCooldown != 0)
						{
							base.npc.ai[1] = (float)Main.rand.Next(9);
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] == 30f)
						{
							if (!Main.dedServ)
							{
								Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Quake1").WithVolume(1.2f).WithPitchVariance(0.1f), -1, -1);
							}
							int p3 = Projectile.NewProjectile(player.Center.X, player.Center.Y - 1000f, 0f, 0f, base.mod.ProjectileType("AkkaIslandSummoner"), 66, 1f, Main.myPlayer, 0f, 0f);
							Main.projectile[p3].netUpdate = true;
						}
						if (base.npc.ai[2] >= 200f)
						{
							this.islandCooldown = 30;
							base.npc.ai[0] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						break;
					case 2:
					{
						base.npc.ai[2] += 1f;
						Point point = Utils.ToTileCoordinates(player.position);
						if (base.npc.ai[2] == 5f && !Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Quake1").WithVolume(1.2f).WithPitchVariance(0.1f), -1, -1);
						}
						if (Main.tile[point.X, point.Y + 3].type != 0)
						{
							player.GetModPlayer<ShakeScreen>().shakeQuake = true;
							this.TremorTimer++;
							if (this.TremorTimer > 50 && this.TremorTimer % 40 == 0)
							{
								int p4 = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, base.mod.ProjectileType("AkkaTremor"), 13, 1f, 255, 0f, 0f);
								Main.projectile[p4].netUpdate = true;
								player.AddBuff(base.mod.BuffType("StunnedDebuff"), 60, true);
							}
						}
						if (base.npc.ai[2] >= 180f)
						{
							this.TremorTimer = 0;
							base.npc.ai[0] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						break;
					}
					case 3:
						if (base.npc.life >= (int)((float)base.npc.lifeMax * 0.8f) || this.barkskinCooldown != 0)
						{
							base.npc.ai[1] = (float)Main.rand.Next(9);
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] < 60f)
						{
							Dust dust = Dust.NewDustDirect(base.npc.position, base.npc.width, base.npc.height, 78, 0f, 0f, 100, default(Color), 2f);
							dust.velocity = -base.npc.DirectionTo(dust.position);
							base.npc.AddBuff(base.mod.BuffType("BarkskinBuff"), 3600, false);
						}
						if (base.npc.ai[2] >= 90f)
						{
							this.barkskinCooldown = 10;
							base.npc.ai[0] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						break;
					case 4:
						if (!player.ZoneSkyHeight)
						{
							base.npc.ai[1] = (float)Main.rand.Next(9);
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] == 5f)
						{
							int p5 = Projectile.NewProjectile(base.npc.Center.X, base.npc.position.Y - 4f, 0f, -10f, base.mod.ProjectileType("Moonbeam"), 0, 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[p5].hostile = false;
							Main.projectile[p5].netUpdate = true;
						}
						if (base.npc.ai[2] == 25f)
						{
							int p6 = Projectile.NewProjectile(player.Center.X, player.Center.Y - 1000f, 0f, 10f, base.mod.ProjectileType("Moonbeam"), 33, 1f, Main.myPlayer, 0f, 0f);
							Main.projectile[p6].netUpdate = true;
						}
						if (base.npc.ai[2] >= 70f)
						{
							base.npc.ai[0] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						break;
					case 5:
						if (!player.ZoneOverworldHeight)
						{
							base.npc.ai[1] = (float)Main.rand.Next(9);
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] % 10f == 0f && base.npc.ai[2] > 40f && base.npc.ai[2] < 160f)
						{
							int p7 = Projectile.NewProjectile(player.Center.X + (float)Main.rand.Next(-300, 300), player.Center.Y + (float)Main.rand.Next(-300, 0), 0f, 0f, base.mod.ProjectileType("AkkaSeed"), 30, 3f, Main.myPlayer, 0f, 0f);
							Main.projectile[p7].netUpdate = true;
						}
						if (base.npc.ai[2] >= 200f)
						{
							base.npc.ai[0] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						break;
					case 6:
						if (base.npc.life >= (int)((float)base.npc.lifeMax * 0.6f) || this.healingCooldown != 0)
						{
							base.npc.ai[1] = (float)Main.rand.Next(9);
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] % 10f == 0f && base.npc.ai[2] > 20f && base.npc.ai[2] < 200f)
						{
							int p8 = Projectile.NewProjectile(base.npc.Center.X + 50f, base.npc.Center.Y + 50f, 0f, 0f, base.mod.ProjectileType("AkkaHealingSpirit"), 0, 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[p8].netUpdate = true;
						}
						if (base.npc.ai[2] >= 260f)
						{
							this.healingCooldown = 20;
							base.npc.ai[0] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						break;
					case 7:
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] % 30f == 0f && base.npc.ai[2] < 100f)
						{
							float Speed = 16f;
							Vector2 vector8 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
							int damage = 46;
							int type = base.mod.ProjectileType("CursedThornPro6");
							float rotation = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
							int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
							Main.projectile[num54].netUpdate = true;
						}
						if (base.npc.ai[2] >= 120f)
						{
							base.npc.ai[0] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						break;
					case 8:
						if (base.npc.life < (int)((float)base.npc.lifeMax * 0.5f))
						{
							base.npc.ai[2] += 1f;
							if (base.npc.ai[2] == 30f)
							{
								int p9 = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, base.mod.ProjectileType("AkkaEarthbind"), 0, 0f, Main.myPlayer, 0f, 0f);
								Main.projectile[p9].netUpdate = true;
							}
							if (base.npc.ai[2] >= 70f)
							{
								base.npc.ai[0] = 0f;
								base.npc.ai[2] = 0f;
								base.npc.netUpdate = true;
								return;
							}
						}
						else
						{
							base.npc.ai[1] = (float)Main.rand.Next(9);
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
						}
						break;
					default:
						return;
					}
				}
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}

		public void MoveToVector2(Vector2 p)
		{
			float moveSpeed = 20f;
			float velMultiplier = 1f;
			Vector2 dist = p - base.npc.Center;
			float length = (dist == Vector2.Zero) ? 0f : dist.Length();
			if (length < moveSpeed)
			{
				velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
			}
			if (length < 100f)
			{
				moveSpeed *= 0.5f;
			}
			if (length < 50f)
			{
				moveSpeed *= 0.5f;
			}
			base.npc.velocity = ((length == 0f) ? Vector2.Zero : Vector2.Normalize(dist));
			base.npc.velocity *= moveSpeed;
			base.npc.velocity *= velMultiplier;
		}

		public Vector2 Pos()
		{
			return new Vector2((this.player.Center.X > base.npc.Center.X) ? (this.player.Center.X + (float)Main.rand.Next(-300, -200)) : (this.player.Center.X + (float)Main.rand.Next(200, 300)), this.player.Center.Y + (float)Main.rand.Next(-400, 200));
		}

		private void Target()
		{
			this.player = Main.player[base.npc.target];
		}

		public override bool CheckActive()
		{
			this.player = Main.player[base.npc.target];
			return !this.player.active || this.player.dead;
		}

		private void DespawnHandler()
		{
			if (!this.player.active || this.player.dead)
			{
				base.npc.TargetClosest(false);
				this.player = Main.player[base.npc.target];
				if (!this.player.active || this.player.dead)
				{
					base.npc.velocity = new Vector2(0f, -20f);
					if (base.npc.timeLeft > 10)
					{
						base.npc.timeLeft = 10;
					}
					return;
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D magicAni = base.mod.GetTexture("NPCs/Bosses/Thorn/Akka_Spell");
			int spriteDirection = base.npc.spriteDirection;
			new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
			if (base.npc.ai[3] == 0f)
			{
				for (int i = this.oldPos.Length - 1; i >= 0; i--)
				{
					float alpha = 1f - (float)(i + 1) / (float)(this.oldPos.Length + 2);
					spriteBatch.Draw(texture, this.oldPos[i] - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor * (0.5f * alpha), this.oldrot[i], Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				}
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			else if (base.npc.ai[3] == 1f)
			{
				int num214 = magicAni.Height / 6;
				int y6 = num214 * this.magicFrame;
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				for (int j = this.oldPos.Length - 1; j >= 0; j--)
				{
					float alpha2 = 1f - (float)(j + 1) / (float)(this.oldPos.Length + 2);
					spriteBatch.Draw(magicAni, this.oldPos[j] - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, magicAni.Width, num214)), drawColor * (0.5f * alpha2), this.oldrot[j], new Vector2((float)magicAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				}
				Main.spriteBatch.Draw(magicAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, magicAni.Width, num214)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)magicAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 35; i++)
				{
					int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 0, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex2].velocity *= 4.6f;
				}
			}
			int dustIndex3 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 0, 0f, 0f, 100, default(Color), 1f);
			Main.dust[dustIndex3].velocity *= 4.6f;
		}

		private Player player;

		private Vector2[] oldPos = new Vector2[3];

		private float[] oldrot = new float[3];

		public Vector2 MoveVector2;

		public Vector2 MoveVector3;

		public int islandCooldown = 10;

		public int barkskinCooldown;

		public int healingCooldown;

		public int TremorTimer;

		public bool teamAttackCheck;

		public int frameCounters;

		public int magicFrame;
	}
}
