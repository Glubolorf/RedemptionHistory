using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses
{
	[AutoloadBossHead]
	public class VlitchWormHead : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Vlitch Gigipede");
			Main.npcFrameCount[base.npc.type] = 1;
		}

		public override void SetDefaults()
		{
			base.npc.lifeMax = 75000;
			base.npc.damage = 150;
			base.npc.defense = 300;
			base.npc.knockBackResist = 0f;
			base.npc.width = 102;
			base.npc.height = 92;
			base.npc.boss = true;
			base.npc.friendly = false;
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.behindTiles = true;
			base.npc.DeathSound = SoundID.NPCDeath14;
			base.npc.buffImmune[20] = true;
			base.npc.buffImmune[31] = true;
			base.npc.buffImmune[39] = true;
			base.npc.buffImmune[24] = true;
			base.npc.value = (float)Item.buyPrice(0, 20, 0, 0);
			base.npc.npcSlots = 1f;
			base.npc.netAlways = true;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/BossVlitch1");
			this.bossBag = base.mod.ItemType("VlitchGigipedeBag");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.Center, base.npc.velocity, base.mod.GetGoreSlot("Gores/VlitchWormGore1"), 1f);
				Gore.NewGore(base.npc.Center, base.npc.velocity, base.mod.GetGoreSlot("Gores/VlitchWormGore1"), 1f);
			}
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("VlitchTrophy"), 1, false, 0, false, false);
			}
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
				return;
			}
			if (Main.rand.Next(14) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("GirusMask"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("CorruptedRocketLauncher"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("CorruptedDoubleRifle"), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("CorruptedXenomite"), Main.rand.Next(18, 28), false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("CorruptedStarlite"), Main.rand.Next(15, 20), false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("VlitchScale"), Main.rand.Next(25, 35), false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("VlitchBattery"), Main.rand.Next(2, 4), false, 0, false, false);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 499;
			if (!RedeWorld.downedVlitch2)
			{
				RedeWorld.redemptionPoints++;
				CombatText.NewText(this.player.getRect(), Color.Gold, "+1", true, false);
				for (int i = 0; i < 255; i++)
				{
					Player player = Main.player[i];
					if (player.active)
					{
						for (int j = 0; j < player.inventory.Length; j++)
						{
							if (player.inventory[j].type == base.mod.ItemType("RedemptionTeller"))
							{
								Main.NewText("<Chalice of Alignment> The second Vlitch Overlord is down! Only 1 more... I think?", Color.DarkGoldenrod, false);
							}
						}
					}
				}
			}
			RedeWorld.downedVlitch2 = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			if (!RedeWorld.girusTalk2 && !NPC.AnyNPCs(base.mod.NPCType("VlitchCleaver")) && !NPC.AnyNPCs(base.mod.NPCType("OmegaOblitDamaged")))
			{
				Projectile.NewProjectile(new Vector2(base.npc.position.X, base.npc.position.Y), new Vector2(0f, 0f), base.mod.ProjectileType("GirusTalking2"), 0, 0f, 255, 0f, 0f);
			}
			if (!Main.dedServ)
			{
				Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/DistortedRoar").WithVolume(0.7f).WithPitchVariance(0.1f), -1, -1);
			}
		}

		public override bool PreAI()
		{
			if (Main.dayTime)
			{
				base.npc.timeLeft = 0;
				NPC npc = base.npc;
				npc.position.Y = npc.position.Y - 300f;
			}
			if (NPC.AnyNPCs(base.mod.NPCType("VlitchCore1")))
			{
				base.npc.dontTakeDamage = true;
			}
			if (NPC.AnyNPCs(base.mod.NPCType("VlitchCore2")))
			{
				base.npc.dontTakeDamage = true;
			}
			if (NPC.AnyNPCs(base.mod.NPCType("VlitchCore3")))
			{
				base.npc.dontTakeDamage = true;
			}
			if (!NPC.AnyNPCs(base.mod.NPCType("VlitchCore1")) && !NPC.AnyNPCs(base.mod.NPCType("VlitchCore2")) && !NPC.AnyNPCs(base.mod.NPCType("VlitchCore3")))
			{
				base.npc.dontTakeDamage = false;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).omegaPower)
			{
				base.npc.ai[1] += 1f;
				if (base.npc.ai[1] == 600f)
				{
					Main.NewText("The probes aren't attacking you!?", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
				}
				if (base.npc.ai[1] == 800f)
				{
					Main.NewText("No matter... The corrupted worms are smart enough.", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
				}
			}
			if (base.npc.life > 0 && !NPC.AnyNPCs(base.mod.NPCType("VlitchWormTail")))
			{
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] == 60f)
				{
					Main.NewText("Huh? My tail despawned!?", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
				}
				if (base.npc.ai[2] == 230f)
				{
					Main.NewText("How am I suppose to summon my minions without my tail!?", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
				}
				if (base.npc.ai[2] == 530f)
				{
					Main.NewText("DAMN IT! STOP FLYING AWAY! YOU'RE BUGGING ME OUT!", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
				}
			}
			if (!this.Core1)
			{
				base.npc.ai[3] += 1f;
			}
			if (base.npc.ai[3] >= 120f && !this.Core1)
			{
				if (!Config.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Color.Red, "[DEPLOYING CORE]", true, false);
				}
				Main.NewText("[DEPLOYING CORE]", Color.Red.R, Color.Red.G, Color.Red.B, false);
				int num = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("VlitchCore1"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[num].netUpdate = true;
				base.npc.ai[3] = 0f;
				this.Core1 = true;
			}
			if (base.npc.life < 75000 && !this.Core2)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] >= 60f)
				{
					if (!Config.NoCombatText)
					{
						CombatText.NewText(base.npc.getRect(), Color.Red, "[DEPLOYING CORE]", true, false);
					}
					Main.NewText("[DEPLOYING CORE]", Color.Red.R, Color.Red.G, Color.Red.B, false);
					int num2 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("VlitchCore2"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num2].netUpdate = true;
					base.npc.ai[3] = 0f;
					this.Core2 = true;
				}
			}
			if (base.npc.life < 25000 && !this.Core3)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 1f)
				{
					Main.NewText("I may have miscalculated...", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
				}
				if (base.npc.ai[3] >= 120f)
				{
					if (!Config.NoCombatText)
					{
						CombatText.NewText(base.npc.getRect(), Color.Red, "[DEPLOYING CORE]", true, false);
					}
					Main.NewText("[DEPLOYING CORE]", Color.Red.R, Color.Red.G, Color.Red.B, false);
					int num3 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("VlitchCore3"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num3].netUpdate = true;
					base.npc.ai[3] = 0f;
					this.Core3 = true;
				}
			}
			this.Target();
			this.DespawnHandler();
			if (Main.netMode != 1)
			{
				if (base.npc.ai[0] == 0f)
				{
					base.npc.realLife = base.npc.whoAmI;
					int num4 = base.npc.whoAmI;
					int num5 = 16;
					for (int i = 0; i < num5; i++)
					{
						num4 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("VlitchWormBody"), base.npc.whoAmI, 0f, (float)num4, 0f, 0f, 255);
						Main.npc[num4].realLife = base.npc.whoAmI;
						Main.npc[num4].ai[3] = (float)base.npc.whoAmI;
					}
					num4 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("VlitchWormTail"), base.npc.whoAmI, 0f, (float)num4, 0f, 0f, 255);
					Main.npc[num4].realLife = base.npc.whoAmI;
					Main.npc[num4].ai[3] = (float)base.npc.whoAmI;
					base.npc.ai[0] = 1f;
					base.npc.netUpdate = true;
				}
				if (Main.rand.Next(2) == 0)
				{
					Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, base.mod.DustType("VlitchFlame"), 0f, 0f, 0, default(Color), 1f);
				}
			}
			int num6 = (int)((double)base.npc.position.X / 16.0) - 1;
			int num7 = (int)((double)(base.npc.position.X + (float)base.npc.width) / 16.0) + 2;
			int num8 = (int)((double)base.npc.position.Y / 16.0) - 1;
			int num9 = (int)((double)(base.npc.position.Y + (float)base.npc.height) / 16.0) + 2;
			if (num6 < 0)
			{
				num6 = 0;
			}
			if (num7 > Main.maxTilesX)
			{
				num7 = Main.maxTilesX;
			}
			if (num8 < 0)
			{
				num8 = 0;
			}
			if (num9 > Main.maxTilesY)
			{
				num9 = Main.maxTilesY;
			}
			bool flag = false;
			for (int j = num6; j < num7; j++)
			{
				for (int k = num8; k < num9; k++)
				{
					if (Main.tile[j, k] != null && ((Main.tile[j, k].nactive() && (Main.tileSolid[(int)Main.tile[j, k].type] || (Main.tileSolidTop[(int)Main.tile[j, k].type] && Main.tile[j, k].frameY == 0))) || Main.tile[j, k].liquid > 64))
					{
						Vector2 vector;
						vector.X = (float)(j * 16);
						vector.Y = (float)(k * 16);
						if (base.npc.position.X + (float)base.npc.width > vector.X && (double)base.npc.position.X < (double)vector.X + 16.0 && (double)(base.npc.position.Y + (float)base.npc.height) > (double)vector.Y && (double)base.npc.position.Y < (double)vector.Y + 16.0)
						{
							flag = true;
							if (Main.rand.Next(100) == 0 && Main.tile[j, k].nactive())
							{
								WorldGen.KillTile(j, k, true, true, false);
							}
						}
					}
				}
			}
			if (!flag)
			{
				Rectangle rectangle;
				rectangle..ctor((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height);
				int num10 = 1000;
				bool flag2 = true;
				for (int l = 0; l < 255; l++)
				{
					if (Main.player[l].active)
					{
						Rectangle rectangle2;
						rectangle2..ctor((int)Main.player[l].position.X - num10, (int)Main.player[l].position.Y - num10, num10 * 2, num10 * 2);
						if (rectangle.Intersects(rectangle2))
						{
							flag2 = false;
							break;
						}
					}
				}
				if (flag2)
				{
					flag = true;
				}
			}
			float num11 = 20f;
			float num12 = 0.38f;
			Vector2 vector2;
			vector2..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
			float num13 = Main.player[base.npc.target].position.X + (float)(Main.player[base.npc.target].width / 2);
			float num14 = Main.player[base.npc.target].position.Y + (float)(Main.player[base.npc.target].height / 2);
			float num15 = (float)((int)((double)num13 / 16.0) * 16);
			float num16 = (float)((int)((double)num14 / 16.0) * 16);
			vector2.X = (float)((int)((double)vector2.X / 16.0) * 16);
			vector2.Y = (float)((int)((double)vector2.Y / 16.0) * 16);
			float num17 = num15 - vector2.X;
			float num18 = num16 - vector2.Y;
			float num19 = (float)Math.Sqrt((double)(num17 * num17 + num18 * num18));
			if (!flag)
			{
				base.npc.TargetClosest(true);
				base.npc.velocity.Y = base.npc.velocity.Y + 0.11f;
				if (base.npc.velocity.Y > num11)
				{
					base.npc.velocity.Y = num11;
				}
				if ((double)(Math.Abs(base.npc.velocity.X) + Math.Abs(base.npc.velocity.Y)) < (double)num11 * 0.4)
				{
					if ((double)base.npc.velocity.X < 0.0)
					{
						base.npc.velocity.X = base.npc.velocity.X - num12 * 1.1f;
					}
					else
					{
						base.npc.velocity.X = base.npc.velocity.X + num12 * 1.1f;
					}
				}
				else if (base.npc.velocity.Y == num11)
				{
					if (base.npc.velocity.X < num17)
					{
						base.npc.velocity.X = base.npc.velocity.X + num12;
					}
					else if (base.npc.velocity.X > num17)
					{
						base.npc.velocity.X = base.npc.velocity.X - num12;
					}
				}
				else if ((double)base.npc.velocity.Y > 4.0)
				{
					if ((double)base.npc.velocity.X < 0.0)
					{
						base.npc.velocity.X = base.npc.velocity.X + num12 * 0.9f;
					}
					else
					{
						base.npc.velocity.X = base.npc.velocity.X - num12 * 0.9f;
					}
				}
			}
			else
			{
				if (base.npc.soundDelay == 0)
				{
					float num20 = num19 / 40f;
					if ((double)num20 < 10.0)
					{
						num20 = 10f;
					}
					if ((double)num20 > 20.0)
					{
						num20 = 20f;
					}
					base.npc.soundDelay = (int)num20;
					Main.PlaySound(15, (int)base.npc.position.X, (int)base.npc.position.Y, 1, 1f, 0f);
				}
				float num21 = Math.Abs(num17);
				float num22 = Math.Abs(num18);
				float num23 = num11 / num19;
				num17 *= num23;
				num18 *= num23;
				if (((double)base.npc.velocity.X > 0.0 && (double)num17 > 0.0) || ((double)base.npc.velocity.X < 0.0 && (double)num17 < 0.0) || ((double)base.npc.velocity.Y > 0.0 && (double)num18 > 0.0) || ((double)base.npc.velocity.Y < 0.0 && (double)num18 < 0.0))
				{
					if (base.npc.velocity.X < num17)
					{
						base.npc.velocity.X = base.npc.velocity.X + num12;
					}
					else if (base.npc.velocity.X > num17)
					{
						base.npc.velocity.X = base.npc.velocity.X - num12;
					}
					if (base.npc.velocity.Y < num18)
					{
						base.npc.velocity.Y = base.npc.velocity.Y + num12;
					}
					else if (base.npc.velocity.Y > num18)
					{
						base.npc.velocity.Y = base.npc.velocity.Y - num12;
					}
					if ((double)Math.Abs(num18) < (double)num11 * 0.2 && (((double)base.npc.velocity.X > 0.0 && (double)num17 < 0.0) || ((double)base.npc.velocity.X < 0.0 && (double)num17 > 0.0)))
					{
						if ((double)base.npc.velocity.Y > 0.0)
						{
							base.npc.velocity.Y = base.npc.velocity.Y + num12 * 2f;
						}
						else
						{
							base.npc.velocity.Y = base.npc.velocity.Y - num12 * 2f;
						}
					}
					if ((double)Math.Abs(num17) < (double)num11 * 0.2 && (((double)base.npc.velocity.Y > 0.0 && (double)num18 < 0.0) || ((double)base.npc.velocity.Y < 0.0 && (double)num18 > 0.0)))
					{
						if ((double)base.npc.velocity.X > 0.0)
						{
							base.npc.velocity.X = base.npc.velocity.X + num12 * 2f;
						}
						else
						{
							base.npc.velocity.X = base.npc.velocity.X - num12 * 2f;
						}
					}
				}
				else if (num21 > num22)
				{
					if (base.npc.velocity.X < num17)
					{
						base.npc.velocity.X = base.npc.velocity.X + num12 * 1.1f;
					}
					else if (base.npc.velocity.X > num17)
					{
						base.npc.velocity.X = base.npc.velocity.X - num12 * 1.1f;
					}
					if ((double)(Math.Abs(base.npc.velocity.X) + Math.Abs(base.npc.velocity.Y)) < (double)num11 * 0.5)
					{
						if ((double)base.npc.velocity.Y > 0.0)
						{
							base.npc.velocity.Y = base.npc.velocity.Y + num12;
						}
						else
						{
							base.npc.velocity.Y = base.npc.velocity.Y - num12;
						}
					}
				}
				else
				{
					if (base.npc.velocity.Y < num18)
					{
						base.npc.velocity.Y = base.npc.velocity.Y + num12 * 1.1f;
					}
					else if (base.npc.velocity.Y > num18)
					{
						base.npc.velocity.Y = base.npc.velocity.Y - num12 * 1.1f;
					}
					if ((double)(Math.Abs(base.npc.velocity.X) + Math.Abs(base.npc.velocity.Y)) < (double)num11 * 0.5)
					{
						if ((double)base.npc.velocity.X > 0.0)
						{
							base.npc.velocity.X = base.npc.velocity.X + num12;
						}
						else
						{
							base.npc.velocity.X = base.npc.velocity.X - num12;
						}
					}
				}
			}
			base.npc.rotation = (float)Math.Atan2((double)base.npc.velocity.Y, (double)base.npc.velocity.X) + 1.57f;
			if (flag)
			{
				if (base.npc.localAI[0] != 1f)
				{
					base.npc.netUpdate = true;
				}
				base.npc.localAI[0] = 1f;
			}
			else
			{
				if ((double)base.npc.localAI[0] != 0.0)
				{
					base.npc.netUpdate = true;
				}
				base.npc.localAI[0] = 0f;
			}
			if ((((double)base.npc.velocity.X > 0.0 && (double)base.npc.oldVelocity.X < 0.0) || ((double)base.npc.velocity.X < 0.0 && (double)base.npc.oldVelocity.X > 0.0) || ((double)base.npc.velocity.Y > 0.0 && (double)base.npc.oldVelocity.Y < 0.0) || ((double)base.npc.velocity.Y < 0.0 && (double)base.npc.oldVelocity.Y > 0.0)) && !base.npc.justHit)
			{
				base.npc.netUpdate = true;
			}
			return false;
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
					base.npc.velocity = new Vector2(0f, -10f);
					if (base.npc.timeLeft > 10)
					{
						base.npc.timeLeft = 10;
					}
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/Bosses/VlitchWormHead_Glow");
			SpriteEffects spriteEffects = (base.npc.spriteDirection == -1) ? 0 : 1;
			Vector2 vector;
			vector..ctor((float)texture2D.Width * 0.5f, (float)texture2D.Height * 0.5f);
			Main.spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, null, drawColor, base.npc.rotation, vector, base.npc.scale, 0, 0f);
			spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, spriteEffects, 0f);
			return false;
		}

		private Player player;

		private bool Core1;

		private bool Core2;

		private bool Core3;
	}
}
