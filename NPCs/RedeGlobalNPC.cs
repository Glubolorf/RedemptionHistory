using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Redemption.ChickenArmy;
using Redemption.Projectiles;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
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
			this.chained = false;
			this.bileDebuff = false;
			this.bioweaponDebuff = false;
			this.necroGouge = false;
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
				int needleCount = 0;
				for (int i = 0; i < 1000; i++)
				{
					Projectile p = Main.projectile[i];
					if (p.active && p.type == ModContent.ProjectileType<NeedlePro>() && p.ai[0] == 1f && p.ai[1] == (float)npc.whoAmI)
					{
						needleCount++;
					}
				}
				npc.lifeRegen -= needleCount * 10 * 20;
				if (damage < needleCount * 10)
				{
					damage = needleCount * 10;
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
				npc.lifeRegen -= 4000;
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
				int crystalCount = 0;
				for (int j = 0; j < 1000; j++)
				{
					Projectile p2 = Main.projectile[j];
					if (p2.active && p2.type == ModContent.ProjectileType<BloodBookPro1>() && p2.ai[0] == 1f && p2.ai[1] == (float)npc.whoAmI)
					{
						crystalCount++;
					}
				}
				npc.lifeRegen -= crystalCount * 10 * 20;
				if (damage < crystalCount * 10)
				{
					damage = crystalCount * 10;
				}
			}
			if (this.cursedCrystalStab)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				int crystalCount2 = 0;
				for (int k = 0; k < 1000; k++)
				{
					Projectile p3 = Main.projectile[k];
					if (p3.active && p3.type == ModContent.ProjectileType<CursedBookPro1>() && p3.ai[0] == 1f && p3.ai[1] == (float)npc.whoAmI)
					{
						crystalCount2++;
					}
				}
				npc.lifeRegen -= crystalCount2 * 11 * 20;
				if (damage < crystalCount2 * 10)
				{
					damage = crystalCount2 * 10;
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
			if (this.chained && !npc.boss)
			{
				npc.velocity.X = 0f;
				npc.velocity.Y = 0f;
			}
			if (this.bileDebuff)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 5;
				npc.defense -= 30;
			}
			if (this.bioweaponDebuff)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 12;
				if (damage < 2)
				{
					damage = 2;
				}
			}
		}

		public override void HitEffect(NPC npc, int hitDirection, double damage)
		{
			if (this.bioweaponDebuff && npc.life <= 0)
			{
				Main.PlaySound(SoundID.Item14, npc.position);
				for (int i = 0; i < 10; i++)
				{
					int dustIndex3 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 31, 0f, 0f, 100, default(Color), 5f);
					Main.dust[dustIndex3].velocity *= 1.4f;
				}
				for (int j = 0; j < 20; j++)
				{
					int dustIndex4 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 6, 0f, 0f, 100, default(Color), 3f);
					Main.dust[dustIndex4].noGravity = true;
					Main.dust[dustIndex4].velocity *= 5f;
					dustIndex4 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 6, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex4].velocity *= 3f;
				}
				for (int g = 0; g < 2; g++)
				{
					int goreIndex = Gore.NewGore(new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
					goreIndex = Gore.NewGore(new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				}
			}
			if (this.necroGouge && npc.life <= 0)
			{
				Main.PlaySound(SoundID.NPCDeath6, npc.position);
				for (int k = 0; k < 30; k++)
				{
					int dustIndex5 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, base.mod.DustType("VoidFlame"), 0f, 0f, 100, default(Color), 3f);
					Main.dust[dustIndex5].velocity *= 1.8f;
				}
				Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 9)), base.mod.ProjectileType("MiniDarkSoulPro"), 5, 2f, Main.myPlayer, 0f, 0f);
				Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 9)), base.mod.ProjectileType("MiniDarkSoulPro"), 5, 2f, Main.myPlayer, 0f, 0f);
				if (Main.rand.Next(2) == 0)
				{
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 9)), base.mod.ProjectileType("MiniDarkSoulPro"), 5, 2f, Main.myPlayer, 0f, 0f);
				}
				if (Main.rand.Next(2) == 0)
				{
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 9)), base.mod.ProjectileType("MiniDarkSoulPro"), 5, 2f, Main.myPlayer, 0f, 0f);
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
			return (!Main.LocalPlayer.GetModPlayer<RedePlayer>().skeletonFriendly || (npc.type != 77 && npc.type != -49 && npc.type != -51 && npc.type != -53 && npc.type != -47 && npc.type != 449 && npc.type != 450 && npc.type != 451 && npc.type != 452 && npc.type != 566 && npc.type != 567 && npc.type != 481 && npc.type != 201 && npc.type != -15 && npc.type != 202 && npc.type != 203 && npc.type != 21 && npc.type != 324 && npc.type != 110 && npc.type != 323 && npc.type != 293 && npc.type != 291 && npc.type != 322 && npc.type != -48 && npc.type != -50 && npc.type != -52 && npc.type != -46 && npc.type != 292)) && base.CanHitPlayer(npc, target, ref cooldownSlot);
		}

		public override void DrawEffects(NPC npc, ref Color drawColor)
		{
			if (this.enjoyment && Main.rand.Next(4) < 3)
			{
				int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 243, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.5f);
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity *= 1.8f;
				Dust dust10 = Main.dust[dust];
				dust10.velocity.Y = dust10.velocity.Y - 0.5f;
				if (Main.rand.Next(4) == 0)
				{
					Main.dust[dust].noGravity = false;
					Main.dust[dust].scale *= 0.5f;
				}
			}
			if (this.ultraFlames && Main.rand.Next(3) < 3)
			{
				int dust2 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 92, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.5f);
				Main.dust[dust2].noGravity = true;
				Main.dust[dust2].velocity *= 1.8f;
				Dust dust11 = Main.dust[dust2];
				dust11.velocity.Y = dust11.velocity.Y - 0.5f;
				if (Main.rand.Next(4) == 0)
				{
					Main.dust[dust2].noGravity = false;
					Main.dust[dust2].scale *= 0.5f;
				}
			}
			if (this.druidBane && Main.rand.Next(3) < 3)
			{
				int dust3 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 163, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.5f);
				Main.dust[dust3].noGravity = true;
				Main.dust[dust3].velocity *= 1.8f;
				Dust dust12 = Main.dust[dust3];
				dust12.velocity.Y = dust12.velocity.Y - 0.5f;
				if (Main.rand.Next(4) == 0)
				{
					Main.dust[dust3].noGravity = false;
					Main.dust[dust3].scale *= 0.5f;
				}
			}
			if (this.holyFire && Main.rand.Next(3) < 3)
			{
				int dust4 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 64, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 2f);
				Main.dust[dust4].noGravity = true;
				Main.dust[dust4].velocity *= 1.8f;
				Dust dust13 = Main.dust[dust4];
				dust13.velocity.Y = dust13.velocity.Y - 0.5f;
				if (Main.rand.Next(4) == 0)
				{
					Main.dust[dust4].noGravity = false;
					Main.dust[dust4].scale *= 0.5f;
				}
			}
			if (this.blackHeart && Main.rand.Next(3) < 3)
			{
				int dust5 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, base.mod.DustType("VoidFlame"), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1f);
				Main.dust[dust5].noGravity = true;
				Main.dust[dust5].velocity *= 1.8f;
				Dust dust14 = Main.dust[dust5];
				dust14.velocity.Y = dust14.velocity.Y - 0.5f;
				if (Main.rand.Next(4) == 0)
				{
					Main.dust[dust5].noGravity = false;
					Main.dust[dust5].scale *= 0.5f;
				}
			}
			if (this.bileDebuff && Main.rand.Next(4) < 3)
			{
				int dust6 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 74, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.5f);
				Main.dust[dust6].noGravity = true;
				Main.dust[dust6].velocity *= 1.8f;
				Dust dust15 = Main.dust[dust6];
				dust15.velocity.Y = dust15.velocity.Y - 0.5f;
				if (Main.rand.Next(4) == 0)
				{
					Main.dust[dust6].noGravity = false;
					Main.dust[dust6].scale *= 0.5f;
				}
			}
			if (this.bileDebuff)
			{
				if (Main.rand.Next(15) < 3)
				{
					int dust7 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 74, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.5f);
					Main.dust[dust7].noGravity = true;
					Main.dust[dust7].velocity *= 1.8f;
					Dust dust16 = Main.dust[dust7];
					dust16.velocity.Y = dust16.velocity.Y - 0.5f;
					if (Main.rand.Next(4) == 0)
					{
						Main.dust[dust7].noGravity = false;
						Main.dust[dust7].scale *= 0.5f;
					}
				}
				if (Main.rand.Next(10) < 3)
				{
					int dust8 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 31, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.5f);
					Main.dust[dust8].noGravity = true;
					Main.dust[dust8].velocity *= 1.8f;
					Dust dust17 = Main.dust[dust8];
					dust17.velocity.Y = dust17.velocity.Y - 0.5f;
					if (Main.rand.Next(4) == 0)
					{
						Main.dust[dust8].noGravity = false;
						Main.dust[dust8].scale *= 0.5f;
					}
				}
			}
			if (this.necroGouge && Main.rand.Next(9) < 3)
			{
				int dust9 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 5, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.5f);
				Main.dust[dust9].noGravity = true;
				Main.dust[dust9].velocity *= 1.8f;
				Dust dust18 = Main.dust[dust9];
				dust18.velocity.Y = dust18.velocity.Y - 0.5f;
				if (Main.rand.Next(4) == 0)
				{
					Main.dust[dust9].noGravity = false;
					Main.dust[dust9].scale *= 0.5f;
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
					spawnRate = 20;
				}
			}
			if (NPC.AnyNPCs(base.mod.NPCType("Nebuleus")) || NPC.AnyNPCs(base.mod.NPCType("BigNebuleus")))
			{
				maxSpawns = 0;
			}
		}

		public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.player.GetModPlayer<RedePlayer>().ZoneXeno || spawnInfo.player.GetModPlayer<RedePlayer>().ZoneEvilXeno)
			{
				int[] TileArray2 = new int[]
				{
					base.mod.TileType("RadioactiveIceTile"),
					147
				};
				int[] TileArray3 = new int[]
				{
					base.mod.TileType("RadioactiveSandTile"),
					base.mod.TileType("RadioactiveSandstoneTile"),
					base.mod.TileType("HardenedRadioactiveSandTile")
				};
				int[] TileArray4 = new int[]
				{
					base.mod.TileType("DeadGrassTileCorruption"),
					base.mod.TileType("IrradiatedEbonstoneTile")
				};
				int[] TileArray5 = new int[]
				{
					base.mod.TileType("DeadGrassTileCrimson"),
					base.mod.TileType("IrradiatedCrimstoneTile")
				};
				if (spawnInfo.player.ZoneRockLayerHeight)
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
						if (Enumerable.Contains<int>(TileArray2, (int)Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type))
						{
							pool.Add(base.mod.NPCType("GreenPigron"), 0.01f);
							pool.Add(base.mod.NPCType("InfectedSnowFlinx"), 0.05f);
							pool.Add(base.mod.NPCType("SneezyInfectedFlinx"), 0.05f);
						}
						if (Enumerable.Contains<int>(TileArray3, (int)Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type))
						{
							pool.Add(base.mod.NPCType("DecayedGhoul"), 0.15f);
						}
						if (Enumerable.Contains<int>(TileArray4, (int)Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type))
						{
							pool.Add(base.mod.NPCType("IrradiatedSpear"), 0.05f);
							pool.Add(base.mod.NPCType("IrradiatedWorldFeederHead"), 0.005f);
							pool.Add(base.mod.NPCType("RadioactiveSlimer"), 0.12f);
							pool.Add(base.mod.NPCType("Injector"), 0.1f);
						}
						if (Enumerable.Contains<int>(TileArray5, (int)Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type))
						{
							pool.Add(base.mod.NPCType("IrradiatedSpear"), 0.05f);
							pool.Add(base.mod.NPCType("BileBoomer"), 0.06f);
							pool.Add(base.mod.NPCType("Superbug"), 0.08f);
						}
					}
					else
					{
						pool.Add(base.mod.NPCType("InfectedCaveBat"), 0.08f);
						pool.Add(base.mod.NPCType("InfectedGiantWormHead"), 0.01f);
					}
					if (Enumerable.Contains<int>(TileArray2, (int)Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type))
					{
						pool.Add(base.mod.NPCType("SpikyRadioactiveSlime"), 0.06f);
					}
				}
				else if (spawnInfo.player.ZoneOverworldHeight && !Main.dayTime)
				{
					pool.Clear();
					if (Main.hardMode)
					{
						pool.Add(base.mod.NPCType("HazmatZombie"), 0.1f);
						pool.Add(base.mod.NPCType("XenomiteGolem"), 0.1f);
						pool.Add(base.mod.NPCType("XenomiteGargantuan"), 0.03f);
						pool.Add(base.mod.NPCType("SludgyBoi"), 0.1f);
						pool.Add(base.mod.NPCType("BobTheBlob"), 0.0004f);
						if (Enumerable.Contains<int>(TileArray2, (int)Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type))
						{
							pool.Add(base.mod.NPCType("InfectedSnowFlinx"), 0.05f);
							pool.Add(base.mod.NPCType("SneezyInfectedFlinx"), 0.05f);
						}
						if (Enumerable.Contains<int>(TileArray3, (int)Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type))
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
						if (Enumerable.Contains<int>(TileArray4, (int)Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type))
						{
							pool.Add(base.mod.NPCType("Injector"), 0.1f);
							pool.Add(base.mod.NPCType("NerveParasite"), 0.15f);
							pool.Add(base.mod.NPCType("IrradiatedWorldFeederHead"), 0.004f);
							pool.Add(base.mod.NPCType("RadioactiveSlimer"), 0.1f);
						}
						if (Enumerable.Contains<int>(TileArray5, (int)Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type))
						{
							pool.Add(base.mod.NPCType("BloatedFaceMonster"), 0.15f);
							pool.Add(base.mod.NPCType("NerveParasite"), 0.15f);
							pool.Add(base.mod.NPCType("Xenoling"), 0.1f);
						}
					}
					else
					{
						pool.Add(base.mod.NPCType("InfectedZombie"), 0.1f);
						pool.Add(base.mod.NPCType("InfectedDemonEye"), 0.1f);
					}
					pool.Add(base.mod.NPCType("RogueTBot"), 0.06f);
					pool.Add(base.mod.NPCType("XenomiteBeast"), 0.02f);
				}
				else if (spawnInfo.player.ZoneOverworldHeight && Main.dayTime)
				{
					pool.Clear();
					if (Main.hardMode)
					{
						pool.Add(base.mod.NPCType("BobTheBlob"), 0.0004f);
						if (Enumerable.Contains<int>(TileArray2, (int)Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type))
						{
							pool.Add(base.mod.NPCType("InfectedSnowFlinx"), 0.05f);
							pool.Add(base.mod.NPCType("SneezyInfectedFlinx"), 0.05f);
						}
						if (Enumerable.Contains<int>(TileArray3, (int)Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type))
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
						if (Enumerable.Contains<int>(TileArray4, (int)Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type))
						{
							pool.Add(base.mod.NPCType("Injector"), 0.1f);
							pool.Add(base.mod.NPCType("NerveParasite"), 0.15f);
							pool.Add(base.mod.NPCType("IrradiatedWorldFeederHead"), 0.004f);
							pool.Add(base.mod.NPCType("RadioactiveSlimer"), 0.1f);
						}
						if (Enumerable.Contains<int>(TileArray5, (int)Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type))
						{
							pool.Add(base.mod.NPCType("BloatedFaceMonster"), 0.15f);
							pool.Add(base.mod.NPCType("NerveParasite"), 0.15f);
							pool.Add(base.mod.NPCType("Xenoling"), 0.1f);
						}
					}
					pool.Add(base.mod.NPCType("XenomiteBeast"), 0.02f);
					pool.Add(base.mod.NPCType("RadioactiveSlime"), 0.06f);
					pool.Add(base.mod.NPCType("NuclearSlime"), 0.002f);
					pool.Add(base.mod.NPCType("InfectedChicken"), 0.02f);
				}
				else if (spawnInfo.player.ZoneDirtLayerHeight)
				{
					pool.Clear();
				}
			}
			if (spawnInfo.player.GetModPlayer<RedePlayer>().ZoneLab)
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

		public bool chained;

		public bool bileDebuff;

		public bool bioweaponDebuff;

		public bool necroGouge;
	}
}
