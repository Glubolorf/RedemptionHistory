using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Dusts;
using Redemption.Items;
using Redemption.Items.Armor;
using Redemption.Items.Placeable;
using Redemption.Items.Weapons;
using Redemption.NPCs.Bosses.OmegaOblit;
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
			base.DisplayName.SetDefault("Vlitch Gigapede");
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
			this.bossBag = ModContent.ItemType<VlitchGigipedeBag>();
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
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<VlitchTrophy>(), 1, false, 0, false, false);
			}
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
				return;
			}
			if (Main.rand.Next(14) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<GirusMask>(), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<CorruptedRocketLauncher>(), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<CorruptedDoubleRifle>(), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<CorruptedXenomite>(), Main.rand.Next(18, 28), false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<CorruptedStarliteBar>(), Main.rand.Next(15, 20), false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<VlitchScale>(), Main.rand.Next(25, 35), false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<VlitchBattery>(), Main.rand.Next(2, 4), false, 0, false, false);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 499;
			if (!RedeWorld.downedVlitch2)
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
								Main.NewText("<Chalice of Alignment> The second Vlitch Overlord is down! Only 1 more... I think?", Color.DarkGoldenrod, false);
							}
						}
						CombatText.NewText(player2.getRect(), Color.Gold, "+1", true, false);
					}
				}
			}
			RedeWorld.downedVlitch2 = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			if (!RedeWorld.girusTalk2 && !NPC.AnyNPCs(ModContent.NPCType<VlitchCleaver>()) && !NPC.AnyNPCs(ModContent.NPCType<OO>()) && !RedeWorld.girusCloaked && !RedeConfigClient.Instance.NoBossText)
			{
				Projectile.NewProjectile(new Vector2(base.npc.position.X, base.npc.position.Y), new Vector2(0f, 0f), ModContent.ProjectileType<GirusTalking2>(), 0, 0f, 255, 0f, 0f);
			}
			if (!Main.dedServ)
			{
				Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/DistortedRoar").WithVolume(0.7f).WithPitchVariance(0.1f), -1, -1);
			}
		}

		public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (projectile.penetrate != 1)
			{
				for (int i = 0; i < 200; i++)
				{
					if (Main.npc[i].active && (Main.npc[i].whoAmI == base.npc.realLife || (Main.npc[i].realLife >= 0 && Main.npc[i].realLife == base.npc.realLife)))
					{
						Main.npc[i].immune[projectile.owner] = 10;
					}
				}
				damage = (int)((float)damage * 0.44f);
			}
			if (projectile.ranged && (projectile.width < 20 || projectile.height < 20) && Main.rand.Next(2) == 0)
			{
				if (!Main.dedServ)
				{
					if (Main.rand.Next(3) == 0)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/BulletBounce1").WithVolume(0.4f).WithPitchVariance(0.1f), -1, -1);
					}
					else if (Main.rand.Next(3) == 1)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/BulletBounce2").WithVolume(0.4f).WithPitchVariance(0.1f), -1, -1);
					}
					else
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/BulletBounce3").WithVolume(0.4f).WithPitchVariance(0.1f), -1, -1);
					}
				}
				damage = (int)((float)damage * 0.5f);
				if (projectile.penetrate == 1)
				{
					projectile.penetrate = 2;
				}
				projectile.velocity.X = -projectile.velocity.X + Utils.NextFloat(Main.rand, -3f, 3f);
				projectile.velocity.Y = -projectile.velocity.Y + Utils.NextFloat(Main.rand, -3f, 3f);
				projectile.friendly = false;
				projectile.hostile = false;
			}
		}

		public override bool PreAI()
		{
			if (!this.title)
			{
				Redemption.ShowTitle(base.npc, 11);
				this.title = true;
			}
			if (Main.dayTime)
			{
				base.npc.timeLeft = 0;
				NPC npc = base.npc;
				npc.position.Y = npc.position.Y - 300f;
			}
			if (NPC.AnyNPCs(ModContent.NPCType<VlitchCore1>()))
			{
				base.npc.dontTakeDamage = true;
			}
			if (NPC.AnyNPCs(ModContent.NPCType<VlitchCore2>()))
			{
				base.npc.dontTakeDamage = true;
			}
			if (NPC.AnyNPCs(ModContent.NPCType<VlitchCore3>()))
			{
				base.npc.dontTakeDamage = true;
			}
			if (!NPC.AnyNPCs(ModContent.NPCType<VlitchCore1>()) && !NPC.AnyNPCs(ModContent.NPCType<VlitchCore2>()) && !NPC.AnyNPCs(ModContent.NPCType<VlitchCore3>()))
			{
				base.npc.dontTakeDamage = false;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().omegaPower)
			{
				base.npc.ai[1] += 1f;
				if (base.npc.ai[1] == 600f && !RedeConfigClient.Instance.NoBossText)
				{
					Main.NewText("The probes aren't attacking you!?", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
				}
				if (base.npc.ai[1] == 800f && !RedeConfigClient.Instance.NoBossText)
				{
					Main.NewText("No matter... The corrupted worms are smart enough.", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
				}
			}
			if (base.npc.life > 0 && !NPC.AnyNPCs(ModContent.NPCType<VlitchWormTail>()))
			{
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] == 60f && !RedeConfigClient.Instance.NoBossText)
				{
					Main.NewText("Huh? My tail despawned!?", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
				}
				if (base.npc.ai[2] == 230f && !RedeConfigClient.Instance.NoBossText)
				{
					Main.NewText("How am I suppose to summon my minions without my tail!?", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
				}
				if (base.npc.ai[2] == 530f && !RedeConfigClient.Instance.NoBossText)
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
				if (!RedeConfigClient.Instance.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Color.Red, "[DEPLOYING CORE]", true, false);
				}
				if (!RedeConfigClient.Instance.NoBossText)
				{
					Main.NewText("[DEPLOYING CORE]", Color.Red.R, Color.Red.G, Color.Red.B, false);
				}
				int Minion = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<VlitchCore1>(), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[Minion].netUpdate = true;
				base.npc.ai[3] = 0f;
				this.Core1 = true;
			}
			if (base.npc.life <= (int)((float)base.npc.lifeMax * 0.5f) && !this.Core2)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] >= 60f)
				{
					if (!RedeConfigClient.Instance.NoCombatText)
					{
						CombatText.NewText(base.npc.getRect(), Color.Red, "[DEPLOYING CORE]", true, false);
					}
					if (!RedeConfigClient.Instance.NoBossText)
					{
						Main.NewText("[DEPLOYING CORE]", Color.Red.R, Color.Red.G, Color.Red.B, false);
					}
					int Minion2 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<VlitchCore2>(), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[Minion2].netUpdate = true;
					base.npc.ai[3] = 0f;
					this.Core2 = true;
				}
			}
			if (base.npc.life <= (int)((float)base.npc.lifeMax * 0.25f) && !this.Core3)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 1f && !RedeConfigClient.Instance.NoBossText)
				{
					Main.NewText("I may have miscalculated...", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
				}
				if (base.npc.ai[3] >= 120f)
				{
					if (!RedeConfigClient.Instance.NoCombatText)
					{
						CombatText.NewText(base.npc.getRect(), Color.Red, "[DEPLOYING CORE]", true, false);
					}
					if (!RedeConfigClient.Instance.NoBossText)
					{
						Main.NewText("[DEPLOYING CORE]", Color.Red.R, Color.Red.G, Color.Red.B, false);
					}
					int Minion3 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<VlitchCore3>(), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[Minion3].netUpdate = true;
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
					int latestNPC = base.npc.whoAmI;
					int randomWormLength = 16;
					for (int i = 0; i < randomWormLength; i++)
					{
						latestNPC = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<VlitchWormBody>(), base.npc.whoAmI, 0f, (float)latestNPC, 0f, 0f, 255);
						Main.npc[latestNPC].realLife = base.npc.whoAmI;
						Main.npc[latestNPC].ai[3] = (float)base.npc.whoAmI;
					}
					latestNPC = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<VlitchWormTail>(), base.npc.whoAmI, 0f, (float)latestNPC, 0f, 0f, 255);
					Main.npc[latestNPC].realLife = base.npc.whoAmI;
					Main.npc[latestNPC].ai[3] = (float)base.npc.whoAmI;
					base.npc.ai[0] = 1f;
					base.npc.netUpdate = true;
				}
				if (Main.rand.Next(2) == 0)
				{
					Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, ModContent.DustType<VlitchFlame>(), 0f, 0f, 0, default(Color), 1f);
				}
			}
			int minTilePosX = (int)((double)base.npc.position.X / 16.0) - 1;
			int maxTilePosX = (int)((double)(base.npc.position.X + (float)base.npc.width) / 16.0) + 2;
			int minTilePosY = (int)((double)base.npc.position.Y / 16.0) - 1;
			int maxTilePosY = (int)((double)(base.npc.position.Y + (float)base.npc.height) / 16.0) + 2;
			if (minTilePosX < 0)
			{
				minTilePosX = 0;
			}
			if (maxTilePosX > Main.maxTilesX)
			{
				maxTilePosX = Main.maxTilesX;
			}
			if (minTilePosY < 0)
			{
				minTilePosY = 0;
			}
			if (maxTilePosY > Main.maxTilesY)
			{
				maxTilePosY = Main.maxTilesY;
			}
			bool collision = false;
			for (int j = minTilePosX; j < maxTilePosX; j++)
			{
				for (int k = minTilePosY; k < maxTilePosY; k++)
				{
					if (Main.tile[j, k] != null && ((Main.tile[j, k].nactive() && (Main.tileSolid[(int)Main.tile[j, k].type] || (Main.tileSolidTop[(int)Main.tile[j, k].type] && Main.tile[j, k].frameY == 0))) || Main.tile[j, k].liquid > 64))
					{
						Vector2 vector2;
						vector2.X = (float)(j * 16);
						vector2.Y = (float)(k * 16);
						if (base.npc.position.X + (float)base.npc.width > vector2.X && (double)base.npc.position.X < (double)vector2.X + 16.0 && (double)(base.npc.position.Y + (float)base.npc.height) > (double)vector2.Y && (double)base.npc.position.Y < (double)vector2.Y + 16.0)
						{
							collision = true;
							if (Main.rand.Next(100) == 0 && Main.tile[j, k].nactive())
							{
								WorldGen.KillTile(j, k, true, true, false);
							}
						}
					}
				}
			}
			if (!collision)
			{
				Rectangle rectangle = new Rectangle((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height);
				int maxDistance = 1000;
				bool playerCollision = true;
				for (int index = 0; index < 255; index++)
				{
					if (Main.player[index].active)
					{
						Rectangle rectangle2 = new Rectangle((int)Main.player[index].position.X - maxDistance, (int)Main.player[index].position.Y - maxDistance, maxDistance * 2, maxDistance * 2);
						if (rectangle.Intersects(rectangle2))
						{
							playerCollision = false;
							break;
						}
					}
				}
				if (playerCollision)
				{
					collision = true;
				}
			}
			float speed = 20f;
			float acceleration = 0.38f;
			Vector2 npcCenter = new Vector2(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
			float targetXPos = Main.player[base.npc.target].position.X + (float)(Main.player[base.npc.target].width / 2);
			double num2 = (double)(Main.player[base.npc.target].position.Y + (float)(Main.player[base.npc.target].height / 2));
			float targetRoundedPosX = (float)((int)((double)targetXPos / 16.0) * 16);
			float num3 = (float)((int)(num2 / 16.0) * 16);
			npcCenter.X = (float)((int)((double)npcCenter.X / 16.0) * 16);
			npcCenter.Y = (float)((int)((double)npcCenter.Y / 16.0) * 16);
			float dirX = targetRoundedPosX - npcCenter.X;
			float dirY = num3 - npcCenter.Y;
			float length = (float)Math.Sqrt((double)(dirX * dirX + dirY * dirY));
			if (!collision)
			{
				base.npc.TargetClosest(true);
				base.npc.velocity.Y = base.npc.velocity.Y + 0.11f;
				if (base.npc.velocity.Y > speed)
				{
					base.npc.velocity.Y = speed;
				}
				if ((double)(Math.Abs(base.npc.velocity.X) + Math.Abs(base.npc.velocity.Y)) < (double)speed * 0.4)
				{
					if ((double)base.npc.velocity.X < 0.0)
					{
						base.npc.velocity.X = base.npc.velocity.X - acceleration * 1.1f;
					}
					else
					{
						base.npc.velocity.X = base.npc.velocity.X + acceleration * 1.1f;
					}
				}
				else if (base.npc.velocity.Y == speed)
				{
					if (base.npc.velocity.X < dirX)
					{
						base.npc.velocity.X = base.npc.velocity.X + acceleration;
					}
					else if (base.npc.velocity.X > dirX)
					{
						base.npc.velocity.X = base.npc.velocity.X - acceleration;
					}
				}
				else if ((double)base.npc.velocity.Y > 4.0)
				{
					if ((double)base.npc.velocity.X < 0.0)
					{
						base.npc.velocity.X = base.npc.velocity.X + acceleration * 0.9f;
					}
					else
					{
						base.npc.velocity.X = base.npc.velocity.X - acceleration * 0.9f;
					}
				}
			}
			else
			{
				if (base.npc.soundDelay == 0)
				{
					float num = length / 40f;
					if ((double)num < 10.0)
					{
						num = 10f;
					}
					if ((double)num > 20.0)
					{
						num = 20f;
					}
					base.npc.soundDelay = (int)num;
					Main.PlaySound(15, (int)base.npc.position.X, (int)base.npc.position.Y, 1, 1f, 0f);
				}
				float absDirX = Math.Abs(dirX);
				float absDirY = Math.Abs(dirY);
				float newSpeed = speed / length;
				dirX *= newSpeed;
				dirY *= newSpeed;
				if (((double)base.npc.velocity.X > 0.0 && (double)dirX > 0.0) || ((double)base.npc.velocity.X < 0.0 && (double)dirX < 0.0) || ((double)base.npc.velocity.Y > 0.0 && (double)dirY > 0.0) || ((double)base.npc.velocity.Y < 0.0 && (double)dirY < 0.0))
				{
					if (base.npc.velocity.X < dirX)
					{
						base.npc.velocity.X = base.npc.velocity.X + acceleration;
					}
					else if (base.npc.velocity.X > dirX)
					{
						base.npc.velocity.X = base.npc.velocity.X - acceleration;
					}
					if (base.npc.velocity.Y < dirY)
					{
						base.npc.velocity.Y = base.npc.velocity.Y + acceleration;
					}
					else if (base.npc.velocity.Y > dirY)
					{
						base.npc.velocity.Y = base.npc.velocity.Y - acceleration;
					}
					if ((double)Math.Abs(dirY) < (double)speed * 0.2 && (((double)base.npc.velocity.X > 0.0 && (double)dirX < 0.0) || ((double)base.npc.velocity.X < 0.0 && (double)dirX > 0.0)))
					{
						if ((double)base.npc.velocity.Y > 0.0)
						{
							base.npc.velocity.Y = base.npc.velocity.Y + acceleration * 2f;
						}
						else
						{
							base.npc.velocity.Y = base.npc.velocity.Y - acceleration * 2f;
						}
					}
					if ((double)Math.Abs(dirX) < (double)speed * 0.2 && (((double)base.npc.velocity.Y > 0.0 && (double)dirY < 0.0) || ((double)base.npc.velocity.Y < 0.0 && (double)dirY > 0.0)))
					{
						if ((double)base.npc.velocity.X > 0.0)
						{
							base.npc.velocity.X = base.npc.velocity.X + acceleration * 2f;
						}
						else
						{
							base.npc.velocity.X = base.npc.velocity.X - acceleration * 2f;
						}
					}
				}
				else if (absDirX > absDirY)
				{
					if (base.npc.velocity.X < dirX)
					{
						base.npc.velocity.X = base.npc.velocity.X + acceleration * 1.1f;
					}
					else if (base.npc.velocity.X > dirX)
					{
						base.npc.velocity.X = base.npc.velocity.X - acceleration * 1.1f;
					}
					if ((double)(Math.Abs(base.npc.velocity.X) + Math.Abs(base.npc.velocity.Y)) < (double)speed * 0.5)
					{
						if ((double)base.npc.velocity.Y > 0.0)
						{
							base.npc.velocity.Y = base.npc.velocity.Y + acceleration;
						}
						else
						{
							base.npc.velocity.Y = base.npc.velocity.Y - acceleration;
						}
					}
				}
				else
				{
					if (base.npc.velocity.Y < dirY)
					{
						base.npc.velocity.Y = base.npc.velocity.Y + acceleration * 1.1f;
					}
					else if (base.npc.velocity.Y > dirY)
					{
						base.npc.velocity.Y = base.npc.velocity.Y - acceleration * 1.1f;
					}
					if ((double)(Math.Abs(base.npc.velocity.X) + Math.Abs(base.npc.velocity.Y)) < (double)speed * 0.5)
					{
						if ((double)base.npc.velocity.X > 0.0)
						{
							base.npc.velocity.X = base.npc.velocity.X + acceleration;
						}
						else
						{
							base.npc.velocity.X = base.npc.velocity.X - acceleration;
						}
					}
				}
			}
			base.npc.rotation = (float)Math.Atan2((double)base.npc.velocity.Y, (double)base.npc.velocity.X) + 1.57f;
			if (collision)
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
					return;
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D glowMask = base.mod.GetTexture("NPCs/Bosses/VlitchWormHead_Glow");
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
			Main.spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, null, drawColor, base.npc.rotation, origin, base.npc.scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(glowMask, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			return false;
		}

		private Player player;

		private bool Core1;

		private bool Core2;

		private bool Core3;

		private bool title;
	}
}
