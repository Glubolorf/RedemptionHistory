using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs;
using Redemption.Items;
using Redemption.Items.Armor.Costumes;
using Redemption.Items.Cores;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;

namespace Redemption
{
	public class RedePlayer : ModPlayer
	{
		public override void Initialize()
		{
			this.medKit = false;
			this.galaxyHeart = false;
		}

		public override TagCompound Save()
		{
			List<string> list = new List<string>();
			if (this.medKit)
			{
				list.Add("medicKit");
			}
			if (this.galaxyHeart)
			{
				list.Add("galaxyHeart");
			}
			TagCompound tagCompound = new TagCompound();
			tagCompound.Add("boost", list);
			return tagCompound;
		}

		public override void Load(TagCompound tag)
		{
			IList<string> list = tag.GetList<string>("boost");
			this.medKit = list.Contains("medicKit");
			this.galaxyHeart = list.Contains("galaxyHeart");
		}

		public override void LoadLegacy(BinaryReader reader)
		{
			int num = reader.ReadInt32();
			if (num == 0)
			{
				BitsByte bitsByte = reader.ReadByte();
				this.medKit = bitsByte[0];
				this.galaxyHeart = bitsByte[1];
				return;
			}
			ErrorLogger.Log("Redemption: Unknown loadVersion: " + num);
		}

		public override void PostUpdateMiscEffects()
		{
			base.player.statLifeMax2 += (this.medKit ? 50 : 0) + (this.galaxyHeart ? 50 : 0);
			if (Main.netMode != 2 && base.player.whoAmI == Main.myPlayer)
			{
				Texture2D texture = base.mod.GetTexture("ExtraTextures/Rain2");
				Texture2D texture2 = base.mod.GetTexture("ExtraTextures/RainOriginal");
				Texture2D texture3 = base.mod.GetTexture("ExtraTextures/HeartMed");
				Texture2D texture4 = base.mod.GetTexture("ExtraTextures/HeartGal");
				Texture2D texture5 = base.mod.GetTexture("ExtraTextures/HeartOriginal");
				int num = (this.medKit ? 1 : 0) + (this.galaxyHeart ? 1 : 0);
				if (num == 2)
				{
					Main.heart2Texture = texture4;
				}
				else if (num == 1)
				{
					Main.heart2Texture = texture3;
				}
				else
				{
					Main.heart2Texture = texture5;
				}
				if (Main.bloodMoon)
				{
					Main.rainTexture = texture2;
				}
				else if (Main.raining && this.ZoneXeno)
				{
					Main.rainTexture = texture;
				}
				else
				{
					Main.rainTexture = texture2;
				}
			}
			if (Main.raining && this.ZoneXeno && (base.player.ZoneOverworldHeight || base.player.ZoneSkyHeight))
			{
				base.player.AddBuff(base.mod.BuffType("HeavyRadiationDebuff"), 2, true);
			}
		}

		public override void UpdateBiomeVisuals()
		{
			bool flag = NPC.AnyNPCs(base.mod.NPCType("Nebuleus"));
			base.player.ManageSpecialBiomeVisuals("Redemption:Nebuleus", flag, default(Vector2));
			bool flag2 = NPC.AnyNPCs(base.mod.NPCType("BigNebuleus"));
			base.player.ManageSpecialBiomeVisuals("Redemption:BigNebuleus", flag2, default(Vector2));
			base.player.ManageSpecialBiomeVisuals("Redemption:XenoSky", this.ZoneXeno, base.player.Center);
		}

		public override void UpdateBiomes()
		{
			this.ZoneXeno = (RedeWorld.xenoBiome > 75);
			this.ZoneLab = (RedeWorld.labBiome > 200);
		}

		public override bool CustomBiomesMatch(Player other)
		{
			RedePlayer modPlayer = other.GetModPlayer<RedePlayer>(base.mod);
			return this.ZoneXeno == modPlayer.ZoneXeno && this.ZoneLab == modPlayer.ZoneLab;
		}

		public override void CopyCustomBiomesTo(Player other)
		{
			RedePlayer modPlayer = other.GetModPlayer<RedePlayer>(base.mod);
			modPlayer.ZoneXeno = this.ZoneXeno;
			modPlayer.ZoneLab = this.ZoneLab;
		}

		public override void SendCustomBiomes(BinaryWriter writer)
		{
			BitsByte bitsByte = default(BitsByte);
			bitsByte[0] = this.ZoneXeno;
			bitsByte[1] = this.ZoneLab;
			writer.Write(bitsByte);
		}

		public override Texture2D GetMapBackgroundImage()
		{
			if (this.ZoneXeno)
			{
				return base.mod.GetTexture("XenoBiomeMapBackground");
			}
			if (this.ZoneLab)
			{
				return base.mod.GetTexture("LabBiomeMapBackground");
			}
			return null;
		}

		public override void ReceiveCustomBiomes(BinaryReader reader)
		{
			BitsByte bitsByte = reader.ReadByte();
			this.ZoneXeno = bitsByte[0];
			this.ZoneLab = bitsByte[1];
		}

		public override void OnRespawn(Player player)
		{
			if (this.heartEmblem)
			{
				player.statLife = player.statLifeMax2 / 100 * 75;
				player.AddBuff(base.mod.BuffType("HeartEmblemBuff"), 1800, true);
			}
		}

		public override void ResetEffects()
		{
			this.chickenMinion = false;
			this.xenoMinion = false;
			this.examplePet = false;
			this.corpseskullMinion = false;
			this.mk1MicrobotMinion = false;
			this.mk2MicrobotMinion = false;
			this.mk3MicrobotMinion = false;
			this.darkSoulMinion = false;
			this.xenoHatchlingMinion = false;
			this.heartEmblem = false;
			this.moreSeeds = false;
			this.fasterStaves = false;
			this.fasterSeedbags = false;
			this.fasterSpirits = false;
			this.moreSpirits = false;
			this.rainbowCatPet = false;
			this.golemWateringCan = false;
			this.spiritHoming = false;
			this.spiritChicken = false;
			this.extraSeed = false;
			this.spiritPierce = false;
			this.frostburnSeedbag = false;
			this.skeleMinion1 = false;
			this.spiritSkull1 = false;
			this.burnStaves = false;
			this.seedHit = false;
			this.bloodyCollar = false;
			this.taintedNecklace = false;
			this.sapphireBonus = false;
			this.scarletBonus = false;
			this.skeletonCan = false;
			this.enjoyment = false;
			this.ultraFlames = false;
			RedePlayer.reflectProjs = false;
			this.creationBonus = false;
			this.druidBane = false;
			this.omegaAccessoryPrevious = this.omegaAccessory;
			this.omegaAccessory = (this.omegaHideVanity = (this.omegaForceVanity = (this.omegaPower = false)));
			this.chickenAccessoryPrevious = this.chickenAccessory;
			this.chickenAccessory = (this.chickenHideVanity = (this.chickenForceVanity = (this.chickenPower = false)));
			this.bloomingLuck = false;
			this.lostSoulSet = false;
			this.wanderingSoulSet = false;
			this.natureGuardian1 = false;
			this.natureGuardian2 = false;
			this.natureGuardian3 = false;
			this.natureGuardian4 = false;
			this.charisma = false;
			this.natureGuardian5 = false;
			this.natureGuardian6 = false;
			this.natureGuardian7 = false;
			this.natureGuardian8 = false;
			this.natureGuardian9 = false;
			this.natureGuardian10 = false;
			this.natureGuardian11 = false;
			this.natureGuardian12 = false;
			this.natureGuardian13 = false;
			this.natureGuardian14 = false;
			this.natureGuardian15 = false;
			this.natureGuardian16 = false;
			this.natureGuardian17 = false;
			this.natureGuardian18 = false;
			this.natureGuardian19 = false;
			this.natureGuardian20 = false;
			this.natureGuardian21 = false;
			this.natureGuardian22 = false;
			this.natureGuardian23 = false;
			this.natureGuardian24 = false;
			this.natureGuardian25 = false;
			this.natureGuardian26 = false;
			this.natureGuardian27 = false;
			this.hazmatAccessoryPrevious = this.hazmatAccessory;
			this.hazmatAccessory = (this.hazmatHideVanity = (this.hazmatForceVanity = (this.hazmatPower = false)));
			this.skeletonFriendly = false;
			this.xeniumMinion = false;
			this.xeniumMinion1 = false;
			this.xeniumMinion2 = false;
			this.xeniumMinion3 = false;
			this.xeniumMinion4 = false;
			this.xeniumMinion5 = false;
			this.xeniumMinion6 = false;
			this.xeniumMinion7 = false;
			this.xeniumMinion8 = false;
			this.xeniumMinion9 = false;
			this.xeniumMinion10 = false;
			this.xeniumMinion11 = false;
			this.xeniumMinion12 = false;
			this.xeniumDischarge = false;
			this.xeniumBarrier = false;
			this.labWaterImmune = false;
			this.xenoPet = false;
			this.holyFire = false;
			this.bInfection = false;
			this.longerGuardians = false;
			this.blightedShield = false;
			this.holoMinion = false;
			this.rapidStave = false;
			this.guardianCooldownReduce = false;
			this.staveStreamShot = false;
			this.staveTripleShot = false;
			this.staveScatterShot = false;
			this.moltenEruption = false;
			this.staveQuadShot = false;
			this.lifeSteal1 = false;
			this.plasmaShield = false;
			this.eldritchRoot = false;
			this.sleepPowder = false;
			this.vendetta = false;
			this.iceShield = false;
			this.lavaCubeMinion = false;
			this.sandDust = false;
			this.badtime = false;
			this.nebPet = false;
			this.eaglecrestMinion = false;
			this.chickenSwarmerMinion = false;
			this.spiritChicken2 = false;
			this.HEVAccessoryPrevious = this.HEVAccessory;
			this.HEVAccessory = (this.HEVHideVanity = (this.HEVForceVanity = (this.HEVPower = false)));
			this.ancientMirror = false;
			this.ancientStoneMinion = false;
			this.powerSurgeSet = false;
			this.bloodShinkiteSet = false;
			this.cursedShinkiteSet = false;
			this.smallShadeSet = false;
			this.shadeSet = false;
		}

		public override void UpdateDead()
		{
			this.enjoyment = false;
			this.ultraFlames = false;
			this.druidBane = false;
			this.holyFire = false;
			this.bInfection = false;
			this.sleepPowder = false;
			this.sandDust = false;
		}

		public override void UpdateBadLifeRegen()
		{
			if (this.enjoyment)
			{
				if (base.player.lifeRegen > 0)
				{
					base.player.lifeRegen = 0;
				}
				base.player.lifeRegenTime = 0;
				base.player.lifeRegen -= 15;
			}
			if (this.ultraFlames)
			{
				if (base.player.lifeRegen > 0)
				{
					base.player.lifeRegen = 0;
				}
				base.player.lifeRegenTime = 0;
				base.player.lifeRegen -= 40;
			}
			if (this.druidBane)
			{
				if (base.player.lifeRegen > 0)
				{
					base.player.lifeRegen = 0;
				}
				base.player.lifeRegenTime = 0;
				base.player.lifeRegen -= 20;
			}
			if ((this.ZoneLab || this.ZoneXeno) && base.player.wet && !this.hazmatPower && !this.labWaterImmune && !base.player.lavaWet && !base.player.honeyWet)
			{
				if (base.player.lifeRegen > 10)
				{
					base.player.lifeRegen = 10;
				}
				base.player.lifeRegenTime = 0;
				base.player.lifeRegen -= 200;
			}
			if (this.holyFire)
			{
				if (base.player.lifeRegen > 0)
				{
					base.player.lifeRegen = 0;
				}
				base.player.lifeRegenTime = 0;
				base.player.lifeRegen -= 500;
			}
			if (this.enjoyment)
			{
				if (base.player.lifeRegen > 0)
				{
					base.player.lifeRegen = 0;
				}
				base.player.lifeRegenTime = 0;
				base.player.lifeRegen -= 15;
			}
			if (this.sleepPowder)
			{
				Player player = base.player;
				player.velocity.X = player.velocity.X * 0.4f;
				Player player2 = base.player;
				player2.velocity.Y = player2.velocity.Y * 0.4f;
				base.player.statDefense -= 25;
			}
			if (this.sandDust)
			{
				base.player.statDefense -= 8;
			}
			if (this.badtime)
			{
				if (base.player.lifeRegen > 0)
				{
					base.player.lifeRegen = 0;
				}
				base.player.lifeRegenTime = 0;
				base.player.lifeRegen -= 2000;
				Player player3 = base.player;
				player3.velocity.X = player3.velocity.X * 0.4f;
				Player player4 = base.player;
				player4.velocity.Y = player4.velocity.Y * 0.4f;
				base.player.statDefense -= 99;
			}
			if (this.ZoneLab && base.player.chaosState)
			{
				if (base.player.lifeRegen > 0)
				{
					base.player.lifeRegen = 0;
				}
				base.player.lifeRegenTime = 0;
				base.player.lifeRegen -= 2000;
			}
		}

		public override void SetupStartInventory(IList<Item> items)
		{
			Item item = new Item();
			item.SetDefaults(base.mod.ItemType("EmptyCore"), false);
			item.stack = 1;
			items.Add(item);
		}

		public override void UpdateVanityAccessories()
		{
			for (int i = 13; i < 18 + base.player.extraAccessorySlots; i++)
			{
				Item item = base.player.armor[i];
				if (item.type == base.mod.ItemType<OmegaCore>())
				{
					this.omegaHideVanity = false;
					this.omegaForceVanity = true;
				}
			}
			for (int j = 13; j < 18 + base.player.extraAccessorySlots; j++)
			{
				Item item2 = base.player.armor[j];
				if (item2.type == base.mod.ItemType<CrownOfTheKing>())
				{
					this.chickenHideVanity = false;
					this.chickenForceVanity = true;
				}
			}
			for (int k = 13; k < 18 + base.player.extraAccessorySlots; k++)
			{
				Item item3 = base.player.armor[k];
				if (item3.type == base.mod.ItemType<HazmatSuit>())
				{
					this.hazmatHideVanity = false;
					this.hazmatForceVanity = true;
				}
			}
			for (int l = 13; l < 18 + base.player.extraAccessorySlots; l++)
			{
				Item item4 = base.player.armor[l];
				if (item4.type == base.mod.ItemType<HEVSuit>())
				{
					this.HEVHideVanity = false;
					this.HEVForceVanity = true;
				}
			}
		}

		public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff)
		{
			if (this.omegaAccessory)
			{
				base.player.AddBuff(base.mod.BuffType<Omega>(), 60, true);
			}
			if (this.chickenAccessory)
			{
				base.player.AddBuff(base.mod.BuffType<ChickenCrownBuff>(), 60, true);
			}
			if (this.hazmatAccessory)
			{
				base.player.AddBuff(base.mod.BuffType<HazmatSuitBuff>(), 60, true);
			}
			if (this.HEVAccessory)
			{
				base.player.AddBuff(base.mod.BuffType<HEVSuitBuff>(), 60, true);
			}
		}

		public override void FrameEffects()
		{
			if ((this.omegaPower || this.omegaForceVanity) && !this.omegaHideVanity)
			{
				base.player.legs = base.mod.GetEquipSlot("OmegaLegs", 2);
				base.player.body = base.mod.GetEquipSlot("OmegaBody", 1);
				base.player.head = base.mod.GetEquipSlot("OmegaHead", 0);
			}
			if ((this.chickenPower || this.chickenForceVanity) && !this.chickenHideVanity)
			{
				base.player.legs = base.mod.GetEquipSlot("ChickenLegs", 2);
				base.player.body = base.mod.GetEquipSlot("ChickenBody", 1);
				base.player.head = base.mod.GetEquipSlot("ChickenHead", 0);
			}
			if ((this.hazmatPower || this.hazmatForceVanity) && !this.hazmatHideVanity)
			{
				base.player.legs = base.mod.GetEquipSlot("HazmatLegs", 2);
				base.player.body = base.mod.GetEquipSlot("HazmatBody", 1);
				base.player.head = base.mod.GetEquipSlot("HazmatHead", 0);
			}
			if ((this.HEVPower || this.HEVForceVanity) && !this.HEVHideVanity)
			{
				base.player.legs = base.mod.GetEquipSlot("HEVLegs", 2);
				base.player.body = base.mod.GetEquipSlot("HEVBody", 1);
				base.player.head = base.mod.GetEquipSlot("HEVHead", 0);
			}
		}

		public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
		{
			if (this.enjoyment && Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
			{
				int num = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), base.player.width + 4, base.player.height + 4, 243, base.player.velocity.X * 0.4f, base.player.velocity.Y * 0.4f, 100, default(Color), 1.2f);
				Main.dust[num].noGravity = true;
				Main.dust[num].velocity *= 1.8f;
				Dust dust = Main.dust[num];
				dust.velocity.Y = dust.velocity.Y - 0.5f;
				Main.playerDrawDust.Add(num);
			}
			if (this.ultraFlames && Main.rand.Next(2) == 0 && drawInfo.shadow == 0f)
			{
				int num2 = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), base.player.width + 4, base.player.height + 4, 92, base.player.velocity.X * 0.4f, base.player.velocity.Y * 0.4f, 100, default(Color), 1.2f);
				Main.dust[num2].noGravity = true;
				Main.dust[num2].velocity *= 1.8f;
				Dust dust2 = Main.dust[num2];
				dust2.velocity.Y = dust2.velocity.Y - 0.5f;
				Main.playerDrawDust.Add(num2);
			}
			if (this.druidBane && Main.rand.Next(2) == 0 && drawInfo.shadow == 0f)
			{
				int num3 = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), base.player.width + 4, base.player.height + 4, 163, base.player.velocity.X * 0.4f, base.player.velocity.Y * 0.4f, 100, default(Color), 1.2f);
				Main.dust[num3].noGravity = true;
				Main.dust[num3].velocity *= 1.8f;
				Dust dust3 = Main.dust[num3];
				dust3.velocity.Y = dust3.velocity.Y - 0.5f;
				Main.playerDrawDust.Add(num3);
			}
			if (this.holyFire && Main.rand.Next(2) == 0 && drawInfo.shadow == 0f)
			{
				int num4 = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), base.player.width + 4, base.player.height + 4, 64, base.player.velocity.X * 0.4f, base.player.velocity.Y * 0.4f, 100, default(Color), 2f);
				Main.dust[num4].noGravity = true;
				Main.dust[num4].velocity *= 1.8f;
				Dust dust4 = Main.dust[num4];
				dust4.velocity.Y = dust4.velocity.Y - 0.5f;
				Main.playerDrawDust.Add(num4);
			}
		}

		public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
		{
			if (this.vendetta)
			{
				npc.AddBuff(20, 300, false);
			}
		}

		public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			return !this.iceShield;
		}

		public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
		{
			if (this.bloodyCollar)
			{
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, 0f, 0f, base.mod.ProjectileType("BloodPulse"), 50, 0f, base.player.whoAmI, 0f, 0f);
			}
			if (this.charisma)
			{
				target.AddBuff(72, 300, false);
			}
			if (this.moltenEruption && Main.rand.Next(3) == 0)
			{
				Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, -3f, 85, 40, 0f, Main.myPlayer, 0f, 0f);
			}
			if (this.lifeSteal1 && Main.rand.Next(2) == 0)
			{
				base.player.statLife++;
				base.player.HealEffect(1, true);
			}
			if (this.eldritchRoot && target.life <= 0)
			{
				base.player.AddBuff(base.mod.BuffType("EldritchRootBuff"), 180, true);
			}
			if (this.powerSurgeSet && Main.rand.Next(10) == 0)
			{
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, 1f, -3f, base.mod.ProjectileType("EnergyOrb1"), 200, 0f, Main.myPlayer, 0f, 0f);
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, 0f, -1f, base.mod.ProjectileType("EnergyOrb1"), 200, 0f, Main.myPlayer, 0f, 0f);
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, 2f, -5f, base.mod.ProjectileType("EnergyOrb1"), 200, 0f, Main.myPlayer, 0f, 0f);
			}
			if (this.bloodShinkiteSet && Main.rand.Next(4) == 0)
			{
				Projectile.NewProjectile(target.Center.X, target.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)Main.rand.Next(-11, 0), base.mod.ProjectileType("BloodOrbPro3"), 0, 0f, Main.myPlayer, 0f, 0f);
			}
			if (this.cursedShinkiteSet && Main.rand.Next(6) == 0)
			{
				Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, base.mod.ProjectileType("CursedExp"), 100, 0f, Main.myPlayer, 0f, 0f);
			}
			if (this.smallShadeSet)
			{
				target.AddBuff(base.mod.BuffType("BlackenedHeartDebuff"), 300, false);
			}
		}

		public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
		{
			if (this.bloodyCollar)
			{
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, 0f, 0f, base.mod.ProjectileType("BloodPulse"), 50, 0f, base.player.whoAmI, 0f, 0f);
			}
			if (this.charisma)
			{
				target.AddBuff(72, 300, false);
			}
			if (this.moltenEruption && Main.rand.Next(3) == 0)
			{
				Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, -3f, 85, 40, 0f, Main.myPlayer, 0f, 0f);
			}
			if (this.lifeSteal1 && Main.rand.Next(2) == 0)
			{
				base.player.statLife++;
				base.player.HealEffect(1, true);
			}
			if (this.eldritchRoot && target.life <= 0)
			{
				base.player.AddBuff(base.mod.BuffType("EldritchRootBuff"), 180, true);
			}
			if (this.powerSurgeSet && Main.rand.Next(10) == 0)
			{
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, 1f, -3f, base.mod.ProjectileType("EnergyOrb1"), 200, 0f, Main.myPlayer, 0f, 0f);
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, 0f, -1f, base.mod.ProjectileType("EnergyOrb1"), 200, 0f, Main.myPlayer, 0f, 0f);
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, 2f, -5f, base.mod.ProjectileType("EnergyOrb1"), 200, 0f, Main.myPlayer, 0f, 0f);
			}
			if (this.bloodShinkiteSet && Main.rand.Next(4) == 0)
			{
				Projectile.NewProjectile(target.Center.X, target.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-3 + Main.rand.Next(-11, 0)), base.mod.ProjectileType("BloodOrbPro3"), 0, 0f, Main.myPlayer, 0f, 0f);
			}
			if (this.cursedShinkiteSet && Main.rand.Next(6) == 0)
			{
				Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, base.mod.ProjectileType("CursedExp"), 100, 0f, Main.myPlayer, 0f, 0f);
			}
			if (this.smallShadeSet)
			{
				target.AddBuff(base.mod.BuffType("BlackenedHeartDebuff"), 300, false);
			}
		}

		public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref bool crit)
		{
			if (this.plasmaShield && Main.rand.Next(4) == 0)
			{
				projectile.damage = damage * 2;
				projectile.velocity.X = -projectile.velocity.X;
				projectile.velocity.Y = -projectile.velocity.Y;
				projectile.friendly = true;
				projectile.hostile = false;
			}
		}

		public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			if (base.player.FindBuffIndex(base.mod.BuffType("XenomiteDebuff")) != -1 && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " got infected");
			}
			if (base.player.FindBuffIndex(base.mod.BuffType("XenomiteDebuff2")) != -1 && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " got heavily infected");
			}
			if (base.player.FindBuffIndex(base.mod.BuffType("RadioactiveFalloutDebuff")) != -1 && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " forgot to wear a gas mask");
			}
			if (base.player.FindBuffIndex(base.mod.BuffType("DisgustingDebuff")) != -1 && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " got diddily darn egg'd");
			}
			if (base.player.FindBuffIndex(base.mod.BuffType("DruidsBane")) != -1 && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " was baned by the druids");
			}
			if (base.player.FindBuffIndex(base.mod.BuffType("EnjoymentDebuff")) != -1 && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " finally discovered happiness");
			}
			if (base.player.FindBuffIndex(base.mod.BuffType("GloomShroomDebuff")) != -1 && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " got gloomed");
			}
			if (base.player.FindBuffIndex(base.mod.BuffType("UltraFlameDebuff")) != -1 && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " got melted to the bone");
			}
			if (base.player.FindBuffIndex(base.mod.BuffType("XenomiteSkullDebuff")) != -1 && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason("The lab's defensive systems overwhelmed " + base.player.name + " with intense energy");
			}
			if (base.player.FindBuffIndex(base.mod.BuffType("HazardLaserDebuff")) != -1 && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " got lasered!");
			}
			if (base.player.FindBuffIndex(base.mod.BuffType("BlackenedHeartDebuff")) != -1 && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " became soulless");
			}
			if (base.player.FindBuffIndex(base.mod.BuffType("HolyFireDebuff")) != -1 && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " was too glorious");
			}
			if (base.player.FindBuffIndex(base.mod.BuffType("BInfectionDebuff")) != -1 && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " got infected");
			}
			if (damageSource.SourceNPCIndex >= 0 && Main.npc[damageSource.SourceNPCIndex].type == base.mod.NPCType("AJollyMadman"))
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " became hollow");
			}
			if (damageSource.SourceNPCIndex >= 0 && Main.npc[damageSource.SourceNPCIndex].type == base.mod.NPCType("Blobble"))
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " got blobble'd!");
			}
			if (damageSource.SourceNPCIndex >= 0 && Main.npc[damageSource.SourceNPCIndex].type == base.mod.NPCType("ChickenCultist"))
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " got pecked to death");
			}
			if (damageSource.SourceNPCIndex >= 0 && Main.npc[damageSource.SourceNPCIndex].type == base.mod.NPCType("ChickenMan"))
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " got pecked to death");
			}
			if (damageSource.SourceNPCIndex >= 0 && Main.npc[damageSource.SourceNPCIndex].type == base.mod.NPCType("ShieldedChickenMan"))
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " got pecked to death");
			}
			return true;
		}

		public override void PostUpdate()
		{
			if (base.player.ZoneSandstorm && this.ZoneXeno)
			{
				RedePlayer.EmitDust();
			}
		}

		public static void EmitDust()
		{
			if (Main.gamePaused)
			{
				return;
			}
			int sandTiles = Main.sandTiles;
			Player player = Main.player[Main.myPlayer];
			bool flag = Sandstorm.Happening && player.ZoneSandstorm && (Main.bgStyle == 2 || Main.bgStyle == 5) && Main.bgDelay < 50;
			Sandstorm.HandleEffectAndSky(flag && Main.UseStormEffects);
			if (sandTiles < 100 || (double)player.position.Y > Main.worldSurface * 16.0 || player.ZoneBeach)
			{
				return;
			}
			int num = 1;
			if (!flag)
			{
				return;
			}
			if (Main.rand.Next(num) != 0)
			{
				return;
			}
			int num2 = Math.Sign(Main.windSpeed);
			float num3 = Math.Abs(Main.windSpeed);
			if (num3 < 0.01f)
			{
				return;
			}
			float num4 = (float)num2 * MathHelper.Lerp(0.9f, 1f, num3);
			float num5 = 2000f / (float)sandTiles;
			float num6 = 3f / num5;
			num6 = MathHelper.Clamp(num6, 0.77f, 1f);
			int num7 = (int)num5;
			float num8 = (float)Main.screenWidth / (float)Main.maxScreenW;
			int num9 = (int)(1000f * num8);
			float num10 = 20f * Sandstorm.Severity;
			float num11 = (float)num9 * (Main.gfxQuality * 0.5f + 0.5f) + (float)num9 * 0.1f - (float)Dust.SandStormCount;
			if (num11 <= 0f)
			{
				return;
			}
			float num12 = (float)Main.screenWidth + 1000f;
			float num13 = (float)Main.screenHeight;
			Vector2 vector = Main.screenPosition + player.velocity;
			WeightedRandom<Color> weightedRandom = new WeightedRandom<Color>();
			weightedRandom.Add(new Color(200, 160, 20, 180), (double)(Main.screenTileCounts[53] + Main.screenTileCounts[396] + Main.screenTileCounts[397]));
			weightedRandom.Add(new Color(103, 98, 122, 180), (double)(Main.screenTileCounts[112] + Main.screenTileCounts[400] + Main.screenTileCounts[398]));
			weightedRandom.Add(new Color(135, 43, 34, 180), (double)(Main.screenTileCounts[234] + Main.screenTileCounts[401] + Main.screenTileCounts[399]));
			weightedRandom.Add(new Color(213, 196, 197, 180), (double)(Main.screenTileCounts[116] + Main.screenTileCounts[403] + Main.screenTileCounts[402]));
			float num14 = MathHelper.Lerp(0.2f, 0.35f, Sandstorm.Severity);
			float num15 = MathHelper.Lerp(0.5f, 0.7f, Sandstorm.Severity);
			float num16 = (num6 - 0.77f) / 0.23000002f;
			int num17 = (int)MathHelper.Lerp(1f, 10f, num16);
			int num18 = 0;
			while ((float)num18 < num10)
			{
				if (Main.rand.Next(num7 / 4) == 0)
				{
					Vector2 vector2;
					vector2..ctor(Utils.NextFloat(Main.rand) * num12 - 500f, Utils.NextFloat(Main.rand) * -50f);
					if (Main.rand.Next(3) == 0 && num2 == 1)
					{
						vector2.X = (float)(Main.rand.Next(500) - 500);
					}
					else if (Main.rand.Next(3) == 0 && num2 == -1)
					{
						vector2.X = (float)(Main.rand.Next(500) + Main.screenWidth);
					}
					if (vector2.X < 0f || vector2.X > (float)Main.screenWidth)
					{
						vector2.Y += Utils.NextFloat(Main.rand) * num13 * 0.9f;
					}
					vector2 += vector;
					int num19 = (int)vector2.X / 16;
					int num20 = (int)vector2.Y / 16;
					if (Main.tile[num19, num20] != null && Main.tile[num19, num20].wall == 0)
					{
						for (int i = 0; i < 1; i++)
						{
							Dust dust = Main.dust[Dust.NewDust(vector2, 10, 10, 256, 0f, 0f, 0, default(Color), 1f)];
							dust.velocity.Y = 2f + Utils.NextFloat(Main.rand) * 0.2f;
							Dust dust2 = dust;
							dust2.velocity.Y = dust2.velocity.Y * dust.scale;
							Dust dust3 = dust;
							dust3.velocity.Y = dust3.velocity.Y * 0.35f;
							dust.velocity.X = num4 * 5f + Utils.NextFloat(Main.rand) * 1f;
							Dust dust4 = dust;
							dust4.velocity.X = dust4.velocity.X + num4 * num15 * 20f;
							dust.fadeIn += num15 * 0.2f;
							dust.velocity *= 1f + num14 * 0.5f;
							dust.color = weightedRandom;
							dust.velocity *= 1f + num14;
							dust.velocity *= num6;
							dust.scale = 0.9f;
							num11 -= 1f;
							if (num11 <= 0f)
							{
								break;
							}
							if (Main.rand.Next(num17) != 0)
							{
								i--;
								vector2 += Utils.RandomVector2(Main.rand, -10f, 10f) + dust.velocity * -1.1f;
								num19 = (int)vector2.X / 16;
								num20 = (int)vector2.Y / 16;
								if (WorldGen.InWorld(num19, num20, 10) && Main.tile[num19, num20] != null)
								{
									ushort wall = Main.tile[num19, num20].wall;
								}
							}
						}
						if (num11 <= 0f)
						{
							return;
						}
					}
				}
				num18++;
			}
		}

		public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
		{
			if (junk)
			{
				return;
			}
			if (this.ZoneXeno && liquidType == 0 && questFish == base.mod.ItemType("XenChomper") && Main.rand.Next(1) == 0)
			{
				caughtType = base.mod.ItemType("XenChomper");
			}
			if (this.ZoneLab && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && Main.rand.Next(6) == 0)
			{
				caughtType = base.mod.ItemType("LabCrate");
			}
		}

		public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
		{
			if (this.omegaAccessory)
			{
				Dust.NewDust(new Vector2(base.player.position.X - base.player.velocity.X * 2f, base.player.position.Y - 2f - base.player.velocity.Y * 2f), base.player.width, base.player.height, 226, 0f, 0f, 100, default(Color), 2f);
			}
			if (this.seedHit && Main.rand.Next(3) == 0)
			{
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), 483, 20, 1f, Main.myPlayer, 0f, 0f);
			}
			if (this.golemWateringCan)
			{
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), 483, 20, 1f, Main.myPlayer, 0f, 0f);
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), 483, 20, 1f, Main.myPlayer, 0f, 0f);
				if (this.moreSeeds)
				{
					Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), 483, 20, 1f, Main.myPlayer, 0f, 0f);
				}
			}
			if (this.skeletonCan)
			{
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("BoneSeed"), 10, 1f, Main.myPlayer, 0f, 0f);
				if (Main.rand.Next(2) == 0)
				{
					Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("BoneSeed"), 10, 1f, Main.myPlayer, 0f, 0f);
				}
				if (this.moreSeeds)
				{
					Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("BoneSeed"), 10, 1f, Main.myPlayer, 0f, 0f);
				}
			}
			if (this.spiritChicken)
			{
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("SpiritChickenPro"), 9, 1f, Main.myPlayer, 0f, 0f);
			}
			if (this.spiritSkull1)
			{
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("SpiritSkullPro"), 12, 1f, Main.myPlayer, 0f, 0f);
			}
			if (this.taintedNecklace)
			{
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("CorruptSoul"), 50, 1f, Main.myPlayer, 0f, 0f);
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("CorruptSoul"), 50, 1f, Main.myPlayer, 0f, 0f);
				if (Main.rand.Next(2) == 0)
				{
					Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("CorruptSoul"), 50, 1f, Main.myPlayer, 0f, 0f);
				}
				if (Main.rand.Next(2) == 0)
				{
					Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("CorruptSoul"), 50, 1f, Main.myPlayer, 0f, 0f);
				}
				if (Main.rand.Next(4) == 0)
				{
					Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("CorruptSoul"), 50, 1f, Main.myPlayer, 0f, 0f);
				}
			}
			if (this.xeniumDischarge && Main.rand.Next(4) == 0)
			{
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, 0f, 0f, base.mod.ProjectileType("XeniumDischarge"), 0, 0f, Main.myPlayer, 0f, 0f);
			}
			if (this.spiritChicken2)
			{
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("EtherealChickenPro"), 300, 1f, Main.myPlayer, 0f, 0f);
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("EtherealChickenPro"), 300, 1f, Main.myPlayer, 0f, 0f);
			}
			if (this.shadeSet && Main.rand.Next(3) == 0)
			{
				int num = 16;
				for (int i = 0; i < num; i++)
				{
					int num2 = Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, 0f, 0f, base.mod.ProjectileType("CriesOfGriefPro3"), 400, 0f, Main.myPlayer, 0f, 0f);
					Main.projectile[num2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)i / (float)num * 6.28f);
				}
			}
		}

		private const int saveVersion = 0;

		public bool examplePet;

		public bool chickenMinion;

		public bool xenoMinion;

		public bool corpseskullMinion;

		public bool mk1MicrobotMinion;

		public bool mk2MicrobotMinion;

		public bool mk3MicrobotMinion;

		public bool darkSoulMinion;

		public bool xenoHatchlingMinion;

		public bool ZoneXeno;

		public bool heartEmblem;

		public bool moreSeeds;

		public bool fasterStaves;

		public bool fasterSeedbags;

		public bool fasterSpirits;

		public bool moreSpirits;

		public bool rainbowCatPet;

		public bool golemWateringCan;

		public bool spiritHoming;

		public bool spiritChicken;

		public bool extraSeed;

		public bool spiritPierce;

		public bool frostburnSeedbag;

		public bool skeleMinion1;

		public bool spiritSkull1;

		public bool burnStaves;

		public bool seedHit;

		public bool bloodyCollar;

		public bool taintedNecklace;

		public bool sapphireBonus;

		public bool scarletBonus;

		public bool skeletonCan;

		public bool enjoyment;

		public bool ultraFlames;

		public bool creationBonus;

		public static bool reflectProjs;

		public bool omegaAccessoryPrevious;

		public bool omegaAccessory;

		public bool omegaHideVanity;

		public bool omegaForceVanity;

		public bool omegaPower;

		public bool druidBane;

		public bool chickenAccessoryPrevious;

		public bool chickenAccessory;

		public bool chickenHideVanity;

		public bool chickenForceVanity;

		public bool chickenPower;

		public bool bloomingLuck;

		public bool lostSoulSet;

		public bool wanderingSoulSet;

		public bool natureGuardian1;

		public bool natureGuardian2;

		public bool natureGuardian3;

		public bool natureGuardian4;

		public bool charisma;

		public bool natureGuardian5;

		public bool natureGuardian6;

		public bool natureGuardian7;

		public bool natureGuardian8;

		public bool natureGuardian9;

		public bool natureGuardian10;

		public bool natureGuardian11;

		public bool natureGuardian12;

		public bool natureGuardian13;

		public bool natureGuardian14;

		public bool natureGuardian15;

		public bool natureGuardian16;

		public bool natureGuardian17;

		public bool natureGuardian18;

		public bool natureGuardian19;

		public bool natureGuardian20;

		public bool natureGuardian21;

		public bool natureGuardian22;

		public bool natureGuardian23;

		public bool natureGuardian24;

		public bool natureGuardian25;

		public bool natureGuardian26;

		public bool natureGuardian27;

		public bool ZoneLab;

		public bool hazmatAccessoryPrevious;

		public bool hazmatAccessory;

		public bool hazmatHideVanity;

		public bool hazmatForceVanity;

		public bool hazmatPower;

		public bool skeletonFriendly;

		public bool xeniumMinion;

		public bool xeniumMinion1;

		public bool xeniumMinion2;

		public bool xeniumMinion3;

		public bool xeniumMinion4;

		public bool xeniumMinion5;

		public bool xeniumMinion6;

		public bool xeniumMinion7;

		public bool xeniumMinion8;

		public bool xeniumMinion9;

		public bool xeniumMinion10;

		public bool xeniumMinion11;

		public bool xeniumMinion12;

		public bool xeniumDischarge;

		public bool xeniumBarrier;

		public bool labWaterImmune;

		public bool xenoPet;

		public bool holyFire;

		public bool bInfection;

		public bool longerGuardians;

		public bool medKit;

		public bool blightedShield;

		public bool holoMinion;

		public bool rapidStave;

		public bool guardianCooldownReduce;

		public bool staveStreamShot;

		public bool staveTripleShot;

		public bool staveScatterShot;

		public bool moltenEruption;

		public bool staveQuadShot;

		public bool lifeSteal1;

		public bool plasmaShield;

		public bool eldritchRoot;

		public bool sleepPowder;

		public bool vendetta;

		public bool iceShield;

		public bool lavaCubeMinion;

		public bool sandDust;

		public bool badtime;

		public bool nebPet;

		public bool eaglecrestMinion;

		public bool chickenSwarmerMinion;

		public bool galaxyHeart;

		public bool spiritChicken2;

		public bool HEVAccessoryPrevious;

		public bool HEVAccessory;

		public bool HEVHideVanity;

		public bool HEVForceVanity;

		public bool HEVPower;

		public bool ancientMirror;

		public bool ancientStoneMinion;

		public bool powerSurgeSet;

		public bool bloodShinkiteSet;

		public bool cursedShinkiteSet;

		public bool smallShadeSet;

		public bool shadeSet;
	}
}
