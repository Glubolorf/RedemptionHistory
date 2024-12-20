using System;
using Microsoft.Xna.Framework;
using Redemption.Items;
using Redemption.Items.Cores;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Redemption
{
	internal class Redemption : Mod
	{
		public Redemption()
		{
			ModProperties properties = default(ModProperties);
			properties.Autoload = true;
			properties.AutoloadGores = true;
			properties.AutoloadSounds = true;
			properties.AutoloadBackgrounds = true;
			base.Properties = properties;
		}

		public override void UpdateMusic(ref int music, ref MusicPriority priority)
		{
			if (Main.myPlayer != -1 && !Main.gameMenu && Main.player[Main.myPlayer].active && Main.player[Main.myPlayer].GetModPlayer<RedePlayer>(this).ZoneXeno)
			{
				music = base.GetSoundSlot(51, "Sounds/Music/XenoCaves");
			}
		}

		public override void AddRecipeGroups()
		{
			RecipeGroup recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Plant", new int[]
			{
				313,
				317,
				315,
				316,
				318,
				314,
				2358
			});
			RecipeGroup.RegisterGroup("Redemption:Plant", recipeGroup);
			RecipeGroup recipeGroup2 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Seedbag", new int[]
			{
				base.ItemType("Seedbag"),
				base.ItemType("BlinkrootBushBag"),
				base.ItemType("CorpseFlowerBag"),
				base.ItemType("CrimthornBushBag"),
				base.ItemType("DeathweedBag"),
				base.ItemType("FireblossomBag"),
				base.ItemType("IcarsFlowerBag"),
				base.ItemType("ShiverthornBag"),
				base.ItemType("SkyflowerBag"),
				base.ItemType("VilethornBushBag"),
				base.ItemType("WaterleafBag"),
				base.ItemType("DaybloomBag"),
				base.ItemType("MoonglowBag"),
				base.ItemType("MossBag"),
				base.ItemType("EyeStalkBag"),
				base.ItemType("MartianTreeBag"),
				base.ItemType("TitaniumBloomBag"),
				base.ItemType("AdamantiteLilyBag"),
				base.ItemType("CreationRoseBag"),
				base.ItemType("DeathsGraspBag")
			});
			RecipeGroup.RegisterGroup("Redemption:Seedbag", recipeGroup2);
			RecipeGroup recipeGroup3 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Phasesaber", new int[]
			{
				3765,
				3764,
				3769,
				3767,
				3766,
				3768
			});
			RecipeGroup.RegisterGroup("Redemption:Phasesabers", recipeGroup3);
			if (RecipeGroup.recipeGroupIDs.ContainsKey("Wood"))
			{
				int key = RecipeGroup.recipeGroupIDs["Wood"];
				RecipeGroup recipeGroup4 = RecipeGroup.recipeGroups[key];
				recipeGroup4.ValidItems.Add(base.ItemType("DeadWood"));
				recipeGroup4.ValidItems.Add(base.ItemType("AncientWood"));
			}
		}

		public override void Load()
		{
			if (!Main.dedServ)
			{
				base.AddEquipTexture(null, 2, "ArchclothRobe_Legs", "Redemption/Items/Armor/ArchclothRobe_Legs", "", "");
				base.AddEquipTexture(null, 2, "HallamRobes_Legs", "Redemption/Items/Armor/HallamRobes_Legs", "", "");
				base.AddEquipTexture(new OmegaHead(), null, 0, "OmegaHead", "Redemption/Items/Cores/OmegaCore_Head", "", "");
				base.AddEquipTexture(new OmegaBody(), null, 1, "OmegaBody", "Redemption/Items/Cores/OmegaCore_Body", "Redemption/Items/Cores/OmegaCore_FemaleBody", "Redemption/Items/Cores/OmegaCore_Arms");
				base.AddEquipTexture(new OmegaLegs(), null, 2, "OmegaLegs", "Redemption/Items/Cores/OmegaCore_Legs", "", "");
			}
			Redemption.FaceCustomCurrencyID = CustomCurrencyManager.RegisterCurrency(new CustomCurrency(base.ItemType<AncientGoldCoin>(), 999L));
		}

		public override void PostSetupContent()
		{
			Mod mod = ModLoader.GetMod("BossChecklist");
			if (mod != null)
			{
				Mod mod2 = mod;
				object[] array = new object[5];
				array[0] = "AddMiniBossWithInfo";
				array[1] = "The Mighty King Chicken";
				array[2] = 0f;
				array[3] = new Func<bool>(() => RedeWorld.downedKingChicken);
				array[4] = "Use an [i:" + base.ItemType<EggCrown>() + "] at day";
				mod2.Call(array);
				Mod mod3 = mod;
				object[] array2 = new object[5];
				array2[0] = "AddBossWithInfo";
				array2[1] = "The Keeper";
				array2[2] = 2.75f;
				array2[3] = new Func<bool>(() => RedeWorld.downedTheKeeper);
				array2[4] = string.Concat(new object[]
				{
					"Use a [i:",
					base.ItemType<MysteriousTabletCorrupt>(),
					"] or [i:",
					base.ItemType<MysteriousTabletCrimson>(),
					"] at night"
				});
				mod3.Call(array2);
				Mod mod4 = mod;
				object[] array3 = new object[5];
				array3[0] = "AddMiniBossWithInfo";
				array3[1] = "Strange Portal";
				array3[2] = 3.49f;
				array3[3] = new Func<bool>(() => RedeWorld.downedStrangePortal);
				array3[4] = "Use an [i:" + base.ItemType<UnstableCrystal>() + "]";
				mod4.Call(array3);
				Mod mod5 = mod;
				object[] array4 = new object[5];
				array4[0] = "AddBossWithInfo";
				array4[1] = "Xenomite Crystal";
				array4[2] = 3.5f;
				array4[3] = new Func<bool>(() => RedeWorld.downedXenomiteCrystal);
				array4[4] = "Use a [i:" + base.ItemType<GeigerCounter>() + "], dropped by the Strange Portal";
				mod5.Call(array4);
				Mod mod6 = mod;
				object[] array5 = new object[5];
				array5[0] = "AddBossWithInfo";
				array5[1] = "Infected Eye";
				array5[2] = 6.25f;
				array5[3] = new Func<bool>(() => RedeWorld.downedInfectedEye);
				array5[4] = "Use a [i:" + base.ItemType<XenoEye>() + "] at night";
				mod6.Call(array5);
				Mod mod7 = mod;
				object[] array6 = new object[5];
				array6[0] = "AddBossWithInfo";
				array6[1] = "King Slayer III";
				array6[2] = 9.999f;
				array6[3] = new Func<bool>(() => RedeWorld.downedSlayer);
				array6[4] = "Use a [i:" + base.ItemType<KingSummon>() + "] at day";
				mod7.Call(array6);
				Mod mod8 = mod;
				object[] array7 = new object[5];
				array7[0] = "AddBossWithInfo";
				array7[1] = "Vlitch Cleaver";
				array7[2] = 11.5f;
				array7[3] = new Func<bool>(() => RedeWorld.downedVlitch1);
				array7[4] = "Use a [i:" + base.ItemType<CorruptedHeroSword>() + "] at night";
				mod8.Call(array7);
				Mod mod9 = mod;
				object[] array8 = new object[5];
				array8[0] = "AddBossWithInfo";
				array8[1] = "Vlitch Gigipede";
				array8[2] = 13.5f;
				array8[3] = new Func<bool>(() => RedeWorld.downedVlitch2);
				array8[4] = "Use a [i:" + base.ItemType<CorruptedWormMedallion>() + "] at night";
				mod9.Call(array8);
			}
		}

		public override void ModifySunLightColor(ref Color tileColor, ref Color backgroundColor)
		{
			if (RedeWorld.xenoBiome > 0)
			{
				float num = (float)RedeWorld.xenoBiome / 200f;
				num = Math.Min(num, 1f);
				int num2 = (int)backgroundColor.R;
				int num3 = (int)backgroundColor.G;
				int num4 = (int)backgroundColor.B;
				num2 -= (int)(200f * num * ((float)backgroundColor.R / 255f));
				num4 -= (int)(200f * num * ((float)backgroundColor.B / 255f));
				num3 -= (int)(170f * num * ((float)backgroundColor.G / 255f));
				num2 = Utils.Clamp<int>(num2, 15, 255);
				num3 = Utils.Clamp<int>(num3, 15, 255);
				num4 = Utils.Clamp<int>(num4, 15, 255);
				backgroundColor.R = (byte)num2;
				backgroundColor.G = (byte)num3;
				backgroundColor.B = (byte)num4;
			}
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(this);
			modRecipe.AddIngredient(null, "CoastScarabShell", 1);
			modRecipe.AddTile(228);
			modRecipe.SetResult(1044, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(this);
			modRecipe.AddIngredient(null, "TreeBugShell", 1);
			modRecipe.AddTile(228);
			modRecipe.SetResult(1011, 1);
			modRecipe.AddRecipe();
		}

		public static int FaceCustomCurrencyID;
	}
}
