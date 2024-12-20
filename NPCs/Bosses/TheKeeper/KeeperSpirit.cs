using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Armor.Vanity;
using Redemption.Items.Materials.PreHM;
using Redemption.Items.Placeable.Trophies;
using Redemption.Items.Usable;
using Redemption.Items.Weapons.PreHM.Magic;
using Redemption.Items.Weapons.PreHM.Melee;
using Redemption.Items.Weapons.PreHM.Ranged;
using Redemption.Items.Weapons.PreHM.Summon;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.TheKeeper
{
	[AutoloadBossHead]
	public class KeeperSpirit : ModNPC
	{
		public KeeperSpirit.ActionState AIState
		{
			get
			{
				return (KeeperSpirit.ActionState)base.npc.ai[0];
			}
			set
			{
				base.npc.ai[0] = (float)value;
			}
		}

		public ref float AITimer
		{
			get
			{
				return ref base.npc.ai[1];
			}
		}

		public ref float TimerRand
		{
			get
			{
				return ref base.npc.ai[2];
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Keeper's Spirit");
			Main.npcFrameCount[base.npc.type] = 8;
			NPCID.Sets.TrailCacheLength[base.npc.type] = 5;
			NPCID.Sets.TrailingMode[base.npc.type] = 1;
			NPCID.Sets.MPAllowedEnemies[base.npc.type] = true;
		}

		public override void SetDefaults()
		{
			base.npc.aiStyle = -1;
			base.npc.lifeMax = 3500;
			base.npc.damage = 30;
			base.npc.defense = 10;
			base.npc.knockBackResist = 0f;
			base.npc.width = 52;
			base.npc.height = 128;
			base.npc.npcSlots = 10f;
			base.npc.value = (float)Item.buyPrice(0, 3, 50, 0);
			base.npc.alpha = 255;
			base.npc.boss = true;
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.netAlways = true;
			base.npc.HitSound = SoundID.NPCHit36;
			base.npc.DeathSound = SoundID.NPCDeath39;
			this.bossBag = ModContent.ItemType<TheKeeperBag>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 100; i++)
				{
					int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 180, 0f, 0f, 0, default(Color), 3f);
					Main.dust[dustIndex].velocity *= 4f;
				}
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}

		public override bool? CanHitNPC(NPC target)
		{
			return new bool?(false);
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 188;
			RedeWorld.downedKeeper = true;
			if (Main.netMode != 0)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<TheKeeperTrophy>(), 1, false, 0, false, false);
			}
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
				return;
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<OldGathicWaraxe>(), 1, false, 0, false, false);
			}
			if (Main.rand.Next(7) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<TheKeeperMask>(), 1, false, 0, false, false);
			}
			int num = Main.rand.Next(5);
			if (num == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<KeepersBow>(), 1, false, 0, false, false);
			}
			if (num == 1)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<KeepersStaff>(), 1, false, 0, false, false);
			}
			if (num == 2)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<KeepersClaw>(), 1, false, 0, false, false);
			}
			if (num == 3)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<KeepersKnife>(), 1, false, 0, false, false);
			}
			if (num == 4)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<KeepersSummon>(), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<DarkShard>(), Main.rand.Next(2, 4), false, 0, false, false);
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			if (Main.netMode == 2 || Main.dedServ)
			{
				writer.Write(this.ID);
				writer.Write(this.Unveiled);
			}
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			base.ReceiveExtraAI(reader);
			if (Main.netMode == 1)
			{
				this.ID = reader.ReadInt32();
				this.Unveiled = reader.ReadBoolean();
			}
		}

		private void AttackChoice()
		{
			for (int attempts = 0; attempts == 0; attempts++)
			{
				if (this.CopyList == null || this.CopyList.Count == 0)
				{
					this.CopyList = new List<int>(this.AttackList);
				}
				this.ID = this.CopyList[Main.rand.Next(0, this.CopyList.Count)];
				this.CopyList.Remove(this.ID);
				base.npc.netUpdate = true;
			}
		}

		public int ID
		{
			get
			{
				return (int)base.npc.ai[3];
			}
			set
			{
				base.npc.ai[3] = (float)value;
			}
		}

		public unsafe override void AI()
		{
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			Player player = Main.player[base.npc.target];
			Rectangle SlashHitbox = new Rectangle((int)((base.npc.spriteDirection == -1) ? (base.npc.Center.X - 64f) : (base.npc.Center.X + 26f)), (int)(base.npc.Center.Y - 38f), 38, 86);
			this.DespawnHandler();
			if (this.AIState != KeeperSpirit.ActionState.Attacks)
			{
				base.npc.LookAtPlayer();
			}
			switch (this.AIState)
			{
			case KeeperSpirit.ActionState.Begin:
				*this.AITimer += 1f;
				base.npc.alpha -= 2;
				if (base.npc.alpha <= 0)
				{
					this.AIState = KeeperSpirit.ActionState.Idle;
					*this.AITimer = 0f;
					base.npc.netUpdate = true;
					return;
				}
				break;
			case KeeperSpirit.ActionState.Idle:
			{
				ref float aitimer = ref this.AITimer;
				float num = aitimer;
				aitimer = num + 1f;
				if (num == 0f)
				{
					this.move = base.npc.Center.X;
					this.speed = 6f;
				}
				base.npc.Move(new Vector2(this.move, player.Center.Y - 50f), this.speed, 20f, false);
				this.MoveClamp();
				if (base.npc.DistanceSQ(player.Center) > 160000f)
				{
					this.speed *= 1.03f;
				}
				if (!this.Unveiled && base.npc.life < base.npc.lifeMax / 2)
				{
					this.Unveiled = true;
					base.npc.netUpdate = true;
				}
				if (*this.AITimer > 60f)
				{
					base.npc.dontTakeDamage = false;
					this.AttackChoice();
					*this.AITimer = 0f;
					this.AIState = KeeperSpirit.ActionState.Attacks;
					base.npc.netUpdate = true;
					if (Main.netMode == 2 && base.npc.whoAmI < 200)
					{
						NetMessage.SendData(23, -1, -1, null, base.npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
						return;
					}
				}
				break;
			}
			case KeeperSpirit.ActionState.Attacks:
				if (!this.Unveiled && base.npc.life < base.npc.lifeMax / 2)
				{
					this.Unveiled = true;
					base.npc.netUpdate = true;
					return;
				}
				switch (this.ID)
				{
				case 0:
				{
					int alphaTimer = Main.expertMode ? 20 : 10;
					*this.AITimer += 1f;
					if (*this.AITimer < 100f)
					{
						if (*this.AITimer < 40f)
						{
							base.npc.LookAtPlayer();
							base.npc.velocity *= 0.9f;
						}
						if (*this.AITimer == 40f)
						{
							Main.PlaySound(29, (int)base.npc.position.X, (int)base.npc.position.Y, 83, 1f, 0.3f);
							base.npc.velocity.Y = 0f;
							base.npc.velocity.X = -6f * (float)base.npc.spriteDirection;
						}
						if (*this.AITimer >= 40f)
						{
							base.npc.alpha += alphaTimer;
							base.npc.velocity *= 0.96f;
						}
						if (base.npc.alpha >= 255)
						{
							base.npc.velocity *= 0f;
							base.npc.position = new Vector2(player.Center.X + (float)((player.velocity.X > 0f) ? 200 : -200) + player.velocity.X * 20f, player.Center.Y - 70f);
							*this.AITimer = 100f;
							return;
						}
					}
					else
					{
						if (*this.AITimer == 100f)
						{
							base.npc.velocity.X = 6f * (float)base.npc.spriteDirection;
						}
						if (*this.AITimer >= 100f && *this.AITimer < 200f)
						{
							base.npc.LookAtPlayer();
							base.npc.alpha -= alphaTimer;
							base.npc.velocity *= 0.96f;
						}
						if (base.npc.alpha <= 0 && *this.AITimer < 200f)
						{
							*this.AITimer = 200f;
							base.npc.frameCounter = 0.0;
							base.npc.frame.Y = 0;
						}
						if (*this.AITimer >= 200f && base.npc.frame.Y >= 568 && base.npc.frame.Y <= 852)
						{
							foreach (NPC target in Enumerable.Take<NPC>(Main.npc, 200))
							{
								if (target.active && target.whoAmI != base.npc.whoAmI && target.friendly && target.immune[base.npc.whoAmI] <= 0 && target.Hitbox.Intersects(SlashHitbox))
								{
									target.immune[base.npc.whoAmI] = 30;
									int hitDirection = (base.npc.Center.X > target.Center.X) ? -1 : 1;
									BaseAI.DamageNPC(target, base.npc.damage, 3f, hitDirection, base.npc, true, false);
									target.AddBuff(30, 600, false);
								}
							}
							for (int p = 0; p < 255; p++)
							{
								Player target2 = Main.player[p];
								if (target2.active && !target2.dead && target2.Hitbox.Intersects(SlashHitbox))
								{
									int hitDirection2 = (base.npc.Center.X > target2.Center.X) ? -1 : 1;
									BaseAI.DamagePlayer(target2, base.npc.damage, 3f, hitDirection2, base.npc, true, false, 0, 1f);
									target2.AddBuff(30, 600, true);
								}
							}
						}
						if (*this.AITimer >= 235f)
						{
							base.npc.frameCounter = 0.0;
							base.npc.frame.Y = 0;
							base.npc.velocity *= 0f;
							if (*this.TimerRand >= (float)((Main.expertMode ? 2 : 1) + (this.Unveiled ? 1 : 0)))
							{
								*this.TimerRand = 0f;
								*this.AITimer = 0f;
								this.AIState = KeeperSpirit.ActionState.Idle;
								base.npc.netUpdate = true;
								return;
							}
							*this.TimerRand += 1f;
							*this.AITimer = 30f;
							base.npc.netUpdate = true;
							return;
						}
					}
					break;
				}
				case 1:
				{
					base.npc.LookAtPlayer();
					base.npc.velocity *= 0.96f;
					ref float aitimer2 = ref this.AITimer;
					float num = aitimer2 + 1f;
					aitimer2 = num;
					if (num == 30f)
					{
						base.npc.velocity = RedeHelper.PolarVector(-6f, Utils.ToRotation(player.Center - base.npc.Center));
					}
					if (*this.AITimer == 60f)
					{
						BaseAI.DamageNPC(base.npc, 50, 0f, player, false, true);
						for (int i = 0; i < 6; i++)
						{
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KeeperBloodWave>(), base.npc.damage, RedeHelper.PolarVector(Utils.NextFloat(Main.rand, 8f, 16f), Utils.ToRotation(player.Center - base.npc.Center) + Utils.NextFloat(Main.rand, -0.3f, 0.3f)), false, SoundID.NPCDeath19, "", (float)base.npc.whoAmI, 0f);
						}
						for (int j = 0; j < 30; j++)
						{
							int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, 0f, 0f, 0, default(Color), 3f);
							Main.dust[dustIndex].velocity *= 5f;
						}
					}
					if (*this.AITimer >= 90f)
					{
						*this.TimerRand = 0f;
						*this.AITimer = 0f;
						this.AIState = KeeperSpirit.ActionState.Idle;
						base.npc.netUpdate = true;
						return;
					}
					break;
				}
				case 2:
				{
					base.npc.LookAtPlayer();
					ref float aitimer3 = ref this.AITimer;
					float num = aitimer3;
					aitimer3 = num + 1f;
					if (num == 0f)
					{
						this.speed = 6f;
					}
					base.npc.Move(new Vector2(this.move, player.Center.Y - 50f), this.speed, 20f, false);
					this.MoveClamp();
					if (base.npc.DistanceSQ(player.Center) > 160000f)
					{
						this.speed *= 1.03f;
					}
					else if (base.npc.velocity.Length() > 6f && base.npc.DistanceSQ(player.Center) <= 160000f)
					{
						this.speed *= 0.96f;
					}
					if (*this.AITimer >= 60f && *this.AITimer % (float)(this.Unveiled ? 20 : 25) == 0f)
					{
						Vector2 pos = base.npc.Center + Utils.RotatedBy(Vector2.One, (double)MathHelper.ToRadians(*this.TimerRand), default(Vector2)) * 60f;
						base.npc.Shoot(pos, ModContent.ProjectileType<ShadowBolt>(), base.npc.damage, RedeHelper.PolarVector((float)(Main.expertMode ? 4 : 3), Utils.ToRotation(player.Center - base.npc.Center)), false, SoundID.Item20, "", 0f, 0f);
						*this.TimerRand += 45f;
					}
					if (*this.TimerRand >= 360f)
					{
						*this.TimerRand = 0f;
						*this.AITimer = 0f;
						this.AIState = KeeperSpirit.ActionState.Idle;
						base.npc.netUpdate = true;
						return;
					}
					break;
				}
				case 3:
					*this.AITimer += 1f;
					if (*this.AITimer < 100f)
					{
						if (*this.AITimer == 5f)
						{
							base.npc.LookAtPlayer();
							Main.PlaySound(29, (int)base.npc.position.X, (int)base.npc.position.Y, 83, 1f, 0.3f);
							base.npc.velocity.Y = 0f;
							base.npc.velocity.X = -6f * (float)base.npc.spriteDirection;
						}
						if (*this.AITimer >= 5f)
						{
							base.npc.alpha += 20;
							base.npc.velocity *= 0.9f;
						}
						if (base.npc.alpha >= 255)
						{
							base.npc.velocity *= 0f;
							base.npc.position = new Vector2(Utils.NextBool(Main.rand, 2) ? (player.Center.X - 180f) : (player.Center.X + 180f), player.Center.Y - 70f);
							*this.AITimer = 100f;
						}
					}
					else
					{
						if (*this.AITimer == 100f)
						{
							base.npc.velocity.X = 6f * (float)base.npc.spriteDirection;
						}
						if (*this.AITimer >= 100f && *this.AITimer < 200f)
						{
							base.npc.LookAtPlayer();
							base.npc.alpha -= 20;
							base.npc.velocity *= 0.9f;
						}
						if (base.npc.alpha <= 0 && *this.AITimer < 200f)
						{
							*this.AITimer = 200f;
						}
						if (*this.AITimer < (float)(this.Unveiled ? 260 : 280))
						{
							base.npc.LookAtPlayer();
							base.npc.MoveToVector2(new Vector2(player.Center.X - (float)(160 * base.npc.spriteDirection), player.Center.Y - 70f), 4f);
							for (int k = 0; k < 2; k++)
							{
								Dust dust2 = Dust.NewDustDirect(base.npc.position, base.npc.width, base.npc.height, 180, 1f, 0f, 0, default(Color), 1f);
								dust2.velocity = -base.npc.DirectionTo(dust2.position);
								dust2.noGravity = true;
							}
							this.origin = player.Center;
						}
						if (*this.AITimer >= (float)(this.Unveiled ? 260 : 280) && *this.AITimer < 320f)
						{
							base.npc.velocity.Y = 0f;
							base.npc.velocity.X = -0.1f * (float)base.npc.spriteDirection;
							player.GetModPlayer<ScreenPlayer>().ScreenShakeIntensity = 3f;
							if (*this.AITimer % 2f == 0f)
							{
								base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KeeperSoulCharge>(), (int)((float)base.npc.damage * 1.4f), RedeHelper.PolarVector(Utils.NextFloat(Main.rand, 14f, 16f), Utils.ToRotation(this.origin - base.npc.Center)), false, SoundID.NPCDeath52.WithVolume(0.5f), "", 0f, 0f);
							}
						}
						if (*this.AITimer >= 320f)
						{
							base.npc.velocity *= 0.98f;
						}
					}
					if (*this.AITimer >= 360f)
					{
						*this.TimerRand = 0f;
						*this.AITimer = 0f;
						this.AIState = KeeperSpirit.ActionState.Idle;
						base.npc.netUpdate = true;
						return;
					}
					break;
				case 4:
					if (this.Unveiled)
					{
						base.npc.LookAtPlayer();
						ref float aitimer4 = ref this.AITimer;
						float num = aitimer4;
						aitimer4 = num + 1f;
						if (num == 0f)
						{
							this.speed = 6f;
						}
						base.npc.Move(new Vector2(this.move, player.Center.Y - 50f), this.speed, 20f, false);
						this.MoveClamp();
						if (base.npc.DistanceSQ(player.Center) > 160000f)
						{
							this.speed *= 1.03f;
						}
						else if (base.npc.velocity.Length() > 6f && base.npc.DistanceSQ(player.Center) <= 160000f)
						{
							this.speed *= 0.96f;
						}
						if (*this.AITimer >= 30f && *this.AITimer % 30f == 0f)
						{
							base.npc.Shoot(new Vector2(base.npc.Center.X + (float)(3 * base.npc.spriteDirection), base.npc.Center.Y - 37f), ModContent.ProjectileType<KeeperDreadCoil>(), base.npc.damage, RedeHelper.PolarVector(7f, Utils.ToRotation(player.Center - base.npc.Center) + Utils.NextFloat(Main.rand, -0.08f, 0.08f)), false, SoundID.Item20, "", 0f, 0f);
						}
						if (*this.AITimer >= 130f)
						{
							*this.TimerRand = 0f;
							*this.AITimer = 0f;
							this.AIState = KeeperSpirit.ActionState.Idle;
							base.npc.netUpdate = true;
							return;
						}
					}
					else
					{
						*this.TimerRand = 0f;
						*this.AITimer = 60f;
						this.AIState = KeeperSpirit.ActionState.Idle;
						base.npc.netUpdate = true;
					}
					break;
				default:
					return;
				}
				break;
			default:
				return;
			}
		}

		public void MoveClamp()
		{
			Player player = Main.player[base.npc.target];
			int xFar = 240;
			if (base.npc.Center.X < player.Center.X)
			{
				if (this.move < player.Center.X - (float)xFar)
				{
					this.move = player.Center.X - (float)xFar;
					return;
				}
				if (this.move > player.Center.X - 120f)
				{
					this.move = player.Center.X - 120f;
					return;
				}
			}
			else
			{
				if (this.move > player.Center.X + (float)xFar)
				{
					this.move = player.Center.X + (float)xFar;
					return;
				}
				if (this.move < player.Center.X + 120f)
				{
					this.move = player.Center.X + 120f;
				}
			}
		}

		public unsafe override void FindFrame(int frameHeight)
		{
			if (Main.netMode != 2)
			{
				Player player = Main.player[base.npc.target];
				for (int i = base.npc.oldPos.Length - 1; i > 0; i--)
				{
					this.oldrot[i] = this.oldrot[i - 1];
				}
				this.oldrot[0] = base.npc.rotation;
				base.npc.frame.Width = Main.npcTexture[base.npc.type].Width / 2;
				double num;
				if (object.Equals(KeeperSpirit.ActionState.Attacks, this.AIState) && this.ID == 0 && *this.AITimer >= 200f)
				{
					base.npc.frame.X = base.npc.frame.Width;
					NPC npc = base.npc;
					num = npc.frameCounter + 1.0;
					npc.frameCounter = num;
					if (num >= 5.0)
					{
						base.npc.frameCounter = 0.0;
						NPC npc2 = base.npc;
						npc2.frame.Y = npc2.frame.Y + frameHeight;
						base.npc.velocity *= 0.8f;
						if (base.npc.frame.Y == 4 * frameHeight)
						{
							Main.PlaySound(SoundID.Item71, base.npc.position);
							base.npc.velocity.X = MathHelper.Clamp(Math.Abs((player.Center.X - base.npc.Center.X) / 30f), 30f, 50f) * (float)base.npc.spriteDirection;
						}
						if (base.npc.frame.Y > 7 * frameHeight)
						{
							base.npc.frame.Y = 0;
						}
					}
					return;
				}
				base.npc.frame.X = 0;
				NPC npc3 = base.npc;
				num = npc3.frameCounter + 1.0;
				npc3.frameCounter = num;
				if (num >= 5.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc4 = base.npc;
					npc4.frame.Y = npc4.frame.Y + frameHeight;
					if (base.npc.frame.Y > 5 * frameHeight)
					{
						base.npc.frame.Y = 0;
					}
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			for (int i = 0; i < NPCID.Sets.TrailCacheLength[base.npc.type]; i++)
			{
				Vector2 oldPos = base.npc.oldPos[i];
				Main.spriteBatch.Draw(Main.npcTexture[base.npc.type], oldPos + base.npc.Size / 2f - Main.screenPosition + new Vector2(0f, base.npc.gfxOffY), new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.LightSkyBlue) * 0.5f, this.oldrot[i], Utils.Size(base.npc.frame) / 2f, base.npc.scale + 0.1f, effects, 0f);
			}
			spriteBatch.Draw(Main.npcTexture[base.npc.type], base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White) * 0.5f, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, null);
			return false;
		}

		private void DespawnHandler()
		{
			Player player = Main.player[base.npc.target];
			if (!player.active || player.dead)
			{
				base.npc.TargetClosest(false);
				player = Main.player[base.npc.target];
				if (!player.active || player.dead)
				{
					base.npc.alpha += 2;
					if (base.npc.alpha >= 255)
					{
						base.npc.active = false;
					}
					if (base.npc.timeLeft > 10)
					{
						base.npc.timeLeft = 10;
					}
					return;
				}
			}
		}

		public float[] oldrot = new float[5];

		public List<int> AttackList = new List<int>
		{
			0,
			1,
			2,
			3,
			4
		};

		public List<int> CopyList;

		private bool Unveiled;

		private float move;

		private float speed = 6f;

		private Vector2 origin;

		public enum ActionState
		{
			Begin,
			Idle,
			Attacks
		}
	}
}
