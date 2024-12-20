using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Redemption.ChickenArmy;
using Redemption.Projectiles;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class RedeGlobalNPC : GlobalNPC
	{
		public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}

		public override void ResetEffects(NPC npc)
		{
			this.enjoyment = false;
			this.ultraFlames = false;
			this.druidBane = false;
			this.holyFire = false;
			this.bInfection = false;
			this.needleStab = false;
			this.sleepPowder = false;
			this.vendetta = false;
			this.sandDust = false;
			this.badtime = false;
			this.bloodCrystalStab = false;
			this.cursedCrystalStab = false;
			this.blackHeart = false;
		}

		public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
			if (this.enjoyment)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 15;
				if (damage < 2)
				{
					damage = 2;
				}
			}
			if (this.ultraFlames)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 40;
				if (damage < 10)
				{
					damage = 10;
				}
			}
			if (this.druidBane)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 200;
				if (damage < 10)
				{
					damage = 10;
				}
				npc.defense -= 10;
			}
			if (this.holyFire)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 500;
				if (damage < 10)
				{
					damage = 10;
				}
			}
			if (this.bInfection)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 15;
				if (damage < 2)
				{
					damage = 2;
				}
			}
			if (this.needleStab)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				int num = 0;
				for (int i = 0; i < 1000; i++)
				{
					Projectile projectile = Main.projectile[i];
					if (projectile.active && projectile.type == base.mod.ProjectileType<NeedlePro>() && projectile.ai[0] == 1f && projectile.ai[1] == (float)npc.whoAmI)
					{
						num++;
					}
				}
				npc.lifeRegen -= num * 10 * 20;
				if (damage < num * 10)
				{
					damage = num * 10;
				}
			}
			if (this.sleepPowder)
			{
				if (!npc.boss)
				{
					npc.velocity.X = npc.velocity.X * 0.4f;
					npc.velocity.Y = npc.velocity.Y * 0.4f;
				}
				npc.defense -= 25;
			}
			if (this.sandDust)
			{
				npc.defense -= 8;
			}
			if (this.badtime)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 2000;
				if (damage < 1)
				{
					damage = 1;
				}
				if (!npc.boss)
				{
					npc.velocity.X = npc.velocity.X * 0.4f;
					npc.velocity.Y = npc.velocity.Y * 0.4f;
				}
				npc.defense -= 99;
			}
			if (this.bloodCrystalStab)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				int num2 = 0;
				for (int j = 0; j < 1000; j++)
				{
					Projectile projectile2 = Main.projectile[j];
					if (projectile2.active && projectile2.type == base.mod.ProjectileType<BloodBookPro1>() && projectile2.ai[0] == 1f && projectile2.ai[1] == (float)npc.whoAmI)
					{
						num2++;
					}
				}
				npc.lifeRegen -= num2 * 10 * 20;
				if (damage < num2 * 10)
				{
					damage = num2 * 10;
				}
			}
			if (this.cursedCrystalStab)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				int num3 = 0;
				for (int k = 0; k < 1000; k++)
				{
					Projectile projectile3 = Main.projectile[k];
					if (projectile3.active && projectile3.type == base.mod.ProjectileType<CursedBookPro1>() && projectile3.ai[0] == 1f && projectile3.ai[1] == (float)npc.whoAmI)
					{
						num3++;
					}
				}
				npc.lifeRegen -= num3 * 11 * 20;
				if (damage < num3 * 10)
				{
					damage = num3 * 10;
				}
			}
			if (this.blackHeart)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 400;
				if (damage < 2)
				{
					damage = 2;
				}
			}
		}

		public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
		{
			if (this.vendetta)
			{
				npc.AddBuff(20, 200, false);
			}
		}

		public override bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot)
		{
			return (!Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).skeletonFriendly || (npc.type != 77 && npc.type != -49 && npc.type != -51 && npc.type != -53 && npc.type != -47 && npc.type != 449 && npc.type != 450 && npc.type != 451 && npc.type != 452 && npc.type != 566 && npc.type != 567 && npc.type != 481 && npc.type != 201 && npc.type != -15 && npc.type != 202 && npc.type != 203 && npc.type != 21 && npc.type != 324 && npc.type != 110 && npc.type != 323 && npc.type != 293 && npc.type != 291 && npc.type != 322 && npc.type != -48 && npc.type != -50 && npc.type != -52 && npc.type != -46 && npc.type != 292)) && base.CanHitPlayer(npc, target, ref cooldownSlot);
		}

		public override void DrawEffects(NPC npc, ref Color drawColor)
		{
			if (this.enjoyment && Main.rand.Next(4) < 3)
			{
				int num = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 243, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.5f);
				Main.dust[num].noGravity = true;
				Main.dust[num].velocity *= 1.8f;
				Dust dust = Main.dust[num];
				dust.velocity.Y = dust.velocity.Y - 0.5f;
				if (Main.rand.Next(4) == 0)
				{
					Main.dust[num].noGravity = false;
					Main.dust[num].scale *= 0.5f;
				}
			}
			if (this.ultraFlames && Main.rand.Next(3) < 3)
			{
				int num2 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 92, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.5f);
				Main.dust[num2].noGravity = true;
				Main.dust[num2].velocity *= 1.8f;
				Dust dust2 = Main.dust[num2];
				dust2.velocity.Y = dust2.velocity.Y - 0.5f;
				if (Main.rand.Next(4) == 0)
				{
					Main.dust[num2].noGravity = false;
					Main.dust[num2].scale *= 0.5f;
				}
			}
			if (this.druidBane && Main.rand.Next(3) < 3)
			{
				int num3 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 163, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.5f);
				Main.dust[num3].noGravity = true;
				Main.dust[num3].velocity *= 1.8f;
				Dust dust3 = Main.dust[num3];
				dust3.velocity.Y = dust3.velocity.Y - 0.5f;
				if (Main.rand.Next(4) == 0)
				{
					Main.dust[num3].noGravity = false;
					Main.dust[num3].scale *= 0.5f;
				}
			}
			if (this.holyFire && Main.rand.Next(3) < 3)
			{
				int num4 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 64, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 2f);
				Main.dust[num4].noGravity = true;
				Main.dust[num4].velocity *= 1.8f;
				Dust dust4 = Main.dust[num4];
				dust4.velocity.Y = dust4.velocity.Y - 0.5f;
				if (Main.rand.Next(4) == 0)
				{
					Main.dust[num4].noGravity = false;
					Main.dust[num4].scale *= 0.5f;
				}
			}
			if (this.blackHeart && Main.rand.Next(3) < 3)
			{
				int num5 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, base.mod.DustType("VoidFlame"), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1f);
				Main.dust[num5].noGravity = true;
				Main.dust[num5].velocity *= 1.8f;
				Dust dust5 = Main.dust[num5];
				dust5.velocity.Y = dust5.velocity.Y - 0.5f;
				if (Main.rand.Next(4) == 0)
				{
					Main.dust[num5].noGravity = false;
					Main.dust[num5].scale *= 0.5f;
				}
			}
		}

		public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
		{
			if (ChickWorld.chickArmy)
			{
				if (RedeWorld.downedPatientZero)
				{
					spawnRate = 15;
					maxSpawns = 12;
				}
				else
				{
					spawnRate = 25;
				}
			}
			if (NPC.AnyNPCs(base.mod.NPCType("Nebuleus")) || NPC.AnyNPCs(base.mod.NPCType("BigNebuleus")))
			{
				maxSpawns = 0;
			}
		}

		public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
		{
			Player player = Main.player[Main.myPlayer];
			if (spawnInfo.player.GetModPlayer<RedePlayer>(base.mod).ZoneXeno)
			{
				int[] array = new int[]
				{
					base.mod.TileType("RadioactiveIceTile"),
					147
				};
				int[] array2 = new int[]
				{
					base.mod.TileType("RadioactiveSandTile"),
					base.mod.TileType("RadioactiveSandstoneTile"),
					base.mod.TileType("HardenedRadioactiveSandTile")
				};
				if (player.ZoneRockLayerHeight)
				{
					pool.Clear();
					if (Main.hardMode)
					{
						pool.Add(base.mod.NPCType("HazmatSkeleton"), 0.1f);
						pool.Add(base.mod.NPCType("XenomiteGolem"), 0.1f);
						pool.Add(base.mod.NPCType("XenomiteGargantuan"), 0.03f);
						pool.Add(base.mod.NPCType("RadiumDiggerHead"), 0.01f);
						pool.Add(base.mod.NPCType("XenonRoller"), 0.08f);
						pool.Add(base.mod.NPCType("SludgyBoi"), 0.1f);
						pool.Add(base.mod.NPCType("BobTheBlob"), 0.0004f);
						pool.Add(base.mod.NPCType("InfectedGiantBat"), 0.06f);
						pool.Add(base.mod.NPCType("InfectedDiggerHead"), 0.01f);
						if (Enumerable.Contains<int>(array, (int)Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type))
						{
							pool.Add(base.mod.NPCType("GreenPigron"), 0.01f);
							pool.Add(base.mod.NPCType("InfectedSnowFlinx"), 0.05f);
							pool.Add(base.mod.NPCType("SneezyInfectedFlinx"), 0.05f);
						}
						if (Enumerable.Contains<int>(array2, (int)Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type))
						{
							pool.Add(base.mod.NPCType("DecayedGhoul"), 0.15f);
						}
					}
					else
					{
						pool.Add(base.mod.NPCType("InfectedCaveBat"), 0.08f);
						pool.Add(base.mod.NPCType("InfectedGiantWormHead"), 0.01f);
					}
					if (Enumerable.Contains<int>(array, (int)Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type))
					{
						pool.Add(base.mod.NPCType("SpikyRadioactiveSlime"), 0.06f);
					}
					pool.Add(base.mod.NPCType("RadiumBeast"), 0.01f);
				}
				else if (player.ZoneOverworldHeight && !Main.dayTime)
				{
					pool.Clear();
					if (Main.hardMode)
					{
						pool.Add(base.mod.NPCType("HazmatZombie"), 0.1f);
						pool.Add(base.mod.NPCType("XenomiteGolem"), 0.1f);
						pool.Add(base.mod.NPCType("XenomiteGargantuan"), 0.03f);
						pool.Add(base.mod.NPCType("SludgyBoi"), 0.1f);
						pool.Add(base.mod.NPCType("BobTheBlob"), 0.0004f);
						if (Enumerable.Contains<int>(array, (int)Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type))
						{
							pool.Add(base.mod.NPCType("InfectedSnowFlinx"), 0.05f);
							pool.Add(base.mod.NPCType("SneezyInfectedFlinx"), 0.05f);
						}
						if (Enumerable.Contains<int>(array2, (int)Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type))
						{
							if (Main.raining)
							{
								pool.Add(base.mod.NPCType("RadiumRampager"), 0.1f);
								pool.Add(base.mod.NPCType("RadiumDigger2Head"), 0.01f);
							}
							pool.Add(base.mod.NPCType("DecayedGhoul"), 0.14f);
							pool.Add(base.mod.NPCType("InfectedSwarmer"), 0.08f);
							pool.Add(base.mod.NPCType("XenonRoller"), 0.07f);
						}
					}
					else
					{
						pool.Add(base.mod.NPCType("InfectedZombie"), 0.1f);
						pool.Add(base.mod.NPCType("InfectedDemonEye"), 0.1f);
					}
					pool.Add(base.mod.NPCType("RadiumBeast"), 0.01f);
					pool.Add(base.mod.NPCType("RogueTBot"), 0.06f);
					pool.Add(base.mod.NPCType("XenomiteBeast"), 0.02f);
				}
				else if (player.ZoneOverworldHeight && Main.dayTime)
				{
					pool.Clear();
					if (Main.hardMode)
					{
						pool.Add(base.mod.NPCType("BobTheBlob"), 0.0004f);
						if (Enumerable.Contains<int>(array, (int)Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type))
						{
							pool.Add(base.mod.NPCType("InfectedSnowFlinx"), 0.05f);
							pool.Add(base.mod.NPCType("SneezyInfectedFlinx"), 0.05f);
						}
						if (Enumerable.Contains<int>(array2, (int)Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type))
						{
							if (Main.raining)
							{
								pool.Add(base.mod.NPCType("RadiumRampager"), 0.1f);
								pool.Add(base.mod.NPCType("RadiumDigger2Head"), 0.01f);
							}
							pool.Add(base.mod.NPCType("DecayedGhoul"), 0.12f);
							pool.Add(base.mod.NPCType("InfectedSwarmer"), 0.05f);
							pool.Add(base.mod.NPCType("XenonRoller"), 0.06f);
						}
					}
					pool.Add(base.mod.NPCType("XenomiteBeast"), 0.02f);
					pool.Add(base.mod.NPCType("RadiumBeast"), 0.01f);
					pool.Add(base.mod.NPCType("RadioactiveSlime"), 0.06f);
					pool.Add(base.mod.NPCType("NuclearSlime"), 0.002f);
				}
				else if (player.ZoneDirtLayerHeight)
				{
					pool.Clear();
				}
			}
			if (spawnInfo.player.GetModPlayer<RedePlayer>(base.mod).ZoneLab)
			{
				pool.Clear();
				if (Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
				{
					if (NPC.downedMoonlord)
					{
						pool.Add(base.mod.NPCType("Stage2Scientist"), 0.01f);
					}
					pool.Add(base.mod.NPCType("InfectionHive"), 0.05f);
					pool.Add(base.mod.NPCType("SludgyBoi2"), 0.1f);
					pool.Add(base.mod.NPCType("WalterInfected"), 0.1f);
				}
			}
			if (ChickWorld.chickArmy)
			{
				pool.Clear();
				if (RedeWorld.downedPatientZero)
				{
					pool.Add(base.mod.NPCType("GreatChickenWarrior"), 20f);
					pool.Add(base.mod.NPCType("ChickenMan"), 30f);
					pool.Add(base.mod.NPCType("ShieldedChickenMan"), 30f);
					pool.Add(base.mod.NPCType("ChickenCavalry"), 27f);
					pool.Add(base.mod.NPCType("Chicken"), 30f);
					if (!NPC.AnyNPCs(base.mod.NPCType("TrojanChicken")) && ChickWorld.ChickPoints2 >= 5 && ChickWorld.ChickPoints2 <= 150 && !NPC.AnyNPCs(base.mod.NPCType("RoosterKing")))
					{
						pool.Add(base.mod.NPCType("TrojanChicken"), 10f);
					}
					if (ChickWorld.ChickPoints2 >= 15)
					{
						pool.Add(base.mod.NPCType("ChickenBallista"), 10f);
					}
					if (ChickWorld.ChickPoints2 >= 30)
					{
						pool.Add(base.mod.NPCType("ChickmanChickromancer"), 15f);
						pool.Add(base.mod.NPCType("ChickmanArchmage"), 15f);
					}
					pool.Add(base.mod.NPCType("BomberChicken"), 20f);
					if (ChickWorld.ChickPoints2 >= 175 && !NPC.AnyNPCs(base.mod.NPCType("RoosterKing")))
					{
						pool.Add(base.mod.NPCType("RoosterKing"), 90f);
						return;
					}
				}
				else
				{
					pool.Add(base.mod.NPCType("GreatChickenWarrior"), 20f);
					pool.Add(base.mod.NPCType("ChickenMan"), 30f);
					pool.Add(base.mod.NPCType("ShieldedChickenMan"), 30f);
					pool.Add(base.mod.NPCType("ChickenCavalry"), 27f);
					pool.Add(base.mod.NPCType("Chicken"), 30f);
					if (!NPC.AnyNPCs(base.mod.NPCType("TrojanChicken")) && ChickWorld.ChickPoints2 >= 25)
					{
						pool.Add(base.mod.NPCType("TrojanChicken"), 10f);
					}
				}
			}
		}

		public bool enjoyment;

		public bool ultraFlames;

		public bool druidBane;

		public bool holyFire;

		public bool bInfection;

		public bool needleStab;

		public bool sleepPowder;

		public bool vendetta;

		public bool sandDust;

		public bool badtime;

		public bool bloodCrystalStab;

		public bool cursedCrystalStab;

		public bool blackHeart;
	}
}
