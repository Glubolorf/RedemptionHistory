using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs;
using Redemption.Items;
using Redemption.Items.Armor.Costumes;
using Redemption.Items.Cores;
using Redemption.Items.DruidDamageClass;
using Redemption.Items.LabThings;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.Graphics.Shaders;
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
			TagCompound tagCompound = new TagCompound();
			tagCompound.Add("boost", boost);
			return tagCompound;
		}

		public override void Load(TagCompound tag)
		{
			IList<string> boost = tag.GetList<string>("boost");
			this.medKit = boost.Contains("medicKit");
			this.galaxyHeart = boost.Contains("galaxyHeart");
		}

		public override void LoadLegacy(BinaryReader reader)
		{
			int loadVersion = reader.ReadInt32();
			if (loadVersion == 0)
			{
				BitsByte flags = reader.ReadByte();
				this.medKit = flags[0];
				this.galaxyHeart = flags[1];
				return;
			}
			base.mod.Logger.Debug("Redemption: Unknown loadVersion: " + loadVersion);
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
				else if (Main.raining && (this.ZoneXeno || this.ZoneEvilXeno))
				{
					Main.rainTexture = rain2;
				}
				else
				{
					Main.rainTexture = rainOriginal;
				}
			}
			if (this.ZoneXeno || this.ZoneEvilXeno)
			{
				if (base.player.GetModPlayer<MullerEffect>().effect && Main.rand.Next(200) == 0 && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Muller1").WithVolume(0.9f).WithPitchVariance(0.1f), base.player.position);
				}
				if (Main.raining)
				{
					if (base.player.ZoneOverworldHeight || base.player.ZoneSkyHeight)
					{
						base.player.AddBuff(base.mod.BuffType("HeavyRadiationDebuff"), 2, true);
					}
					if (Main.rand.Next(80000) == 0 && base.player.GetModPlayer<RedePlayer>().irradiatedLevel == 0)
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
				if (Main.rand.Next(80000) == 0 && base.player.GetModPlayer<RedePlayer>().irradiatedLevel == 0)
				{
					base.player.GetModPlayer<RedePlayer>().irradiatedLevel++;
				}
				Point point = Utils.ToTileCoordinates(base.player.position);
				if (!RedeWorld.labSafe && ((int)Main.tile[point.X, point.Y].wall == base.mod.WallType("HardenedlyHardenedSludgeWallTile") || (int)Main.tile[point.X, point.Y].wall == base.mod.WallType("HardenedSludgeWallTile") || (int)Main.tile[point.X, point.Y].wall == base.mod.WallType("LabWallTileUnsafe") || (int)Main.tile[point.X, point.Y].wall == base.mod.WallType("VentWallTile")))
				{
					base.player.AddBuff(base.mod.BuffType("IntruderAlertDebuff"), 60, true);
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
						base.player.AddBuff(base.mod.BuffType("HeadacheDebuff"), 120, true);
					}
				}
				else if (this.irradiatedTimer >= 40000 && this.irradiatedTimer < 48000)
				{
					this.irradiatedEffect = 1;
					if (Main.rand.Next(2000) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("FatigueDebuff"), 800, true);
					}
					if (Main.rand.Next(12000) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("NauseaDebuff"), 1200, true);
					}
				}
				else if (this.irradiatedTimer >= 48000 && this.irradiatedTimer < 52000)
				{
					this.irradiatedEffect = 2;
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("FatigueDebuff"), 800, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("NauseaDebuff"), 1200, true);
					}
					if (Main.rand.Next(80000) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("HairLossDebuff"), 60000, true);
					}
				}
				else if (this.irradiatedTimer >= 52000 && this.irradiatedTimer < 58000)
				{
					this.irradiatedEffect = 3;
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("HairLossDebuff"), 60000, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("SkinBurnDebuff"), 60000, true);
					}
					if (Main.rand.Next(4000) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("FeverDebuff"), 60000, true);
					}
				}
				else if (this.irradiatedTimer >= 58000)
				{
					this.irradiatedEffect = 4;
					base.player.AddBuff(base.mod.BuffType("RadiationDebuff"), 5, true);
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
						base.player.AddBuff(base.mod.BuffType("HeadacheDebuff"), 120, true);
					}
				}
				else if (this.irradiatedTimer >= 38000 && this.irradiatedTimer < 46000)
				{
					this.irradiatedEffect = 1;
					if (Main.rand.Next(2000) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("FatigueDebuff"), 800, true);
					}
					if (Main.rand.Next(10000) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("NauseaDebuff"), 1200, true);
					}
				}
				else if (this.irradiatedTimer >= 46000 && this.irradiatedTimer < 50000)
				{
					this.irradiatedEffect = 2;
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("FatigueDebuff"), 800, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("NauseaDebuff"), 1200, true);
					}
					if (Main.rand.Next(50000) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("HairLossDebuff"), 60000, true);
					}
				}
				else if (this.irradiatedTimer >= 50000 && this.irradiatedTimer < 56000)
				{
					this.irradiatedEffect = 3;
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("HairLossDebuff"), 60000, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("SkinBurnDebuff"), 60000, true);
					}
					if (Main.rand.Next(4000) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("FeverDebuff"), 60000, true);
					}
				}
				else if (this.irradiatedTimer >= 56000)
				{
					this.irradiatedEffect = 4;
					base.player.AddBuff(base.mod.BuffType("RadiationDebuff"), 5, true);
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
						base.player.AddBuff(base.mod.BuffType("HeadacheDebuff"), 120, true);
					}
				}
				else if (this.irradiatedTimer >= 34000 && this.irradiatedTimer < 42000)
				{
					this.irradiatedEffect = 1;
					if (Main.rand.Next(2000) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("FatigueDebuff"), 800, true);
					}
					if (Main.rand.Next(5000) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("NauseaDebuff"), 1200, true);
					}
				}
				else if (this.irradiatedTimer >= 42000 && this.irradiatedTimer < 46000)
				{
					this.irradiatedEffect = 2;
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("FatigueDebuff"), 800, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("NauseaDebuff"), 1200, true);
					}
					if (Main.rand.Next(30000) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("HairLossDebuff"), 60000, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("SkinBurnDebuff"), 60000, true);
					}
				}
				else if (this.irradiatedTimer >= 46000 && this.irradiatedTimer < 49000)
				{
					this.irradiatedEffect = 3;
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("HairLossDebuff"), 60000, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("SkinBurnDebuff"), 60000, true);
					}
					if (Main.rand.Next(4000) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("FeverDebuff"), 60000, true);
					}
				}
				else if (this.irradiatedTimer >= 49000)
				{
					this.irradiatedEffect = 4;
					base.player.AddBuff(base.mod.BuffType("RadiationDebuff"), 5, true);
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
						base.player.AddBuff(base.mod.BuffType("HeadacheDebuff"), 120, true);
					}
				}
				else if (this.irradiatedTimer >= 34000 && this.irradiatedTimer < 38000)
				{
					this.irradiatedEffect = 1;
					if (Main.rand.Next(2000) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("FatigueDebuff"), 800, true);
					}
					if (Main.rand.Next(2000) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("NauseaDebuff"), 1200, true);
					}
				}
				else if (this.irradiatedTimer >= 38000 && this.irradiatedTimer < 40000)
				{
					this.irradiatedEffect = 2;
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("FatigueDebuff"), 800, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("NauseaDebuff"), 1200, true);
					}
					if (Main.rand.Next(2000) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("HairLossDebuff"), 60000, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("SkinBurnDebuff"), 60000, true);
					}
				}
				else if (this.irradiatedTimer >= 40000 && this.irradiatedTimer < 41000)
				{
					this.irradiatedEffect = 3;
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("HairLossDebuff"), 60000, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("SkinBurnDebuff"), 60000, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("FeverDebuff"), 60000, true);
					}
				}
				else if (this.irradiatedTimer >= 41000)
				{
					this.irradiatedEffect = 4;
					base.player.AddBuff(base.mod.BuffType("RadiationDebuff"), 5, true);
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
						base.player.AddBuff(base.mod.BuffType("HeadacheDebuff"), 120, true);
					}
				}
				else if (this.irradiatedTimer >= 28000 && this.irradiatedTimer < 30000)
				{
					this.irradiatedEffect = 1;
					if (Main.rand.Next(300) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("FatigueDebuff"), 800, true);
					}
					if (Main.rand.Next(300) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("NauseaDebuff"), 1200, true);
					}
				}
				else if (this.irradiatedTimer >= 30000 && this.irradiatedTimer < 31000)
				{
					this.irradiatedEffect = 2;
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("FatigueDebuff"), 800, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("NauseaDebuff"), 1200, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("HairLossDebuff"), 60000, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("SkinBurnDebuff"), 60000, true);
					}
				}
				else if (this.irradiatedTimer >= 31000 && this.irradiatedTimer < 32000)
				{
					this.irradiatedEffect = 3;
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("HairLossDebuff"), 60000, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("SkinBurnDebuff"), 60000, true);
					}
					if (Main.rand.Next(400) == 0)
					{
						base.player.AddBuff(base.mod.BuffType("FeverDebuff"), 60000, true);
					}
				}
				else if (this.irradiatedTimer >= 32000)
				{
					this.irradiatedEffect = 4;
					base.player.AddBuff(base.mod.BuffType("RadiationDebuff"), 5, true);
				}
			}
			else if (this.irradiatedLevel > 5)
			{
				this.irradiatedLevel = 5;
			}
			if (base.player.pulley)
			{
				this.ModDashMovement();
			}
			else if (base.player.grappling[0] == -1 && !base.player.tongued)
			{
				this.ModHorizontalMovement();
				this.ModDashMovement();
			}
			if (Main.hasFocus)
			{
				for (int i = 0; i < this.modDoubleTapCardinalTimer.Length; i++)
				{
					this.modDoubleTapCardinalTimer[i]--;
					if (this.modDoubleTapCardinalTimer[i] < 0)
					{
						this.modDoubleTapCardinalTimer[i] = 0;
					}
				}
				for (int j = 0; j < 4; j++)
				{
					bool flag5 = false;
					bool flag6 = false;
					switch (j)
					{
					case 0:
						flag5 = (base.player.controlDown && base.player.releaseDown);
						flag6 = base.player.controlDown;
						break;
					case 1:
						flag5 = (base.player.controlUp && base.player.releaseUp);
						flag6 = base.player.controlUp;
						break;
					case 2:
						flag5 = (base.player.controlRight && base.player.releaseRight);
						flag6 = base.player.controlRight;
						break;
					case 3:
						flag5 = (base.player.controlLeft && base.player.releaseLeft);
						flag6 = base.player.controlLeft;
						break;
					}
					if (flag5)
					{
						if (this.modDoubleTapCardinalTimer[j] > 0)
						{
							this.ModKeyDoubleTap(j);
						}
						else
						{
							this.modDoubleTapCardinalTimer[j] = 15;
						}
					}
					if (flag6)
					{
						this.modHoldDownCardinalTimer[j]++;
						base.player.KeyHoldDown(j, this.modHoldDownCardinalTimer[j]);
					}
					else
					{
						this.modHoldDownCardinalTimer[j] = 0;
					}
				}
			}
		}

		public override void UpdateBiomeVisuals()
		{
			bool useFire = NPC.AnyNPCs(base.mod.NPCType("Nebuleus"));
			base.player.ManageSpecialBiomeVisuals("Redemption:Nebuleus", useFire, default(Vector2));
			bool useFire2 = NPC.AnyNPCs(base.mod.NPCType("BigNebuleus"));
			base.player.ManageSpecialBiomeVisuals("Redemption:BigNebuleus", useFire2, default(Vector2));
			base.player.ManageSpecialBiomeVisuals("Redemption:XenoSky", this.ZoneXeno || this.ZoneEvilXeno, base.player.Center);
			if (!this.ZoneXeno && !this.ZoneEvilXeno)
			{
				base.player.HasBuff(base.mod.BuffType("RadiationDebuff"));
			}
		}

		public override void UpdateBiomes()
		{
			this.ZoneXeno = (RedeWorld.xenoBiome > 75);
			this.ZoneEvilXeno = (RedeWorld.evilXenoBiome > 50);
			this.ZoneLab = (RedeWorld.labBiome > 200);
			this.ZoneSlayer = (RedeWorld.slayerBiome > 75);
		}

		public override bool CustomBiomesMatch(Player other)
		{
			RedePlayer modOther = other.GetModPlayer<RedePlayer>();
			return this.ZoneXeno == modOther.ZoneXeno && this.ZoneEvilXeno == modOther.ZoneEvilXeno && this.ZoneLab == modOther.ZoneLab && this.ZoneSlayer == modOther.ZoneSlayer;
		}

		public override void CopyCustomBiomesTo(Player other)
		{
			RedePlayer modPlayer = other.GetModPlayer<RedePlayer>();
			modPlayer.ZoneXeno = this.ZoneXeno;
			modPlayer.ZoneEvilXeno = this.ZoneEvilXeno;
			modPlayer.ZoneLab = this.ZoneLab;
			modPlayer.ZoneSlayer = this.ZoneSlayer;
		}

		public override void SendCustomBiomes(BinaryWriter writer)
		{
			BitsByte flags = default(BitsByte);
			flags[0] = this.ZoneXeno;
			flags[1] = this.ZoneLab;
			flags[2] = this.ZoneEvilXeno;
			flags[3] = this.ZoneSlayer;
			writer.Write(flags);
		}

		public override Texture2D GetMapBackgroundImage()
		{
			if (this.ZoneXeno || this.ZoneEvilXeno)
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
			this.ZoneSlayer = flags[3];
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
			this.infectedThornshield = false;
			this.dashMod = 0;
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
		}

		public void ModDashMovement()
		{
			if (this.dashMod == 1 && this.dashDelayMod < 0 && base.player.whoAmI == Main.myPlayer)
			{
				Rectangle rectangle = new Rectangle((int)((double)base.player.position.X + (double)base.player.velocity.X * 0.5 - 4.0), (int)((double)base.player.position.Y + (double)base.player.velocity.Y * 0.5 - 4.0), base.player.width + 8, base.player.height + 8);
				for (int i = 0; i < 200; i++)
				{
					if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && !Main.npc[i].friendly && Main.npc[i].immune[base.player.whoAmI] <= 0)
					{
						NPC nPC = Main.npc[i];
						Rectangle rect = nPC.getRect();
						DruidDamagePlayer modPlayer = DruidDamagePlayer.ModPlayer(base.player);
						if (rectangle.Intersects(rect) && (nPC.noTileCollide || base.player.CanHit(nPC)))
						{
							float num = 40f;
							float num2 = 8f;
							bool crit = false;
							if (base.player.kbGlove)
							{
								num2 *= 2f;
							}
							if (base.player.kbBuff)
							{
								num2 *= 1.5f;
							}
							if (Main.rand.Next(100) < modPlayer.druidCrit)
							{
								crit = true;
							}
							int direction = base.player.direction;
							if (base.player.velocity.X < 0f)
							{
								direction = -1;
							}
							if (base.player.velocity.X > 0f)
							{
								direction = 1;
							}
							base.player.velocity.X = -base.player.velocity.X;
							if (base.player.whoAmI == Main.myPlayer)
							{
								base.player.ApplyDamageToNPC(nPC, (int)num, num2, direction, crit);
								nPC.AddBuff(39, 300, false);
							}
							nPC.immune[base.player.whoAmI] = 6;
							base.player.immune = true;
							base.player.immuneNoBlink = true;
							base.player.immuneTime = 8;
						}
					}
				}
			}
			if (this.dashDelayMod > 0)
			{
				if (base.player.eocDash > 0)
				{
					base.player.eocDash--;
				}
				if (base.player.eocDash == 0)
				{
					base.player.eocHit = -1;
				}
				this.dashDelayMod--;
				return;
			}
			if (this.dashDelayMod < 0)
			{
				float num3 = 12f;
				float num4 = 0.992f;
				float num5 = Math.Max(base.player.accRunSpeed, base.player.maxRunSpeed);
				float num6 = 0.96f;
				int num7 = 20;
				if (this.dashMod == 1)
				{
					for (int j = 0; j < 2; j++)
					{
						int num8;
						if (base.player.velocity.Y == 0f)
						{
							num8 = Dust.NewDust(new Vector2(base.player.position.X, base.player.position.Y + (float)base.player.height - 4f), base.player.width, 8, 74, 0f, 0f, 100, default(Color), 1.4f);
						}
						else
						{
							num8 = Dust.NewDust(new Vector2(base.player.position.X, base.player.position.Y + (float)(base.player.height / 2) - 8f), base.player.width, 16, 74, 0f, 0f, 100, default(Color), 1.4f);
						}
						Main.dust[num8].velocity *= 0.1f;
						Main.dust[num8].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
						Main.dust[num8].shader = GameShaders.Armor.GetSecondaryShader(base.player.cShoe, base.player);
					}
				}
				if (this.dashMod > 0)
				{
					base.player.vortexStealthActive = false;
					if (base.player.velocity.X > num3 || base.player.velocity.X < -num3)
					{
						base.player.velocity.X = base.player.velocity.X * num4;
						return;
					}
					if (base.player.velocity.X > num5 || base.player.velocity.X < -num5)
					{
						base.player.velocity.X = base.player.velocity.X * num6;
						return;
					}
					this.dashDelayMod = num7;
					if (base.player.velocity.X < 0f)
					{
						base.player.velocity.X = -num5;
						return;
					}
					if (base.player.velocity.X > 0f)
					{
						base.player.velocity.X = num5;
						return;
					}
				}
			}
			else if (this.dashMod > 0 && !base.player.mount.Active && this.dashMod == 1)
			{
				int num9 = 0;
				bool flag = false;
				if (this.dashTimeMod > 0)
				{
					this.dashTimeMod--;
				}
				if (this.dashTimeMod < 0)
				{
					this.dashTimeMod++;
				}
				if (base.player.controlRight && base.player.releaseRight)
				{
					if (this.dashTimeMod > 0)
					{
						num9 = 1;
						flag = true;
						this.dashTimeMod = 0;
					}
					else
					{
						this.dashTimeMod = 15;
					}
				}
				else if (base.player.controlLeft && base.player.releaseLeft)
				{
					if (this.dashTimeMod < 0)
					{
						num9 = -1;
						flag = true;
						this.dashTimeMod = 0;
					}
					else
					{
						this.dashTimeMod = -15;
					}
				}
				if (flag)
				{
					base.player.velocity.X = 16.9f * (float)num9;
					Point point = Utils.ToTileCoordinates(base.player.Center + new Vector2((float)(num9 * base.player.width / 2 + 2), base.player.gravDir * -(float)base.player.height / 2f + base.player.gravDir * 2f));
					Point point2 = Utils.ToTileCoordinates(base.player.Center + new Vector2((float)(num9 * base.player.width / 2 + 2), 0f));
					if (WorldGen.SolidOrSlopedTile(point.X, point.Y) || WorldGen.SolidOrSlopedTile(point2.X, point2.Y))
					{
						base.player.velocity.X = base.player.velocity.X / 2f;
					}
					this.dashDelayMod = -1;
					for (int num10 = 0; num10 < 20; num10++)
					{
						int num11 = Dust.NewDust(new Vector2(base.player.position.X, base.player.position.Y), base.player.width, base.player.height, 74, 0f, 0f, 100, default(Color), 2f);
						Dust expr_CDB_cp_0 = Main.dust[num11];
						expr_CDB_cp_0.position.X = expr_CDB_cp_0.position.X + (float)Main.rand.Next(-5, 6);
						Dust expr_D02_cp_0 = Main.dust[num11];
						expr_D02_cp_0.position.Y = expr_D02_cp_0.position.Y + (float)Main.rand.Next(-5, 6);
						Main.dust[num11].velocity *= 0.2f;
						Main.dust[num11].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
						Main.dust[num11].shader = GameShaders.Armor.GetSecondaryShader(base.player.cShoe, base.player);
					}
					return;
				}
			}
		}

		public void ModHorizontalMovement()
		{
			float num = (base.player.accRunSpeed + base.player.maxRunSpeed) / 2f;
			if (base.player.controlLeft && base.player.velocity.X > -base.player.accRunSpeed && this.dashDelayMod >= 0)
			{
				if (base.player.mount.Active && base.player.mount.Cart)
				{
					if (base.player.velocity.X < 0f)
					{
						base.player.direction = -1;
					}
				}
				else if ((base.player.itemAnimation == 0 || base.player.inventory[base.player.selectedItem].useTurn) && base.player.mount.AllowDirectionChange)
				{
					base.player.direction = -1;
				}
				if (base.player.velocity.Y == 0f || base.player.wingsLogic > 0 || base.player.mount.CanFly)
				{
					if (base.player.velocity.X > base.player.runSlowdown)
					{
						base.player.velocity.X = base.player.velocity.X - base.player.runSlowdown;
					}
					base.player.velocity.X = base.player.velocity.X - base.player.runAcceleration * 0.2f;
					if (base.player.wingsLogic > 0)
					{
						base.player.velocity.X = base.player.velocity.X - base.player.runAcceleration * 0.2f;
					}
				}
				if (base.player.onWrongGround)
				{
					if (base.player.velocity.X < base.player.runSlowdown)
					{
						base.player.velocity.X = base.player.velocity.X + base.player.runSlowdown;
					}
					else
					{
						base.player.velocity.X = 0f;
					}
				}
				if (base.player.velocity.X < -num && base.player.velocity.Y == 0f && !base.player.mount.Active)
				{
					int num2 = 0;
					if (base.player.gravDir == -1f)
					{
						num2 -= base.player.height;
					}
					if (this.dashMod == 1)
					{
						int num3 = Dust.NewDust(new Vector2(base.player.position.X - 4f, base.player.position.Y + (float)base.player.height + (float)num2), base.player.width + 8, 4, 74, -base.player.velocity.X * 0.5f, base.player.velocity.Y * 0.5f, 50, default(Color), 1.5f);
						Main.dust[num3].velocity.X = Main.dust[num3].velocity.X * 0.2f;
						Main.dust[num3].velocity.Y = Main.dust[num3].velocity.Y * 0.2f;
						Main.dust[num3].shader = GameShaders.Armor.GetSecondaryShader(base.player.cShoe, base.player);
						return;
					}
				}
			}
			else if (base.player.controlRight && base.player.velocity.X < base.player.accRunSpeed && this.dashDelayMod >= 0)
			{
				if (base.player.mount.Active && base.player.mount.Cart)
				{
					if (base.player.velocity.X > 0f)
					{
						base.player.direction = -1;
					}
				}
				else if ((base.player.itemAnimation == 0 || base.player.inventory[base.player.selectedItem].useTurn) && base.player.mount.AllowDirectionChange)
				{
					base.player.direction = 1;
				}
				if (base.player.velocity.Y == 0f || base.player.wingsLogic > 0 || base.player.mount.CanFly)
				{
					if (base.player.velocity.X < -base.player.runSlowdown)
					{
						base.player.velocity.X = base.player.velocity.X + base.player.runSlowdown;
					}
					base.player.velocity.X = base.player.velocity.X + base.player.runAcceleration * 0.2f;
					if (base.player.wingsLogic > 0)
					{
						base.player.velocity.X = base.player.velocity.X + base.player.runAcceleration * 0.2f;
					}
				}
				if (base.player.onWrongGround)
				{
					if (base.player.velocity.X > base.player.runSlowdown)
					{
						base.player.velocity.X = base.player.velocity.X - base.player.runSlowdown;
					}
					else
					{
						base.player.velocity.X = 0f;
					}
				}
				if (base.player.velocity.X > num && base.player.velocity.Y == 0f && !base.player.mount.Active)
				{
					int num4 = 0;
					if (base.player.gravDir == -1f)
					{
						num4 -= base.player.height;
					}
					if (this.dashMod == 1)
					{
						int num5 = Dust.NewDust(new Vector2(base.player.position.X - 4f, base.player.position.Y + (float)base.player.height + (float)num4), base.player.width + 8, 4, 74, -base.player.velocity.X * 0.5f, base.player.velocity.Y * 0.5f, 50, default(Color), 1.5f);
						Main.dust[num5].velocity.X = Main.dust[num5].velocity.X * 0.2f;
						Main.dust[num5].velocity.Y = Main.dust[num5].velocity.Y * 0.2f;
						Main.dust[num5].shader = GameShaders.Armor.GetSecondaryShader(base.player.cShoe, base.player);
					}
				}
			}
		}

		public void ModKeyDoubleTap(int keyDir)
		{
			bool reversedUpDownArmorSetBonuses = Main.ReversedUpDownArmorSetBonuses;
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
			this.irradiatedLevel = 0;
			this.irradiatedTimer = 0;
			this.hairLoss = false;
			this.irradiatedEffect = 0;
			this.girusCloakTimer = 0;
			Redemption.templeOfHeroes = false;
			this.bileDebuff = false;
			this.bioweaponDebuff = false;
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
			if ((this.ZoneLab || this.ZoneXeno || this.ZoneEvilXeno) && base.player.wet && !this.hazmatPower && !this.labWaterImmune && !base.player.lavaWet && !base.player.honeyWet)
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
		}

		public override void SetupStartInventory(IList<Item> items)
		{
			if (!RedeConfigClient.Instance.NoStarterCore)
			{
				Item item = new Item();
				item.SetDefaults(base.mod.ItemType("EmptyCore"), false);
				item.stack = 1;
				items.Add(item);
			}
		}

		public override void UpdateVanityAccessories()
		{
			for (int i = 13; i < 18 + base.player.extraAccessorySlots; i++)
			{
				if (base.player.armor[i].type == ModContent.ItemType<OmegaCore>())
				{
					this.omegaHideVanity = false;
					this.omegaForceVanity = true;
				}
			}
			for (int j = 13; j < 18 + base.player.extraAccessorySlots; j++)
			{
				if (base.player.armor[j].type == ModContent.ItemType<CrownOfTheKing>())
				{
					this.chickenHideVanity = false;
					this.chickenForceVanity = true;
				}
			}
			for (int k = 13; k < 18 + base.player.extraAccessorySlots; k++)
			{
				if (base.player.armor[k].type == ModContent.ItemType<HazmatSuit>())
				{
					this.hazmatHideVanity = false;
					this.hazmatForceVanity = true;
				}
			}
			for (int l = 13; l < 18 + base.player.extraAccessorySlots; l++)
			{
				if (base.player.armor[l].type == ModContent.ItemType<HEVSuit>())
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
				base.player.AddBuff(ModContent.BuffType<Omega>(), 60, true);
			}
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
		}

		public override void ModifyDrawHeadLayers(List<PlayerHeadLayer> layers)
		{
			if (this.hairLoss)
			{
				layers.Remove(PlayerHeadLayer.Hair);
				layers.Remove(PlayerHeadLayer.AltHair);
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
				int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), base.player.width + 4, base.player.height + 4, 243, base.player.velocity.X * 0.4f, base.player.velocity.Y * 0.4f, 100, default(Color), 1.2f);
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity *= 1.8f;
				Dust dust8 = Main.dust[dust];
				dust8.velocity.Y = dust8.velocity.Y - 0.5f;
				Main.playerDrawDust.Add(dust);
			}
			if (this.ultraFlames && Main.rand.Next(2) == 0 && drawInfo.shadow == 0f)
			{
				int dust2 = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), base.player.width + 4, base.player.height + 4, 92, base.player.velocity.X * 0.4f, base.player.velocity.Y * 0.4f, 100, default(Color), 1.2f);
				Main.dust[dust2].noGravity = true;
				Main.dust[dust2].velocity *= 1.8f;
				Dust dust9 = Main.dust[dust2];
				dust9.velocity.Y = dust9.velocity.Y - 0.5f;
				Main.playerDrawDust.Add(dust2);
			}
			if (this.druidBane && Main.rand.Next(2) == 0 && drawInfo.shadow == 0f)
			{
				int dust3 = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), base.player.width + 4, base.player.height + 4, 163, base.player.velocity.X * 0.4f, base.player.velocity.Y * 0.4f, 100, default(Color), 1.2f);
				Main.dust[dust3].noGravity = true;
				Main.dust[dust3].velocity *= 1.8f;
				Dust dust10 = Main.dust[dust3];
				dust10.velocity.Y = dust10.velocity.Y - 0.5f;
				Main.playerDrawDust.Add(dust3);
			}
			if (this.holyFire && Main.rand.Next(2) == 0 && drawInfo.shadow == 0f)
			{
				int dust4 = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), base.player.width + 4, base.player.height + 4, 64, base.player.velocity.X * 0.4f, base.player.velocity.Y * 0.4f, 100, default(Color), 2f);
				Main.dust[dust4].noGravity = true;
				Main.dust[dust4].velocity *= 1.8f;
				Dust dust11 = Main.dust[dust4];
				dust11.velocity.Y = dust11.velocity.Y - 0.5f;
				Main.playerDrawDust.Add(dust4);
			}
			if (this.bileDebuff && Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
			{
				int dust5 = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), base.player.width + 4, base.player.height + 4, 74, base.player.velocity.X * 0.4f, base.player.velocity.Y * 0.4f, 100, default(Color), 1.2f);
				Main.dust[dust5].noGravity = true;
				Main.dust[dust5].velocity *= 1.8f;
				Dust dust12 = Main.dust[dust5];
				dust12.velocity.Y = dust12.velocity.Y - 0.5f;
				Main.playerDrawDust.Add(dust5);
			}
			if (this.bioweaponDebuff)
			{
				if (Main.rand.Next(15) == 0 && drawInfo.shadow == 0f)
				{
					int dust6 = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), base.player.width + 4, base.player.height + 4, 74, base.player.velocity.X * 0.4f, base.player.velocity.Y * 0.4f, 100, default(Color), 1.2f);
					Main.dust[dust6].noGravity = true;
					Main.dust[dust6].velocity *= 1.8f;
					Dust dust13 = Main.dust[dust6];
					dust13.velocity.Y = dust13.velocity.Y - 0.5f;
					Main.playerDrawDust.Add(dust6);
				}
				if (Main.rand.Next(10) == 0 && drawInfo.shadow == 0f)
				{
					int dust7 = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), base.player.width + 4, base.player.height + 4, 31, base.player.velocity.X * 0.4f, base.player.velocity.Y * 0.4f, 100, default(Color), 1.2f);
					Main.dust[dust7].noGravity = true;
					Main.dust[dust7].velocity *= 1.8f;
					Dust dust14 = Main.dust[dust7];
					dust14.velocity.Y = dust14.velocity.Y - 0.5f;
					Main.playerDrawDust.Add(dust7);
				}
			}
		}

		public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
		{
			if (this.vendetta)
			{
				npc.AddBuff(20, 300, false);
			}
		}

		public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
		{
			if (base.player.HasBuff(base.mod.BuffType("BioweaponFlaskBuff")))
			{
				target.AddBuff(base.mod.BuffType("BioweaponDebuff"), 900, false);
			}
			if (base.player.HasBuff(base.mod.BuffType("BileFlaskBuff")))
			{
				target.AddBuff(base.mod.BuffType("BileDebuff"), 900, false);
			}
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
			if (this.oblitDrive && Main.rand.Next(10) == 0)
			{
				if (!base.player.HasBuff(base.mod.BuffType("OblitBuff1")) && !base.player.HasBuff(base.mod.BuffType("OblitBuff2")) && !base.player.HasBuff(base.mod.BuffType("OblitBuff3")) && !base.player.HasBuff(base.mod.BuffType("OblitBuff4")) && !base.player.HasBuff(base.mod.BuffType("OblitBuff5")))
				{
					base.player.AddBuff(base.mod.BuffType("OblitBuff1"), 600, true);
					return;
				}
				if (base.player.HasBuff(base.mod.BuffType("OblitBuff1")))
				{
					base.player.AddBuff(base.mod.BuffType("OblitBuff2"), 600, true);
					return;
				}
				if (base.player.HasBuff(base.mod.BuffType("OblitBuff2")))
				{
					base.player.AddBuff(base.mod.BuffType("OblitBuff3"), 600, true);
					return;
				}
				if (base.player.HasBuff(base.mod.BuffType("OblitBuff3")))
				{
					base.player.AddBuff(base.mod.BuffType("OblitBuff4"), 600, true);
					return;
				}
				if (base.player.HasBuff(base.mod.BuffType("OblitBuff4")))
				{
					base.player.AddBuff(base.mod.BuffType("OblitBuff5"), 600, true);
					return;
				}
				if (base.player.HasBuff(base.mod.BuffType("OblitBuff5")))
				{
					base.player.AddBuff(base.mod.BuffType("OblitBuff5"), 600, true);
				}
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
			if (this.oblitDrive && Main.rand.Next(10) == 0)
			{
				if (!base.player.HasBuff(base.mod.BuffType("OblitBuff1")) && !base.player.HasBuff(base.mod.BuffType("OblitBuff2")) && !base.player.HasBuff(base.mod.BuffType("OblitBuff3")) && !base.player.HasBuff(base.mod.BuffType("OblitBuff4")) && !base.player.HasBuff(base.mod.BuffType("OblitBuff5")))
				{
					base.player.AddBuff(base.mod.BuffType("OblitBuff1"), 600, true);
					return;
				}
				if (base.player.HasBuff(base.mod.BuffType("OblitBuff1")))
				{
					base.player.AddBuff(base.mod.BuffType("OblitBuff2"), 600, true);
					base.player.DelBuff(base.mod.BuffType("OblitBuff1"));
					return;
				}
				if (base.player.HasBuff(base.mod.BuffType("OblitBuff2")))
				{
					base.player.AddBuff(base.mod.BuffType("OblitBuff3"), 600, true);
					base.player.DelBuff(base.mod.BuffType("OblitBuff2"));
					return;
				}
				if (base.player.HasBuff(base.mod.BuffType("OblitBuff3")))
				{
					base.player.AddBuff(base.mod.BuffType("OblitBuff4"), 600, true);
					base.player.DelBuff(base.mod.BuffType("OblitBuff3"));
					return;
				}
				if (base.player.HasBuff(base.mod.BuffType("OblitBuff4")))
				{
					base.player.AddBuff(base.mod.BuffType("OblitBuff5"), 600, true);
					base.player.DelBuff(base.mod.BuffType("OblitBuff4"));
					return;
				}
				if (base.player.HasBuff(base.mod.BuffType("OblitBuff5")))
				{
					base.player.AddBuff(base.mod.BuffType("OblitBuff5"), 600, true);
				}
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
				if (base.player.wet && !base.player.lavaWet && !base.player.honeyWet)
				{
					Main.PlaySound(SoundID.Item14, base.player.position);
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
				Gore.NewGore(base.player.position, base.player.velocity, base.mod.GetGoreSlot("Gores/v08/GirusCoreGore1"), 1f);
				Gore.NewGore(base.player.position, base.player.velocity, base.mod.GetGoreSlot("Gores/v08/GirusCoreGore1"), 1f);
				Gore.NewGore(base.player.position, base.player.velocity, base.mod.GetGoreSlot("Gores/v08/GirusCoreGore2"), 1f);
				Gore.NewGore(base.player.position, base.player.velocity, base.mod.GetGoreSlot("Gores/v08/GirusCoreGore3"), 1f);
			}
			if (this.ksShieldGenerator && base.player.FindBuffIndex(base.mod.BuffType("KSShieldCooldown")) == -1)
			{
				base.player.statLife = 100;
				base.player.HealEffect(100, true);
				base.player.immune = true;
				base.player.AddBuff(base.mod.BuffType("KSShieldBuff"), 3600, true);
				base.player.AddBuff(base.mod.BuffType("KSShieldCooldown"), 36000, true);
				if (base.player.ownedProjectileCounts[base.mod.ProjectileType("KSShieldPro")] == 0)
				{
					Projectile.NewProjectile(base.player.position, Vector2.Zero, base.mod.ProjectileType("KSShieldPro"), 0, 0f, base.player.whoAmI, 0f, 0f);
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

		public override void PostUpdate()
		{
			if (base.player.ZoneSandstorm && (this.ZoneXeno || this.ZoneEvilXeno))
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
							Dust expr_460_cp_0 = dust;
							expr_460_cp_0.velocity.Y = expr_460_cp_0.velocity.Y * dust.scale;
							Dust expr_47A_cp_0 = dust;
							expr_47A_cp_0.velocity.Y = expr_47A_cp_0.velocity.Y * 0.35f;
							dust.velocity.X = num3 * 5f + Utils.NextFloat(Main.rand) * 1f;
							Dust expr_4B7_cp_0 = dust;
							expr_4B7_cp_0.velocity.X = expr_4B7_cp_0.velocity.X + num3 * num14 * 20f;
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
								num16 = (int)vector.X / 16;
								num17 = (int)vector.Y / 16;
								if (WorldGen.InWorld(num16, num17, 10) && Main.tile[num16, num17] != null)
								{
									ushort wall = Main.tile[num16, num17].wall;
								}
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
			if (junk)
			{
				return;
			}
			if ((this.ZoneXeno || this.ZoneEvilXeno) && liquidType == 0 && questFish == base.mod.ItemType("XenChomper") && Main.rand.Next(1) == 0)
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
				int pieCut = 16;
				for (int i = 0; i < pieCut; i++)
				{
					int projID = Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, 0f, 0f, base.mod.ProjectileType("CriesOfGriefPro3"), 400, 0f, Main.myPlayer, 0f, 0f);
					Main.projectile[projID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)i / (float)pieCut * 6.28f);
				}
			}
			if (this.thornCirclet)
			{
				for (int j = 0; j < 6; j++)
				{
					int Proj = Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), 55, 20, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[Proj].hostile = false;
					Main.projectile[Proj].friendly = true;
					Main.projectile[Proj].timeLeft = 180;
				}
			}
			if (this.thornCrown)
			{
				for (int k = 0; k < 6; k++)
				{
					int Proj2 = Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), 484, 200, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[Proj2].hostile = false;
					Main.projectile[Proj2].friendly = true;
					Main.projectile[Proj2].timeLeft = 180;
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

		public bool ZoneEvilXeno;

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

		public bool infectedThornshield;

		public int dashMod;

		public int dashTimeMod;

		public int dashDelayMod;

		public int[] modDoubleTapCardinalTimer = new int[4];

		public int[] modHoldDownCardinalTimer = new int[4];

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

		public bool ksShieldGenerator;

		public bool vlitchCoreAcc;

		public bool oblitDrive;

		public bool thornCrown;

		public bool infectedHeart;

		public bool StarSerpentMinion;
	}
}
