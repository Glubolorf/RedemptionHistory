using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs;
using Redemption.Dusts;
using Redemption.Items;
using Redemption.Items.Accessories.PostML;
using Redemption.Items.Armor.Vanity;
using Redemption.Items.Placeable.Trophies;
using Redemption.Items.Usable;
using Redemption.Items.Weapons.PostML.Druid.Seedbags;
using Redemption.Items.Weapons.PostML.Magic;
using Redemption.Items.Weapons.PostML.Melee;
using Redemption.Items.Weapons.PostML.Ranged;
using Redemption.Items.Weapons.PostML.Summon;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Neb.Phase2
{
	[AutoloadBossHead]
	public class NebP2 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nebuleus, Angel of the Cosmos");
			Main.npcFrameCount[base.npc.type] = 9;
		}

		public override void SetDefaults()
		{
			base.npc.lifeMax = 950000;
			base.npc.defense = 170;
			base.npc.damage = 250;
			base.npc.width = 90;
			base.npc.height = 108;
			base.npc.aiStyle = -1;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.knockBackResist = 0f;
			base.npc.noGravity = true;
			base.npc.boss = true;
			base.npc.netAlways = true;
			base.npc.noTileCollide = true;
			base.npc.dontTakeDamage = true;
			this.bossBag = ModContent.ItemType<NebBag>();
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return base.npc.ai[3] == 6f;
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override void NPCLoot()
		{
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<ThankYouLetter>(), 1, false, 0, false, false);
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<NebuleusTrophy>(), 1, false, 0, false, false);
			}
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
				return;
			}
			if (Main.rand.Next(7) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<NebuleusMask>(), 1, false, 0, false, false);
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<NebuleusVanity>(), 1, false, 0, false, false);
			}
			if (Main.rand.Next(4) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<GildedBonnet>(), 1, false, 0, false, false);
			}
			if (Main.rand.Next(2) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<FreedomStarN>(), 1, false, 0, false, false);
			}
			if (Main.rand.Next(2) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<NebulaStarFlail>(), 1, false, 0, false, false);
			}
			if (Main.rand.Next(2) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<ConstellationsBook>(), 1, false, 0, false, false);
			}
			if (Main.rand.Next(2) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<StarfruitSeedbag>(), 1, false, 0, false, false);
			}
			if (Main.rand.Next(2) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<CosmosChainWeapon>(), 1, false, 0, false, false);
			}
			if (Main.rand.Next(2) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<PiercingNebulaWeapon>(), 1, false, 0, false, false);
			}
			if (Main.rand.Next(2) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<StarSerpentsCollar>(), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<GalaxyHeart>(), 1, false, 0, false, false);
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				this.RazzleDazzle();
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 58, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			Main.player[base.npc.target].GetModPlayer<ScreenPlayer>().lockScreen = false;
			potionType = 3544;
			if (!RedeWorld.downedNebuleus)
			{
				RedeWorld.redemptionPoints -= 4;
				for (int i = 0; i < 255; i++)
				{
					Player player2 = Main.player[i];
					if (player2.active)
					{
						for (int j = 0; j < player2.inventory.Length; j++)
						{
							if (player2.inventory[j].type == ModContent.ItemType<RedemptionTeller>())
							{
								Main.NewText("<Chalice of Alignment> Trust me when I say this, you've dug yourself a 1000-feet hole here.", Color.DarkGoldenrod, false);
							}
						}
						CombatText.NewText(player2.getRect(), Color.Red, "-4", true, false);
					}
				}
			}
			RedeWorld.downedNebuleus = true;
			if (RedeWorld.nebDeath < 7)
			{
				RedeWorld.nebDeath = 7;
			}
			if (Main.netMode != 1)
			{
				int Proj = Projectile.NewProjectile(base.npc.Center, Vector2.Zero, ModContent.ProjectileType<NebFalling>(), 0, 0f, Main.myPlayer, 0f, 0f);
				Main.npc[Proj].netUpdate = true;
			}
			if (Main.netMode != 0)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			damage *= 0.75;
			return true;
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			if (Main.netMode == 2 || Main.dedServ)
			{
				writer.Write(this.repeat);
				writer.Write(this.phase);
				writer.Write(this.ID);
				writer.Write(this.attackTimer[0]);
				writer.Write(this.attackTimer[1]);
			}
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			base.ReceiveExtraAI(reader);
			if (Main.netMode == 1)
			{
				this.repeat = reader.ReadInt32();
				this.phase = reader.ReadInt32();
				this.ID = reader.ReadInt32();
				this.attackTimer[0] = reader.ReadFloat();
				this.attackTimer[1] = reader.ReadFloat();
			}
		}

		public int ID
		{
			get
			{
				return (int)base.npc.ai[1];
			}
			set
			{
				base.npc.ai[1] = (float)value;
			}
		}

		private void AttackChoice()
		{
			if (this.CopyList == null || this.CopyList.Count == 0)
			{
				this.CopyList = new List<int>(this.AttackList);
			}
			this.ID = this.CopyList[Main.rand.Next(0, this.CopyList.Count)];
			this.CopyList.Remove(this.ID);
			base.npc.netUpdate = true;
		}

		public override void AI()
		{
			Main.time = 16200.0;
			Main.dayTime = false;
			if (!this.title)
			{
				for (int i = 0; i < this.ChainPos.Length; i++)
				{
					this.ChainPos[i] = base.npc.Center;
				}
				this.title = true;
			}
			for (int j = this.oldPos.Length - 1; j > 0; j--)
			{
				this.oldPos[j] = this.oldPos[j - 1];
				this.oldrot[j] = this.oldrot[j - 1];
			}
			this.oldPos[0] = base.npc.Center;
			this.oldrot[0] = base.npc.rotation;
			Player player = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			if (base.npc.ai[3] != 6f)
			{
				if (this.floatTimer == 0)
				{
					NPC npc = base.npc;
					npc.velocity.Y = npc.velocity.Y + 0.007f;
					if (base.npc.velocity.Y > 0.3f)
					{
						this.floatTimer = 1;
						base.npc.netUpdate = true;
					}
				}
				else if (this.floatTimer == 1)
				{
					NPC npc2 = base.npc;
					npc2.velocity.Y = npc2.velocity.Y - 0.007f;
					if (base.npc.velocity.Y < -0.3f)
					{
						this.floatTimer = 0;
						base.npc.netUpdate = true;
					}
				}
				if (this.floatTimer2 == 0)
				{
					NPC npc3 = base.npc;
					npc3.velocity.X = npc3.velocity.X + 0.01f;
					if (base.npc.velocity.X > 0.4f)
					{
						this.floatTimer2 = 1;
						base.npc.netUpdate = true;
					}
				}
				else if (this.floatTimer2 == 1)
				{
					NPC npc4 = base.npc;
					npc4.velocity.X = npc4.velocity.X - 0.01f;
					if (base.npc.velocity.X < -0.4f)
					{
						this.floatTimer2 = 0;
						base.npc.netUpdate = true;
					}
				}
			}
			if (base.npc.ai[3] != 6f)
			{
				base.npc.frameCounter += 1.0;
				if (base.npc.frameCounter >= 5.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc5 = base.npc;
					npc5.frame.Y = npc5.frame.Y + 124;
					if (base.npc.frame.Y > 868)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 0;
					}
				}
			}
			float num = base.npc.ai[3];
			if (!0f.Equals(num))
			{
				if (!1f.Equals(num))
				{
					if (!2f.Equals(num))
					{
						if (!3f.Equals(num))
						{
							if (!4f.Equals(num))
							{
								if (!5f.Equals(num))
								{
									if (!6f.Equals(num))
									{
										if (8f.Equals(num))
										{
											this.frameCounters++;
											if (this.frameCounters >= 5)
											{
												this.armFrames[5]++;
												this.frameCounters = 0;
											}
											if (this.armFrames[5] >= 8)
											{
												base.npc.ai[3] = 0f;
												this.armFrames[5] = 0;
											}
										}
									}
									else
									{
										base.npc.frameCounter = 0.0;
										base.npc.frame.Y = 992;
										base.npc.rotation = Utils.ToRotation(base.npc.velocity) + 1.57f;
										if (base.npc.velocity.X < 0f)
										{
											base.npc.spriteDirection = -1;
										}
										else
										{
											base.npc.spriteDirection = 1;
										}
									}
								}
								else
								{
									this.frameCounters++;
									if (this.frameCounters >= 5)
									{
										this.armFrames[3]++;
										this.frameCounters = 0;
									}
									if (this.armFrames[3] >= 6)
									{
										base.npc.ai[3] = 0f;
										this.armFrames[3] = 0;
									}
								}
							}
							else
							{
								this.frameCounters++;
								if (this.frameCounters >= 5)
								{
									this.armFrames[2]++;
									this.frameCounters = 0;
								}
								if (this.armFrames[2] >= 7)
								{
									base.npc.ai[3] = 0f;
									this.armFrames[2] = 0;
								}
							}
						}
						else
						{
							this.frameCounters++;
							if (this.frameCounters >= 5)
							{
								this.armFrames[2]++;
								this.frameCounters = 0;
							}
							if (this.armFrames[2] >= 4)
							{
								this.armFrames[2] = 2;
							}
						}
					}
					else
					{
						this.frameCounters++;
						if (this.frameCounters >= 5)
						{
							this.armFrames[1]++;
							this.frameCounters = 0;
						}
						if (this.armFrames[1] >= 5)
						{
							base.npc.ai[3] = 0f;
							this.armFrames[1] = 0;
						}
					}
				}
				else
				{
					this.frameCounters++;
					if (this.frameCounters >= 5)
					{
						this.armFrames[1]++;
						this.frameCounters = 0;
					}
					if (this.armFrames[1] >= 4)
					{
						this.armFrames[1] = 2;
					}
				}
			}
			else
			{
				this.frameCounters++;
				if (this.frameCounters >= 5)
				{
					this.armFrames[0]++;
					this.frameCounters = 0;
				}
				if (this.armFrames[0] >= 4)
				{
					this.armFrames[0] = 0;
				}
			}
			if (this.eyeFlare)
			{
				this.eyeFlareTimer += 1f;
				if (this.eyeFlareTimer > 60f)
				{
					this.eyeFlare = false;
					this.eyeFlareTimer = 0f;
				}
			}
			if (this.teleGlow)
			{
				this.teleGlowTimer += 3f;
				if (this.teleGlowTimer > 60f)
				{
					this.teleGlow = false;
					this.teleGlowTimer = 0f;
				}
			}
			this.DespawnHandler();
			if (base.npc.ai[0] > 1f && base.npc.ai[1] != 10f)
			{
				base.npc.dontTakeDamage = false;
			}
			else
			{
				base.npc.dontTakeDamage = true;
			}
			player.GetModPlayer<ScreenPlayer>().ScreenFocusPosition = base.npc.Center;
			switch ((int)base.npc.ai[0])
			{
			case 0:
			{
				base.npc.LookAtPlayer();
				if (!RedeConfigClient.Instance.NoLoreElements && RedeWorld.nebDeath != 5)
				{
					player.GetModPlayer<ScreenPlayer>().lockScreen = true;
				}
				if (base.npc.ai[2] == 1f)
				{
					DustHelper.DrawStar(base.npc.Center, 58, 5f, 4f, 1f, 4f, 2f, 0f, true, 0f, -1f);
					DustHelper.DrawStar(base.npc.Center, 59, 5f, 5f, 1f, 4f, 2f, 0f, true, 0f, -1f);
					DustHelper.DrawStar(base.npc.Center, 60, 5f, 6f, 1f, 4f, 2f, 0f, true, 0f, -1f);
					DustHelper.DrawStar(base.npc.Center, 62, 5f, 7f, 1f, 4f, 2f, 0f, true, 0f, -1f);
					for (int d = 0; d < 32; d++)
					{
						int dustIndex = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, ModContent.DustType<RainbowStarDust>(), 0f, 0f, 0, default(Color), 1f);
						Main.dust[dustIndex].velocity *= 6f;
					}
				}
				float[] ai = base.npc.ai;
				int num2 = 2;
				num = ai[num2] + 1f;
				ai[num2] = num;
				if (num >= 10f)
				{
					base.npc.ai[0] = 1f;
					if (RedeConfigClient.Instance.NoLoreElements && RedeWorld.nebDeath == 5)
					{
						base.npc.ai[2] = 1360f;
					}
					else
					{
						base.npc.ai[2] = 0f;
					}
					base.npc.netUpdate = true;
				}
				break;
			}
			case 1:
				base.npc.LookAtPlayer();
				base.npc.ai[2] += 1f;
				if (RedeWorld.nebDeath == 5)
				{
					if (base.npc.ai[2] < 930f)
					{
						player.GetModPlayer<ScreenPlayer>().lockScreen = true;
					}
					if (!Main.dedServ)
					{
						if (base.npc.ai[2] == 30f)
						{
							Main.PlaySound(12, -1, -1, 1, 1f, 0f);
							Redemption.Inst.DialogueUIElement.DisplayDialogue("I still have this hope in my mind that you're mortal,\nso even if I can't figure out how to kill you...", 300, 1, 0.6f, "Nebuleus:", 1f, new Color?(RedeColor.NebColour), null, null, new Vector2?(base.npc.Center), 0, 0);
						}
						if (base.npc.ai[2] == 330f)
						{
							Main.PlaySound(12, -1, -1, 1, 1f, 0f);
							Redemption.Inst.DialogueUIElement.DisplayDialogue("... There are still many great foes ahead of you!", 200, 1, 0.6f, "Nebuleus:", 1f, new Color?(RedeColor.NebColour), null, null, new Vector2?(base.npc.Center), 0, 0);
						}
						if (base.npc.ai[2] == 530f)
						{
							Main.PlaySound(12, -1, -1, 1, 1f, 0f);
							Redemption.Inst.DialogueUIElement.DisplayDialogue("... Epidotra's Protector...", 100, 1, 0.6f, "Nebuleus:", 1f, new Color?(RedeColor.NebColour), null, null, new Vector2?(base.npc.Center), 0, 0);
						}
						if (base.npc.ai[2] == 630f)
						{
							Main.PlaySound(12, -1, -1, 1, 1f, 0f);
							Redemption.Inst.DialogueUIElement.DisplayDialogue("... The Royal Knight...", 100, 1, 0.6f, "Nebuleus:", 1f, new Color?(RedeColor.NebColour), null, null, new Vector2?(base.npc.Center), 0, 0);
						}
						if (base.npc.ai[2] == 730f)
						{
							Main.PlaySound(12, -1, -1, 1, 1f, 0f);
							Redemption.Inst.DialogueUIElement.DisplayDialogue("... and the Demigod of Light...", 100, 1, 0.6f, "Nebuleus:", 1f, new Color?(RedeColor.NebColour), null, null, new Vector2?(base.npc.Center), 0, 0);
						}
						if (base.npc.ai[2] == 830f)
						{
							Main.PlaySound(12, -1, -1, 1, 1f, 0f);
							Redemption.Inst.DialogueUIElement.DisplayDialogue("But enough talk, I'm your opponent!", 200, 1, 0.6f, "Nebuleus:", 1.5f, new Color?(RedeColor.NebColour), null, null, new Vector2?(base.npc.Center), 0, 0);
						}
					}
					if (base.npc.ai[2] >= 1030f)
					{
						RedeWorld.nebDeath = 6;
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 2f;
						if (!RedeConfigClient.Instance.NoLoreElements && !Main.dedServ)
						{
							Redemption.Inst.TitleCardUIElement.DisplayTitle("Nebuleus", 60, 90, 0.8f, 0, new Color?(Color.HotPink), "Ultimate Form", true);
						}
						base.npc.netUpdate = true;
					}
				}
				else if (base.npc.ai[2] >= 120f)
				{
					if (!RedeConfigClient.Instance.NoLoreElements && !Main.dedServ)
					{
						Redemption.Inst.TitleCardUIElement.DisplayTitle("Nebuleus", 60, 90, 0.8f, 0, new Color?(Color.HotPink), "Ultimate Form", true);
					}
					base.npc.ai[3] = 0f;
					base.npc.ai[2] = 0f;
					base.npc.ai[0] = 2f;
					base.npc.netUpdate = true;
				}
				break;
			case 2:
				this.repeat = 0;
				base.npc.LookAtPlayer();
				this.Teleport(false, Vector2.Zero);
				this.frameCounters = 0;
				base.npc.rotation = 0f;
				base.npc.velocity = Vector2.Zero;
				base.npc.ai[3] = 0f;
				base.npc.ai[0] = 3f;
				base.npc.ai[2] = 0f;
				this.AttackChoice();
				this.circleTimer = 0;
				this.circleRadius = 800;
				base.npc.netUpdate = true;
				break;
			case 3:
				switch (this.ID)
				{
				case 0:
					base.npc.LookAtPlayer();
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 10f)
					{
						base.npc.ai[3] = 1f;
					}
					if (base.npc.ai[2] == 30f || base.npc.ai[2] == 50f)
					{
						int pieCut = 8;
						for (int k = 0; k < pieCut; k++)
						{
							if (Main.netMode != 1)
							{
								int projID = Projectile.NewProjectile(base.npc.Center, Vector2.Zero, ModContent.ProjectileType<CurvingStarTele4>(), 40, 0f, Main.myPlayer, 1.01f, 0f);
								Main.projectile[projID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)k / (float)pieCut * 6.28f);
								Main.projectile[projID].netUpdate = true;
							}
						}
					}
					if (base.npc.ai[2] == 40f || base.npc.ai[2] == 60f)
					{
						int pieCut2 = 8;
						for (int l = 0; l < pieCut2; l++)
						{
							if (Main.netMode != 1)
							{
								int projID2 = Projectile.NewProjectile(base.npc.Center, Vector2.Zero, ModContent.ProjectileType<CurvingStarTele4>(), 40, 0f, Main.myPlayer, 1.01f, 1f);
								Main.projectile[projID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)l / (float)pieCut2 * 6.28f);
								Main.projectile[projID2].netUpdate = true;
							}
						}
					}
					if (base.npc.ai[2] == 80f)
					{
						base.npc.ai[3] = 2f;
					}
					if (base.npc.ai[2] >= 120f)
					{
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 2f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					break;
				case 1:
					base.npc.LookAtPlayer();
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 10f)
					{
						base.npc.ai[3] = 1f;
					}
					if (base.npc.ai[2] > 30f && ((base.npc.life <= (int)((float)base.npc.lifeMax * 0.5f)) ? (base.npc.ai[2] % 3f == 0f) : (base.npc.ai[2] % 4f == 0f)) && base.npc.ai[2] <= 140f)
					{
						this.attackTimer[0] += 0.0010908308f * base.npc.ai[2];
						if (this.attackTimer[0] > 3.1415927f)
						{
							this.attackTimer[0] -= 6.2831855f;
						}
						if (Main.netMode != 1)
						{
							for (int m = 0; m < 4; m++)
							{
								Projectile.NewProjectile(base.npc.Center, Utils.RotatedBy(new Vector2(6f, 0f), (double)this.attackTimer[0] + 1.5707963267948966 * (double)m, default(Vector2)), ModContent.ProjectileType<CurvingStarTele2>(), 40, 0f, Main.myPlayer, 1.005f, 0f);
							}
						}
					}
					if (base.npc.ai[2] == 140f)
					{
						base.npc.ai[3] = 2f;
					}
					if (base.npc.ai[2] >= 180f)
					{
						this.attackTimer[0] = 0f;
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 2f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					break;
				case 2:
					base.npc.LookAtPlayer();
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 10f)
					{
						base.npc.ai[3] = 1f;
					}
					if (base.npc.ai[2] == 2f)
					{
						base.npc.Shoot(new Vector2(base.npc.Center.X - 900f, base.npc.Center.Y - 900f), ModContent.ProjectileType<CosmicEye2>(), 150, new Vector2(0f, 10f), true, SoundID.Item1, "Sounds/Custom/NebSound1", (float)base.npc.whoAmI, 0f);
						base.npc.Shoot(new Vector2(base.npc.Center.X - 900f, base.npc.Center.Y + 900f), ModContent.ProjectileType<CosmicEye2>(), 150, new Vector2(10f, 0f), true, SoundID.Item1, "Sounds/Custom/NebSound1", (float)base.npc.whoAmI, 0f);
						base.npc.Shoot(new Vector2(base.npc.Center.X + 900f, base.npc.Center.Y + 900f), ModContent.ProjectileType<CosmicEye2>(), 150, new Vector2(0f, -10f), true, SoundID.Item1, "Sounds/Custom/NebSound1", (float)base.npc.whoAmI, 0f);
						base.npc.Shoot(new Vector2(base.npc.Center.X + 900f, base.npc.Center.Y - 900f), ModContent.ProjectileType<CosmicEye2>(), 150, new Vector2(-10f, 0f), true, SoundID.Item1, "Sounds/Custom/NebSound1", (float)base.npc.whoAmI, 0f);
					}
					if (base.npc.ai[2] == 30f && Main.netMode != 1)
					{
						int pieCut3 = 4;
						for (int n = 0; n < pieCut3; n++)
						{
							int projID3 = Projectile.NewProjectile(base.npc.Center, Vector2.Zero, ModContent.ProjectileType<CurvingStarTele4>(), 40, 0f, Main.myPlayer, 1.001f, 2f);
							Main.projectile[projID3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)n / (float)pieCut3 * 6.28f);
							Main.projectile[projID3].netUpdate = true;
						}
						for (int m2 = 0; m2 < pieCut3; m2++)
						{
							int projID4 = Projectile.NewProjectile(base.npc.Center, Vector2.Zero, ModContent.ProjectileType<CurvingStarTele4>(), 40, 0f, Main.myPlayer, 1.001f, 3f);
							Main.projectile[projID4].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)m2 / (float)pieCut3 * 6.28f);
							Main.projectile[projID4].netUpdate = true;
						}
					}
					if (base.npc.ai[2] == 40f && Main.netMode != 1)
					{
						int pieCut4 = 4;
						for (int m3 = 0; m3 < pieCut4; m3++)
						{
							int projID5 = Projectile.NewProjectile(base.npc.Center, Vector2.Zero, ModContent.ProjectileType<CurvingStarTele4>(), 40, 0f, Main.myPlayer, 1.001f, 2f);
							Main.projectile[projID5].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(9f, 0f), (float)m3 / (float)pieCut4 * 6.28f);
							Main.projectile[projID5].netUpdate = true;
						}
						for (int m4 = 0; m4 < pieCut4; m4++)
						{
							int projID6 = Projectile.NewProjectile(base.npc.Center, Vector2.Zero, ModContent.ProjectileType<CurvingStarTele4>(), 40, 0f, Main.myPlayer, 1.001f, 3f);
							Main.projectile[projID6].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(9f, 0f), (float)m4 / (float)pieCut4 * 6.28f);
							Main.projectile[projID6].netUpdate = true;
						}
					}
					if (base.npc.ai[2] == 35f && Main.netMode != 1)
					{
						int pieCut5 = 8;
						for (int m5 = 0; m5 < pieCut5; m5++)
						{
							int projID7 = Projectile.NewProjectile(base.npc.Center, Vector2.Zero, ModContent.ProjectileType<CurvingStarTele4>(), 40, 0f, Main.myPlayer, 1.001f, 2f);
							Main.projectile[projID7].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(3f, 0f), (float)m5 / (float)pieCut5 * 6.28f);
							Main.projectile[projID7].netUpdate = true;
						}
						for (int m6 = 0; m6 < pieCut5; m6++)
						{
							int projID8 = Projectile.NewProjectile(base.npc.Center, Vector2.Zero, ModContent.ProjectileType<CurvingStarTele4>(), 40, 0f, Main.myPlayer, 1.001f, 3f);
							Main.projectile[projID8].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(3f, 0f), (float)m6 / (float)pieCut5 * 6.28f);
							Main.projectile[projID8].netUpdate = true;
						}
					}
					if (base.npc.ai[2] == 340f)
					{
						base.npc.ai[3] = 2f;
					}
					if (base.npc.ai[2] >= 360f)
					{
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 2f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					break;
				case 3:
					base.npc.LookAtPlayer();
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 10f)
					{
						base.npc.ai[3] = 3f;
					}
					if (base.npc.ai[2] >= 30f && base.npc.ai[2] <= 70f && Main.rand.Next(2) == 0)
					{
						int A;
						if (base.npc.spriteDirection != 1)
						{
							A = Main.rand.Next(600, 650);
						}
						else
						{
							A = Main.rand.Next(-650, -600);
						}
						int B = Main.rand.Next(-200, 200) - 700;
						base.npc.Shoot(new Vector2(player.Center.X + (float)A, player.Center.Y + (float)B), ModContent.ProjectileType<StarfallTele2>(), 120, new Vector2((base.npc.spriteDirection != 1) ? -12f : 12f, 14f), false, SoundID.Item9.WithVolume(0.5f), "", 0f, 0f);
					}
					if (base.npc.ai[2] == 40f)
					{
						base.npc.ai[3] = 4f;
					}
					if (base.npc.ai[2] >= 100f)
					{
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 2f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					break;
				case 4:
					base.npc.LookAtPlayer();
					if (this.circleRadius > 900)
					{
						this.circleRadius--;
					}
					for (int k2 = 0; k2 < 6; k2++)
					{
						double angle = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
						this.vector.X = (float)(Math.Sin(angle) * (double)this.circleRadius);
						this.vector.Y = (float)(Math.Cos(angle) * (double)this.circleRadius);
						Dust dust2 = Main.dust[Dust.NewDust(base.npc.Center + this.vector, 2, 2, 58, 0f, 0f, 100, default(Color), 2f)];
						dust2.noGravity = true;
						dust2.velocity = -base.npc.DirectionTo(dust2.position) * 2f;
					}
					if (base.npc.Distance(player.Center) > (float)this.circleRadius)
					{
						Vector2 movement = base.npc.Center - player.Center;
						float difference = movement.Length() - (float)this.circleRadius;
						movement.Normalize();
						movement *= ((difference < 17f) ? difference : 17f);
						player.position += movement;
					}
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 5f)
					{
						this.circleRadius = 1200;
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<NebRing>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
					}
					if (base.npc.ai[2] == 10f)
					{
						base.npc.ai[3] = 8f;
					}
					if (base.npc.ai[2] > 30f && base.npc.ai[2] % 3f == 0f && base.npc.ai[2] <= 180f)
					{
						this.attackTimer[0] += 0.20943952f;
						if (this.attackTimer[0] > 3.1415927f)
						{
							this.attackTimer[0] -= 6.2831855f;
						}
						if (Main.netMode != 1)
						{
							Projectile.NewProjectile(base.npc.Center, Utils.RotatedBy(new Vector2(6f, 0f), (double)this.attackTimer[0] + 1.5707963267948966, default(Vector2)), ModContent.ProjectileType<CosmicEye3>(), 50, 0f, Main.myPlayer, 0f, 0f);
						}
					}
					if ((base.npc.ai[2] == 40f || base.npc.ai[2] == 120f) && Main.netMode != 1)
					{
						int pieCut6 = 8;
						for (int m7 = 0; m7 < pieCut6; m7++)
						{
							int projID9 = Projectile.NewProjectile(base.npc.Center, Vector2.Zero, ModContent.ProjectileType<CurvingStarTele4>(), 40, 0f, Main.myPlayer, 1.001f, 0f);
							Main.projectile[projID9].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(2f, 0f), (float)m7 / (float)pieCut6 * 6.28f);
							Main.projectile[projID9].netUpdate = true;
						}
					}
					if (base.npc.ai[2] >= 300f)
					{
						this.attackTimer[0] = 0f;
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 2f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					break;
				case 5:
					base.npc.LookAtPlayer();
					if (this.circleRadius > 600)
					{
						this.circleRadius--;
					}
					for (int k3 = 0; k3 < 6; k3++)
					{
						double angle2 = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
						this.vector.X = (float)(Math.Sin(angle2) * (double)this.circleRadius);
						this.vector.Y = (float)(Math.Cos(angle2) * (double)this.circleRadius);
						Dust dust3 = Main.dust[Dust.NewDust(base.npc.Center + this.vector, 2, 2, 58, 0f, 0f, 100, default(Color), 2f)];
						dust3.noGravity = true;
						dust3.velocity = -base.npc.DirectionTo(dust3.position) * 2f;
					}
					if (base.npc.Distance(player.Center) > (float)this.circleRadius)
					{
						Vector2 movement2 = base.npc.Center - player.Center;
						float difference2 = movement2.Length() - (float)this.circleRadius;
						movement2.Normalize();
						movement2 *= ((difference2 < 17f) ? difference2 : 17f);
						player.position += movement2;
					}
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 5f)
					{
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<NebRing>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
					}
					if (base.npc.ai[2] == 20f)
					{
						base.npc.ai[3] = 8f;
					}
					if (base.npc.ai[2] == 30f && Main.netMode != 1)
					{
						for (int k4 = 0; k4 < 16; k4++)
						{
							double angle3 = (double)k4 * 0.39269908169872414;
							this.vector.X = (float)(Math.Sin(angle3) * 180.0);
							this.vector.Y = (float)(Math.Cos(angle3) * 180.0);
							base.npc.Shoot(new Vector2((float)((int)base.npc.Center.X + (int)this.vector.X), (float)((int)base.npc.Center.Y + (int)this.vector.Y)), ModContent.ProjectileType<CosmicEye>(), 140, Vector2.Zero, true, SoundID.Item1, "Sounds/Custom/NebSound1", (float)base.npc.whoAmI, 0f);
						}
					}
					if (base.npc.ai[2] == 100f)
					{
						base.npc.ai[3] = 1f;
					}
					if ((base.npc.ai[2] == 130f || base.npc.ai[2] == 150f) && Main.netMode != 1)
					{
						int pieCut7 = 8;
						for (int m8 = 0; m8 < pieCut7; m8++)
						{
							int projID10 = Projectile.NewProjectile(base.npc.Center, Vector2.Zero, ModContent.ProjectileType<CurvingStarTele4>(), 40, 0f, Main.myPlayer, 1.02f, 0f);
							Main.projectile[projID10].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)m8 / (float)pieCut7 * 6.28f);
							Main.projectile[projID10].netUpdate = true;
						}
					}
					if ((base.npc.ai[2] == 140f || base.npc.ai[2] == 160f) && Main.netMode != 1)
					{
						int pieCut8 = 8;
						for (int m9 = 0; m9 < pieCut8; m9++)
						{
							int projID11 = Projectile.NewProjectile(base.npc.Center, Vector2.Zero, ModContent.ProjectileType<CurvingStarTele4>(), 40, 0f, Main.myPlayer, 1.02f, 1f);
							Main.projectile[projID11].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)m9 / (float)pieCut8 * 6.28f);
							Main.projectile[projID11].netUpdate = true;
						}
					}
					if (base.npc.ai[2] == 220f)
					{
						base.npc.ai[3] = 2f;
					}
					if (base.npc.ai[2] >= 250f)
					{
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 2f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					break;
				case 7:
					base.npc.LookAtPlayer();
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 10f)
					{
						base.npc.ai[3] = 3f;
					}
					if (base.npc.ai[2] >= 40f && base.npc.ai[2] < 120f && Main.rand.Next(4) == 0)
					{
						int A2 = Main.rand.Next(-200, 200) * 6;
						int B2 = Main.rand.Next(-200, 200) - 1000;
						base.npc.Shoot(new Vector2(player.Center.X + (float)A2, player.Center.Y + (float)B2), ModContent.ProjectileType<StarfallTele2>(), 120, new Vector2((base.npc.spriteDirection != 1) ? -2f : 2f, 6f), false, SoundID.Item9.WithVolume(0.5f), "", 0f, 0f);
					}
					if (base.npc.ai[2] == 50f)
					{
						base.npc.ai[3] = 4f;
					}
					if (base.npc.ai[2] >= 180f)
					{
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 2f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					break;
				case 8:
					base.npc.LookAtPlayer();
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 10f)
					{
						base.npc.ai[3] = 1f;
					}
					if (base.npc.ai[2] % 3f == 0f && base.npc.ai[2] >= 30f && base.npc.ai[2] <= 60f)
					{
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<CurvingStarTele4>(), 120, new Vector2((float)Main.rand.Next(-7, 7), (float)Main.rand.Next(-7, 7)), false, SoundID.Item9.WithVolume(0f), "", 1.01f, 0f);
					}
					if (base.npc.ai[2] == 60f)
					{
						base.npc.ai[3] = 2f;
					}
					if (base.npc.ai[2] >= 100f)
					{
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 2f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					break;
				case 9:
					base.npc.LookAtPlayer();
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 10f)
					{
						base.npc.ai[3] = 3f;
					}
					if (base.npc.ai[2] >= 40f && base.npc.ai[2] < 120f && base.npc.ai[2] % 12f == 0f)
					{
						int A3 = Main.rand.Next(-200, 200) * 6;
						int B3 = Main.rand.Next(-200, 200) - 1000;
						base.npc.Shoot(new Vector2(player.Center.X + (float)A3, player.Center.Y + (float)B3), ModContent.ProjectileType<CrystalStarTele>(), 120, new Vector2((base.npc.spriteDirection != 1) ? -2f : 2f, 6f), false, SoundID.Item9.WithVolume(0.5f), "", 0f, 0f);
					}
					if (base.npc.ai[2] == 50f)
					{
						base.npc.ai[3] = 4f;
					}
					if (base.npc.ai[2] >= 120f)
					{
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 2f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					break;
				case 10:
				{
					base.npc.LookAtPlayer();
					if (!ScreenPlayer.NebCutscene)
					{
						if (this.circleRadius > 700)
						{
							this.circleRadius -= 2;
						}
						for (int k5 = 0; k5 < 6; k5++)
						{
							double angle4 = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
							this.vector.X = (float)(Math.Sin(angle4) * (double)this.circleRadius);
							this.vector.Y = (float)(Math.Cos(angle4) * (double)this.circleRadius);
							Dust dust4 = Main.dust[Dust.NewDust(base.npc.Center + this.vector, 2, 2, 58, 0f, 0f, 100, default(Color), 2f)];
							dust4.noGravity = true;
							dust4.velocity = -base.npc.DirectionTo(dust4.position) * 2f;
						}
						if (base.npc.Distance(player.Center) > (float)this.circleRadius)
						{
							Vector2 movement3 = base.npc.Center - player.Center;
							float difference3 = movement3.Length() - (float)this.circleRadius;
							movement3.Normalize();
							movement3 *= ((difference3 < 17f) ? difference3 : 17f);
							player.position += movement3;
						}
					}
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 40f)
					{
						base.npc.ai[3] = 8f;
					}
					if (base.npc.ai[2] == 50f)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
					}
					int sizeOfChains = 32;
					float speed = 1f;
					base.npc.TargetClosest(true);
					if (base.npc.ai[2] == 1f)
					{
						int randFactor = 80;
						for (int i2 = 0; i2 < this.ChainPos.Length; i2++)
						{
							this.ChainPos[i2] = base.npc.Center;
						}
						this.temp[0] = player.Center + new Vector2((float)Main.rand.Next(-randFactor, randFactor), (float)Main.rand.Next(-randFactor, randFactor));
						this.temp[1] = player.Center + new Vector2((base.npc.Center.X - player.Center.X) * 2f, 0f) + new Vector2((float)Main.rand.Next(-randFactor, randFactor), (float)Main.rand.Next(-randFactor, randFactor));
						this.temp[2] = player.Center + new Vector2((base.npc.Center.X - player.Center.X) * 2f, (base.npc.Center.Y - player.Center.Y) * 2f) + new Vector2((float)Main.rand.Next(-randFactor, randFactor), (float)Main.rand.Next(-randFactor, randFactor));
						this.temp[3] = player.Center + new Vector2(0f, (base.npc.Center.Y - player.Center.Y) * 2f) + new Vector2((float)Main.rand.Next(-randFactor, randFactor), (float)Main.rand.Next(-randFactor, randFactor));
						for (int i3 = 0; i3 < this.ChainPos.Length; i3++)
						{
							this.temp[i3] += this.temp[i3] - this.ChainPos[i3];
						}
					}
					for (int i4 = 0; i4 < this.ChainPos.Length; i4++)
					{
						this.getGrad[i4] = (this.temp[i4] - this.ChainPos[i4]) / 32f;
						if (!this.ChainHitBoxArea[i4].Intersects(this.PlayerSafeHitBox) && base.npc.ai[2] < 800f && base.npc.ai[2] > 50f)
						{
							this.ChainPos[i4] += this.getGrad[i4] * speed;
						}
						this.ChainHitBoxArea[i4] = new Rectangle((int)this.ChainPos[i4].X - sizeOfChains / 2, (int)this.ChainPos[i4].Y - sizeOfChains / 2, sizeOfChains, sizeOfChains);
					}
					this.PlayerSafeHitBox = new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height);
					for (int i5 = 0; i5 < this.ChainPos.Length; i5++)
					{
						if (this.ChainHitBoxArea[i5].Intersects(this.PlayerSafeHitBox))
						{
							if (!ScreenPlayer.NebCutscene && base.npc.ai[2] < 300f)
							{
								base.npc.ai[2] = 180f;
								for (int m10 = 0; m10 < 8; m10++)
								{
									int dustID = Dust.NewDust(new Vector2(player.Center.X - 1f, player.Center.Y - 1f), 2, 2, 58, 0f, 0f, 100, Color.White, 2f);
									Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(12f, 0f), (float)m10 / 8f * 6.28f);
									Main.dust[dustID].noLight = false;
									Main.dust[dustID].noGravity = true;
								}
								ScreenPlayer.NebCutscene = true;
							}
							if (base.npc.ai[2] < 300f)
							{
								Vector2[] chainPos = this.ChainPos;
								int num3 = i5;
								chainPos[num3].Y = chainPos[num3].Y + (base.npc.ai[2] - 180f) / 30f;
								player.Center = this.ChainPos[i5];
							}
							else
							{
								if (base.npc.ai[2] == 300f)
								{
									for (int m11 = 0; m11 < 8; m11++)
									{
										int dustID2 = Dust.NewDust(new Vector2(player.Center.X, player.Center.Y - 1000f), 2, 2, 58, 0f, 0f, 100, Color.White, 2f);
										Main.dust[dustID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(12f, 0f), (float)m11 / 8f * 6.28f);
										Main.dust[dustID2].noLight = false;
										Main.dust[dustID2].noGravity = true;
									}
									this.temp[i5] = new Vector2(player.Center.X, player.Center.Y - 1000f);
									base.npc.Shoot(new Vector2(player.Center.X, player.Center.Y - 1000f), ModContent.ProjectileType<StationaryStar>(), 200, Vector2.Zero, false, SoundID.Item117, "", 0f, 0f);
								}
								else if (this.temp[i5].Y > player.Center.Y && ScreenPlayer.NebCutscene)
								{
									base.npc.ai[2] = 800f;
									ScreenPlayer.NebCutscene = false;
								}
								if (base.npc.ai[2] < 800f)
								{
									Vector2[] chainPos2 = this.ChainPos;
									int num4 = i5;
									chainPos2[num4].Y = chainPos2[num4].Y - (base.npc.ai[2] - 180f) / 4f;
									player.Center = this.ChainPos[i5];
								}
							}
						}
					}
					if (!this.ChainHitBoxArea[0].Intersects(this.PlayerSafeHitBox) && !this.ChainHitBoxArea[1].Intersects(this.PlayerSafeHitBox) && !this.ChainHitBoxArea[2].Intersects(this.PlayerSafeHitBox) && !this.ChainHitBoxArea[3].Intersects(this.PlayerSafeHitBox))
					{
						ScreenPlayer.NebCutscene = false;
					}
					for (int i6 = 0; i6 < this.ChainPos.Length; i6++)
					{
						if (base.npc.ai[2] > 800f)
						{
							this.ChainPos[i6] += (base.npc.Center - this.ChainPos[i6]) / 10f;
						}
					}
					if (base.npc.ai[2] == 850f)
					{
						for (int i7 = 0; i7 < this.ChainPos.Length; i7++)
						{
							this.ChainPos[i7] = base.npc.Center;
						}
						base.npc.velocity = Vector2.Zero;
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 2f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					if (base.npc.ai[2] >= 240f && !this.ChainHitBoxArea[0].Intersects(this.PlayerSafeHitBox) && !this.ChainHitBoxArea[1].Intersects(this.PlayerSafeHitBox) && !this.ChainHitBoxArea[2].Intersects(this.PlayerSafeHitBox) && !this.ChainHitBoxArea[3].Intersects(this.PlayerSafeHitBox) && base.npc.ai[2] < 800f)
					{
						ScreenPlayer.NebCutsceneflag = false;
						ScreenPlayer.NebCutscene = false;
						base.npc.velocity = Vector2.Zero;
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 2f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					break;
				}
				case 11:
					if (base.npc.ai[3] != 6f)
					{
						base.npc.LookAtPlayer();
						base.npc.netUpdate = true;
					}
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 25f)
					{
						base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<NebTeleLine1>(), 0, base.npc.DirectionTo(player.Center + player.velocity * 20f), false, SoundID.Item1.WithVolume(0f), "", 115f, (float)base.npc.whoAmI);
					}
					if (base.npc.ai[2] < 55f)
					{
						this.vector = player.Center + player.velocity * 20f;
					}
					if (base.npc.ai[2] == 65f)
					{
						base.npc.ai[3] = 6f;
						this.Dash((int)base.npc.Distance(player.Center) / 16, true, this.vector);
					}
					else if (base.npc.ai[2] == 86f)
					{
						base.npc.rotation = 0f;
						base.npc.velocity = Vector2.Zero;
						base.npc.netUpdate = true;
						if (this.repeat < 3)
						{
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<GiantStarPro>(), base.npc.damage, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
						}
					}
					if (base.npc.ai[2] > 65f && base.npc.ai[2] < 86f && base.npc.ai[2] % 2f == 0f)
					{
						base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<StarBolt>(), 120, RedeHelper.PolarVector(3f, base.npc.rotation + 1.5707964f), false, SoundID.Item91, "", 0f, 0f);
						base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<StarBolt>(), 120, RedeHelper.PolarVector(3f, base.npc.rotation - 1.5707964f), false, SoundID.Item91, "", 0f, 0f);
					}
					if (base.npc.ai[2] >= 90f)
					{
						if (this.repeat <= 2)
						{
							this.repeat++;
							base.npc.ai[2] = 20f;
							base.npc.netUpdate = true;
						}
						else
						{
							this.repeat = 0;
							base.npc.velocity = Vector2.Zero;
							base.npc.ai[3] = 0f;
							base.npc.ai[0] = 2f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
						}
					}
					if (base.npc.velocity.Length() < 10f && base.npc.ai[3] == 6f)
					{
						base.npc.ai[3] = 0f;
					}
					break;
				case 12:
					if (base.npc.ai[3] != 6f)
					{
						base.npc.LookAtPlayer();
						base.npc.netUpdate = true;
					}
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] < 15f)
					{
						NPC npc6 = base.npc;
						npc6.velocity.Y = npc6.velocity.Y - 1f;
					}
					if (base.npc.ai[2] == 15f)
					{
						base.npc.ai[3] = 6f;
						base.npc.netUpdate = true;
					}
					if (base.npc.ai[2] == 5f || base.npc.ai[2] == 15f)
					{
						base.npc.Shoot(new Vector2(player.Center.X, player.Center.Y + 350f), ModContent.ProjectileType<DashT2>(), 0, new Vector2(0f, -6f), false, SoundID.Item1.WithVolume(0f), "", 0f, 0f);
						for (int m12 = 0; m12 < 4; m12++)
						{
							int dustID3 = Dust.NewDust(new Vector2(player.Center.X - 1f, player.Center.Y - 1f + 350f), 2, 2, 58, 0f, 0f, 100, Color.White, 2f);
							Main.dust[dustID3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(12f, 0f), (float)m12 / 8f * 6.28f);
							Main.dust[dustID3].noLight = false;
							Main.dust[dustID3].noGravity = true;
						}
					}
					if (base.npc.ai[2] == 10f || base.npc.ai[2] == 20f)
					{
						base.npc.Shoot(new Vector2(player.Center.X, player.Center.Y - 350f), ModContent.ProjectileType<DashT2>(), 0, new Vector2(0f, 6f), false, SoundID.Item1.WithVolume(0f), "", 0f, 0f);
						for (int m13 = 0; m13 < 4; m13++)
						{
							int dustID4 = Dust.NewDust(new Vector2(player.Center.X - 1f, player.Center.Y - 1f - 350f), 2, 2, 58, 0f, 0f, 100, Color.White, 2f);
							Main.dust[dustID4].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(12f, 0f), (float)m13 / 8f * 6.28f);
							Main.dust[dustID4].noLight = false;
							Main.dust[dustID4].noGravity = true;
						}
					}
					if (base.npc.ai[2] == 50f)
					{
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						base.npc.velocity.Y = -35f;
						this.Teleport(true, player.Center + new Vector2(0f, 400f));
					}
					if (base.npc.ai[2] == 65f)
					{
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						base.npc.velocity.Y = 35f;
						this.Teleport(true, player.Center + new Vector2(0f, -350f));
					}
					if (base.npc.ai[2] == 80f)
					{
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						base.npc.velocity.Y = -35f;
						this.Teleport(true, player.Center + new Vector2(0f, 350f));
					}
					if (base.npc.ai[2] == 95f)
					{
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						base.npc.velocity.Y = 35f;
						this.Teleport(true, player.Center + new Vector2(0f, -350f));
					}
					if (base.npc.ai[2] == 95f)
					{
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						base.npc.velocity.Y = -30f;
						this.Teleport(true, player.Center + new Vector2(400f, 400f));
					}
					if (base.npc.ai[2] == 95f || base.npc.ai[2] == 115f || base.npc.ai[2] == 135f)
					{
						base.npc.ai[3] = 5f;
						this.armFrames[3] = 0;
					}
					if (base.npc.ai[2] == 105f || base.npc.ai[2] == 125f || base.npc.ai[2] == 145f)
					{
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<PNebula1Tele>(), 120, RedeHelper.PolarVector(18f, Utils.ToRotation(player.Center - base.npc.Center)), false, SoundID.Item125.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
					}
					if (base.npc.ai[2] > 95f)
					{
						base.npc.velocity *= 0.94f;
					}
					if (base.npc.ai[2] >= 180f)
					{
						base.npc.velocity = Vector2.Zero;
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 2f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					break;
				case 13:
					base.npc.LookAtPlayer();
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 20f || base.npc.ai[2] == 40f || base.npc.ai[2] == 60f)
					{
						base.npc.ai[3] = 5f;
						this.armFrames[3] = 0;
					}
					if (base.npc.ai[2] == 30f || base.npc.ai[2] == 50f || base.npc.ai[2] == 70f)
					{
						this.Teleport(false, Vector2.Zero);
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<PNebula1Tele>(), 120, RedeHelper.PolarVector(18f, Utils.ToRotation(player.Center - base.npc.Center)), false, SoundID.Item125.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
					}
					if (base.npc.ai[2] >= 120f)
					{
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 2f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					break;
				case 14:
					base.npc.LookAtPlayer();
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 20f || base.npc.ai[2] == 40f || base.npc.ai[2] == 60f)
					{
						base.npc.ai[3] = 5f;
						this.armFrames[3] = 0;
					}
					if (base.npc.ai[2] == 30f || base.npc.ai[2] == 70f)
					{
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<PNebula1Tele>(), 120, RedeHelper.PolarVector(18f, Utils.ToRotation(player.Center - base.npc.Center)), false, SoundID.Item125.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<PNebula1Tele>(), 120, RedeHelper.PolarVector(18f, Utils.ToRotation(player.Center - base.npc.Center) + 0.78f), false, SoundID.Item125.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<PNebula1Tele>(), 120, RedeHelper.PolarVector(18f, Utils.ToRotation(player.Center - base.npc.Center) - 0.78f), false, SoundID.Item125.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
					}
					if (base.npc.ai[2] == 50f)
					{
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<PNebula1Tele>(), 120, RedeHelper.PolarVector(18f, Utils.ToRotation(player.Center - base.npc.Center)), false, SoundID.Item125.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<PNebula1Tele>(), 120, RedeHelper.PolarVector(18f, Utils.ToRotation(player.Center - base.npc.Center) + 1.2f), false, SoundID.Item125.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<PNebula1Tele>(), 120, RedeHelper.PolarVector(18f, Utils.ToRotation(player.Center - base.npc.Center) - 1.2f), false, SoundID.Item125.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<PNebula1Tele>(), 120, RedeHelper.PolarVector(18f, Utils.ToRotation(player.Center - base.npc.Center) + 0.6f), false, SoundID.Item125.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<PNebula1Tele>(), 120, RedeHelper.PolarVector(18f, Utils.ToRotation(player.Center - base.npc.Center) - 0.6f), false, SoundID.Item125.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
					}
					if (base.npc.ai[2] >= 120f)
					{
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 2f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					break;
				case 15:
					if (base.npc.ai[3] != 6f)
					{
						base.npc.LookAtPlayer();
						base.npc.netUpdate = true;
					}
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] >= 100f)
					{
						base.npc.velocity *= 0.9f;
					}
					else
					{
						base.npc.velocity *= 0.98f;
					}
					if (base.npc.ai[2] == 5f)
					{
						this.eyeFlare = true;
						this.Teleport(false, Vector2.Zero);
						base.npc.velocity = -base.npc.DirectionTo(player.Center) * 16f;
					}
					if (base.npc.ai[2] == 50f)
					{
						this.Teleport(true, player.Center + new Vector2(-800f, 0f));
						base.npc.netUpdate = true;
					}
					if (base.npc.ai[2] == 90f)
					{
						this.Teleport(true, player.Center + new Vector2(800f, 0f));
						base.npc.netUpdate = true;
					}
					if (base.npc.ai[2] == 50f || base.npc.ai[2] == 90f)
					{
						base.npc.ai[3] = 6f;
						this.Dash(70, false, Vector2.Zero);
						base.npc.netUpdate = true;
					}
					if (base.npc.ai[2] % 3f == 0f && base.npc.velocity.Length() > 40f)
					{
						base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<StarBolt>(), 120, Utils.RotatedBy(base.npc.velocity, 1.5707963267948966, default(Vector2)) / 20f, false, SoundID.Item117, "", 0f, 0f);
						base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<StarBolt>(), 120, Utils.RotatedBy(base.npc.velocity, -1.5707963267948966, default(Vector2)) / 20f, false, SoundID.Item117, "", 0f, 0f);
					}
					if (base.npc.velocity.Length() < 10f && base.npc.ai[3] == 6f)
					{
						base.npc.ai[3] = 0f;
					}
					if (base.npc.ai[2] >= 140f && base.npc.velocity.Length() < 6f)
					{
						base.npc.velocity = Vector2.Zero;
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 2f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					break;
				case 16:
					base.npc.LookAtPlayer();
					if (this.circleRadius > 700)
					{
						this.circleRadius--;
					}
					for (int k6 = 0; k6 < 6; k6++)
					{
						double angle5 = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
						this.vector.X = (float)(Math.Sin(angle5) * (double)this.circleRadius);
						this.vector.Y = (float)(Math.Cos(angle5) * (double)this.circleRadius);
						Dust dust5 = Main.dust[Dust.NewDust(base.npc.Center + this.vector, 2, 2, 58, 0f, 0f, 100, default(Color), 2f)];
						dust5.noGravity = true;
						dust5.velocity = -base.npc.DirectionTo(dust5.position) * 2f;
					}
					if (base.npc.Distance(player.Center) > (float)this.circleRadius)
					{
						Vector2 movement4 = base.npc.Center - player.Center;
						float difference4 = movement4.Length() - (float)this.circleRadius;
						movement4.Normalize();
						movement4 *= ((difference4 < 17f) ? difference4 : 17f);
						player.position += movement4;
					}
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 5f)
					{
						this.circleRadius = 900;
						this.eyeFlare = true;
						base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<NebRing>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
					}
					if (base.npc.ai[2] == 10f)
					{
						base.npc.ai[3] = 1f;
					}
					if (base.npc.ai[2] > 30f && base.npc.ai[2] <= 300f)
					{
						this.attackTimer[0] += 0.20943952f;
						if (this.attackTimer[0] > 3.1415927f)
						{
							this.attackTimer[0] -= 6.2831855f;
						}
						if (Main.netMode != 1)
						{
							Projectile.NewProjectile(base.npc.Center, Utils.RotatedBy(new Vector2(2f, 0f), (double)this.attackTimer[0] + 1.5707963267948966, default(Vector2)), ModContent.ProjectileType<StarBolt>(), 50, 0f, Main.myPlayer, 0f, 0f);
						}
					}
					if (base.npc.ai[2] == 300f)
					{
						base.npc.ai[3] = 2f;
					}
					if (base.npc.ai[2] >= 350f)
					{
						this.attackTimer[0] = 0f;
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 2f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					break;
				case 17:
					base.npc.LookAtPlayer();
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 1f && !Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/NebSound2").WithPitchVariance(0.1f), (int)base.npc.position.X, (int)base.npc.position.Y);
					}
					if (base.npc.ai[2] >= 80f)
					{
						if (base.npc.ai[3] != 6f)
						{
							base.npc.LookAtPlayer();
							base.npc.netUpdate = true;
						}
						if (base.npc.ai[2] == 85f)
						{
							this.vector = player.Center;
							this.Teleport(true, player.Center + new Vector2(0f, -700f));
						}
						if (base.npc.ai[2] > 85f && base.npc.ai[2] % 3f == 0f && Main.netMode != 1)
						{
							int p = Projectile.NewProjectile(base.npc.Center, RedeHelper.PolarVector(2f, Utils.ToRotation(this.vector - base.npc.Center)), ModContent.ProjectileType<StarBolt>(), 50, 0f, Main.myPlayer, (float)base.npc.whoAmI, 0f);
							Main.projectile[p].timeLeft = 155;
						}
						if (base.npc.ai[2] > 85f)
						{
							base.npc.rotation = Utils.ToRotation(this.vector - base.npc.Center);
						}
						if (base.npc.ai[2] > 85f && this.attackTimer[0] < 90f)
						{
							base.npc.ai[3] = 6f;
							this.attackTimer[0] += 3f;
							base.npc.Center = this.vector + Utils.RotatedBy(new Vector2(0f, -1f), (double)MathHelper.ToRadians(this.attackTimer[0]), default(Vector2)) * 700f;
						}
						if (this.attackTimer[0] == 90f)
						{
							this.Teleport(true, this.vector + new Vector2(0f, 700f));
						}
						if (this.attackTimer[0] >= 90f && this.attackTimer[0] < 180f)
						{
							base.npc.ai[3] = 6f;
							this.attackTimer[0] += 3f;
							base.npc.Center = this.vector + Utils.RotatedBy(new Vector2(0f, -1f), (double)MathHelper.ToRadians(this.attackTimer[0] + 90f), default(Vector2)) * 700f;
						}
						if (this.attackTimer[0] == 180f)
						{
							this.Teleport(true, this.vector + new Vector2(700f, 0f));
						}
						if (this.attackTimer[0] >= 180f && this.attackTimer[0] < 270f)
						{
							base.npc.ai[3] = 6f;
							this.attackTimer[0] += 3f;
							base.npc.Center = this.vector + Utils.RotatedBy(new Vector2(0f, -1f), (double)MathHelper.ToRadians(this.attackTimer[0] - 90f), default(Vector2)) * 700f;
						}
						if (this.attackTimer[0] == 270f)
						{
							this.Teleport(true, this.vector + new Vector2(-700f, 0f));
						}
						if (this.attackTimer[0] >= 270f && this.attackTimer[0] < 360f)
						{
							base.npc.ai[3] = 6f;
							this.attackTimer[0] += 3f;
							base.npc.Center = this.vector + Utils.RotatedBy(new Vector2(0f, -1f), (double)MathHelper.ToRadians(this.attackTimer[0]), default(Vector2)) * 700f;
						}
					}
					if (this.attackTimer[0] >= 360f)
					{
						base.npc.rotation = 0f;
						base.npc.velocity *= 0f;
						this.attackTimer[0] = 0f;
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 2f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					break;
				}
				break;
			}
			if (MoRDialogueUI.Visible)
			{
				Vector2? pointPos = Redemption.Inst.DialogueUIElement.PointPos;
				Vector2 center = base.npc.Center;
				if (pointPos != null && (pointPos == null || pointPos.GetValueOrDefault() == center))
				{
					Redemption.Inst.DialogueUIElement.PointPos = new Vector2?(base.npc.Center);
					Redemption.Inst.DialogueUIElement.TextColor = new Color?(RedeColor.NebColour);
				}
			}
			if (Vector2.Distance(base.npc.Center, player.Center) <= 200f)
			{
				player.AddBuff(ModContent.BuffType<NebHealBuff>(), 20, true);
			}
			if (Vector2.Distance(base.npc.Center, player.Center) >= 950f && base.npc.ai[0] > 0f && base.npc.ai[1] != 2f && base.npc.ai[1] != 4f && base.npc.ai[1] != 5f && base.npc.ai[1] != 10f && base.npc.ai[1] != 11f && base.npc.ai[1] != 12f && base.npc.ai[1] != 15f && base.npc.ai[1] != 17f && !player.GetModPlayer<ScreenPlayer>().lockScreen)
			{
				this.Teleport(false, Vector2.Zero);
			}
		}

		public override bool CheckDead()
		{
			return true;
		}

		public void Dash(int speed, bool directional, Vector2 target)
		{
			Player player = Main.player[base.npc.target];
			this.RazzleDazzle();
			Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
			if (target == Vector2.Zero)
			{
				target = player.Center;
			}
			if (directional)
			{
				base.npc.velocity = base.npc.DirectionTo(target) * (float)speed;
				return;
			}
			base.npc.velocity.X = (float)((target.X > base.npc.Center.X) ? speed : (-(float)speed));
		}

		public void Teleport(bool specialPos, Vector2 teleportPos)
		{
			Player player = Main.player[base.npc.target];
			this.RazzleDazzle();
			this.teleGlow = true;
			this.teleGlowTimer = 0f;
			this.teleVector = base.npc.Center;
			if (Main.netMode != 1)
			{
				if (!specialPos)
				{
					int num = Main.rand.Next(2);
					if (num != 0)
					{
						if (num == 1)
						{
							Vector2 newPos2 = new Vector2((float)Main.rand.Next(250, 400), (float)Main.rand.Next(-200, 50));
							base.npc.Center = Main.player[base.npc.target].Center + newPos2;
							base.npc.netUpdate = true;
						}
					}
					else
					{
						Vector2 newPos3 = new Vector2((float)Main.rand.Next(-400, -250), (float)Main.rand.Next(-200, 50));
						base.npc.Center = Main.player[base.npc.target].Center + newPos3;
						base.npc.netUpdate = true;
					}
				}
				else
				{
					base.npc.Center = teleportPos;
					base.npc.netUpdate = true;
				}
			}
			this.teleVector = base.npc.Center;
			if (!Main.dedServ)
			{
				Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Teleport2").WithPitchVariance(0.1f), (int)base.npc.position.X, (int)base.npc.position.Y);
			}
			player.GetModPlayer<ScreenPlayer>().Rumble(5, 6);
			this.RazzleDazzle();
		}

		private void DespawnHandler()
		{
			Player player = Main.player[base.npc.target];
			if (!player.active || player.dead)
			{
				base.npc.velocity *= 0.96f;
				NPC npc = base.npc;
				npc.velocity.Y = npc.velocity.Y - 1f;
				if (base.npc.timeLeft > 10)
				{
					base.npc.timeLeft = 10;
				}
				return;
			}
		}

		public void RazzleDazzle()
		{
			if (Main.netMode != 1)
			{
				int dustType = 58;
				int pieCut = 8;
				for (int i = 0; i < pieCut; i++)
				{
					int dustID = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 4f);
					Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)i / (float)pieCut * 6.28f);
					Main.dust[dustID].noLight = false;
					Main.dust[dustID].noGravity = true;
				}
				int dustType2 = 59;
				int pieCut2 = 10;
				for (int j = 0; j < pieCut2; j++)
				{
					int dustID2 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, dustType2, 0f, 0f, 100, Color.White, 4f);
					Main.dust[dustID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(12f, 0f), (float)j / (float)pieCut2 * 6.28f);
					Main.dust[dustID2].noLight = false;
					Main.dust[dustID2].noGravity = true;
				}
				int dustType3 = 60;
				int pieCut3 = 12;
				for (int k = 0; k < pieCut3; k++)
				{
					int dustID3 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, dustType3, 0f, 0f, 100, Color.White, 4f);
					Main.dust[dustID3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(14f, 0f), (float)k / (float)pieCut3 * 6.28f);
					Main.dust[dustID3].noLight = false;
					Main.dust[dustID3].noGravity = true;
				}
				int dustType4 = 62;
				int pieCut4 = 14;
				for (int l = 0; l < pieCut4; l++)
				{
					int dustID4 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, dustType4, 0f, 0f, 100, Color.White, 4f);
					Main.dust[dustID4].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(16f, 0f), (float)l / (float)pieCut4 * 6.28f);
					Main.dust[dustID4].noLight = false;
					Main.dust[dustID4].noGravity = true;
				}
			}
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 1.5f;
			return null;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D chain = base.mod.GetTexture("NPCs/Bosses/Neb/CosmosChain1");
			Texture2D wings = base.mod.GetTexture("NPCs/Bosses/Neb/Phase2/NebP2_Wings");
			Texture2D armsAni = base.mod.GetTexture("NPCs/Bosses/Neb/Phase2/NebP2_Arms_Idle");
			Texture2D armsPrayAni = base.mod.GetTexture("NPCs/Bosses/Neb/Phase2/NebP2_Pray");
			Texture2D armsPrayGlow = base.mod.GetTexture("NPCs/Bosses/Neb/Phase2/NebP2_Pray_Glow");
			Texture2D armsStarfallAni = base.mod.GetTexture("NPCs/Bosses/Neb/Phase2/NebP2_Starfall");
			Texture2D armsStarfallGlow = base.mod.GetTexture("NPCs/Bosses/Neb/Phase2/NebP2_Starfall_Glow");
			Texture2D armsEyesAni = base.mod.GetTexture("NPCs/Bosses/Neb/Phase2/NebP2_Punch");
			Texture2D armsPiercingAni = base.mod.GetTexture("NPCs/Bosses/Neb/Phase2/NebP2_Arms_PiercingNebula");
			Texture2D armsPiercingGlow = base.mod.GetTexture("NPCs/Bosses/Neb/Phase2/NebP2_Arms_PiercingNebula_Glow");
			int spriteDirection = base.npc.spriteDirection;
			int shader = GameShaders.Armor.GetShaderIdFromItemId(2870);
			Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
			for (int i = this.oldPos.Length - 1; i >= 0; i--)
			{
				float alpha = 1f - (float)(i + 1) / (float)(this.oldPos.Length + 2);
				spriteBatch.Draw(texture, this.oldPos[i] - Main.screenPosition, new Rectangle?(base.npc.frame), Main.DiscoColor * (0.5f * alpha), this.oldrot[i], Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (base.npc.ai[1] == 10f && base.npc.ai[2] > 50f)
			{
				for (int j = 0; j < this.ChainPos.Length; j++)
				{
					RedeHelper.DrawBezier(spriteBatch, chain, "", Main.DiscoColor, base.npc.Center, this.ChainPos[j], (base.npc.Center + this.ChainPos[j]) / 2f + new Vector2(0f, (float)(130 + (int)(Math.Sin((double)(base.npc.ai[2] / 12f)) * (double)(150f - base.npc.ai[2] / 3f)))), (base.npc.Center + this.ChainPos[j]) / 2f + new Vector2(0f, (float)(130 + (int)(Math.Sin((double)(base.npc.ai[2] / 12f)) * (double)(150f - base.npc.ai[2] / 3f)))), 0.04f, 0f);
				}
			}
			spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			BaseDrawing.DrawTexture(spriteBatch, wings, shader, base.npc, new Color?(base.npc.GetAlpha(Color.White)), true, Utils.Size(base.npc.frame) / 2f);
			if (base.npc.ai[3] != 6f)
			{
				if (base.npc.ai[3] == 0f)
				{
					int num214 = armsAni.Height / 4;
					int y6 = num214 * this.armFrames[0];
					Main.spriteBatch.Draw(armsAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, armsAni.Width, num214)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)armsAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				}
				if (base.npc.ai[3] == 1f || base.npc.ai[3] == 2f)
				{
					int num215 = armsPrayAni.Height / 5;
					int y7 = num215 * this.armFrames[1];
					Main.spriteBatch.Draw(armsPrayAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, armsPrayAni.Width, num215)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)armsPrayAni.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
					Main.spriteBatch.Draw(armsPrayGlow, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, armsPrayAni.Width, num215)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)armsPrayAni.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				}
				if (base.npc.ai[3] == 3f || base.npc.ai[3] == 4f)
				{
					int num216 = armsStarfallAni.Height / 7;
					int y8 = num216 * this.armFrames[2];
					Main.spriteBatch.Draw(armsStarfallAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y8, armsStarfallAni.Width, num216)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)armsStarfallAni.Width / 2f, (float)num216 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
					Main.spriteBatch.Draw(armsStarfallGlow, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y8, armsStarfallAni.Width, num216)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)armsStarfallAni.Width / 2f, (float)num216 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				}
				if (base.npc.ai[3] == 5f)
				{
					int num217 = armsPiercingAni.Height / 6;
					int y9 = num217 * this.armFrames[3];
					Main.spriteBatch.Draw(armsPiercingAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y9, armsPiercingAni.Width, num217)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)armsPiercingAni.Width / 2f, (float)num217 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
					Main.spriteBatch.Draw(armsPiercingGlow, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y9, armsPiercingAni.Width, num217)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)armsPiercingAni.Width / 2f, (float)num217 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				}
				if (base.npc.ai[3] == 8f)
				{
					int num218 = armsEyesAni.Height / 8;
					int y10 = num218 * this.armFrames[5];
					Main.spriteBatch.Draw(armsEyesAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y10, armsEyesAni.Width, num218)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)armsEyesAni.Width / 2f, (float)num218 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				}
			}
			return false;
		}

		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			Texture2D flare = base.mod.GetTexture("ExtraTextures/PurpleEyeFlare");
			Rectangle rect = new Rectangle(0, 0, flare.Width, flare.Height);
			Vector2 origin = new Vector2((float)(flare.Width / 2), (float)(flare.Height / 2));
			Vector2 position = base.npc.Center - Main.screenPosition + new Vector2(0f, -14f);
			Vector2 position2 = base.npc.Center - Main.screenPosition + new Vector2((float)((base.npc.spriteDirection == 1) ? 8 : -8), -14f);
			Color colour = Color.Lerp(Color.Pink, Color.White, 1f / this.eyeFlareTimer * 10f) * (1f / this.eyeFlareTimer * 10f);
			if (this.eyeFlare)
			{
				spriteBatch.Draw(flare, position, new Rectangle?(rect), colour, base.npc.rotation, origin, 1f, SpriteEffects.None, 0f);
				spriteBatch.Draw(flare, position, new Rectangle?(rect), colour * 0.4f, base.npc.rotation, origin, 1f, SpriteEffects.None, 0f);
				spriteBatch.Draw(flare, position2, new Rectangle?(rect), colour, base.npc.rotation, origin, 0.95f, SpriteEffects.None, 0f);
				spriteBatch.Draw(flare, position2, new Rectangle?(rect), colour * 0.4f, base.npc.rotation, origin, 0.95f, SpriteEffects.None, 0f);
			}
			Texture2D teleportGlow = base.mod.GetTexture("ExtraTextures/WhiteGlow");
			Rectangle rect2 = new Rectangle(0, 0, teleportGlow.Width, teleportGlow.Height);
			Vector2 origin2 = new Vector2((float)(teleportGlow.Width / 2), (float)(teleportGlow.Height / 2));
			Vector2 position3 = this.teleVector - Main.screenPosition;
			Color colour2 = Color.Lerp(Color.White, Main.DiscoColor, 1f / this.teleGlowTimer * 10f) * (1f / this.teleGlowTimer * 10f);
			if (this.teleGlow)
			{
				spriteBatch.Draw(teleportGlow, position3, new Rectangle?(rect2), colour2, base.npc.rotation, origin2, 2f, SpriteEffects.None, 0f);
				spriteBatch.Draw(teleportGlow, position3, new Rectangle?(rect2), colour2 * 0.4f, base.npc.rotation, origin2, 2f, SpriteEffects.None, 0f);
			}
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
		}

		public Vector2[] oldPos = new Vector2[5];

		public float[] oldrot = new float[5];

		public Vector2 MoveVector2;

		private Vector2 vector;

		public int frameCounters;

		public int repeat;

		public int floatTimer;

		public int floatTimer2;

		public int[] armFrames = new int[6];

		private Vector2[] ChainPos = new Vector2[4];

		private readonly Vector2[] getGrad = new Vector2[4];

		public Vector2[] temp = new Vector2[4];

		private readonly Rectangle[] ChainHitBoxArea = new Rectangle[4];

		private Rectangle PlayerSafeHitBox;

		private bool title;

		public int phase;

		public float[] attackTimer = new float[2];

		public bool eyeFlare;

		public float eyeFlareTimer;

		public int circleTimer;

		public int circleRadius;

		public float teleGlowTimer;

		public bool teleGlow;

		public Vector2 teleVector;

		public List<int> AttackList = new List<int>
		{
			0,
			1,
			2,
			3,
			4,
			5,
			7,
			8,
			9,
			10,
			11,
			12,
			13,
			14,
			15,
			16,
			17
		};

		public List<int> CopyList;
	}
}
