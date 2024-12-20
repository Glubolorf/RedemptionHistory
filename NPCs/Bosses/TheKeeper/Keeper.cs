using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Dusts;
using Redemption.Items.Accessories.PreHM;
using Redemption.Items.Armor.Vanity;
using Redemption.Items.Materials.PreHM;
using Redemption.Items.Placeable.Trophies;
using Redemption.Items.Usable;
using Redemption.Items.Weapons.PreHM.Magic;
using Redemption.Items.Weapons.PreHM.Melee;
using Redemption.Items.Weapons.PreHM.Ranged;
using Redemption.Items.Weapons.PreHM.Summon;
using Redemption.NPCs.Minibosses.SkullDigger;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.TheKeeper
{
	[AutoloadBossHead]
	public class Keeper : ModNPC
	{
		public Keeper.ActionState AIState
		{
			get
			{
				return (Keeper.ActionState)base.npc.ai[0];
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
			base.DisplayName.SetDefault("The Keeper");
			Main.npcFrameCount[base.npc.type] = 9;
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
			base.npc.HitSound = SoundID.NPCHit13;
			base.npc.DeathSound = SoundID.NPCDeath19;
			this.bossBag = ModContent.ItemType<TheKeeperBag>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 100; i++)
				{
					int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, 0f, 0f, 0, default(Color), 3f);
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
			if (!RedeWorld.downedKeeper)
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
								Main.NewText("<Chalice of Alignment> An undead... disgusting. Good thing you killed it.", Color.DarkGoldenrod, false);
							}
						}
						CombatText.NewText(player2.getRect(), Color.Gold, "+1", true, false);
					}
				}
			}
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
			Player player = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || player.dead || !player.active)
			{
				base.npc.TargetClosest(true);
			}
			Rectangle SlashHitbox = new Rectangle((int)((base.npc.spriteDirection == -1) ? (base.npc.Center.X - 64f) : (base.npc.Center.X + 26f)), (int)(base.npc.Center.Y - 38f), 38, 86);
			this.SoulCharging = false;
			bool teddy = false;
			for (int i = 0; i < 255; i++)
			{
				Player player2 = Main.player[i];
				if (player2.active && !player2.dead && player2.HasItem(ModContent.ItemType<AbandonedTeddy>()))
				{
					teddy = true;
				}
			}
			this.DespawnHandler();
			if (this.AIState != Keeper.ActionState.Death && this.AIState != Keeper.ActionState.Unveiled && this.AIState != Keeper.ActionState.Attacks)
			{
				base.npc.LookAtPlayer();
			}
			switch (this.AIState)
			{
			case Keeper.ActionState.Begin:
			{
				ref float aitimer = ref this.AITimer;
				float num = aitimer;
				aitimer = num + 1f;
				if (num == 0f && !Main.dedServ)
				{
					Redemption.Inst.TitleCardUIElement.DisplayTitle("The Keeper", 60, 90, 0.8f, 0, new Color?(Color.MediumPurple), "Octavia von Gailon", true);
				}
				base.npc.alpha -= 2;
				if (base.npc.alpha <= 0)
				{
					if (teddy && !RedeConfigClient.Instance.NoLoreElements)
					{
						this.AIState = Keeper.ActionState.Teddy;
					}
					else
					{
						this.AIState = Keeper.ActionState.Idle;
					}
					*this.AITimer = 0f;
					base.npc.netUpdate = true;
					return;
				}
				break;
			}
			case Keeper.ActionState.Idle:
			{
				ref float aitimer2 = ref this.AITimer;
				float num = aitimer2;
				aitimer2 = num + 1f;
				if (num == 0f)
				{
					this.move = base.npc.Center.X;
					this.speed = 6f;
				}
				this.Reap = false;
				base.npc.Move(new Vector2(this.move, player.Center.Y - 50f), this.speed, 20f, false);
				this.MoveClamp();
				if (base.npc.DistanceSQ(player.Center) > (float)(base.npc.dontTakeDamage ? 640000 : 160000))
				{
					this.speed *= 1.03f;
				}
				else if (base.npc.dontTakeDamage && base.npc.velocity.Length() > 6f && base.npc.DistanceSQ(player.Center) <= 640000f)
				{
					this.speed *= 0.96f;
				}
				if (!this.Unveiled && base.npc.life < base.npc.lifeMax / 2)
				{
					base.npc.velocity *= 0f;
					*this.AITimer = 0f;
					this.AIState = Keeper.ActionState.Unveiled;
					base.npc.netUpdate = true;
					return;
				}
				if (base.npc.dontTakeDamage ? (*this.AITimer == -1f) : (*this.AITimer > 60f))
				{
					base.npc.dontTakeDamage = false;
					this.AttackChoice();
					*this.AITimer = 0f;
					this.AIState = Keeper.ActionState.Attacks;
					base.npc.netUpdate = true;
					if (Main.netMode == 2 && base.npc.whoAmI < 200)
					{
						NetMessage.SendData(23, -1, -1, null, base.npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
						return;
					}
				}
				break;
			}
			case Keeper.ActionState.Attacks:
				if (!this.Unveiled && base.npc.life < base.npc.lifeMax / 2)
				{
					*this.AITimer = 0f;
					*this.TimerRand = 0f;
					base.npc.velocity *= 0f;
					this.AIState = Keeper.ActionState.Unveiled;
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
							this.Reap = true;
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
							base.npc.velocity *= 0f;
							if (*this.TimerRand >= (float)((Main.expertMode ? 2 : 1) + (this.Unveiled ? 1 : 0)))
							{
								this.Reap = false;
								*this.TimerRand = 0f;
								*this.AITimer = 0f;
								this.AIState = Keeper.ActionState.Idle;
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
					ref float aitimer3 = ref this.AITimer;
					float num = aitimer3 + 1f;
					aitimer3 = num;
					if (num == 30f)
					{
						base.npc.velocity = RedeHelper.PolarVector(-6f, Utils.ToRotation(player.Center - base.npc.Center));
					}
					if (*this.AITimer == 60f)
					{
						BaseAI.DamageNPC(base.npc, 50, 0f, player, false, true);
						for (int j = 0; j < 6; j++)
						{
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KeeperBloodWave>(), base.npc.damage, RedeHelper.PolarVector(Utils.NextFloat(Main.rand, 8f, 16f), Utils.ToRotation(player.Center - base.npc.Center) + Utils.NextFloat(Main.rand, -0.3f, 0.3f)), false, SoundID.NPCDeath19, "", (float)base.npc.whoAmI, 0f);
						}
						for (int k = 0; k < 30; k++)
						{
							int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, 0f, 0f, 0, default(Color), 3f);
							Main.dust[dustIndex].velocity *= 5f;
						}
					}
					if (*this.AITimer >= 90f)
					{
						*this.TimerRand = 0f;
						*this.AITimer = 0f;
						this.AIState = Keeper.ActionState.Idle;
						base.npc.netUpdate = true;
						return;
					}
					break;
				}
				case 2:
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
						this.AIState = Keeper.ActionState.Idle;
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
							for (int l = 0; l < 2; l++)
							{
								Dust dust2 = Dust.NewDustDirect(base.npc.position, base.npc.width, base.npc.height, 180, 1f, 0f, 0, default(Color), 1f);
								dust2.velocity = -base.npc.DirectionTo(dust2.position);
								dust2.noGravity = true;
							}
							this.origin = player.Center;
						}
						if (*this.AITimer >= (float)(this.Unveiled ? 260 : 280) && *this.AITimer < 320f)
						{
							this.SoulCharging = true;
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
						this.AIState = Keeper.ActionState.Idle;
						base.npc.netUpdate = true;
						return;
					}
					break;
				case 4:
				{
					if (!this.Unveiled)
					{
						*this.TimerRand = 0f;
						*this.AITimer = 60f;
						this.AIState = Keeper.ActionState.Idle;
						base.npc.netUpdate = true;
						return;
					}
					base.npc.LookAtPlayer();
					ref float aitimer5 = ref this.AITimer;
					float num = aitimer5;
					aitimer5 = num + 1f;
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
						this.AIState = Keeper.ActionState.Idle;
						base.npc.netUpdate = true;
						return;
					}
					break;
				}
				case 5:
					break;
				default:
					return;
				}
				break;
			case Keeper.ActionState.Unveiled:
			{
				base.npc.alpha = 0;
				player.GetModPlayer<ScreenPlayer>().ScreenFocusPosition = base.npc.Center;
				player.GetModPlayer<ScreenPlayer>().lockScreen = true;
				player.GetModPlayer<ScreenPlayer>().ScreenShakeIntensity = 3f;
				this.Unveiled = true;
				this.Reap = false;
				ref float aitimer6 = ref this.AITimer;
				float num = aitimer6;
				aitimer6 = num + 1f;
				if (num == 1f)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Shriek").WithVolume(0.5f).WithPitchVariance(0.1f), -1, -1);
					}
					base.npc.Shoot(new Vector2(base.npc.Center.X + (float)(3 * base.npc.spriteDirection), base.npc.Center.Y - 37f), ModContent.ProjectileType<VeilFX>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", 0f, 0f);
					base.npc.dontTakeDamage = true;
					if (Main.netMode == 2 && base.npc.whoAmI < 200)
					{
						NetMessage.SendData(23, -1, -1, null, base.npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (*this.AITimer >= 220f)
				{
					*this.AITimer = 0f;
					base.npc.dontTakeDamage = false;
					this.AIState = Keeper.ActionState.Idle;
					base.npc.netUpdate = true;
					if (Main.netMode == 2 && base.npc.whoAmI < 200)
					{
						NetMessage.SendData(23, -1, -1, null, base.npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
						return;
					}
				}
				break;
			}
			case Keeper.ActionState.Death:
			{
				if (!NPC.AnyNPCs(ModContent.NPCType<SkullDigger>()))
				{
					player.GetModPlayer<ScreenPlayer>().ScreenFocusPosition = base.npc.Center;
					player.GetModPlayer<ScreenPlayer>().lockScreen = true;
				}
				player.GetModPlayer<ScreenPlayer>().ScreenShakeIntensity = 3f;
				base.npc.velocity *= 0f;
				this.Reap = false;
				ref float aitimer7 = ref this.AITimer;
				float num = aitimer7;
				aitimer7 = num + 1f;
				if (num == 0f)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Shriek").WithVolume(0.4f).WithPitchVariance(0.1f), -1, -1);
					}
					base.npc.alpha = 0;
					base.npc.dontTakeDamage = true;
					if (Main.netMode == 2 && base.npc.whoAmI < 200)
					{
						NetMessage.SendData(23, -1, -1, null, base.npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				base.npc.alpha++;
				if (base.npc.alpha > 150)
				{
					for (int m = 0; m < 5; m++)
					{
						int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, 0f, 0f, 0, default(Color), 3f);
						Main.dust[dustIndex2].velocity *= 5f;
					}
				}
				else
				{
					for (int n = 0; n < 3; n++)
					{
						int dustIndex3 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, 0f, 0f, 0, default(Color), 3f);
						Main.dust[dustIndex3].velocity *= 2f;
					}
				}
				if (base.npc.alpha >= 255)
				{
					base.npc.dontTakeDamage = false;
					player.ApplyDamageToNPC(base.npc, 9999, 0f, 0, false);
					if (Main.netMode == 2 && base.npc.whoAmI < 200)
					{
						NetMessage.SendData(23, -1, -1, null, base.npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				break;
			}
			case Keeper.ActionState.Teddy:
			{
				player.GetModPlayer<ScreenPlayer>().ScreenFocusPosition = base.npc.Center;
				player.GetModPlayer<ScreenPlayer>().lockScreen = true;
				this.Unveiled = true;
				if (!Main.dedServ)
				{
					this.music = base.mod.GetSoundSlot(51, "Sounds/Music/silence");
				}
				ref float aitimer8 = ref this.AITimer;
				float num = aitimer8;
				aitimer8 = num + 1f;
				if (num == 0f)
				{
					base.npc.alpha = 0;
					base.npc.Shoot(new Vector2(base.npc.Center.X + (float)(3 * base.npc.spriteDirection), base.npc.Center.Y - 37f), ModContent.ProjectileType<VeilFX>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", 0f, 0f);
					base.npc.dontTakeDamage = true;
					if (Main.netMode == 2 && base.npc.whoAmI < 200)
					{
						NetMessage.SendData(23, -1, -1, null, base.npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (*this.AITimer == 60f)
				{
					string text = "The Keeper noticed the abandoned teddy you're holding...";
					Color rarityPurple = Colors.RarityPurple;
					byte r = rarityPurple.R;
					rarityPurple = Colors.RarityPurple;
					byte g = rarityPurple.G;
					rarityPurple = Colors.RarityPurple;
					Main.NewText(text, r, g, rarityPurple.B, false);
				}
				if (*this.AITimer == 120f)
				{
					*this.TimerRand = 1f;
				}
				if (*this.AITimer == 320f)
				{
					string text2 = "She starts to remember something...";
					Color rarityPurple = Colors.RarityPurple;
					byte r2 = rarityPurple.R;
					rarityPurple = Colors.RarityPurple;
					byte g2 = rarityPurple.G;
					rarityPurple = Colors.RarityPurple;
					Main.NewText(text2, r2, g2, rarityPurple.B, false);
				}
				if (*this.AITimer == 400f)
				{
					base.npc.frame.Y = 0;
					base.npc.frameCounter = 0.0;
					*this.TimerRand = 2f;
				}
				if (*this.AITimer == 540f)
				{
					string text3 = "Pain, Anger, Sadness. All those feelings were washed away...";
					Color rarityPurple = Colors.RarityPurple;
					byte r3 = rarityPurple.R;
					rarityPurple = Colors.RarityPurple;
					byte g3 = rarityPurple.G;
					rarityPurple = Colors.RarityPurple;
					Main.NewText(text3, r3, g3, rarityPurple.B, false);
				}
				if (*this.AITimer == 750f)
				{
					string text4 = "She only feels... at peace...";
					Color rarityPurple = Colors.RarityPurple;
					byte r4 = rarityPurple.R;
					rarityPurple = Colors.RarityPurple;
					byte g4 = rarityPurple.G;
					rarityPurple = Colors.RarityPurple;
					Main.NewText(text4, r4, g4, rarityPurple.B, false);
				}
				if (*this.AITimer == 840f)
				{
					base.npc.frame.Y = 0;
					base.npc.frameCounter = 0.0;
					base.npc.frame.X = 0;
					*this.TimerRand = 3f;
				}
				if (*this.AITimer >= 840f)
				{
					base.npc.alpha++;
					if (Utils.NextBool(Main.rand, 5))
					{
						Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 20, 0f, 0f, 0, default(Color), 1f);
					}
				}
				if (*this.AITimer == 900f)
				{
					CombatText.NewText(base.npc.getRect(), Color.GhostWhite, "Thank...", true, false);
				}
				if (*this.AITimer == 960f)
				{
					CombatText.NewText(base.npc.getRect(), Color.GhostWhite, "You...", true, false);
				}
				if (*this.AITimer >= 960f)
				{
					for (int k2 = 0; k2 < 1; k2++)
					{
						double angle = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
						Vector2 vector;
						vector.X = (float)(Math.Sin(angle) * 100.0);
						vector.Y = (float)(Math.Cos(angle) * 100.0);
						Dust dust3 = Main.dust[Dust.NewDust(base.npc.Center + vector, 2, 2, ModContent.DustType<VoidFlame>(), 0f, 0f, 100, default(Color), 3f)];
						dust3.noGravity = true;
						dust3.velocity = -base.npc.DirectionTo(dust3.position) * 10f;
					}
				}
				if (base.npc.alpha >= 255)
				{
					for (int i2 = 0; i2 < 50; i2++)
					{
						int dustIndex4 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 20, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[dustIndex4].velocity *= 2.6f;
						int dustIndex5 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, ModContent.DustType<VoidFlame>(), 0f, 0f, 100, default(Color), 3f);
						Main.dust[dustIndex5].velocity *= 2.6f;
					}
					string text5 = "The Keeper's Spirit fades away... ?";
					Color rarityPurple = Colors.RarityPurple;
					byte r5 = rarityPurple.R;
					rarityPurple = Colors.RarityPurple;
					byte g5 = rarityPurple.G;
					rarityPurple = Colors.RarityPurple;
					Main.NewText(text5, r5, g5, rarityPurple.B, false);
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<KeeperAcc>(), 1, false, 0, false, false);
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<TheKeeperTrophy>(), 1, false, 0, false, false);
					base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<KeeperSoul>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", 0f, 0f);
					if (!RedeWorld.keeperSaved)
					{
						RedeWorld.redemptionPoints += 2;
						for (int p2 = 0; p2 < 255; p2++)
						{
							Player player3 = Main.player[p2];
							if (player3.active)
							{
								CombatText.NewText(player3.getRect(), Color.Gold, "+2", true, false);
								if (player3.HasItem(ModContent.ItemType<RedemptionTeller>()))
								{
									Main.NewText("<Chalice of Alignment> You've redeemed yourself, Octavia may rest in undisturbed peace... Hm, Strange, something feels off... I'm sure it's nothing.", Color.DarkGoldenrod, false);
								}
							}
						}
					}
					base.npc.netUpdate = true;
					RedeWorld.keeperSaved = true;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
					base.npc.active = false;
					return;
				}
				break;
			}
			default:
				return;
			}
		}

		public void MoveClamp()
		{
			Player player = Main.player[base.npc.target];
			int xFar = 240;
			if (base.npc.dontTakeDamage)
			{
				xFar = 600;
			}
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

		public override bool CheckActive()
		{
			return this.AIState != Keeper.ActionState.Death;
		}

		public unsafe override bool CheckDead()
		{
			if (object.Equals(Keeper.ActionState.Death, this.AIState) && *this.AITimer > 0f)
			{
				return true;
			}
			base.npc.dontTakeDamage = true;
			Main.PlaySound(SoundID.NPCDeath19, base.npc.position);
			base.npc.velocity *= 0f;
			base.npc.alpha = 0;
			base.npc.life = 1;
			*this.AITimer = 0f;
			this.AIState = Keeper.ActionState.Death;
			if (Main.netMode == 2 && base.npc.whoAmI < 200)
			{
				NetMessage.SendData(23, -1, -1, null, base.npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
			}
			return false;
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
				int num = this.VeilCounter + 1;
				this.VeilCounter = num;
				if (num >= 5)
				{
					this.VeilCounter = 0;
					this.VeilFrameY++;
					if (this.VeilFrameY > 5)
					{
						this.VeilFrameY = 0;
					}
				}
				base.npc.frame.Width = Main.npcTexture[base.npc.type].Width / 4;
				double num2;
				if (object.Equals(Keeper.ActionState.Teddy, this.AIState))
				{
					if (*this.TimerRand < 3f)
					{
						base.npc.frame.X = ((*this.TimerRand == 2f) ? 3 : 2) * base.npc.frame.Width;
					}
					NPC npc = base.npc;
					num2 = npc.frameCounter + 1.0;
					npc.frameCounter = num2;
					if (num2 >= 5.0)
					{
						base.npc.frameCounter = 0.0;
						NPC npc2 = base.npc;
						npc2.frame.Y = npc2.frame.Y + frameHeight;
						float obj = *this.TimerRand;
						if (!0f.Equals(obj))
						{
							if (!1f.Equals(obj))
							{
								if (!2f.Equals(obj))
								{
									if (!3f.Equals(obj))
									{
										return;
									}
									if (base.npc.frame.Y > 9 * frameHeight)
									{
										base.npc.frame.Y = 8 * frameHeight;
									}
								}
								else if (base.npc.frame.Y > 5 * frameHeight)
								{
									base.npc.frame.Y = 3 * frameHeight;
									return;
								}
							}
							else if (base.npc.frame.Y > 8 * frameHeight)
							{
								base.npc.frame.Y = 6 * frameHeight;
								return;
							}
						}
						else if (base.npc.frame.Y > 2 * frameHeight)
						{
							base.npc.frame.Y = 0;
							return;
						}
					}
					return;
				}
				if (object.Equals(Keeper.ActionState.Attacks, this.AIState) && this.ID == 0 && *this.AITimer >= 200f)
				{
					base.npc.frame.X = base.npc.frame.Width;
					NPC npc3 = base.npc;
					num2 = npc3.frameCounter + 1.0;
					npc3.frameCounter = num2;
					if (num2 >= 5.0)
					{
						base.npc.frameCounter = 0.0;
						NPC npc4 = base.npc;
						npc4.frame.Y = npc4.frame.Y + frameHeight;
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
				if (this.AIState == Keeper.ActionState.Unveiled || this.AIState == Keeper.ActionState.Death || this.SoulCharging)
				{
					if (base.npc.frame.Y < 6 * frameHeight)
					{
						base.npc.frame.Y = 6 * frameHeight;
					}
					NPC npc5 = base.npc;
					num2 = npc5.frameCounter + 1.0;
					npc5.frameCounter = num2;
					if (num2 >= 10.0)
					{
						base.npc.frameCounter = 0.0;
						NPC npc6 = base.npc;
						npc6.frame.Y = npc6.frame.Y + frameHeight;
						if (base.npc.frame.Y > 8 * frameHeight)
						{
							base.npc.frame.Y = 7 * frameHeight;
						}
					}
					return;
				}
				NPC npc7 = base.npc;
				num2 = npc7.frameCounter + 1.0;
				npc7.frameCounter = num2;
				if (num2 >= 5.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc8 = base.npc;
					npc8.frame.Y = npc8.frame.Y + frameHeight;
					if (base.npc.frame.Y > 5 * frameHeight)
					{
						base.npc.frame.Y = 0;
					}
				}
			}
		}

		public unsafe override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D glow = base.mod.GetTexture("NPCs/Bosses/TheKeeper/Keeper_Glow");
			Texture2D veilTex = base.mod.GetTexture("NPCs/Bosses/TheKeeper/VeilFX");
			Texture2D closureTex = base.mod.GetTexture("NPCs/Bosses/TheKeeper/Keeper_Closure");
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			Color angryColor = BaseUtility.MultiLerpColor((float)(Main.LocalPlayer.miscCounter % 100) / 100f, new Color[]
			{
				Color.DarkSlateBlue,
				Color.DarkRed * 0.7f,
				Color.DarkSlateBlue
			});
			if (this.AIState != Keeper.ActionState.Teddy)
			{
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
				for (int i = 0; i < NPCID.Sets.TrailCacheLength[base.npc.type]; i++)
				{
					Vector2 oldPos = base.npc.oldPos[i];
					Main.spriteBatch.Draw(Main.npcTexture[base.npc.type], oldPos + base.npc.Size / 2f - Main.screenPosition + new Vector2(0f, base.npc.gfxOffY), new Rectangle?(base.npc.frame), base.npc.GetAlpha(this.SoulCharging ? Color.GhostWhite : (this.Unveiled ? angryColor : Color.DarkSlateBlue)) * 0.5f, this.oldrot[i], Utils.Size(base.npc.frame) / 2f, base.npc.scale + 0.1f, effects, 0f);
				}
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			}
			int reapShader = GameShaders.Armor.GetShaderIdFromItemId(3530);
			if (this.Reap)
			{
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
				GameShaders.Armor.ApplySecondary(reapShader, Main.player[Main.myPlayer], null);
			}
			if (object.Equals(Keeper.ActionState.Teddy, this.AIState) && *this.TimerRand == 3f)
			{
				spriteBatch.Draw(closureTex, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			}
			else
			{
				spriteBatch.Draw(Main.npcTexture[base.npc.type], base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(drawColor), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
				spriteBatch.Draw(glow, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			}
			int height = veilTex.Height / 6;
			int y = height * this.VeilFrameY;
			Rectangle rect = new Rectangle(0, y, veilTex.Width, height);
			Vector2 origin = new Vector2((float)veilTex.Width / 2f, (float)height / 2f);
			Vector2 VeilPos = new Vector2(base.npc.Center.X + (float)(3 * base.npc.spriteDirection), base.npc.Center.Y - 37f);
			if (!this.Unveiled && base.npc.life > base.npc.lifeMax / 2)
			{
				Main.spriteBatch.Draw(veilTex, VeilPos - Main.screenPosition, new Rectangle?(rect), base.npc.GetAlpha(drawColor), base.npc.rotation, origin, base.npc.scale, effects, 0f);
			}
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

		private bool SoulCharging;

		private bool Reap;

		private Vector2 origin;

		private int VeilFrameY;

		private int VeilCounter;

		public enum ActionState
		{
			Begin,
			Idle,
			Attacks,
			Unveiled,
			Death,
			Teddy
		}
	}
}
