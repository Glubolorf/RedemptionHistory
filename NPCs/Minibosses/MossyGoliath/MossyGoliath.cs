using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs.Debuffs;
using Redemption.Items.Quest;
using Redemption.Items.Quest.Daerel;
using Redemption.Items.Quest.Zephos;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Minibosses.MossyGoliath
{
	[AutoloadBossHead]
	public class MossyGoliath : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mossy Goliath");
			Main.npcFrameCount[base.npc.type] = 6;
		}

		public override void SetDefaults()
		{
			base.npc.lifeMax = 4000;
			base.npc.damage = 40;
			base.npc.defense = 14;
			base.npc.knockBackResist = 0f;
			base.npc.value = (float)Item.buyPrice(0, 4, 0, 0);
			base.npc.aiStyle = -1;
			base.npc.width = 126;
			base.npc.height = 84;
			base.npc.HitSound = SoundID.NPCHit24;
			base.npc.DeathSound = SoundID.NPCDeath27;
			base.npc.lavaImmune = true;
			base.npc.boss = true;
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 188;
			RedeWorld.downedMossyGoliath = true;
			if (Main.netMode != 0)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override void NPCLoot()
		{
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<MossyWimpGun>(), 1, false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<MudMace>(), 1, false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<TastySteak>(), 1, false, 0, false, false);
			if (RedeQuests.zephosQuests == 4)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<SwordSlicerFrag1>(), 1, false, 0, false, false);
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<SwordSlicerFrag2>(), 1, false, 0, false, false);
			}
			if (RedeQuests.daerelQuests == 4)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<SilverwoodBowFrag1>(), 1, false, 0, false, false);
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<SilverwoodBowFrag2>(), 1, false, 0, false, false);
			}
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override void AI()
		{
			Player player = Main.player[base.npc.target];
			for (int i = this.oldPos.Length - 1; i > 0; i--)
			{
				this.oldPos[i] = this.oldPos[i - 1];
				this.oldrot[i] = this.oldrot[i - 1];
			}
			this.oldPos[0] = base.npc.Center;
			this.oldrot[0] = base.npc.rotation;
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
				if (base.npc.ai[3] == 0f)
				{
					if (base.npc.velocity.X == 0f)
					{
						this.runFrame = 0;
						this.frameCounters = 0f;
						base.npc.frameCounter += 1.0;
						if (base.npc.frameCounter >= 5.0)
						{
							base.npc.frameCounter = 0.0;
							NPC npc = base.npc;
							npc.frame.Y = npc.frame.Y + 88;
							if (base.npc.frame.Y > 440)
							{
								base.npc.frameCounter = 0.0;
								base.npc.frame.Y = 0;
							}
						}
					}
					else
					{
						this.frameCounters += 1f;
						if ((this.roarCooldown > 0) ? (this.frameCounters > 3f) : (this.frameCounters >= 7f))
						{
							this.runFrame++;
							this.frameCounters = 0f;
						}
						if (this.runFrame >= 7)
						{
							this.runFrame = 0;
							this.frameCounters = 0f;
						}
					}
				}
				else if (base.npc.ai[3] == 1f)
				{
					this.frameCounters += 1f;
					if (this.frameCounters > 5f)
					{
						this.roarFrame++;
						this.frameCounters = 0f;
					}
					if (this.roarFrame >= 7)
					{
						this.roarFrame = 5;
					}
				}
				else if (base.npc.ai[3] == 2f)
				{
					this.frameCounters += 1f;
					if (this.frameCounters > 5f)
					{
						this.roarFrame++;
						this.frameCounters = 0f;
					}
					if (this.roarFrame >= 5)
					{
						this.roarFrame = 4;
						base.npc.ai[3] = 0f;
					}
				}
			}
			else
			{
				this.roarFrame = 2;
			}
			Point point = Utils.ToTileCoordinates(player.Bottom);
			Utils.ToTileCoordinates(base.npc.Bottom);
			float distance = base.npc.Distance(Main.player[base.npc.target].Center);
			if (base.npc.ai[0] == 0f)
			{
				base.npc.ai[3] = 1f;
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] == 25f && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Roar1").WithVolume(1f).WithPitchVariance(0.1f), -1, -1);
				}
				if (base.npc.ai[2] == 30f)
				{
					player.GetModPlayer<ScreenPlayer>().ScreenShakeIntensity = 10f;
				}
				if (base.npc.ai[2] >= 80f)
				{
					base.npc.ai[3] = 0f;
					base.npc.ai[0] = 1f;
					base.npc.ai[2] = 0f;
					this.roarFrame = 0;
					base.npc.netUpdate = true;
				}
			}
			if (base.npc.ai[0] != 1f)
			{
				if (base.npc.ai[0] == 2f)
				{
					if (this.roarCooldown > 0)
					{
						this.aiType = 0;
						BaseAI.AIZombie(base.npc, ref base.npc.ai, false, false, -1, 0.6f, 8f, 38, 38, 60, true, 10, 60, false, null, false);
					}
					else
					{
						this.aiType = 0;
						BaseAI.AIZombie(base.npc, ref base.npc.ai, false, false, -1, 0.3f, 4f, 26, 26, 60, true, 10, 60, false, null, false);
					}
					float[] ai = base.npc.ai;
					int num = 2;
					float num2 = ai[num] + 1f;
					ai[num] = num2;
					if (num2 > 120f && base.npc.velocity.Y == 0f)
					{
						base.npc.aiStyle = -1;
						NPC npc2 = base.npc;
						npc2.velocity.X = npc2.velocity.X * 0f;
						base.npc.ai[0] = 4f;
						base.npc.ai[2] = 0f;
						base.npc.ai[1] = (float)Main.rand.Next(4);
						base.npc.netUpdate = true;
						return;
					}
				}
				else if (base.npc.ai[0] == 3f)
				{
					float[] ai2 = base.npc.ai;
					int num3 = 2;
					float num2 = ai2[num3] + 1f;
					ai2[num3] = num2;
					if (num2 > 80f && base.npc.velocity.Y == 0f)
					{
						base.npc.aiStyle = -1;
						NPC npc3 = base.npc;
						npc3.velocity.X = npc3.velocity.X * 0f;
						base.npc.ai[0] = 4f;
						base.npc.ai[2] = 0f;
						base.npc.ai[1] = (float)Main.rand.Next(4);
						base.npc.netUpdate = true;
						return;
					}
				}
				else if (base.npc.ai[0] == 4f)
				{
					switch ((int)base.npc.ai[1])
					{
					case 0:
						base.npc.ai[3] = 1f;
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] == 25f && !Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Roar1").WithVolume(1f).WithPitchVariance(0.1f), -1, -1);
						}
						if (base.npc.ai[2] == 30f)
						{
							player.GetModPlayer<ScreenPlayer>().ScreenShakeIntensity = 10f;
						}
						if (base.npc.ai[2] > 25f && base.npc.ai[2] % 10f == 0f)
						{
							int p = Projectile.NewProjectile(new Vector2((base.npc.spriteDirection == -1) ? (base.npc.position.X + 12f) : (base.npc.position.X + 148f), base.npc.position.Y + 56f), new Vector2((base.npc.spriteDirection == -1) ? -3f : 3f, 0f), ModContent.ProjectileType<GoliathScreech>(), 0, 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[p].netUpdate = true;
						}
						if (base.npc.ai[2] >= 80f)
						{
							base.npc.ai[3] = 0f;
							base.npc.ai[0] = 1f;
							base.npc.ai[2] = 0f;
							this.roarCooldown = 2;
							this.roarFrame = 0;
							base.npc.netUpdate = true;
							return;
						}
						break;
					case 1:
						base.npc.ai[3] = 2f;
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] == 20f)
						{
							Main.PlaySound(SoundID.Item14, (int)base.npc.position.X, (int)base.npc.position.Y);
							for (int j = 0; j < 20; j++)
							{
								int dustIndex2 = Dust.NewDust(new Vector2(base.npc.Center.X - 300f, base.npc.Center.Y), base.npc.width + 600, base.npc.height / 2, 31, 0f, 0f, 100, default(Color), 2f);
								Dust dust = Main.dust[dustIndex2];
								dust.velocity.Y = dust.velocity.Y * 3.6f;
								Dust dust2 = Main.dust[dustIndex2];
								dust2.velocity.X = dust2.velocity.X * 0f;
							}
							if (Main.tile[point.X, point.Y].type != 0 && Main.tile[point.X, point.Y].active() && player.velocity.Y == 0f && distance < 600f)
							{
								int hitDirection = (base.npc.Center.X > player.Center.X) ? -1 : 1;
								player.Hurt(PlayerDeathReason.ByNPC(base.npc.whoAmI), base.npc.damage, hitDirection, false, false, false, 0);
								player.AddBuff(ModContent.BuffType<StunnedDebuff>(), 60, true);
							}
						}
						if (base.npc.ai[2] >= 60f)
						{
							base.npc.ai[3] = 0f;
							base.npc.ai[0] = 1f;
							base.npc.ai[2] = 0f;
							this.roarFrame = 0;
							base.npc.netUpdate = true;
							return;
						}
						break;
					case 2:
					{
						if (base.npc.life >= (int)((float)base.npc.lifeMax * 0.7f))
						{
							base.npc.ai[1] = (float)Main.rand.Next(4);
							base.npc.ai[2] = 0f;
							base.npc.ai[3] = 0f;
							base.npc.netUpdate = true;
							return;
						}
						Vector2 FallPos = new Vector2(player.Center.X, player.Center.Y - 250f);
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] < 180f)
						{
							if (Vector2.Distance(base.npc.Center, FallPos) < 200f || base.npc.ai[2] == 179f)
							{
								base.npc.noTileCollide = false;
								base.npc.noGravity = false;
								base.npc.velocity.X = 0f;
								base.npc.ai[2] = 180f;
								base.npc.netUpdate = true;
							}
							else
							{
								base.npc.noTileCollide = true;
								base.npc.noGravity = true;
								this.MoveToVector2(FallPos);
							}
						}
						if (base.npc.ai[2] == 180f)
						{
							NPC npc4 = base.npc;
							npc4.velocity.Y = npc4.velocity.Y + 10f;
						}
						if (base.npc.ai[2] > 180f && base.npc.velocity.Y == 0f)
						{
							Main.PlaySound(SoundID.Item14, (int)base.npc.position.X, (int)base.npc.position.Y);
							for (int k = 0; k < 20; k++)
							{
								int dustIndex3 = Dust.NewDust(new Vector2(base.npc.Center.X - 300f, base.npc.Center.Y), base.npc.width + 600, base.npc.height / 2, 31, 0f, 0f, 100, default(Color), 2f);
								Dust dust3 = Main.dust[dustIndex3];
								dust3.velocity.Y = dust3.velocity.Y * 3.6f;
								Dust dust4 = Main.dust[dustIndex3];
								dust4.velocity.X = dust4.velocity.X * 0f;
							}
							if (Main.tile[point.X, point.Y].type != 0 && Main.tile[point.X, point.Y].active() && player.velocity.Y == 0f && distance < 600f)
							{
								int hitDirection2 = (base.npc.Center.X > player.Center.X) ? -1 : 1;
								player.Hurt(PlayerDeathReason.ByNPC(base.npc.whoAmI), base.npc.damage, hitDirection2, false, false, false, 0);
								player.AddBuff(ModContent.BuffType<StunnedDebuff>(), 60, true);
							}
							base.npc.ai[3] = 0f;
							base.npc.ai[0] = 1f;
							base.npc.ai[2] = 0f;
							this.roarFrame = 0;
							base.npc.netUpdate = true;
							return;
						}
						break;
					}
					case 3:
						base.npc.ai[3] = 1f;
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] == 25f && !Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Roar1").WithVolume(0.3f).WithPitchVariance(0.1f), -1, -1);
						}
						if (base.npc.ai[2] > 25f && base.npc.ai[2] % 4f == 0f)
						{
							int p2 = Projectile.NewProjectile(new Vector2((base.npc.spriteDirection == -1) ? (base.npc.position.X + 12f) : (base.npc.position.X + 148f), base.npc.position.Y + 46f), new Vector2((base.npc.spriteDirection == -1) ? -3f : 3f, Utils.NextFloat(Main.rand, -1f, 1f)), ModContent.ProjectileType<ToxicBreath>(), 13, 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[p2].netUpdate = true;
						}
						if (base.npc.ai[2] >= 80f)
						{
							base.npc.ai[3] = 0f;
							base.npc.ai[0] = 1f;
							base.npc.ai[2] = 0f;
							this.roarFrame = 0;
							base.npc.netUpdate = true;
						}
						break;
					default:
						return;
					}
				}
				return;
			}
			this.frameCounters = 0f;
			this.runFrame = 0;
			if (this.roarCooldown > 0)
			{
				this.roarCooldown--;
			}
			if (Main.rand.Next(3) == 0 && this.roarCooldown == 0)
			{
				base.npc.ai[0] = 3f;
				return;
			}
			base.npc.ai[0] = 2f;
		}

		public void MoveToVector2(Vector2 p)
		{
			float moveSpeed = 10f;
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

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D textureZ = base.mod.GetTexture("NPCs/Minibosses/MossyGoliath/MossyGoliath_Z");
			Texture2D runAni = base.mod.GetTexture("NPCs/Minibosses/MossyGoliath/MossyGoliath_Run");
			Texture2D runAniZ = base.mod.GetTexture("NPCs/Minibosses/MossyGoliath/MossyGoliath_Run_Z");
			Texture2D roarAni = base.mod.GetTexture("NPCs/Minibosses/MossyGoliath/MossyGoliath_Roar");
			Texture2D roarAniZ = base.mod.GetTexture("NPCs/Minibosses/MossyGoliath/MossyGoliath_Roar_Z");
			int spriteDirection = base.npc.spriteDirection;
			if (base.npc.ai[3] == 0f && base.npc.velocity.Y == 0f)
			{
				if (base.npc.velocity.X == 0f)
				{
					if (RedeQuests.zephosQuests == 4)
					{
						spriteBatch.Draw(textureZ, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
					}
					spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				}
				else
				{
					Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y - 3f);
					int num214 = runAni.Height / 7;
					int y6 = num214 * this.runFrame;
					if (this.roarCooldown > 0 && base.npc.ai[0] == 2f)
					{
						for (int i = this.oldPos.Length - 1; i >= 0; i--)
						{
							float alpha = 1f - (float)(i + 1) / (float)(this.oldPos.Length + 2);
							spriteBatch.Draw(runAni, this.oldPos[i] - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, runAni.Width, num214)), drawColor * (0.5f * alpha), this.oldrot[i], new Vector2((float)runAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
						}
					}
					if (RedeQuests.zephosQuests == 4)
					{
						Main.spriteBatch.Draw(runAniZ, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, runAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)runAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
					}
					Main.spriteBatch.Draw(runAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, runAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)runAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				}
			}
			else
			{
				Vector2 drawCenter2 = new Vector2(base.npc.Center.X, base.npc.Center.Y - 5f);
				int num215 = roarAni.Height / 7;
				int y7 = num215 * this.roarFrame;
				if (RedeQuests.zephosQuests == 4)
				{
					Main.spriteBatch.Draw(roarAniZ, drawCenter2 - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, roarAni.Width, num215)), drawColor, base.npc.rotation, new Vector2((float)roarAni.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				}
				Main.spriteBatch.Draw(roarAni, drawCenter2 - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, roarAni.Width, num215)), drawColor, base.npc.rotation, new Vector2((float)roarAni.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
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
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Boss/MossyGoliathGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Boss/MossyGoliathGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Boss/MossyGoliathGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Boss/MossyGoliathGore4"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Boss/MossyGoliathGore5"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Boss/MossyGoliathGore6"), 1f);
			}
			int dustIndex3 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 3, 0f, 0f, 100, default(Color), 1f);
			Main.dust[dustIndex3].velocity *= 4.6f;
		}

		private Vector2[] oldPos = new Vector2[3];

		private float[] oldrot = new float[3];

		public int runFrame;

		public int roarFrame;

		public float frameCounters;

		public int roarCooldown;
	}
}
