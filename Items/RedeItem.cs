using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Redemption.Items
{
	public class RedeItem : GlobalItem
	{
		public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}

		public override int ChoosePrefix(Item item, UnifiedRandom rand)
		{
			if (item.type == base.mod.ItemType("AcornStaff") || item.type == base.mod.ItemType("AdamantiteStave") || item.type == base.mod.ItemType("AncientWoodStave") || item.type == base.mod.ItemType("AngelicStave") || item.type == base.mod.ItemType("AdamantiteLilyBag") || item.type == base.mod.ItemType("AncientSoulCaller") || item.type == base.mod.ItemType("AngelicFan") || item.type == base.mod.ItemType("Belrose1") || item.type == base.mod.ItemType("Belrose2") || item.type == base.mod.ItemType("BonfireDagger") || item.type == base.mod.ItemType("BonfireShuriken") || item.type == base.mod.ItemType("BorealStave") || item.type == base.mod.ItemType("Brynildra") || item.type == base.mod.ItemType("BunnySpiritBottle") || item.type == base.mod.ItemType("CobaltStave") || item.type == base.mod.ItemType("CorpseFlowerBag") || item.type == base.mod.ItemType("CreationRoseBag") || item.type == base.mod.ItemType("CrimtaneStave") || item.type == base.mod.ItemType("CrimthornBushBag") || item.type == base.mod.ItemType("CrystalDagger") || item.type == base.mod.ItemType("CrystalShuriken") || item.type == base.mod.ItemType("CrystalStave") || item.type == base.mod.ItemType("Dandelion") || item.type == base.mod.ItemType("DaybloomBag") || item.type == base.mod.ItemType("DBCommonGround") || item.type == base.mod.ItemType("DBLiquidFlames") || item.type == base.mod.ItemType("DBSunAndMoon") || item.type == base.mod.ItemType("DeathsGraspBag") || item.type == base.mod.ItemType("DeathweedBag") || item.type == base.mod.ItemType("DemoniteStave") || item.type == base.mod.ItemType("DonjonStave") || item.type == base.mod.ItemType("DruidDagger") || item.type == base.mod.ItemType("DruidShuriken") || item.type == base.mod.ItemType("EbonwoodStave") || item.type == base.mod.ItemType("ElegantStave") || item.type == base.mod.ItemType("EyeStalkBag") || item.type == base.mod.ItemType("FDruidBow") || item.type == base.mod.ItemType("FDruidStave") || item.type == base.mod.ItemType("FireblossomBag") || item.type == base.mod.ItemType("ForestNymphsSickle") || item.type == base.mod.ItemType("GardenOfMadness") || item.type == base.mod.ItemType("GloomShroomBag") || item.type == base.mod.ItemType("GloopContainer") || item.type == base.mod.ItemType("GoldenOrangeBag") || item.type == base.mod.ItemType("GrassStave") || item.type == base.mod.ItemType("GreatSkiesStave") || item.type == base.mod.ItemType("HallowedStave") || item.type == base.mod.ItemType("HellstoneStave") || item.type == base.mod.ItemType("IcarsFlowerBag") || item.type == base.mod.ItemType("KingsGreatstave") || item.type == base.mod.ItemType("LivingWoodStave") || item.type == base.mod.ItemType("LunarCrescentStave") || item.type == base.mod.ItemType("MagSoulbound") || item.type == base.mod.ItemType("MahoganyStave") || item.type == base.mod.ItemType("MartianTreeBag") || item.type == base.mod.ItemType("MoonflareStave") || item.type == base.mod.ItemType("MoonglowBag") || item.type == base.mod.ItemType("MossBag") || item.type == base.mod.ItemType("MysticStave") || item.type == base.mod.ItemType("MythrilStave") || item.type == base.mod.ItemType("NightshadeSeedbag") || item.type == base.mod.ItemType("OrichalcumStave") || item.type == base.mod.ItemType("PalladiumStave") || item.type == base.mod.ItemType("PalmStave") || item.type == base.mod.ItemType("PearlwoodStave") || item.type == base.mod.ItemType("Petridish") || item.type == base.mod.ItemType("PetrifiedStave") || item.type == base.mod.ItemType("PlanterasStave1") || item.type == base.mod.ItemType("PlanterasStave2") || item.type == base.mod.ItemType("Pleasure") || item.type == base.mod.ItemType("Seedbag") || item.type == base.mod.ItemType("SeedNade") || item.type == base.mod.ItemType("ShadewoodStave") || item.type == base.mod.ItemType("ShiverthornBag") || item.type == base.mod.ItemType("SkyflowerBag") || item.type == base.mod.ItemType("SoulGuidingStave") || item.type == base.mod.ItemType("SpiritSquirrel") || item.type == base.mod.ItemType("StaveOfLife") || item.type == base.mod.ItemType("SunshardStave") || item.type == base.mod.ItemType("SyringePistol") || item.type == base.mod.ItemType("TerraStave") || item.type == base.mod.ItemType("TitaniumBloomBag") || item.type == base.mod.ItemType("TitaniumStave") || item.type == base.mod.ItemType("TrueHallowedStave") || item.type == base.mod.ItemType("TrueLunarCrescentStave") || item.type == base.mod.ItemType("UkkosStave") || item.type == base.mod.ItemType("VilethornBushBag") || item.type == base.mod.ItemType("WallsClaw") || item.type == base.mod.ItemType("WaterleafBag") || item.type == base.mod.ItemType("WyvernSpiritBottle") || item.type == base.mod.ItemType("XeniumStave") || item.type == base.mod.ItemType("XenoCanister") || item.type == base.mod.ItemType("XenomiteStave") || item.type == base.mod.ItemType("SapphireStave") || item.type == base.mod.ItemType("ScarletStave") || item.type == base.mod.ItemType("AncientPowerStave") || item.type == base.mod.ItemType("BloodRootSeedbag") || item.type == base.mod.ItemType("CriesOfGrief") || item.type == base.mod.ItemType("HauntedIceStave") || item.type == base.mod.ItemType("LuminiteDruidDagger") || item.type == base.mod.ItemType("LuminiteDruidShuriken") || item.type == base.mod.ItemType("PocketSand") || item.type == base.mod.ItemType("PocketSans") || item.type == base.mod.ItemType("Shadebound") || item.type == base.mod.ItemType("SleepPowder") || item.type == base.mod.ItemType("StarfruitSeedbag") || item.type == base.mod.ItemType("ThornSeedBag") || item.type == base.mod.ItemType("ViciousShroomBag") || item.type == base.mod.ItemType("VileShroomBag") || item.type == base.mod.ItemType("XenomiteDruidKunai") || item.type == base.mod.ItemType("XenomiteShuriken") || item.type == base.mod.ItemType("GasrootSeedbag") || item.type == base.mod.ItemType("Bionade"))
			{
				int prefixChoice = Main.rand.Next(200);
				if (prefixChoice == 0)
				{
					int choice = Main.rand.Next(2);
					if (choice == 0)
					{
						return (int)base.mod.PrefixType("Mother Nature's");
					}
					if (choice == 1)
					{
						return (int)base.mod.PrefixType("Dryad's");
					}
				}
				if (prefixChoice >= 1 && prefixChoice < 20)
				{
					int choice2 = Main.rand.Next(2);
					if (choice2 == 0)
					{
						return (int)base.mod.PrefixType("Blessed");
					}
					if (choice2 == 1)
					{
						return (int)base.mod.PrefixType("Exotic");
					}
				}
				if (prefixChoice >= 20 && prefixChoice < 60)
				{
					switch (Main.rand.Next(3))
					{
					case 0:
						return (int)base.mod.PrefixType("Enchanted");
					case 1:
						return (int)base.mod.PrefixType("Mesmerizing");
					case 2:
						return (int)base.mod.PrefixType("Forgotten");
					}
				}
				if (prefixChoice >= 60)
				{
					switch (Main.rand.Next(9))
					{
					case 0:
						return (int)base.mod.PrefixType("Old");
					case 1:
						return (int)base.mod.PrefixType("Wild");
					case 2:
						return (int)base.mod.PrefixType("Blighted");
					case 3:
						return (int)base.mod.PrefixType("Dry");
					case 4:
						return (int)base.mod.PrefixType("Fruitful");
					case 5:
						return (int)base.mod.PrefixType("Lively");
					case 6:
						return (int)base.mod.PrefixType("Prickly");
					case 7:
						return (int)base.mod.PrefixType("Rotten");
					case 8:
						return (int)base.mod.PrefixType("Blooming");
					}
				}
			}
			return base.ChoosePrefix(item, rand);
		}

		public override void OnCraft(Item item, Recipe recipe)
		{
			if (item.type == base.mod.ItemType("Loreholder"))
			{
				Main.NewText("<Loreholder> Who awakens me from my slumber?", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
			}
			if (item.type == base.mod.ItemType("RedemptionTeller"))
			{
				Main.NewText("<Chalice of Alignment> Greetings, I am the Chalice of Alignment, and I believe any action can be redeemed.", Color.DarkGoldenrod.R, Color.DarkGoldenrod.G, Color.DarkGoldenrod.B, false);
			}
		}
	}
}
