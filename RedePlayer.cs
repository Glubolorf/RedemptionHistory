using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs;
using Redemption.Buffs.Cooldowns;
using Redemption.Buffs.Debuffs;
using Redemption.Buffs.Minions;
using Redemption.Buffs.Wasteland;
using Redemption.Dusts;
using Redemption.Items;
using Redemption.Items.Accessories.HM;
using Redemption.Items.Accessories.PostML;
using Redemption.Items.Accessories.PreHM;
using Redemption.Items.Armor.Cores;
using Redemption.Items.Materials.PostML;
using Redemption.Items.Placeable.Furniture.Lab;
using Redemption.Items.Placeable.Furniture.Shade;
using Redemption.Items.Weapons.PostML.Melee;
using Redemption.Items.Weapons.PostML.Ranged;
using Redemption.NPCs.Bosses.EaglecrestGolem;
using Redemption.NPCs.Bosses.Neb;
using Redemption.NPCs.Bosses.Neb.Clone;
using Redemption.NPCs.Bosses.Neb.Phase2;
using Redemption.NPCs.Bosses.Thorn;
using Redemption.NPCs.ChickenInv;
using Redemption.NPCs.Friendly;
using Redemption.NPCs.PreHM;
using Redemption.NPCs.Soulless;
using Redemption.Projectiles.Druid;
using Redemption.Projectiles.Druid.Stave;
using Redemption.Projectiles.Magic;
using Redemption.Projectiles.Melee;
using Redemption.Projectiles.Misc;
using Redemption.Walls;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.Graphics.Effects;
using Terraria.ID;
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
			this.shadowBinderCharge = 0;
			this.foundHall = false;
		}

		public override TagCompound Save()
		{
			List<string> boost = new List<string>();
			if (this.medKit)
			{
				boost.Add("medicKit");
			}
			if (this.galaxyHeart)
			{
				boost.Add("galaxyHeart");
			}
			if (this.foundHall)
			{
				boost.Add("foundHall");
			}
			TagCompound tagCompound = new TagCompound();
			tagCompound.Add("boost", boost);
			tagCompound.Add("sbCharge", this.shadowBinderCharge);
			return tagCompound;
		}

		public override void Load(TagCompound tag)
		{
			IList<string> boost = tag.GetList<string>("boost");
			this.medKit = boost.Contains("medicKit");
			this.galaxyHeart = boost.Contains("galaxyHeart");
			this.foundHall = boost.Contains("foundHall");
			this.shadowBinderCharge = tag.GetInt("sbCharge");
		}

		public override void PostUpdateMiscEffects()
		{
			base.player.statLifeMax2 += (this.medKit ? 50 : 0) + (this.galaxyHeart ? 50 : 0);
			if (Main.netMode != 2 && base.player.whoAmI == Main.myPlayer)
			{
				Texture2D rain2 = base.mod.GetTexture("ExtraTextures/Rain2");
				Texture2D rainOriginal = base.mod.GetTexture("ExtraTextures/RainOriginal");
				Texture2D heartMed = base.mod.GetTexture("ExtraTextures/HeartMed");
				Texture2D heartGalaxy = base.mod.GetTexture("ExtraTextures/HeartGal");
				Texture2D heartOriginal = base.mod.GetTexture("ExtraTextures/HeartOriginal");
				int totalHealthBoost = (this.medKit ? 1 : 0) + (this.galaxyHeart ? 1 : 0);
				if (totalHealthBoost == 2)
				{
					Main.heart2Texture = heartGalaxy;
				}
				else if (totalHealthBoost == 1)
				{
					Main.heart2Texture = heartMed;
				}
				else
				{
					Main.heart2Texture = heartOriginal;
				}
				if (Main.bloodMoon)
				{
					Main.rainTexture = rainOriginal;
				}
				else if (Main.raining && (this.ZoneXeno || this.ZoneEvilXeno || this.ZoneEvilXeno2))
				{
					Main.rainTexture = rain2;
				}
				else
				{
					Main.rainTexture = rainOriginal;
				}
			}
			if (this.ZoneXeno || this.ZoneEvilXeno || this.ZoneEvilXeno2)
			{
				if (base.player.GetModPlayer<MullerEffect>().effect && Main.rand.Next(200) == 0 && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Muller1").WithVolume(0.9f).WithPitchVariance(0.1f), base.player.position);
				}
				if (Main.raining)
				{
					if (base.player.ZoneOverworldHeight || base.player.ZoneSkyHeight)
					{
						base.player.AddBuff(ModContent.BuffType<HeavyRadiationDebuff>(), 2, true);
					}
					if (Main.rand.Next(80000) == 0 && base.player.GetModPlayer<RedePlayer>().irradiatedLevel == 0 && !this.HEVPower && !this.hazmatPower)
					{
						base.player.GetModPlayer<RedePlayer>().irradiatedLevel++;
					}
				}
			}
			if (this.ZoneLab)
			{
				if (base.player.GetModPlayer<MullerEffect>().effect && Main.rand.Next(200) == 0 && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Muller1").WithVolume(0.9f).WithPitchVariance(0.1f), base.player.position);
				}
				if (Main.rand.Next(80000) == 0 && base.player.GetModPlayer<RedePlayer>().irradiatedLevel == 0 && !this.HEVPower && !this.hazmatPower)
				{
					base.player.GetModPlayer<RedePlayer>().irradiatedLevel++;
				}
				Point point = Utils.ToTileCoordinates(base.player.position);
				if (!RedeWorld.labSafe && ((int)Main.tile[point.X, point.Y].wall == ModContent.WallType<HardenedlyHardenedSludgeWallTile>() || (int)Main.tile[point.X, point.Y].wall == ModContent.WallType<HardenedSludgeWallTile>() || (int)Main.tile[point.X, point.Y].wall == ModContent.WallType<LabWallTileUnsafe>() || (int)Main.tile[point.X, point.Y].wall == ModContent.WallType<VentWallTile>()))
				{
					base.player.AddBuff(ModContent.BuffType<IntruderAlertDebuff>(), 60, true);
				}
			}
			if (this.irradiatedLevel == 1)
			{
				this.irradiatedTimer++;
				if (this.irradiatedTimer == 39999 || this.irradiatedTimer == 47999)
				{
					if (this.hazmatPower || this.HEVPower)
					{
						this.irradiatedEffect = 0;
						this.irradiatedLevel = 0;
						this.irradiatedTimer = 0;
					}
					else if (Main.rand.Next(2) == 0)
					{
						this.irradiatedEffect = 0;
						this.irradiatedLevel = 0;
						this.irradiatedTimer = 0;
					}
				}
				if (this.irradiatedTimer >= 38000 && this.irradiatedTimer < 40000)
				{
					if (Main.rand.Next(800) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<HeadacheDebuff>(), 120, true);
					}
				}
				else if (this.irradiatedTimer >= 40000 && this.irradiatedTimer < 48000)
				{
					this.irradiatedEffect = 1;
					if (Main.rand.Next(2000) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<FatigueDebuff>(), 800, true);
					}
					if (Main.rand.Next(12000) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<NauseaDebuff>(), 1200, true);
					}
				}
				else if (this.irradiatedTimer >= 48000 && this.irradiatedTimer < 52000)
				{
					this.irradiatedEffect = 2;
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<FatigueDebuff>(), 800, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<NauseaDebuff>(), 1200, true);
					}
					if (Main.rand.Next(80000) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<HairLossDebuff>(), 60000, true);
					}
				}
				else if (this.irradiatedTimer >= 52000 && this.irradiatedTimer < 58000)
				{
					this.irradiatedEffect = 3;
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<HairLossDebuff>(), 60000, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<SkinBurnDebuff>(), 60000, true);
					}
					if (Main.rand.Next(4000) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<FeverDebuff>(), 60000, true);
					}
				}
				else if (this.irradiatedTimer >= 58000)
				{
					this.irradiatedEffect = 4;
					base.player.AddBuff(ModContent.BuffType<RadiationDebuff>(), 5, true);
				}
			}
			else if (this.irradiatedLevel == 2)
			{
				this.irradiatedTimer++;
				if (this.irradiatedTimer == 37999 || this.irradiatedTimer == 45999)
				{
					if (this.hazmatPower || this.HEVPower)
					{
						this.irradiatedEffect = 0;
						this.irradiatedLevel = 0;
						this.irradiatedTimer = 0;
					}
					else if (Main.rand.Next(3) == 0)
					{
						this.irradiatedEffect = 0;
						this.irradiatedLevel = 0;
						this.irradiatedTimer = 0;
					}
				}
				if (this.irradiatedTimer >= 36000 && this.irradiatedTimer < 38000)
				{
					if (Main.rand.Next(800) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<HeadacheDebuff>(), 120, true);
					}
				}
				else if (this.irradiatedTimer >= 38000 && this.irradiatedTimer < 46000)
				{
					this.irradiatedEffect = 1;
					if (Main.rand.Next(2000) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<FatigueDebuff>(), 800, true);
					}
					if (Main.rand.Next(10000) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<NauseaDebuff>(), 1200, true);
					}
				}
				else if (this.irradiatedTimer >= 46000 && this.irradiatedTimer < 50000)
				{
					this.irradiatedEffect = 2;
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<FatigueDebuff>(), 800, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<NauseaDebuff>(), 1200, true);
					}
					if (Main.rand.Next(50000) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<HairLossDebuff>(), 60000, true);
					}
				}
				else if (this.irradiatedTimer >= 50000 && this.irradiatedTimer < 56000)
				{
					this.irradiatedEffect = 3;
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<HairLossDebuff>(), 60000, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<SkinBurnDebuff>(), 60000, true);
					}
					if (Main.rand.Next(4000) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<FeverDebuff>(), 60000, true);
					}
				}
				else if (this.irradiatedTimer >= 56000)
				{
					this.irradiatedEffect = 4;
					base.player.AddBuff(ModContent.BuffType<RadiationDebuff>(), 5, true);
				}
			}
			else if (this.irradiatedLevel == 3)
			{
				this.irradiatedTimer++;
				if (this.irradiatedTimer == 33999 || this.irradiatedTimer == 41999)
				{
					if (this.HEVPower)
					{
						this.irradiatedEffect = 0;
						this.irradiatedLevel = 0;
						this.irradiatedTimer = 0;
					}
					else if (this.hazmatPower)
					{
						if (Main.rand.Next(2) == 0)
						{
							this.irradiatedEffect = 0;
							this.irradiatedLevel = 0;
							this.irradiatedTimer = 0;
						}
					}
					else if (Main.rand.Next(5) == 0)
					{
						this.irradiatedEffect = 0;
						this.irradiatedLevel = 0;
						this.irradiatedTimer = 0;
					}
				}
				if (this.irradiatedTimer >= 32000 && this.irradiatedTimer < 34000)
				{
					if (Main.rand.Next(800) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<HeadacheDebuff>(), 120, true);
					}
				}
				else if (this.irradiatedTimer >= 34000 && this.irradiatedTimer < 42000)
				{
					this.irradiatedEffect = 1;
					if (Main.rand.Next(2000) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<FatigueDebuff>(), 800, true);
					}
					if (Main.rand.Next(5000) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<NauseaDebuff>(), 1200, true);
					}
				}
				else if (this.irradiatedTimer >= 42000 && this.irradiatedTimer < 46000)
				{
					this.irradiatedEffect = 2;
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<FatigueDebuff>(), 800, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<NauseaDebuff>(), 1200, true);
					}
					if (Main.rand.Next(30000) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<HairLossDebuff>(), 60000, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<SkinBurnDebuff>(), 60000, true);
					}
				}
				else if (this.irradiatedTimer >= 46000 && this.irradiatedTimer < 49000)
				{
					this.irradiatedEffect = 3;
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<HairLossDebuff>(), 60000, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<SkinBurnDebuff>(), 60000, true);
					}
					if (Main.rand.Next(4000) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<FeverDebuff>(), 60000, true);
					}
				}
				else if (this.irradiatedTimer >= 49000)
				{
					this.irradiatedEffect = 4;
					base.player.AddBuff(ModContent.BuffType<RadiationDebuff>(), 5, true);
				}
			}
			else if (this.irradiatedLevel == 4)
			{
				this.irradiatedTimer++;
				if (this.irradiatedTimer == 33999 || this.irradiatedTimer == 37999)
				{
					if (this.HEVPower)
					{
						if (Main.rand.Next(5) == 0)
						{
							this.irradiatedEffect = 0;
							this.irradiatedLevel = 0;
							this.irradiatedTimer = 0;
						}
					}
					else if (this.hazmatPower)
					{
						if (Main.rand.Next(10) == 0)
						{
							this.irradiatedEffect = 0;
							this.irradiatedLevel = 0;
							this.irradiatedTimer = 0;
						}
					}
					else if (Main.rand.Next(15) == 0)
					{
						this.irradiatedEffect = 0;
						this.irradiatedLevel = 0;
						this.irradiatedTimer = 0;
					}
				}
				if (this.irradiatedTimer >= 30000 && this.irradiatedTimer < 34000)
				{
					if (Main.rand.Next(800) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<HeadacheDebuff>(), 120, true);
					}
				}
				else if (this.irradiatedTimer >= 34000 && this.irradiatedTimer < 38000)
				{
					this.irradiatedEffect = 1;
					if (Main.rand.Next(2000) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<FatigueDebuff>(), 800, true);
					}
					if (Main.rand.Next(2000) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<NauseaDebuff>(), 1200, true);
					}
				}
				else if (this.irradiatedTimer >= 38000 && this.irradiatedTimer < 40000)
				{
					this.irradiatedEffect = 2;
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<FatigueDebuff>(), 800, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<NauseaDebuff>(), 1200, true);
					}
					if (Main.rand.Next(2000) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<HairLossDebuff>(), 60000, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<SkinBurnDebuff>(), 60000, true);
					}
				}
				else if (this.irradiatedTimer >= 40000 && this.irradiatedTimer < 41000)
				{
					this.irradiatedEffect = 3;
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<HairLossDebuff>(), 60000, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<SkinBurnDebuff>(), 60000, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<FeverDebuff>(), 60000, true);
					}
				}
				else if (this.irradiatedTimer >= 41000)
				{
					this.irradiatedEffect = 4;
					base.player.AddBuff(ModContent.BuffType<RadiationDebuff>(), 5, true);
				}
			}
			else if (this.irradiatedLevel == 5)
			{
				this.irradiatedTimer++;
				if (this.irradiatedTimer == 27999 || this.irradiatedTimer == 29999)
				{
					if (this.HEVPower)
					{
						if (Main.rand.Next(20) == 0)
						{
							this.irradiatedEffect = 0;
							this.irradiatedLevel = 0;
							this.irradiatedTimer = 0;
						}
					}
					else if (this.hazmatPower)
					{
						if (Main.rand.Next(30) == 0)
						{
							this.irradiatedEffect = 0;
							this.irradiatedLevel = 0;
							this.irradiatedTimer = 0;
						}
					}
					else if (Main.rand.Next(40) == 0)
					{
						this.irradiatedEffect = 0;
						this.irradiatedLevel = 0;
						this.irradiatedTimer = 0;
					}
				}
				if (this.irradiatedTimer >= 26000 && this.irradiatedTimer < 28000)
				{
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<HeadacheDebuff>(), 120, true);
					}
				}
				else if (this.irradiatedTimer >= 28000 && this.irradiatedTimer < 30000)
				{
					this.irradiatedEffect = 1;
					if (Main.rand.Next(300) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<FatigueDebuff>(), 800, true);
					}
					if (Main.rand.Next(300) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<NauseaDebuff>(), 1200, true);
					}
				}
				else if (this.irradiatedTimer >= 30000 && this.irradiatedTimer < 31000)
				{
					this.irradiatedEffect = 2;
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<FatigueDebuff>(), 800, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<NauseaDebuff>(), 1200, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<HairLossDebuff>(), 60000, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<SkinBurnDebuff>(), 60000, true);
					}
				}
				else if (this.irradiatedTimer >= 31000 && this.irradiatedTimer < 32000)
				{
					this.irradiatedEffect = 3;
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<HairLossDebuff>(), 60000, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<SkinBurnDebuff>(), 60000, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(ModContent.BuffType<FeverDebuff>(), 60000, true);
					}
				}
				else if (this.irradiatedTimer >= 32000)
				{
					this.irradiatedEffect = 4;
					base.player.AddBuff(ModContent.BuffType<RadiationDebuff>(), 5, true);
				}
			}
			else if (this.irradiatedLevel > 5)
			{
				this.irradiatedLevel = 5;
			}
			if (this.spiritWyvern1 && !base.player.HasBuff(ModContent.BuffType<SpiritWyvernBuff>()))
			{
				this.spiritWyvern1 = false;
			}
			if (this.spiritWyvern2 && !base.player.HasBuff(ModContent.BuffType<SpiritDragonBuff>()))
			{
				this.spiritWyvern2 = false;
			}
			if (base.player.HasBuff(ModContent.BuffType<HKStatueBuff>()))
			{
				this.foundHall = true;
			}
		}

		public override void UpdateBiomeVisuals()
		{
			bool flag = BasePlayer.HasAccessory(base.player, ModContent.ItemType<GasMask>(), true, false) || BasePlayer.HasAccessory(base.player, ModContent.ItemType<HEVSuit>(), true, false);
			bool useFireC = NPC.AnyNPCs(ModContent.NPCType<NebP1_Clone>());
			bool useFire2C = NPC.AnyNPCs(ModContent.NPCType<NebP2_Clone>());
			bool useFire = NPC.AnyNPCs(ModContent.NPCType<NebP1>());
			base.player.ManageSpecialBiomeVisuals("Redemption:NebP1", useFire || useFireC, default(Vector2));
			bool useFire2 = NPC.AnyNPCs(ModContent.NPCType<NebP2>());
			base.player.ManageSpecialBiomeVisuals("Redemption:NebP2", useFire2 || useFire2C, default(Vector2));
			bool useFire3 = NPC.AnyNPCs(ModContent.NPCType<Ukko>());
			base.player.ManageSpecialBiomeVisuals("Redemption:Ukko", useFire3, default(Vector2));
			base.player.ManageSpecialBiomeVisuals("Redemption:XenoSky", this.ZoneXeno || this.ZoneEvilXeno || this.ZoneEvilXeno2, base.player.Center);
			if (flag)
			{
				Filter filter = Filters.Scene["MoR:FogOverlay"];
				if (filter != null)
				{
					filter.GetShader().UseOpacity(0.25f).UseIntensity(0.6f).UseColor(Color.DarkOliveGreen).UseImage(ModContent.GetTexture("Redemption/Effects/Perlin"), 0, null);
				}
			}
			else
			{
				Filter filter2 = Filters.Scene["MoR:FogOverlay"];
				if (filter2 != null)
				{
					filter2.GetShader().UseOpacity(0.3f).UseIntensity(1f).UseColor(Color.DarkOliveGreen).UseImage(ModContent.GetTexture("Redemption/Effects/Perlin"), 0, null);
				}
			}
			base.player.ManageSpecialBiomeVisuals("MoR:FogOverlay", this.ZoneXeno || this.ZoneEvilXeno || this.ZoneEvilXeno2 || this.irradiatedEffect >= 4, default(Vector2));
			base.player.ManageSpecialBiomeVisuals("Redemption:SoullessSky", this.ZoneSoulless && !this.dreamsong, base.player.Center);
		}

		public override void UpdateBiomes()
		{
			this.ZoneXeno = (RedeWorld.xenoBiome > 75);
			this.ZoneEvilXeno = (RedeWorld.evilXenoBiome > 40);
			this.ZoneEvilXeno2 = (RedeWorld.evilXenoBiome2 > 40);
			this.ZoneLab = (RedeWorld.labBiome > 200);
			this.ZoneSlayer = (RedeWorld.slayerBiome > 75);
			this.ZoneSoulless = (RedeWorld.soullessBiome > 5);
		}

		public override bool CustomBiomesMatch(Player other)
		{
			RedePlayer modOther = other.GetModPlayer<RedePlayer>();
			return this.ZoneXeno == modOther.ZoneXeno && this.ZoneEvilXeno == modOther.ZoneEvilXeno && this.ZoneEvilXeno2 == modOther.ZoneEvilXeno2 && this.ZoneLab == modOther.ZoneLab && this.ZoneSlayer == modOther.ZoneSlayer && this.ZoneSoulless == modOther.ZoneSoulless;
		}

		public override void CopyCustomBiomesTo(Player other)
		{
			RedePlayer modPlayer = other.GetModPlayer<RedePlayer>();
			modPlayer.ZoneXeno = this.ZoneXeno;
			modPlayer.ZoneEvilXeno = this.ZoneEvilXeno;
			modPlayer.ZoneEvilXeno2 = this.ZoneEvilXeno2;
			modPlayer.ZoneLab = this.ZoneLab;
			modPlayer.ZoneSlayer = this.ZoneSlayer;
			modPlayer.ZoneSoulless = this.ZoneSoulless;
		}

		public override void SendCustomBiomes(BinaryWriter writer)
		{
			BitsByte flags = default(BitsByte);
			flags[0] = this.ZoneXeno;
			flags[1] = this.ZoneLab;
			flags[2] = this.ZoneEvilXeno;
			flags[3] = this.ZoneEvilXeno2;
			flags[4] = this.ZoneSlayer;
			flags[5] = this.ZoneSoulless;
			writer.Write(flags);
		}

		public override Texture2D GetMapBackgroundImage()
		{
			if (this.ZoneXeno || this.ZoneEvilXeno || this.ZoneEvilXeno2)
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
			BitsByte flags = reader.ReadByte();
			this.ZoneXeno = flags[0];
			this.ZoneLab = flags[1];
			this.ZoneEvilXeno = flags[2];
			this.ZoneEvilXeno2 = flags[3];
			this.ZoneSlayer = flags[4];
			this.ZoneSoulless = flags[5];
		}

		public override void OnRespawn(Player player)
		{
			if (this.heartEmblem)
			{
				player.statLife = player.statLifeMax2 / 100 * 75;
				player.AddBuff(ModContent.BuffType<HeartEmblemBuff>(), 1800, true);
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
			this.staveSpeed = 1f;
			this.fasterSeedbags = false;
			this.fasterSpirits = false;
			this.moreSpirits = false;
			this.golemWateringCan = false;
			this.spiritHoming = false;
			this.spiritChicken = false;
			this.extraSeed = false;
			this.spiritPierce = false;
			this.frostburnSeedbag = false;
			this.spiritSkull1 = false;
			this.burnStaves = false;
			this.seedHit = false;
			this.bloodyCollar = false;
			this.taintedNecklace = false;
			this.sapphireBonus = false;
			this.scarletBonus = false;
			this.skeletonCan = false;
			this.ultraFlames = false;
			this.creationBonus = false;
			this.druidBane = false;
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
			this.natureGuardian28 = false;
			this.hazmatAccessoryPrevious = this.hazmatAccessory;
			this.hazmatAccessory = (this.hazmatHideVanity = (this.hazmatForceVanity = (this.hazmatPower = false)));
			this.skeletonFriendly = false;
			this.xeniumMinion = false;
			this.xeniumDischarge = false;
			this.xeniumBarrier = false;
			this.labWaterImmune = false;
			this.xenoPet = false;
			this.holyFire = false;
			this.bInfection = false;
			this.longerGuardians = false;
			this.blightedShield = false;
			this.holoMinion = false;
			this.guardianCooldownReduce = false;
			this.staveStreamShot = false;
			this.staveTripleShot = false;
			this.staveScatterShot = false;
			this.moltenEruption = false;
			this.staveQuadShot = false;
			this.lifeSteal1 = false;
			this.eldritchRoot = false;
			this.sleepPowder = false;
			this.vendetta = false;
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
			this.forestFriendly = false;
			this.girusCloaked = false;
			this.girusCloakTimer = 0;
			this.hairLoss = false;
			this.irradiatedEffect = 0;
			this.androidMinion = false;
			this.bileDebuff = false;
			this.bioweaponDebuff = false;
			this.thornCirclet = false;
			this.ksShieldGenerator = false;
			this.vlitchCoreAcc = false;
			this.oblitDrive = false;
			this.thornCrown = false;
			this.infectedHeart = false;
			this.StarSerpentMinion = false;
			this.spiritLevel = 0;
			this.spiritExtras = 0;
			this.corruptedTalisman = false;
			this.bloodedTalisman = false;
			this.birdMinion = false;
			this.wispSet = false;
			this.spiritWyvern1 = false;
			this.spiritWyvern2 = false;
			this.spiritGolemCross = false;
			this.lacerated = false;
			this.ukkonenMinion = false;
			this.dreamsong = false;
			this.cursedThornSet = false;
			this.shadowBinder = false;
			this.xenomiteSet = false;
			this.corruptedXenomiteSet = false;
			this.seedLifeTime = 1f;
			this.corruptedCopter = false;
			this.girusSniperDrone = false;
			this.shieldDrone = false;
			this.jellyfishDrone = false;
			this.moonStaves = false;
			this.omegaPower = false;
			this.halPet = false;
			this.tiedPet = false;
			this.lantardPet = false;
			this.tbotEyes = false;
			this.zapField = false;
			this.dreambound = false;
			this.brokenBlade = false;
			this.trueMeleeDamage = 1f;
			this.snipped = false;
			this.anglerPot = false;
			this.consecutiveStrikes = false;
			this.earthbind = false;
		}

		public override void UpdateDead()
		{
			this.ultraFlames = false;
			this.druidBane = false;
			this.holyFire = false;
			this.bInfection = false;
			this.sleepPowder = false;
			this.sandDust = false;
			this.irradiatedLevel = 0;
			this.irradiatedTimer = 0;
			this.hairLoss = false;
			this.irradiatedEffect = 0;
			this.girusCloakTimer = 0;
			this.bileDebuff = false;
			this.bioweaponDebuff = false;
			this.lacerated = false;
			this.earthbind = false;
		}

		public override void UpdateBadLifeRegen()
		{
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
			if ((this.ZoneLab || this.ZoneXeno || this.ZoneEvilXeno || this.ZoneEvilXeno2) && base.player.wet && !this.hazmatPower && !this.labWaterImmune && !base.player.lavaWet && !base.player.honeyWet)
			{
				if (base.player.lifeRegen > 10)
				{
					base.player.lifeRegen = 10;
				}
				base.player.lifeRegenTime = 0;
				base.player.lifeRegen -= 60;
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
			if (this.bileDebuff)
			{
				if (base.player.lifeRegen > 0)
				{
					base.player.lifeRegen = 0;
				}
				base.player.lifeRegenTime = 0;
				base.player.lifeRegen -= 5;
				base.player.statDefense -= 30;
			}
			if (this.bioweaponDebuff)
			{
				if (base.player.lifeRegen > 0)
				{
					base.player.lifeRegen = 0;
				}
				base.player.lifeRegenTime = 0;
				base.player.lifeRegen -= 12;
			}
			if (this.lacerated)
			{
				if (base.player.lifeRegen > 0)
				{
					base.player.lifeRegen = 0;
				}
				base.player.lifeRegenTime = 0;
				base.player.lifeRegen -= 25;
			}
		}

		public override void SetupStartInventory(IList<Item> items, bool mediumcoreDeath)
		{
			if (RedeConfigClient.Instance.StarterCore)
			{
				Item item = new Item();
				item.SetDefaults(ModContent.ItemType<EmptyCore>(), false);
				item.stack = 1;
				items.Add(item);
			}
		}

		public override void UpdateVanityAccessories()
		{
			for (int i = 13; i < 18 + base.player.extraAccessorySlots; i++)
			{
				Item item = base.player.armor[i];
				if (item.type == ModContent.ItemType<HazmatSuit>())
				{
					this.hazmatHideVanity = false;
					this.hazmatForceVanity = true;
				}
				if (item.type == ModContent.ItemType<CrownOfTheKing>())
				{
					this.chickenHideVanity = false;
					this.chickenForceVanity = true;
				}
				if (item.type == ModContent.ItemType<HEVSuit>())
				{
					this.HEVHideVanity = false;
					this.HEVForceVanity = true;
				}
			}
		}

		public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff)
		{
			if (this.chickenAccessory)
			{
				base.player.AddBuff(ModContent.BuffType<ChickenCrownBuff>(), 60, true);
			}
			if (this.hazmatAccessory)
			{
				base.player.AddBuff(ModContent.BuffType<HazmatSuitBuff>(), 60, true);
			}
			if (this.HEVAccessory)
			{
				base.player.AddBuff(ModContent.BuffType<HEVSuitBuff>(), 60, true);
			}
			if (this.snipped)
			{
				if (base.player.mount.CanFly)
				{
					base.player.mount.Dismount(base.player);
				}
				base.player.wingTimeMax /= 2;
				if (base.player.wingTime > (float)base.player.wingTimeMax)
				{
					base.player.wingTime = (float)base.player.wingTimeMax;
				}
			}
		}

		public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
		{
			if (this.hairLoss)
			{
				drawInfo.drawHair = false;
				drawInfo.drawAltHair = false;
			}
		}

		public override void FrameEffects()
		{
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
			if (this.ultraFlames && Main.rand.Next(2) == 0 && drawInfo.shadow == 0f)
			{
				int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), base.player.width + 4, base.player.height + 4, 92, base.player.velocity.X * 0.4f, base.player.velocity.Y * 0.4f, 100, default(Color), 1.2f);
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity *= 1.8f;
				Dust dust10 = Main.dust[dust];
				dust10.velocity.Y = dust10.velocity.Y - 0.5f;
				Main.playerDrawDust.Add(dust);
			}
			if (this.druidBane && Main.rand.Next(2) == 0 && drawInfo.shadow == 0f)
			{
				int dust2 = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), base.player.width + 4, base.player.height + 4, 163, base.player.velocity.X * 0.4f, base.player.velocity.Y * 0.4f, 100, default(Color), 1.2f);
				Main.dust[dust2].noGravity = true;
				Main.dust[dust2].velocity *= 1.8f;
				Dust dust11 = Main.dust[dust2];
				dust11.velocity.Y = dust11.velocity.Y - 0.5f;
				Main.playerDrawDust.Add(dust2);
			}
			if (this.holyFire && Main.rand.Next(2) == 0 && drawInfo.shadow == 0f)
			{
				int dust3 = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), base.player.width + 4, base.player.height + 4, 64, base.player.velocity.X * 0.4f, base.player.velocity.Y * 0.4f, 100, default(Color), 2f);
				Main.dust[dust3].noGravity = true;
				Main.dust[dust3].velocity *= 1.8f;
				Dust dust12 = Main.dust[dust3];
				dust12.velocity.Y = dust12.velocity.Y - 0.5f;
				Main.playerDrawDust.Add(dust3);
			}
			if (this.bileDebuff && Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
			{
				int dust4 = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), base.player.width + 4, base.player.height + 4, 74, base.player.velocity.X * 0.4f, base.player.velocity.Y * 0.4f, 100, default(Color), 1.2f);
				Main.dust[dust4].noGravity = true;
				Main.dust[dust4].velocity *= 1.8f;
				Dust dust13 = Main.dust[dust4];
				dust13.velocity.Y = dust13.velocity.Y - 0.5f;
				Main.playerDrawDust.Add(dust4);
			}
			if (this.bioweaponDebuff)
			{
				if (Main.rand.Next(15) == 0 && drawInfo.shadow == 0f)
				{
					int dust5 = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), base.player.width + 4, base.player.height + 4, 74, base.player.velocity.X * 0.4f, base.player.velocity.Y * 0.4f, 100, default(Color), 1.2f);
					Main.dust[dust5].noGravity = true;
					Main.dust[dust5].velocity *= 1.8f;
					Dust dust14 = Main.dust[dust5];
					dust14.velocity.Y = dust14.velocity.Y - 0.5f;
					Main.playerDrawDust.Add(dust5);
				}
				if (Main.rand.Next(10) == 0 && drawInfo.shadow == 0f)
				{
					int dust6 = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), base.player.width + 4, base.player.height + 4, 31, base.player.velocity.X * 0.4f, base.player.velocity.Y * 0.4f, 100, default(Color), 1.2f);
					Main.dust[dust6].noGravity = true;
					Main.dust[dust6].velocity *= 1.8f;
					Dust dust15 = Main.dust[dust6];
					dust15.velocity.Y = dust15.velocity.Y - 0.5f;
					Main.playerDrawDust.Add(dust6);
				}
			}
			if (base.player.HasBuff(ModContent.BuffType<SoulboundBuff>()) && Main.rand.Next(5) == 0 && drawInfo.shadow == 0f)
			{
				int dust7 = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), base.player.width + 4, base.player.height + 4, 20, base.player.velocity.X * 0.4f, base.player.velocity.Y * 0.4f, 100, default(Color), 1.2f);
				Main.dust[dust7].noGravity = true;
				Main.dust[dust7].velocity *= 1.8f;
				Dust dust16 = Main.dust[dust7];
				dust16.velocity.Y = dust16.velocity.Y - 0.5f;
				Main.playerDrawDust.Add(dust7);
			}
			if (base.player.HasBuff(ModContent.BuffType<ShadeboundBuff>()) && Main.rand.Next(5) == 0 && drawInfo.shadow == 0f)
			{
				int dust8 = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), base.player.width + 4, base.player.height + 4, ModContent.DustType<VoidFlame>(), base.player.velocity.X * 0.4f, base.player.velocity.Y * 0.4f, 100, default(Color), 1.8f);
				Main.dust[dust8].noGravity = true;
				Main.dust[dust8].velocity *= 1.8f;
				Dust dust17 = Main.dust[dust8];
				dust17.velocity.Y = dust17.velocity.Y - 0.5f;
				Main.playerDrawDust.Add(dust8);
			}
			if (base.player.HasBuff(ModContent.BuffType<StaticStunDebuff>()) && Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
			{
				int dust9 = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), base.player.width + 4, base.player.height + 4, 226, base.player.velocity.X * 0.4f, base.player.velocity.Y * 0.4f, 100, default(Color), 1.8f);
				Main.dust[dust9].noGravity = true;
				Main.dust[dust9].velocity *= 1.8f;
				Dust dust18 = Main.dust[dust9];
				dust18.velocity.Y = dust18.velocity.Y - 0.5f;
				Main.playerDrawDust.Add(dust9);
			}
		}

		public override void ModifyDrawLayers(List<PlayerLayer> layers)
		{
			if (this.hairLoss)
			{
				foreach (PlayerLayer playerLayer in layers)
				{
					if (playerLayer == PlayerLayer.Head)
					{
						playerLayer.visible = false;
					}
				}
			}
			if (this.earthbind)
			{
				RedePlayer.EarthbindLayer.visible = true;
				layers.Add(RedePlayer.EarthbindLayer);
			}
			if (base.player.HeldItem.type == ModContent.ItemType<SneakloneRemote>())
			{
				RedePlayer.MissileLauncherLayer.visible = true;
				layers.Insert(0, RedePlayer.MissileLauncherLayer);
			}
		}

		public override void OnEnterWorld(Player player)
		{
			if (!Redemption.musicPackLoaded)
			{
				Main.NewText("Also download 'Mod of Redemption: Music Pack' from the Mod Browser or perish.", 244, 71, byte.MaxValue, false);
			}
		}

		public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (this.thornCirclet && !item.summon)
			{
				this.thornCircletCounter++;
				if (this.thornCircletCounter >= 5)
				{
					for (int i = 0; i < Main.rand.Next(2, 6); i++)
					{
						Projectile.NewProjectile(base.player.Center, RedeHelper.PolarVector(Utils.NextFloat(Main.rand, 7f, 13f), Utils.ToRotation(Main.MouseWorld - base.player.Center) + Utils.NextFloat(Main.rand, -0.2f, 0.2f)), ModContent.ProjectileType<StingerFriendly>(), damage, knockBack, Main.myPlayer, 0f, 0f);
					}
					this.thornCircletCounter = 0;
				}
			}
			if (this.thornCrown && !item.summon)
			{
				this.thornCircletCounter++;
				if (this.thornCircletCounter >= 5)
				{
					for (int j = 0; j < Main.rand.Next(3, 7); j++)
					{
						Projectile.NewProjectile(base.player.Center, RedeHelper.PolarVector(Utils.NextFloat(Main.rand, 8f, 17f), Utils.ToRotation(Main.MouseWorld - base.player.Center) + Utils.NextFloat(Main.rand, -0.2f, 0.2f)), 484, damage, knockBack, Main.myPlayer, 0f, 0f);
					}
					this.thornCircletCounter = 0;
				}
			}
			return base.Shoot(item, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
		}

		public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
		{
			if (this.vendetta)
			{
				npc.AddBuff(20, 300, false);
			}
		}

		public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
		{
			if (this.powerSurgeSet)
			{
				this.powerSurgeCharge += damage;
			}
		}

		public override void OnHitByNPC(NPC npc, int damage, bool crit)
		{
			if (this.powerSurgeSet)
			{
				this.powerSurgeCharge += damage;
			}
		}

		public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
		{
			damage = (int)((float)damage * this.trueMeleeDamage);
			if (base.player.HasBuff(ModContent.BuffType<BioweaponFlaskBuff>()))
			{
				target.AddBuff(ModContent.BuffType<BioweaponDebuff>(), 900, false);
			}
			if (base.player.HasBuff(ModContent.BuffType<BileFlaskBuff>()))
			{
				target.AddBuff(ModContent.BuffType<BileDebuff>(), 900, false);
			}
		}

		public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
		{
			if (this.bloodyCollar && Main.rand.Next(5) == 0)
			{
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, 0f, 0f, ModContent.ProjectileType<BloodPulse>(), 50, 0f, base.player.whoAmI, 0f, 0f);
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
				base.player.AddBuff(ModContent.BuffType<EldritchRootBuff>(), 180, true);
			}
			if (this.powerSurgeSet && Main.rand.Next(10) == 0)
			{
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, 1f, -3f, ModContent.ProjectileType<EnergyOrb1>(), 200, 0f, Main.myPlayer, 0f, 0f);
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, 0f, -1f, ModContent.ProjectileType<EnergyOrb1>(), 200, 0f, Main.myPlayer, 0f, 0f);
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, 2f, -5f, ModContent.ProjectileType<EnergyOrb1>(), 200, 0f, Main.myPlayer, 0f, 0f);
			}
			if (this.bloodShinkiteSet && Main.rand.Next(4) == 0)
			{
				Projectile.NewProjectile(target.Center.X, target.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)Main.rand.Next(-11, 0), ModContent.ProjectileType<BloodOrbPro3>(), 0, 0f, Main.myPlayer, 0f, 0f);
			}
			if (this.cursedShinkiteSet && Main.rand.Next(6) == 0 && proj.type != ModContent.ProjectileType<CursedExp>())
			{
				Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, ModContent.ProjectileType<CursedExp>(), 100, 0f, Main.myPlayer, 0f, 0f);
			}
			if (this.smallShadeSet)
			{
				target.AddBuff(ModContent.BuffType<BlackenedHeartDebuff>(), 300, false);
			}
			if (this.oblitDrive && Main.rand.Next(10) == 0)
			{
				if (!base.player.HasBuff(ModContent.BuffType<OblitBuff1>()) && !base.player.HasBuff(ModContent.BuffType<OblitBuff2>()) && !base.player.HasBuff(ModContent.BuffType<OblitBuff3>()) && !base.player.HasBuff(ModContent.BuffType<OblitBuff4>()) && !base.player.HasBuff(ModContent.BuffType<OblitBuff5>()))
				{
					base.player.AddBuff(ModContent.BuffType<OblitBuff1>(), 600, true);
				}
				else if (base.player.HasBuff(ModContent.BuffType<OblitBuff1>()))
				{
					base.player.AddBuff(ModContent.BuffType<OblitBuff2>(), 600, true);
				}
				else if (base.player.HasBuff(ModContent.BuffType<OblitBuff2>()))
				{
					base.player.AddBuff(ModContent.BuffType<OblitBuff3>(), 600, true);
				}
				else if (base.player.HasBuff(ModContent.BuffType<OblitBuff3>()))
				{
					base.player.AddBuff(ModContent.BuffType<OblitBuff4>(), 600, true);
				}
				else if (base.player.HasBuff(ModContent.BuffType<OblitBuff4>()))
				{
					base.player.AddBuff(ModContent.BuffType<OblitBuff5>(), 600, true);
				}
				else if (base.player.HasBuff(ModContent.BuffType<OblitBuff5>()))
				{
					base.player.AddBuff(ModContent.BuffType<OblitBuff5>(), 600, true);
				}
			}
			if (this.wispSet && target.life <= 0 && target.lifeMax > 5 && Main.rand.Next(10) == 0 && target.type != 288 && target.type != ModContent.NPCType<LostSoul1>() && target.type != ModContent.NPCType<LostSoul2>() && target.type != ModContent.NPCType<LostSoul3>() && target.type != ModContent.NPCType<SmallShadesoulNPC>() && target.type != ModContent.NPCType<ShadesoulNPC>())
			{
				NPC.NewNPC((int)target.Center.X, (int)target.Center.Y, 288, 0, 0f, 0f, 0f, 0f, 255);
			}
			if (base.player.HasBuff(ModContent.BuffType<SoulboundBuff>()))
			{
				if (this.spiritLevel == 0)
				{
					if (target.life <= 0 && target.lifeMax > 5 && Main.rand.Next(15) == 0 && target.type != 288 && target.type != ModContent.NPCType<LostSoul1>() && target.type != ModContent.NPCType<LostSoul2>() && target.type != ModContent.NPCType<LostSoul3>() && target.type != ModContent.NPCType<SmallShadesoulNPC>() && target.type != ModContent.NPCType<ShadesoulNPC>())
					{
						NPC.NewNPC((int)target.Center.X, (int)target.Center.Y, ModContent.NPCType<LostSoul1>(), 0, 0f, 0f, 0f, 0f, 255);
					}
				}
				else if (this.spiritLevel == 1)
				{
					if (target.life <= 0 && target.lifeMax > 5 && Main.rand.Next(10) == 0 && target.type != 288 && target.type != ModContent.NPCType<LostSoul1>() && target.type != ModContent.NPCType<LostSoul2>() && target.type != ModContent.NPCType<LostSoul3>() && target.type != ModContent.NPCType<SmallShadesoulNPC>() && target.type != ModContent.NPCType<ShadesoulNPC>())
					{
						NPC.NewNPC((int)target.Center.X, (int)target.Center.Y, ModContent.NPCType<LostSoul1>(), 0, 0f, 0f, 0f, 0f, 255);
					}
				}
				else if (this.spiritLevel == 2)
				{
					if (target.life <= 0 && target.lifeMax > 5 && Main.rand.Next(6) == 0 && target.type != 288 && target.type != ModContent.NPCType<LostSoul1>() && target.type != ModContent.NPCType<LostSoul2>() && target.type != ModContent.NPCType<LostSoul3>() && target.type != ModContent.NPCType<SmallShadesoulNPC>() && target.type != ModContent.NPCType<ShadesoulNPC>())
					{
						NPC.NewNPC((int)target.Center.X, (int)target.Center.Y, ModContent.NPCType<LostSoul1>(), 0, 0f, 0f, 0f, 0f, 255);
					}
				}
				else if (this.spiritLevel >= 3 && target.life <= 0 && target.lifeMax > 5 && Main.rand.Next(4) == 0 && target.type != 288 && target.type != ModContent.NPCType<LostSoul1>() && target.type != ModContent.NPCType<LostSoul2>() && target.type != ModContent.NPCType<LostSoul3>() && target.type != ModContent.NPCType<SmallShadesoulNPC>() && target.type != ModContent.NPCType<ShadesoulNPC>())
				{
					NPC.NewNPC((int)target.Center.X, (int)target.Center.Y, ModContent.NPCType<LostSoul1>(), 0, 0f, 0f, 0f, 0f, 255);
				}
			}
			if (base.player.HasBuff(ModContent.BuffType<ShadeboundBuff>()))
			{
				if (this.spiritLevel == 5)
				{
					if (target.life <= 0 && target.lifeMax > 5 && Main.rand.Next(10) == 0 && target.type != 288 && target.type != ModContent.NPCType<LostSoul1>() && target.type != ModContent.NPCType<LostSoul2>() && target.type != ModContent.NPCType<LostSoul3>() && target.type != ModContent.NPCType<SmallShadesoulNPC>() && target.type != ModContent.NPCType<ShadesoulNPC>())
					{
						NPC.NewNPC((int)target.Center.X, (int)target.Center.Y, ModContent.NPCType<SmallShadesoulNPC>(), 0, 0f, 0f, 0f, 0f, 255);
					}
				}
				else if (this.spiritLevel == 6)
				{
					if (target.life <= 0 && target.lifeMax > 5 && Main.rand.Next(6) == 0 && target.type != 288 && target.type != ModContent.NPCType<LostSoul1>() && target.type != ModContent.NPCType<LostSoul2>() && target.type != ModContent.NPCType<LostSoul3>() && target.type != ModContent.NPCType<SmallShadesoulNPC>() && target.type != ModContent.NPCType<ShadesoulNPC>())
					{
						NPC.NewNPC((int)target.Center.X, (int)target.Center.Y, ModContent.NPCType<SmallShadesoulNPC>(), 0, 0f, 0f, 0f, 0f, 255);
					}
				}
				else if (this.spiritLevel >= 7 && target.life <= 0 && target.lifeMax > 5 && Main.rand.Next(3) == 0 && target.type != 288 && target.type != ModContent.NPCType<LostSoul1>() && target.type != ModContent.NPCType<LostSoul2>() && target.type != ModContent.NPCType<LostSoul3>() && target.type != ModContent.NPCType<SmallShadesoulNPC>() && target.type != ModContent.NPCType<ShadesoulNPC>())
				{
					NPC.NewNPC((int)target.Center.X, (int)target.Center.Y, ModContent.NPCType<SmallShadesoulNPC>(), 0, 0f, 0f, 0f, 0f, 255);
				}
			}
			if (target.life <= 0 && (proj.type == ModContent.ProjectileType<NightSoulPro1>() || proj.type == ModContent.ProjectileType<LightSoulPro1>()))
			{
				base.player.AddBuff(ModContent.BuffType<SoulStaveBuff>(), 3600, true);
			}
			if (this.cursedThornSet && crit)
			{
				Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, ModContent.ProjectileType<AkkaSeedF>(), 400, 3f, Main.myPlayer, 0f, 0f);
			}
			if (this.shadowBinder && target.life <= 0 && target.lifeMax >= 5000)
			{
				for (int index = 0; index < 10; index++)
				{
					Dust dust = Dust.NewDustDirect(new Vector2(target.position.X, target.position.Y), target.width, target.height, 20, 0f, 0f, 100, default(Color), 2f);
					dust.velocity = -base.player.DirectionTo(dust.position) * 20f;
					dust.noGravity = true;
				}
				if (this.shadowBinderCharge < 100)
				{
					this.shadowBinderCharge++;
				}
			}
			if (this.xenomiteSet && crit && proj.type != ModContent.ProjectileType<XenoYoyoShard>() && Main.rand.Next(2) == 0)
			{
				for (int i = 0; i < 6; i++)
				{
					int p = Projectile.NewProjectile(new Vector2(base.player.Center.X, base.player.Center.Y), RedeHelper.PolarVector(15f + Utils.NextFloat(Main.rand, -4f, 4f), Utils.ToRotation(Main.MouseWorld - base.player.Center) + Utils.NextFloat(Main.rand, -0.1f, 0.1f)), ModContent.ProjectileType<XenoYoyoShard>(), 30, 3f, Main.myPlayer, 0f, 0f);
					Main.projectile[p].melee = false;
				}
			}
			if (this.corruptedXenomiteSet && crit && proj.type != ModContent.ProjectileType<PhantomDagger>())
			{
				Projectile.NewProjectile(new Vector2(base.player.Center.X, base.player.Center.Y), RedeHelper.PolarVector(20f, Utils.ToRotation(Main.MouseWorld - base.player.Center)), ModContent.ProjectileType<PhantomDagger>(), 140, 3f, Main.myPlayer, 0f, 0f);
			}
		}

		public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
		{
			if (this.bloodyCollar && Main.rand.Next(5) == 0)
			{
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, 0f, 0f, ModContent.ProjectileType<BloodPulse>(), 50, 0f, base.player.whoAmI, 0f, 0f);
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
				base.player.AddBuff(ModContent.BuffType<EldritchRootBuff>(), 180, true);
			}
			if (this.powerSurgeSet && Main.rand.Next(10) == 0)
			{
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, 1f, -3f, ModContent.ProjectileType<EnergyOrb1>(), 200, 0f, Main.myPlayer, 0f, 0f);
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, 0f, -1f, ModContent.ProjectileType<EnergyOrb1>(), 200, 0f, Main.myPlayer, 0f, 0f);
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, 2f, -5f, ModContent.ProjectileType<EnergyOrb1>(), 200, 0f, Main.myPlayer, 0f, 0f);
			}
			if (this.bloodShinkiteSet && Main.rand.Next(4) == 0)
			{
				Projectile.NewProjectile(target.Center.X, target.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-3 + Main.rand.Next(-11, 0)), ModContent.ProjectileType<BloodOrbPro3>(), 0, 0f, Main.myPlayer, 0f, 0f);
			}
			if (this.cursedShinkiteSet && Main.rand.Next(6) == 0)
			{
				Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, ModContent.ProjectileType<CursedExp>(), 100, 0f, Main.myPlayer, 0f, 0f);
			}
			if (this.smallShadeSet)
			{
				target.AddBuff(ModContent.BuffType<BlackenedHeartDebuff>(), 300, false);
			}
			if (this.oblitDrive && Main.rand.Next(10) == 0)
			{
				if (!base.player.HasBuff(ModContent.BuffType<OblitBuff1>()) && !base.player.HasBuff(ModContent.BuffType<OblitBuff2>()) && !base.player.HasBuff(ModContent.BuffType<OblitBuff3>()) && !base.player.HasBuff(ModContent.BuffType<OblitBuff4>()) && !base.player.HasBuff(ModContent.BuffType<OblitBuff5>()))
				{
					base.player.AddBuff(ModContent.BuffType<OblitBuff1>(), 600, true);
				}
				else if (base.player.HasBuff(ModContent.BuffType<OblitBuff1>()))
				{
					base.player.AddBuff(ModContent.BuffType<OblitBuff2>(), 600, true);
					base.player.DelBuff(ModContent.BuffType<OblitBuff1>());
				}
				else if (base.player.HasBuff(ModContent.BuffType<OblitBuff2>()))
				{
					base.player.AddBuff(ModContent.BuffType<OblitBuff3>(), 600, true);
					base.player.DelBuff(ModContent.BuffType<OblitBuff2>());
				}
				else if (base.player.HasBuff(ModContent.BuffType<OblitBuff3>()))
				{
					base.player.AddBuff(ModContent.BuffType<OblitBuff4>(), 600, true);
					base.player.DelBuff(ModContent.BuffType<OblitBuff3>());
				}
				else if (base.player.HasBuff(ModContent.BuffType<OblitBuff4>()))
				{
					base.player.AddBuff(ModContent.BuffType<OblitBuff5>(), 600, true);
					base.player.DelBuff(ModContent.BuffType<OblitBuff4>());
				}
				else if (base.player.HasBuff(ModContent.BuffType<OblitBuff5>()))
				{
					base.player.AddBuff(ModContent.BuffType<OblitBuff5>(), 600, true);
				}
			}
			if (this.wispSet && target.life <= 0 && Main.rand.Next(10) == 0 && target.type != 288)
			{
				NPC.NewNPC((int)target.Center.X, (int)target.Center.Y, 288, 0, 0f, 0f, 0f, 0f, 255);
			}
			if (this.shadowBinder && target.life <= 0 && target.lifeMax >= 5000)
			{
				for (int index = 0; index < 10; index++)
				{
					Dust dust = Dust.NewDustDirect(new Vector2(target.position.X, target.position.Y), target.width, target.height, 20, 0f, 0f, 100, default(Color), 2f);
					dust.velocity = -base.player.DirectionTo(dust.position) * 20f;
					dust.noGravity = true;
				}
				if (this.shadowBinderCharge < 100)
				{
					this.shadowBinderCharge++;
				}
			}
			if (this.xenomiteSet && crit && Main.rand.Next(2) == 0)
			{
				for (int i = 0; i < 6; i++)
				{
					int p = Projectile.NewProjectile(new Vector2(base.player.Center.X, base.player.Center.Y), RedeHelper.PolarVector(15f + Utils.NextFloat(Main.rand, -4f, 4f), Utils.ToRotation(Main.MouseWorld - base.player.Center) + Utils.NextFloat(Main.rand, -0.1f, 0.1f)), ModContent.ProjectileType<XenoYoyoShard>(), 30, 3f, Main.myPlayer, 0f, 0f);
					Main.projectile[p].melee = false;
				}
			}
			if (this.corruptedXenomiteSet && crit)
			{
				Projectile.NewProjectile(new Vector2(base.player.Center.X, base.player.Center.Y), RedeHelper.PolarVector(20f, Utils.ToRotation(Main.MouseWorld - base.player.Center)), ModContent.ProjectileType<PhantomDagger>(), 140, 3f, Main.myPlayer, 0f, 0f);
			}
			if (this.brokenBlade && base.player.ownedProjectileCounts[ModContent.ProjectileType<PhantomCleaverF>()] == 0 && RedeHelper.Chance(0.1f))
			{
				Projectile.NewProjectile(new Vector2(target.Center.X, target.position.Y - 200f), Vector2.Zero, ModContent.ProjectileType<PhantomCleaverF>(), item.damage * 3, item.knockBack, Main.myPlayer, (float)target.whoAmI, 0f);
			}
		}

		public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			if (base.player.FindBuffIndex(ModContent.BuffType<HazardLaserDebuff>()) != -1 && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " got lasered!");
			}
			if (base.player.FindBuffIndex(ModContent.BuffType<XenomiteDebuff>()) != -1 && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " got infected");
			}
			if (base.player.FindBuffIndex(ModContent.BuffType<XenomiteDebuff2>()) != -1 && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " got heavily infected");
			}
			if (base.player.FindBuffIndex(ModContent.BuffType<RadioactiveFalloutDebuff>()) != -1 && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " forgot to wear a gas mask");
			}
			if (base.player.FindBuffIndex(ModContent.BuffType<DisgustingDebuff>()) != -1 && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " got diddily darn egg'd");
			}
			if (base.player.FindBuffIndex(ModContent.BuffType<DruidsBane>()) != -1 && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " was baned by the druids");
			}
			if (base.player.FindBuffIndex(ModContent.BuffType<GloomShroomDebuff>()) != -1 && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " got gloomed");
			}
			if (base.player.FindBuffIndex(ModContent.BuffType<UltraFlameDebuff>()) != -1 && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " got melted to the bone");
			}
			if (base.player.FindBuffIndex(ModContent.BuffType<BlackenedHeartDebuff>()) != -1 && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " became soulless");
			}
			if (base.player.FindBuffIndex(ModContent.BuffType<HolyFireDebuff>()) != -1 && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " was too glorious");
			}
			if (base.player.FindBuffIndex(ModContent.BuffType<BInfectionDebuff>()) != -1 && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " got infected");
			}
			if (damageSource.SourceNPCIndex >= 0 && Main.npc[damageSource.SourceNPCIndex].type == ModContent.NPCType<AJollyMadman>())
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " became hollow");
			}
			if (damageSource.SourceNPCIndex >= 0 && Main.npc[damageSource.SourceNPCIndex].type == ModContent.NPCType<Blobble>())
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " got blobble'd!");
			}
			if (damageSource.SourceNPCIndex >= 0 && (Main.npc[damageSource.SourceNPCIndex].type == ModContent.NPCType<ChickenCultist>() || Main.npc[damageSource.SourceNPCIndex].type == ModContent.NPCType<ChickenMan>() || Main.npc[damageSource.SourceNPCIndex].type == ModContent.NPCType<ShieldedChickenMan>()))
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " got pecked to death");
			}
			if (this.bioweaponDebuff)
			{
				Main.PlaySound(SoundID.Item14, base.player.position);
				for (int i = 0; i < 10; i++)
				{
					int dustIndex3 = Dust.NewDust(new Vector2(base.player.position.X, base.player.position.Y), base.player.width, base.player.height, 31, 0f, 0f, 100, default(Color), 5f);
					Main.dust[dustIndex3].velocity *= 1.4f;
				}
				for (int j = 0; j < 20; j++)
				{
					int dustIndex4 = Dust.NewDust(new Vector2(base.player.position.X, base.player.position.Y), base.player.width, base.player.height, 6, 0f, 0f, 100, default(Color), 3f);
					Main.dust[dustIndex4].noGravity = true;
					Main.dust[dustIndex4].velocity *= 5f;
					dustIndex4 = Dust.NewDust(new Vector2(base.player.position.X, base.player.position.Y), base.player.width, base.player.height, 6, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex4].velocity *= 3f;
				}
				for (int g = 0; g < 2; g++)
				{
					int goreIndex = Gore.NewGore(new Vector2(base.player.position.X + (float)(base.player.width / 2) - 24f, base.player.position.Y + (float)(base.player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
					goreIndex = Gore.NewGore(new Vector2(base.player.position.X + (float)(base.player.width / 2) - 24f, base.player.position.Y + (float)(base.player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				}
			}
			if (this.omegaPower)
			{
				playSound = false;
				genGore = false;
				Main.PlaySound(SoundID.Item14, base.player.position);
				if (base.player.wet && !base.player.lavaWet && !base.player.honeyWet)
				{
					for (int k = 0; k < 30; k++)
					{
						int dustIndex5 = Dust.NewDust(new Vector2(base.player.position.X, base.player.position.Y), base.player.width, base.player.height, 31, 0f, 0f, 100, default(Color), 5f);
						Main.dust[dustIndex5].velocity *= 1.4f;
					}
					for (int l = 0; l < 40; l++)
					{
						int dustIndex6 = Dust.NewDust(new Vector2(base.player.position.X, base.player.position.Y), base.player.width, base.player.height, 6, 0f, 0f, 100, default(Color), 3f);
						Main.dust[dustIndex6].noGravity = true;
						Main.dust[dustIndex6].velocity *= 5f;
						dustIndex6 = Dust.NewDust(new Vector2(base.player.position.X, base.player.position.Y), base.player.width, base.player.height, 6, 0f, 0f, 100, default(Color), 2f);
						Main.dust[dustIndex6].velocity *= 3f;
					}
					for (int g2 = 0; g2 < 2; g2++)
					{
						int goreIndex2 = Gore.NewGore(new Vector2(base.player.position.X + (float)(base.player.width / 2) - 24f, base.player.position.Y + (float)(base.player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
						Main.gore[goreIndex2].scale = 1.5f;
						Main.gore[goreIndex2].velocity.X = Main.gore[goreIndex2].velocity.X + 1.5f;
						Main.gore[goreIndex2].velocity.Y = Main.gore[goreIndex2].velocity.Y + 1.5f;
						goreIndex2 = Gore.NewGore(new Vector2(base.player.position.X + (float)(base.player.width / 2) - 24f, base.player.position.Y + (float)(base.player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
						Main.gore[goreIndex2].scale = 1.5f;
						Main.gore[goreIndex2].velocity.X = Main.gore[goreIndex2].velocity.X - 1.5f;
						Main.gore[goreIndex2].velocity.Y = Main.gore[goreIndex2].velocity.Y + 1.5f;
						goreIndex2 = Gore.NewGore(new Vector2(base.player.position.X + (float)(base.player.width / 2) - 24f, base.player.position.Y + (float)(base.player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
						Main.gore[goreIndex2].scale = 1.5f;
						Main.gore[goreIndex2].velocity.X = Main.gore[goreIndex2].velocity.X + 1.5f;
						Main.gore[goreIndex2].velocity.Y = Main.gore[goreIndex2].velocity.Y - 1.5f;
						goreIndex2 = Gore.NewGore(new Vector2(base.player.position.X + (float)(base.player.width / 2) - 24f, base.player.position.Y + (float)(base.player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
						Main.gore[goreIndex2].scale = 1.5f;
						Main.gore[goreIndex2].velocity.X = Main.gore[goreIndex2].velocity.X - 1.5f;
						Main.gore[goreIndex2].velocity.Y = Main.gore[goreIndex2].velocity.Y - 1.5f;
					}
				}
				else
				{
					for (int m = 0; m < 10; m++)
					{
						int dustIndex7 = Dust.NewDust(new Vector2(base.player.position.X, base.player.position.Y), base.player.width, base.player.height, 31, 0f, 0f, 100, default(Color), 5f);
						Main.dust[dustIndex7].velocity *= 1.4f;
					}
					for (int n = 0; n < 15; n++)
					{
						int dustIndex8 = Dust.NewDust(new Vector2(base.player.position.X, base.player.position.Y), base.player.width, base.player.height, 6, 0f, 0f, 100, default(Color), 3f);
						Main.dust[dustIndex8].noGravity = true;
						Main.dust[dustIndex8].velocity *= 5f;
						dustIndex8 = Dust.NewDust(new Vector2(base.player.position.X, base.player.position.Y), base.player.width, base.player.height, 6, 0f, 0f, 100, default(Color), 2f);
						Main.dust[dustIndex8].velocity *= 3f;
					}
				}
			}
			if (this.ksShieldGenerator && base.player.FindBuffIndex(ModContent.BuffType<KSShieldCooldown>()) == -1)
			{
				base.player.statLife = 100;
				base.player.HealEffect(100, true);
				base.player.immune = true;
				base.player.AddBuff(ModContent.BuffType<KSShieldBuff>(), 3600, true);
				base.player.AddBuff(ModContent.BuffType<KSShieldCooldown>(), 18000, true);
				if (base.player.ownedProjectileCounts[ModContent.ProjectileType<KSShieldPro>()] == 0)
				{
					Projectile.NewProjectile(base.player.position, Vector2.Zero, ModContent.ProjectileType<KSShieldPro>(), 0, 0f, base.player.whoAmI, 0f, 0f);
				}
				Main.PlaySound(SoundID.Item93, base.player.position);
				return false;
			}
			if (this.natureGuardian26)
			{
				base.player.statLife++;
				base.player.HealEffect(1, true);
				base.player.immune = true;
				this.natureGuardian26 = false;
				if (!Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Reflect").WithVolume(0.4f).WithPitchVariance(0.1f), -1, -1);
				}
				return false;
			}
			return true;
		}

		public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			if (this.omegaPower)
			{
				playSound = false;
				Main.PlaySound(SoundID.NPCHit4, base.player.position);
			}
			return true;
		}

		public override void PostUpdate()
		{
			if (base.player.ZoneSandstorm && (this.ZoneXeno || this.ZoneEvilXeno || this.ZoneEvilXeno2))
			{
				RedePlayer.EmitDust();
			}
			if (this.powerSurgeCharge >= 300)
			{
				Main.PlaySound(SoundID.Item74, base.player.position);
				for (int i = 0; i < 25; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.player.position.X, base.player.position.Y), base.player.width, base.player.height, 269, 0f, 0f, 100, default(Color), 3f);
					Main.dust[dustIndex].velocity *= 5.4f;
				}
				base.player.AddBuff(ModContent.BuffType<PowerSurgeBuff>(), 420, true);
				this.powerSurgeCharge = 0;
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
			int maxValue = 1;
			if (!flag)
			{
				return;
			}
			if (Main.rand.Next(maxValue) != 0)
			{
				return;
			}
			int num = Math.Sign(Main.windSpeed);
			float num2 = Math.Abs(Main.windSpeed);
			if (num2 < 0.01f)
			{
				return;
			}
			float num3 = (float)num * MathHelper.Lerp(0.9f, 1f, num2);
			float num4 = 2000f / (float)sandTiles;
			float num5 = 3f / num4;
			num5 = MathHelper.Clamp(num5, 0.77f, 1f);
			int num6 = (int)num4;
			float num7 = (float)Main.screenWidth / (float)Main.maxScreenW;
			int num8 = (int)(1000f * num7);
			float num9 = 20f * Sandstorm.Severity;
			float num10 = (float)num8 * (Main.gfxQuality * 0.5f + 0.5f) + (float)num8 * 0.1f - (float)Dust.SandStormCount;
			if (num10 <= 0f)
			{
				return;
			}
			float num11 = (float)Main.screenWidth + 1000f;
			float num12 = (float)Main.screenHeight;
			Vector2 value = Main.screenPosition + player.velocity;
			WeightedRandom<Color> weightedRandom = new WeightedRandom<Color>();
			weightedRandom.Add(new Color(200, 160, 20, 180), (double)(Main.screenTileCounts[53] + Main.screenTileCounts[396] + Main.screenTileCounts[397]));
			weightedRandom.Add(new Color(103, 98, 122, 180), (double)(Main.screenTileCounts[112] + Main.screenTileCounts[400] + Main.screenTileCounts[398]));
			weightedRandom.Add(new Color(135, 43, 34, 180), (double)(Main.screenTileCounts[234] + Main.screenTileCounts[401] + Main.screenTileCounts[399]));
			weightedRandom.Add(new Color(213, 196, 197, 180), (double)(Main.screenTileCounts[116] + Main.screenTileCounts[403] + Main.screenTileCounts[402]));
			float num13 = MathHelper.Lerp(0.2f, 0.35f, Sandstorm.Severity);
			float num14 = MathHelper.Lerp(0.5f, 0.7f, Sandstorm.Severity);
			float amount = (num5 - 0.77f) / 0.23000002f;
			int maxValue2 = (int)MathHelper.Lerp(1f, 10f, amount);
			int num15 = 0;
			while ((float)num15 < num9)
			{
				if (Main.rand.Next(num6 / 4) == 0)
				{
					Vector2 vector = new Vector2(Utils.NextFloat(Main.rand) * num11 - 500f, Utils.NextFloat(Main.rand) * -50f);
					if (Main.rand.Next(3) == 0 && num == 1)
					{
						vector.X = (float)(Main.rand.Next(500) - 500);
					}
					else if (Main.rand.Next(3) == 0 && num == -1)
					{
						vector.X = (float)(Main.rand.Next(500) + Main.screenWidth);
					}
					if (vector.X < 0f || vector.X > (float)Main.screenWidth)
					{
						vector.Y += Utils.NextFloat(Main.rand) * num12 * 0.9f;
					}
					vector += value;
					int num16 = (int)vector.X / 16;
					int num17 = (int)vector.Y / 16;
					if (Main.tile[num16, num17] != null && Main.tile[num16, num17].wall == 0)
					{
						for (int i = 0; i < 1; i++)
						{
							Dust dust = Main.dust[Dust.NewDust(vector, 10, 10, 256, 0f, 0f, 0, default(Color), 1f)];
							dust.velocity.Y = 2f + Utils.NextFloat(Main.rand) * 0.2f;
							Dust dust2 = dust;
							dust2.velocity.Y = dust2.velocity.Y * dust.scale;
							Dust dust3 = dust;
							dust3.velocity.Y = dust3.velocity.Y * 0.35f;
							dust.velocity.X = num3 * 5f + Utils.NextFloat(Main.rand) * 1f;
							Dust dust4 = dust;
							dust4.velocity.X = dust4.velocity.X + num3 * num14 * 20f;
							dust.fadeIn += num14 * 0.2f;
							dust.velocity *= 1f + num13 * 0.5f;
							dust.color = weightedRandom;
							dust.velocity *= 1f + num13;
							dust.velocity *= num5;
							dust.scale = 0.9f;
							num10 -= 1f;
							if (num10 <= 0f)
							{
								break;
							}
							if (Main.rand.Next(maxValue2) != 0)
							{
								i--;
								vector += Utils.RandomVector2(Main.rand, -10f, 10f) + dust.velocity * -1.1f;
							}
						}
						if (num10 <= 0f)
						{
							return;
						}
					}
				}
				num15++;
			}
		}

		public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
		{
			if (Main.rand.Next(100) < 10 + (base.player.cratePotion ? 10 : 0) && liquidType == 0 && this.ZoneLab && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
			{
				caughtType = ModContent.ItemType<LabCrate>();
			}
			if (liquidType == 0 && this.ZoneSoulless)
			{
				if (Main.rand.Next(100) < 10 + (base.player.cratePotion ? 10 : 0))
				{
					caughtType = ModContent.ItemType<ShadeCrate>();
				}
				else if (Main.rand.Next(8) == 0)
				{
					caughtType = ModContent.ItemType<AbyssBloskus>();
				}
				else if (Main.rand.Next(8) == 0)
				{
					caughtType = ModContent.ItemType<SlumberEel>();
				}
				else if (Main.rand.Next(6) == 0)
				{
					caughtType = ModContent.ItemType<ChakrogAngler>();
				}
				else if (Main.rand.Next(6) == 0)
				{
					caughtType = ModContent.ItemType<AbyssStinger>();
				}
				else if (Main.rand.Next(18) == 0)
				{
					caughtType = ModContent.ItemType<DarkStar>();
				}
				else if (Main.rand.Next(3) == 0)
				{
					caughtType = ModContent.ItemType<LurkingKetred>();
				}
				else if (Main.rand.Next(5) == 0)
				{
					caughtType = ModContent.ItemType<MaskFish>();
				}
				else
				{
					caughtType = ModContent.ItemType<ShadeFish>();
				}
			}
			if ((this.ZoneXeno || this.ZoneEvilXeno || this.ZoneEvilXeno2) && liquidType == 0 && questFish == ModContent.ItemType<XenChomper>() && Main.rand.Next(2) == 0)
			{
				caughtType = ModContent.ItemType<XenChomper>();
			}
		}

		public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
		{
			if (this.omegaPower)
			{
				Dust.NewDust(new Vector2(base.player.position.X - base.player.velocity.X * 2f, base.player.position.Y - 2f - base.player.velocity.Y * 2f), base.player.width, base.player.height, 226, 0f, 0f, 100, default(Color), 2f);
			}
			if (this.seedHit && Main.rand.Next(3) == 0)
			{
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), 483, 20, 1f, Main.myPlayer, 0f, 0f);
			}
			if (this.golemWateringCan)
			{
				for (int i = 0; i < 2; i++)
				{
					Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), 483, 20, 1f, Main.myPlayer, 0f, 0f);
				}
				if (this.moreSeeds)
				{
					Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), 483, 20, 1f, Main.myPlayer, 0f, 0f);
				}
			}
			if (this.skeletonCan)
			{
				for (int j = 0; j < Main.rand.Next(1, 3); j++)
				{
					Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<BoneSeed>(), 10, 1f, Main.myPlayer, 0f, 0f);
				}
				if (this.moreSeeds)
				{
					Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<BoneSeed>(), 10, 1f, Main.myPlayer, 0f, 0f);
				}
			}
			if (this.spiritChicken)
			{
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<SpiritChickenPro>(), 9, 1f, Main.myPlayer, 0f, 0f);
			}
			if (this.spiritSkull1)
			{
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<SpiritSkullPro>(), 12, 1f, Main.myPlayer, 0f, 0f);
			}
			if (this.taintedNecklace)
			{
				for (int k = 0; k < Main.rand.Next(2, 5); k++)
				{
					Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<CorruptSoul>(), 50, 1f, Main.myPlayer, 0f, 0f);
				}
			}
			if (this.xeniumDischarge && Main.rand.Next(4) == 0)
			{
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, 0f, 0f, ModContent.ProjectileType<XeniumDischarge>(), 0, 0f, Main.myPlayer, 0f, 0f);
			}
			if (this.spiritChicken2)
			{
				for (int l = 0; l < 2; l++)
				{
					Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<EtherealChickenPro>(), 300, 1f, Main.myPlayer, 0f, 0f);
				}
			}
			if (this.shadeSet && Main.rand.Next(3) == 0)
			{
				int pieCut = 16;
				for (int m = 0; m < pieCut; m++)
				{
					int projID = Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, 0f, 0f, ModContent.ProjectileType<CriesOfGriefPro3>(), 400, 0f, Main.myPlayer, 0f, 0f);
					Main.projectile[projID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)m / (float)pieCut * 6.28f);
				}
			}
		}

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

		public bool ZoneEvilXeno;

		public bool ZoneEvilXeno2;

		public bool heartEmblem;

		public bool moreSeeds;

		public float staveSpeed = 1f;

		public bool fasterSeedbags;

		public bool fasterSpirits;

		public bool moreSpirits;

		public bool golemWateringCan;

		public bool spiritHoming;

		public bool spiritChicken;

		public bool extraSeed;

		public bool spiritPierce;

		public bool frostburnSeedbag;

		public bool spiritSkull1;

		public bool burnStaves;

		public bool seedHit;

		public bool bloodyCollar;

		public bool taintedNecklace;

		public bool sapphireBonus;

		public bool scarletBonus;

		public bool skeletonCan;

		public bool ultraFlames;

		public bool creationBonus;

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

		public bool natureGuardian28;

		public bool ZoneLab;

		public bool hazmatAccessoryPrevious;

		public bool hazmatAccessory;

		public bool hazmatHideVanity;

		public bool hazmatForceVanity;

		public bool hazmatPower;

		public bool skeletonFriendly;

		public bool xeniumMinion;

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

		public bool guardianCooldownReduce;

		public bool staveStreamShot;

		public bool staveTripleShot;

		public bool staveScatterShot;

		public bool moltenEruption;

		public bool staveQuadShot;

		public bool lifeSteal1;

		public bool eldritchRoot;

		public bool sleepPowder;

		public bool vendetta;

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

		public bool forestFriendly;

		public int irradiatedLevel;

		public int irradiatedTimer;

		public bool girusCloaked;

		public int girusCloakTimer;

		public bool hairLoss;

		public int irradiatedEffect;

		public bool ZoneSlayer;

		public bool androidMinion;

		public bool bileDebuff;

		public bool bioweaponDebuff;

		public bool thornCirclet;

		public int thornCircletCounter;

		public bool ksShieldGenerator;

		public bool vlitchCoreAcc;

		public bool oblitDrive;

		public bool thornCrown;

		public bool infectedHeart;

		public bool StarSerpentMinion;

		public int spiritLevel;

		public int spiritExtras;

		public bool corruptedTalisman;

		public bool bloodedTalisman;

		public bool birdMinion;

		public bool wispSet;

		public bool spiritWyvern1;

		public bool spiritWyvern2;

		public bool foundHall;

		public bool spiritGolemCross;

		public bool lacerated;

		public bool ukkonenMinion;

		public bool ZoneSoulless;

		public bool dreamsong;

		public bool cursedThornSet;

		public int powerSurgeCharge;

		public bool shadowBinder;

		public int shadowBinderCharge;

		public bool xenomiteSet;

		public bool corruptedXenomiteSet;

		public float seedLifeTime = 1f;

		public bool corruptedCopter;

		public bool girusSniperDrone;

		public bool shieldDrone;

		public bool jellyfishDrone;

		public bool moonStaves;

		public bool omegaPower;

		public bool halPet;

		public bool tiedPet;

		public bool lantardPet;

		public bool tbotEyes;

		public bool zapField;

		public bool dreambound;

		public bool brokenBlade;

		public float trueMeleeDamage = 1f;

		public bool snipped;

		public bool anglerPot;

		public bool consecutiveStrikes;

		public bool earthbind;

		public static readonly PlayerLayer EarthbindLayer = new PlayerLayer("Redemption", "EarthbindLayer", PlayerLayer.Body, delegate(PlayerDrawInfo drawInfo)
		{
			if (drawInfo.shadow != 0f)
			{
				return;
			}
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("Redemption");
			drawPlayer.GetModPlayer<RedePlayer>();
			if (drawPlayer.active && !drawPlayer.dead && !drawPlayer.outOfRange)
			{
				Texture2D texture = mod.GetTexture("ExtraTextures/AkkaEarthbindEffect");
				Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
				Vector2 drawPos = drawPlayer.position + new Vector2((float)drawPlayer.width * 0.5f, (float)drawPlayer.height * 0.5f);
				drawPos.X = (float)((int)drawPos.X);
				drawPos.Y = (float)((int)drawPos.Y);
				DrawData drawData;
				drawData..ctor(texture, drawPos - Main.screenPosition, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0);
				Main.playerDrawData.Add(drawData);
			}
		});

		public static readonly PlayerLayer MissileLauncherLayer = new PlayerLayer("Redemption", "MissileLauncherLayer", PlayerLayer.MiscEffectsBack, delegate(PlayerDrawInfo drawInfo)
		{
			if (drawInfo.shadow != 0f)
			{
				return;
			}
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("Redemption");
			drawPlayer.GetModPlayer<RedePlayer>();
			if (drawPlayer.active && !drawPlayer.dead && !drawPlayer.outOfRange)
			{
				Texture2D texture = mod.GetTexture("ExtraTextures/SneakLoneRemote_Extra");
				Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
				SpriteEffects spriteEffects = (drawPlayer.direction == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
				Vector2 drawPos = drawPlayer.position + new Vector2((float)drawPlayer.width * 0.5f, (float)drawPlayer.height * 0.5f);
				drawPos.X = (float)((drawPlayer.direction == 1) ? ((int)drawPos.X + 3) : ((int)drawPos.X - 3));
				drawPos.Y = (float)((int)drawPos.Y + 2);
				DrawData drawData;
				drawData..ctor(texture, drawPos - Main.screenPosition, null, Color.White, 0f, origin, 1f, spriteEffects, 0);
				Main.playerDrawData.Add(drawData);
			}
		});
	}
}
