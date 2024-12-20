using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items;
using Redemption.Items.Armor.Costumes;
using Redemption.Items.Cores;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

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
			if (Main.myPlayer != -1 && !Main.gameMenu)
			{
				if (Main.player[Main.myPlayer].active && Main.player[Main.myPlayer].GetModPlayer<RedePlayer>(this).ZoneXeno)
				{
					music = base.GetSoundSlot(51, "Sounds/Music/XenoCaves");
					priority = 3;
				}
				if (Main.player[Main.myPlayer].active && Main.player[Main.myPlayer].GetModPlayer<RedePlayer>(this).ZoneLab)
				{
					music = base.GetSoundSlot(51, "Sounds/Music/LabMusic");
					priority = 3;
				}
				if (Redemption.GirusSilence)
				{
					music = base.GetSoundSlot(51, "Sounds/Music/silence");
					priority = 8;
				}
				if (Redemption.templeOfHeroes)
				{
					music = base.GetSoundSlot(51, "Sounds/Music/HallofHeroes");
					priority = 3;
				}
			}
		}

		public override void AddRecipeGroups()
		{
			Func<string> func = () => Language.GetTextValue("LegacyMisc.37") + " Plant";
			int[] array = new int[]
			{
				313,
				317,
				315,
				316,
				318,
				314,
				2358,
				0
			};
			array[7] = base.ItemType("Nightshade");
			RecipeGroup recipeGroup = new RecipeGroup(func, array);
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
			RecipeGroup recipeGroup4 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Evil Wood", new int[]
			{
				911,
				619
			});
			RecipeGroup.RegisterGroup("Redemption:EvilWood", recipeGroup4);
			RecipeGroup recipeGroup5 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Capacitator", new int[]
			{
				base.ItemType("Mk1Capacitator"),
				base.ItemType("Mk2Capacitator"),
				base.ItemType("Mk3Capacitator")
			});
			RecipeGroup.RegisterGroup("Redemption:Capacitators", recipeGroup5);
			RecipeGroup recipeGroup6 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Plating", new int[]
			{
				base.ItemType("Mk1Plating"),
				base.ItemType("Mk2Plating"),
				base.ItemType("Mk3Plating")
			});
			RecipeGroup.RegisterGroup("Redemption:Plating", recipeGroup6);
			if (RecipeGroup.recipeGroupIDs.ContainsKey("Wood"))
			{
				int key = RecipeGroup.recipeGroupIDs["Wood"];
				RecipeGroup recipeGroup7 = RecipeGroup.recipeGroups[key];
				recipeGroup7.ValidItems.Add(base.ItemType("DeadWood"));
				recipeGroup7.ValidItems.Add(base.ItemType("AncientWood"));
			}
		}

		public override void Load()
		{
			Redemption.inst = this;
			if (Main.rand == null)
			{
				Main.rand = new UnifiedRandom();
			}
			if (!Main.dedServ)
			{
				base.AddEquipTexture(null, 2, "ArchclothRobe_Legs", "Redemption/Items/Armor/ArchclothRobe_Legs", "", "");
				base.AddEquipTexture(null, 2, "HallamRobes_Legs", "Redemption/Items/Armor/HallamRobes_Legs", "", "");
				base.AddEquipTexture(new OmegaHead(), null, 0, "OmegaHead", "Redemption/Items/Cores/OmegaCore_Head", "", "");
				base.AddEquipTexture(new OmegaBody(), null, 1, "OmegaBody", "Redemption/Items/Cores/OmegaCore_Body", "Redemption/Items/Cores/OmegaCore_FemaleBody", "Redemption/Items/Cores/OmegaCore_Arms");
				base.AddEquipTexture(new OmegaLegs(), null, 2, "OmegaLegs", "Redemption/Items/Cores/OmegaCore_Legs", "", "");
				base.AddEquipTexture(new ChickenHead(), null, 0, "ChickenHead", "Redemption/Items/CrownOfTheKing_Head", "", "");
				base.AddEquipTexture(new ChickenBody(), null, 1, "ChickenBody", "Redemption/Items/CrownOfTheKing_Body", "Redemption/Items/CrownOfTheKing_Arms", "");
				base.AddEquipTexture(new ChickenLegs(), null, 2, "ChickenLegs", "Redemption/Items/CrownOfTheKing_Legs", "", "");
				base.AddEquipTexture(new HazmatHead(), null, 0, "HazmatHead", "Redemption/Items/Armor/Costumes/HazmatSuit_Head", "", "");
				base.AddEquipTexture(new HazmatBody(), null, 1, "HazmatBody", "Redemption/Items/Armor/Costumes/HazmatSuit_Body", "Redemption/Items/Armor/Costumes/HazmatSuit_FemaleBody", "Redemption/Items/Armor/Costumes/HazmatSuit_Arms");
				base.AddEquipTexture(new HazmatLegs(), null, 2, "HazmatLegs", "Redemption/Items/Armor/Costumes/HazmatSuit_Legs", "", "");
				base.AddMusicBox(base.GetSoundSlot(51, "Sounds/Music/XenoCaves"), base.ItemType("WastelandBox"), base.TileType("WastelandBoxTile"), 0);
				base.AddMusicBox(base.GetSoundSlot(51, "Sounds/Music/BossXeno1"), base.ItemType("XenomiteCrystalBox"), base.TileType("XenomiteCrystalBoxTile"), 0);
				base.AddMusicBox(base.GetSoundSlot(51, "Sounds/Music/BossXeno2"), base.ItemType("InfectedEyeBox"), base.TileType("InfectedEyeBoxTile"), 0);
				base.AddMusicBox(base.GetSoundSlot(51, "Sounds/Music/BossSlayer"), base.ItemType("KSBox"), base.TileType("KSBoxTile"), 0);
				base.AddMusicBox(base.GetSoundSlot(51, "Sounds/Music/BossVlitch1"), base.ItemType("VlitchBox"), base.TileType("VlitchBoxTile"), 0);
				base.AddMusicBox(base.GetSoundSlot(51, "Sounds/Music/BossKeeper"), base.ItemType("KeeperBox"), base.TileType("KeeperBoxTile"), 0);
				base.AddMusicBox(base.GetSoundSlot(51, "Sounds/Music/BossVlitch2"), base.ItemType("VlitchBox2"), base.TileType("VlitchBoxTile2"), 0);
			}
			Redemption.FaceCustomCurrencyID = CustomCurrencyManager.RegisterCurrency(new CustomCurrency(base.ItemType<AncientGoldCoin>(), 999L));
			ModTranslation modTranslation = base.CreateTranslation("DruidicOre");
			modTranslation.SetDefault("Druidic energy courses through the world's ore...");
			base.AddTranslation(modTranslation);
			modTranslation = base.CreateTranslation("DragonLeadMessage");
			modTranslation.SetDefault("The caverns are heated with dragon bone...");
			base.AddTranslation(modTranslation);
			modTranslation = base.CreateTranslation("InfectionMessage1");
			modTranslation.SetDefault("The Infection begins...");
			base.AddTranslation(modTranslation);
			modTranslation = base.CreateTranslation("LabIsSafe");
			modTranslation.SetDefault("The lab's defence systems have malfunctioned...");
			base.AddTranslation(modTranslation);
			modTranslation = base.CreateTranslation("GrowingInfection");
			modTranslation.SetDefault("The Infection grows...");
			base.AddTranslation(modTranslation);
			modTranslation = base.CreateTranslation("Lasers1");
			modTranslation.SetDefault("Laser Systems #1 has been deactivated...");
			base.AddTranslation(modTranslation);
			modTranslation = base.CreateTranslation("Lasers2");
			modTranslation.SetDefault("Laser Systems #2 has been deactivated...");
			base.AddTranslation(modTranslation);
			modTranslation = base.CreateTranslation("Lasers3");
			modTranslation.SetDefault("Laser Systems #3 has been deactivated...");
			base.AddTranslation(modTranslation);
			modTranslation = base.CreateTranslation("Lasers4");
			modTranslation.SetDefault("Laser Systems #4 has been deactivated...");
			base.AddTranslation(modTranslation);
			modTranslation = base.CreateTranslation("Lasers5");
			modTranslation.SetDefault("Laser Systems #5 has been deactivated...");
			base.AddTranslation(modTranslation);
			modTranslation = base.CreateTranslation("Lasers6");
			modTranslation.SetDefault("Laser Systems #6 has been deactivated...");
			base.AddTranslation(modTranslation);
			modTranslation = base.CreateTranslation("Lasers7");
			modTranslation.SetDefault("All Laser Systems have been deactivated...");
			base.AddTranslation(modTranslation);
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
				array3[2] = 3.48f;
				array3[3] = new Func<bool>(() => RedeWorld.downedStrangePortal);
				array3[4] = "Use an [i:" + base.ItemType<UnstableCrystal>() + "]";
				mod4.Call(array3);
				Mod mod5 = mod;
				object[] array4 = new object[5];
				array4[0] = "AddBossWithInfo";
				array4[1] = "Xenomite Crystal";
				array4[2] = 3.49f;
				array4[3] = new Func<bool>(() => RedeWorld.downedXenomiteCrystal);
				array4[4] = "Use a [i:" + base.ItemType<GeigerCounter>() + "], dropped by the Strange Portal";
				mod5.Call(array4);
				Mod mod6 = mod;
				object[] array5 = new object[5];
				array5[0] = "AddBossWithInfo";
				array5[1] = "Infected Eye";
				array5[2] = 6.25f;
				array5[3] = new Func<bool>(() => RedeWorld.downedInfectedEye);
				array5[4] = "Use a [i:" + base.ItemType<XenoEye>() + "] at night, requires the Xenomite Crystal to be defeated";
				mod6.Call(array5);
				Mod mod7 = mod;
				object[] array6 = new object[5];
				array6[0] = "AddBossWithInfo";
				array6[1] = "The Abandoned Lab";
				array6[2] = 9.1f;
				array6[3] = new Func<bool>(() => RedeWorld.downedIBehemoth);
				array6[4] = "Find the Abandoned Lab far below the surface, defeat the first 3 minibosses within. (Lab does NOT work in Multiplayer) [i:" + base.ItemType<LabHelpMessage>() + "]";
				mod7.Call(array6);
				Mod mod8 = mod;
				object[] array7 = new object[5];
				array7[0] = "AddBossWithInfo";
				array7[1] = "King Slayer III";
				array7[2] = 9.99999f;
				array7[3] = new Func<bool>(() => RedeWorld.downedSlayer);
				array7[4] = "Use a [i:" + base.ItemType<KingSummon>() + "] at day. (Although I would recommend fighting at a later point in the game)";
				mod8.Call(array7);
				Mod mod9 = mod;
				object[] array8 = new object[5];
				array8[0] = "AddBossWithInfo";
				array8[1] = "1st Vlitch Overlord";
				array8[2] = 11.5f;
				array8[3] = new Func<bool>(() => RedeWorld.downedVlitch1);
				array8[4] = "Use a [i:" + base.ItemType<CorruptedHeroSword>() + "] at night";
				mod9.Call(array8);
				Mod mod10 = mod;
				object[] array9 = new object[5];
				array9[0] = "AddBossWithInfo";
				array9[1] = "2nd Vlitch Overlord";
				array9[2] = 13.5f;
				array9[3] = new Func<bool>(() => RedeWorld.downedVlitch2);
				array9[4] = "Use a [i:" + base.ItemType<CorruptedWormMedallion>() + "] at night";
				mod10.Call(array9);
				Mod mod11 = mod;
				object[] array10 = new object[5];
				array10[0] = "AddBossWithInfo";
				array10[1] = "3rd Vlitch Overlord";
				array10[2] = 14.05f;
				array10[3] = new Func<bool>(() => RedeWorld.downedVlitch3);
				array10[4] = "Use an [i:" + base.ItemType<OmegaRadar>() + "] at night";
				mod11.Call(array10);
				Mod mod12 = mod;
				object[] array11 = new object[5];
				array11[0] = "AddBossWithInfo";
				array11[1] = "Patient Zero";
				array11[2] = 15f;
				array11[3] = new Func<bool>(() => RedeWorld.downedPatientZero);
				array11[4] = "Use a lunar pickaxe to mine the hardened sludge in the Abandoned Lab to explore further. Beware what awaits beyond.";
				mod12.Call(array11);
			}
			Mod mod13 = ModLoader.GetMod("Census");
			if (mod13 != null)
			{
				mod13.Call(new object[]
				{
					"TownNPCCondition",
					base.NPCType("Squire"),
					"Have a suitable house"
				});
				mod13.Call(new object[]
				{
					"TownNPCCondition",
					base.NPCType("Fallen"),
					"Defeat the Keeper and have a suitable house"
				});
				mod13.Call(new object[]
				{
					"TownNPCCondition",
					base.NPCType("Newb"),
					"Find the Suspicious Dirt Pile in the caverns"
				});
				mod13.Call(new object[]
				{
					"TownNPCCondition",
					base.NPCType("TBot"),
					"Defeat the Infected Eye and have a suitable house"
				});
				mod13.Call(new object[]
				{
					"TownNPCCondition",
					base.NPCType("Slicer"),
					"Defeat the Dark Slime miniboss, which spawns very rarely on the surface after all mech bosses are defeated"
				});
				mod13.Call(new object[]
				{
					"TownNPCCondition",
					base.NPCType("DHunter"),
					"Defeat the Dark Slime and Plantera, and have a suitable house"
				});
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
			modRecipe = new ModRecipe(this);
			modRecipe.AddIngredient(23, 40);
			modRecipe.AddIngredient(null, "BlankGoldCrown", 1);
			modRecipe.AddTile(26);
			modRecipe.SetResult(560, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(this);
			modRecipe.AddIngredient(23, 40);
			modRecipe.AddIngredient(null, "BlankPlatCrown", 1);
			modRecipe.AddTile(26);
			modRecipe.SetResult(560, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(this);
			modRecipe.AddIngredient(null, "DruidEmblem", 1);
			modRecipe.AddIngredient(548, 5);
			modRecipe.AddIngredient(549, 5);
			modRecipe.AddIngredient(547, 5);
			modRecipe.AddTile(114);
			modRecipe.SetResult(935, 1);
			modRecipe.AddRecipe();
		}

		public override void HandlePacket(BinaryReader bb, int whoAmI)
		{
			MsgType msgType = (MsgType)bb.ReadByte();
			if (msgType == MsgType.ProjectileHostility)
			{
				int num = bb.ReadInt32();
				int num2 = bb.ReadInt32();
				bool flag = bb.ReadBoolean();
				bool flag2 = bb.ReadBoolean();
				if (Main.projectile[num2] != null)
				{
					Main.projectile[num2].owner = num;
					Main.projectile[num2].friendly = flag;
					Main.projectile[num2].hostile = flag2;
				}
				if (Main.netMode == 2)
				{
					MNet.SendBaseNetMessage(0, new object[]
					{
						num,
						num2,
						flag,
						flag2
					});
					return;
				}
			}
			else if (msgType == MsgType.SyncAI)
			{
				int num3 = (int)bb.ReadByte();
				int num4 = (int)bb.ReadInt16();
				int num5 = (int)bb.ReadByte();
				int num6 = (int)bb.ReadByte();
				float[] array = new float[num6];
				for (int i = 0; i < num6; i++)
				{
					array[i] = bb.ReadSingle();
				}
				if (num3 == 0 && Main.npc[num4] != null && Main.npc[num4].active && Main.npc[num4].modNPC != null && Main.npc[num4].modNPC is ParentNPC)
				{
					((ParentNPC)Main.npc[num4].modNPC).SetAI(array, num5);
				}
				else if (num3 == 1 && Main.projectile[num4] != null && Main.projectile[num4].active && Main.projectile[num4].modProjectile != null && Main.projectile[num4].modProjectile is ParentProjectile)
				{
					((ParentProjectile)Main.projectile[num4].modProjectile).SetAI(array, num5);
				}
				if (Main.netMode == 2)
				{
					BaseNet.SyncAI(num3, num4, array, num5);
				}
			}
		}

		public static int FaceCustomCurrencyID;

		public static bool GirusSilence;

		public static Redemption inst = null;

		public static bool templeOfHeroes;

		public static IDictionary<string, Texture2D> Textures = null;

		public static Dictionary<string, Texture2D> precachedTextures = new Dictionary<string, Texture2D>();
	}
}
