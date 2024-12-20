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
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Redemption
{
	public class RedePlayer : ModPlayer
	{
		public override void Initialize()
		{
			this.medKit = false;
		}

		public override TagCompound Save()
		{
			List<string> list = new List<string>();
			if (this.medKit)
			{
				list.Add("medicKit");
			}
			TagCompound tagCompound = new TagCompound();
			tagCompound.Add("boost", list);
			return tagCompound;
		}

		public override void Load(TagCompound tag)
		{
			IList<string> list = tag.GetList<string>("boost");
			this.medKit = list.Contains("medicKit");
		}

		public override void LoadLegacy(BinaryReader reader)
		{
			int num = reader.ReadInt32();
			if (num == 0)
			{
				this.medKit = reader.ReadByte()[0];
				return;
			}
			ErrorLogger.Log("Redemption: Unknown loadVersion: " + num);
		}

		public override void PostUpdateMiscEffects()
		{
			base.player.statLifeMax2 += (this.medKit ? 50 : 0);
			if (Main.netMode != 2 && base.player.whoAmI == Main.myPlayer)
			{
				Texture2D texture = base.mod.GetTexture("ExtraTextures/HeartMed");
				Texture2D texture2 = base.mod.GetTexture("ExtraTextures/HeartOriginal");
				int num = this.medKit ? 1 : 0;
				if (num == 1)
				{
					Main.heart2Texture = texture;
					return;
				}
				Main.heart2Texture = texture2;
			}
		}

		public override void UpdateBiomes()
		{
			this.ZoneXeno = (RedeWorld.xenoBiome > 75 || RedeWorld.xenoBiome2 > 75);
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
			if (this.ZoneLab && base.player.wet && !this.hazmatPower && !this.labWaterImmune && !base.player.lavaWet && !base.player.honeyWet)
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
			return true;
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
	}
}
