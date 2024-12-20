using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using On.Terraria;
using Redemption.Backgrounds.Skies;
using Redemption.ChickenArmy;
using Redemption.CrossMod;
using Redemption.Effects;
using Redemption.Items.Accessories.HM;
using Redemption.Items.Accessories.PostML;
using Redemption.Items.Accessories.PreHM;
using Redemption.Items.Critters;
using Redemption.Items.Materials.HM;
using Redemption.Items.Placeable.Tiles;
using Redemption.Items.Usable;
using Redemption.Items.Weapons.HM.Druid.Seedbags;
using Redemption.Items.Weapons.PreHM.Druid.Seedbags;
using Redemption.Localization;
using Redemption.NPCs.Bosses.SeedOfInfection;
using Redemption.StructureHelper;
using Redemption.StructureHelper.ChestHelper.GUI;
using Redemption.Tiles.Tiles;
using Redemption.UI;
using ReLogic.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.Utilities;

namespace Redemption
{
	internal class Redemption : Mod
	{
		public static Redemption Inst
		{
			get
			{
				return ModContent.GetInstance<Redemption>();
			}
		}

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
			if (Main.gameMenu)
			{
				return;
			}
			if (priority > 4)
			{
				return;
			}
			if (!Main.LocalPlayer.active)
			{
				return;
			}
			if (Main.myPlayer != -1 && !Main.gameMenu)
			{
				if (Redemption.GirusSilence)
				{
					music = base.GetSoundSlot(51, "Sounds/Music/silence");
					priority = 8;
				}
				if (Main.player[Main.myPlayer].active)
				{
					if (Main.player[Main.myPlayer].GetModPlayer<RedePlayer>().irradiatedEffect == 2)
					{
						music = base.GetSoundSlot(51, "Sounds/Music/silence");
						priority = 8;
					}
					if (Main.player[Main.myPlayer].GetModPlayer<RedePlayer>().irradiatedEffect == 3)
					{
						music = base.GetSoundSlot(51, "Sounds/Music/Rad1");
						priority = 8;
					}
					if (Main.player[Main.myPlayer].GetModPlayer<RedePlayer>().irradiatedEffect >= 4)
					{
						music = base.GetSoundSlot(51, "Sounds/Music/Rad2");
						priority = 8;
					}
					if (RedeWorld.nukeCountdownActive)
					{
						music = base.GetSoundSlot(51, "Sounds/Music/Warhead");
						priority = 8;
					}
				}
			}
		}

		public override void AddRecipeGroups()
		{
			RecipeGroup group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Plant", new int[]
			{
				313,
				317,
				315,
				316,
				318,
				314,
				2358,
				0
			});
			RecipeGroup.RegisterGroup("Redemption:Plant", group);
			RecipeGroup group2 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Seedbag", new int[]
			{
				ModContent.ItemType<Seedbag>(),
				ModContent.ItemType<BlinkrootBushBag>(),
				ModContent.ItemType<CorpseFlowerBag>(),
				ModContent.ItemType<CrimthornBushBag>(),
				ModContent.ItemType<DeathweedBag>(),
				ModContent.ItemType<FireblossomBag>(),
				ModContent.ItemType<IcarsFlowerBag>(),
				ModContent.ItemType<ShiverthornBag>(),
				ModContent.ItemType<SkyflowerBag>(),
				ModContent.ItemType<VilethornBushBag>(),
				ModContent.ItemType<WaterleafBag>(),
				ModContent.ItemType<DaybloomBag>(),
				ModContent.ItemType<MoonglowBag>(),
				ModContent.ItemType<MossBag>(),
				ModContent.ItemType<EyeStalkBag>(),
				ModContent.ItemType<MartianTreeBag>(),
				ModContent.ItemType<TitaniumBloomBag>(),
				ModContent.ItemType<AdamantiteLilyBag>(),
				ModContent.ItemType<CreationRoseBag>(),
				ModContent.ItemType<DeathsGraspBag>(),
				ModContent.ItemType<GloomShroomBag>(),
				ModContent.ItemType<GlowingMBag>(),
				ModContent.ItemType<GoldenOrangeBag>(),
				ModContent.ItemType<HealShroomBag>(),
				ModContent.ItemType<NightshadeSeedbag>(),
				ModContent.ItemType<BloodRootSeedbag>(),
				ModContent.ItemType<ViciousShroomBag>(),
				ModContent.ItemType<VileShroomBag>()
			});
			RecipeGroup.RegisterGroup("Redemption:Seedbag", group2);
			RecipeGroup group3 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Phasesaber", new int[]
			{
				3765,
				3764,
				3769,
				3767,
				3766,
				3768
			});
			RecipeGroup.RegisterGroup("Redemption:Phasesabers", group3);
			RecipeGroup group4 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Evil Wood", new int[]
			{
				911,
				619
			});
			RecipeGroup.RegisterGroup("Redemption:EvilWood", group4);
			RecipeGroup group5 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Capacitator", new int[]
			{
				ModContent.ItemType<Mk1Capacitator>(),
				ModContent.ItemType<Mk2Capacitator>(),
				ModContent.ItemType<Mk3Capacitator>()
			});
			RecipeGroup.RegisterGroup("Redemption:Capacitators", group5);
			RecipeGroup group6 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Plating", new int[]
			{
				ModContent.ItemType<Mk1Plating>(),
				ModContent.ItemType<Mk2Plating>(),
				ModContent.ItemType<Mk3Plating>()
			});
			RecipeGroup.RegisterGroup("Redemption:Plating", group6);
			RecipeGroup group7 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Chicken", new int[]
			{
				ModContent.ItemType<ChickenItem>(),
				ModContent.ItemType<ChickenLeghornItem>(),
				ModContent.ItemType<ChickenRedItem>(),
				ModContent.ItemType<ChickenGoldItem>(),
				ModContent.ItemType<ChickenLongItem>(),
				ModContent.ItemType<ChickenVlitchItem>()
			});
			RecipeGroup.RegisterGroup("Redemption:Chicken", group7);
			RecipeGroup group8 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Gathic Stone", new int[]
			{
				ModContent.ItemType<AncientStone>(),
				ModContent.ItemType<AncientStone2>()
			});
			RecipeGroup.RegisterGroup("Redemption:GathicStone", group8);
			RecipeGroup group9 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Gathic Stone Brick", new int[]
			{
				ModContent.ItemType<AncientStoneBrick>(),
				ModContent.ItemType<AncientStoneBrick2>()
			});
			RecipeGroup.RegisterGroup("Redemption:GathicStoneBrick", group9);
			RecipeGroup group10 = new RecipeGroup(() => "Bioweapon or Bile", new int[]
			{
				ModContent.ItemType<Bioweapon>(),
				ModContent.ItemType<Bile>()
			});
			RecipeGroup.RegisterGroup("Redemption:BioweaponBile", group10);
			if (RecipeGroup.recipeGroupIDs.ContainsKey("Wood"))
			{
				int index = RecipeGroup.recipeGroupIDs["Wood"];
				RecipeGroup recipeGroup = RecipeGroup.recipeGroups[index];
				recipeGroup.ValidItems.Add(base.ItemType("DeadWood"));
				recipeGroup.ValidItems.Add(base.ItemType("AncientWood"));
			}
		}

		private void ActuateForced(Wiring.orig_ActuateForced orig, int i, int j)
		{
			Tile tile = Main.tile[i, j];
			if ((int)tile.type == ModContent.TileType<LabTileUnsafe>() || (int)tile.type == ModContent.TileType<HardenedSludgeTile>() || (int)tile.type == ModContent.TileType<HardenedSludge3Tile>() || (int)tile.type == ModContent.TileType<OvergrownLabTile>())
			{
				return;
			}
			orig.Invoke(i, j);
		}

		private static bool Actuate(Wiring.orig_Actuate orig, int i, int j)
		{
			Tile tile = Main.tile[i, j];
			return (int)tile.type != ModContent.TileType<LabTileUnsafe>() && (int)tile.type != ModContent.TileType<HardenedSludgeTile>() && (int)tile.type != ModContent.TileType<HardenedSludge3Tile>() && (int)tile.type != ModContent.TileType<OvergrownLabTile>() && orig.Invoke(i, j);
		}

		public override void Load()
		{
			this.LoadCache();
			RedeDetours.Initialize();
			RedemptionLocalization.AddLocalizations();
			if (Main.rand == null)
			{
				Main.rand = new UnifiedRandom();
			}
			Wiring.ActuateForced += new Wiring.hook_ActuateForced(this.ActuateForced);
			Wiring.Actuate += new Wiring.hook_Actuate(Redemption.Actuate);
			if (!Main.dedServ)
			{
				this.LoadClient();
			}
			Redemption.FaceCustomCurrencyID = CustomCurrencyManager.RegisterCurrency(new CustomCurrency(ModContent.ItemType<AncientGoldCoin>(), 999L));
			Filters.Scene["Redemption:XenoSky"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0f, 0.2f, 0f).UseOpacity(0.4f), 3);
			Filters.Scene["Redemption:SoullessSky"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0f, 0f, 0f).UseOpacity(0.55f), 3);
			ModTranslation text = base.CreateTranslation("DragonLeadMessage");
			text.SetDefault("The caverns are heated with dragon bone...");
			base.AddTranslation(text);
			text = base.CreateTranslation("InfectionMessage1");
			text.SetDefault("Creatures start getting infected...");
			base.AddTranslation(text);
			text = base.CreateTranslation("LabIsSafe");
			text.SetDefault("The lab's defence systems have malfunctioned...");
			base.AddTranslation(text);
			text = base.CreateTranslation("Lasers1");
			text.SetDefault("Laser Systems #1 has been deactivated...");
			base.AddTranslation(text);
			text = base.CreateTranslation("Lasers2");
			text.SetDefault("Laser Systems #2 has been deactivated...");
			base.AddTranslation(text);
			text = base.CreateTranslation("Lasers3");
			text.SetDefault("Laser Systems #3 has been deactivated...");
			base.AddTranslation(text);
			text = base.CreateTranslation("Lasers4");
			text.SetDefault("Laser Systems #4 has been deactivated...");
			base.AddTranslation(text);
			text = base.CreateTranslation("Lasers5");
			text.SetDefault("Laser Systems #5 has been deactivated...");
			base.AddTranslation(text);
			text = base.CreateTranslation("Lasers6");
			text.SetDefault("Laser Systems #6 has been deactivated...");
			base.AddTranslation(text);
			text = base.CreateTranslation("Lasers7");
			text.SetDefault("Laser Systems #7 has been deactivated...");
			base.AddTranslation(text);
			text = base.CreateTranslation("PatientZeroMessage1");
			text.SetDefault("As the Infection halts, strong creatures of Epidotra return...");
			base.AddTranslation(text);
			text = base.CreateTranslation("PatientZeroMessage2");
			text.SetDefault("Cursed rocks of crimson form in the ash of the Underworld...");
			base.AddTranslation(text);
			text = base.CreateTranslation("PatientZeroMessage3");
			text.SetDefault("Powerful creatures roam the forests, caverns, and skies...");
			base.AddTranslation(text);
			text = base.CreateTranslation("GirusHide");
			text.SetDefault("Thought you could hide from me?");
			base.AddTranslation(text);
			text = base.CreateTranslation("KeeperAwoken");
			text.SetDefault("The Keeper has awoken!");
			base.AddTranslation(text);
			text = base.CreateTranslation("SoIChecklist");
			text.SetDefault("New entries have appeared on the Boss Checklist!");
			base.AddTranslation(text);
			text = base.CreateTranslation("KingSlayerMoonlord");
			text.SetDefault("Hey terrarian, it's King Slayer. I'll be leaving ship soon so if you have any unfinished business with me then I'd do it now.");
			base.AddTranslation(text);
			text = base.CreateTranslation("ThornMessage");
			text.SetDefault("The forest's flora blooms...");
			base.AddTranslation(text);
			SubworldCache.InitCache();
		}

		private void LoadCache()
		{
			this._loadCache = new List<ILoadable>();
			foreach (Type type in base.Code.GetTypes())
			{
				if (!type.IsAbstract && Enumerable.Contains<Type>(type.GetInterfaces(), typeof(ILoadable)))
				{
					this._loadCache.Add(Activator.CreateInstance(type) as ILoadable);
				}
			}
			this._loadCache.Sort(delegate(ILoadable x, ILoadable y)
			{
				if (x.Priority <= y.Priority)
				{
					return -1;
				}
				return 1;
			});
			for (int i = 0; i < this._loadCache.Count; i++)
			{
				if (!Main.dedServ || this._loadCache[i].LoadOnDedServer)
				{
					this._loadCache[i].Load();
				}
			}
		}

		public override void Unload()
		{
			Redemption.TrailManager = null;
			SubworldCache.UnloadCache();
			Generator.StructureDataCache.Clear();
		}

		public override void MidUpdateProjectileItem()
		{
			if (Main.netMode != 2)
			{
				Redemption.TrailManager.UpdateTrails();
			}
		}

		public void LoadClient()
		{
			Redemption.TrailManager = new TrailManager(this);
			this.TitleUILayer = new UserInterface();
			this.TitleCardUIElement = new TitleCard();
			this.TitleUILayer.SetState(this.TitleCardUIElement);
			this.DialogueUILayer = new UserInterface();
			this.DialogueUIElement = new MoRDialogueUI();
			this.DialogueUILayer.SetState(this.DialogueUIElement);
			this.NukeUILayer = new UserInterface();
			this.NukeUIElement = new NukeDetonationUI();
			this.NukeUILayer.SetState(this.NukeUIElement);
			this.AMemoryUILayer = new UserInterface();
			this.AMemoryUIElement = new AMemoryUIState();
			this.AMemoryUILayer.SetState(this.AMemoryUIElement);
			this.GeneratorMenuUI = new UserInterface();
			this.GeneratorMenu = new ManualGeneratorMenu();
			this.GeneratorMenuUI.SetState(this.GeneratorMenu);
			this.ChestMenuUI = new UserInterface();
			this.ChestCustomizer = new ChestCustomizerState();
			this.ChestMenuUI.SetState(this.ChestCustomizer);
			base.AddEquipTexture(null, 2, "ArchclothRobe_Legs", "Redemption/Items/Armor/Single/ArchclothRobe_Legs", "", "");
			base.AddEquipTexture(null, 2, "HallamRobes_Legs", "Redemption/Items/Armor/Vanity/HallamRobes_Legs", "", "");
			base.AddEquipTexture(null, 2, "NebuleusVanity_Legs", "Redemption/Items/Armor/Vanity/NebuleusVanity_Legs", "", "");
			base.AddEquipTexture(null, 2, "ArchePatreonVanityLegs_FemaleLegs", "Redemption/Items/Donator/Arche/ArchePatreonVanityLegs_FemaleLegs", "", "");
			base.AddEquipTexture(new ChickenHead(), null, 0, "ChickenHead", "Redemption/Items/Accessories/PreHM/CrownOfTheKing_Head", "", "");
			base.AddEquipTexture(new ChickenBody(), null, 1, "ChickenBody", "Redemption/Items/Accessories/PreHM/CrownOfTheKing_Body", "Redemption/Items/Accessories/PreHM/CrownOfTheKing_Arms", "");
			base.AddEquipTexture(new ChickenLegs(), null, 2, "ChickenLegs", "Redemption/Items/Accessories/PreHM/CrownOfTheKing_Legs", "", "");
			base.AddEquipTexture(new HazmatHead(), null, 0, "HazmatHead", "Redemption/Items/Accessories/HM/HazmatSuit_Head", "", "");
			base.AddEquipTexture(new HazmatBody(), null, 1, "HazmatBody", "Redemption/Items/Accessories/HM/HazmatSuit_Body", "Redemption/Items/Accessories/HM/HazmatSuit_FemaleBody", "Redemption/Items/Accessories/HM/HazmatSuit_Arms");
			base.AddEquipTexture(new HazmatLegs(), null, 2, "HazmatLegs", "Redemption/Items/Accessories/HM/HazmatSuit_Legs", "", "");
			base.AddEquipTexture(new HEVHead(), null, 0, "HEVHead", "Redemption/Items/Accessories/PostML/HEVSuit_Head", "", "");
			base.AddEquipTexture(new HEVBody(), null, 1, "HEVBody", "Redemption/Items/Accessories/PostML/HEVSuit_Body", "Redemption/Items/Accessories/PostML/HEVSuit_Arms", "");
			base.AddEquipTexture(new HEVLegs(), null, 2, "HEVLegs", "Redemption/Items/Accessories/PostML/HEVSuit_Legs", "", "");
			base.AddMusicBox(base.GetSoundSlot(51, "Sounds/Music/HallofHeroes"), base.ItemType("HallOfHeroesBox"), base.TileType("HallOfHeroesBoxTile"), 0);
			Texture2D darknessTex = base.GetTexture("Backgrounds/Effects/DarknessTex");
			Redemption.PremultiplyTexture(ref darknessTex);
			Texture2D scanPro = base.GetTexture("NPCs/Bosses/KSIII/ScanPro");
			Redemption.PremultiplyTexture(ref scanPro);
			Texture2D oSBubble = base.GetTexture("NPCs/Bosses/OmegaSlayer/OSBubble");
			Redemption.PremultiplyTexture(ref oSBubble);
			Texture2D shadeVortex = base.GetTexture("NPCs/Bosses/Warden/ShadeVortex");
			Redemption.PremultiplyTexture(ref shadeVortex);
			Texture2D soullessPortal = base.GetTexture("NPCs/Bosses/Warden/SoullessPortal");
			Redemption.PremultiplyTexture(ref soullessPortal);
			Texture2D holyGlow = base.GetTexture("ExtraTextures/HolyGlow");
			Redemption.PremultiplyTexture(ref holyGlow);
			Texture2D whiteFlare = base.GetTexture("ExtraTextures/WhiteFlare");
			Redemption.PremultiplyTexture(ref whiteFlare);
			Texture2D whiteOrb = base.GetTexture("ExtraTextures/WhiteOrb");
			Redemption.PremultiplyTexture(ref whiteOrb);
			Texture2D whiteLightBeam = base.GetTexture("ExtraTextures/WhiteLightBeam");
			Redemption.PremultiplyTexture(ref whiteLightBeam);
			Texture2D transitionTex = base.GetTexture("TransitionTex");
			Redemption.PremultiplyTexture(ref transitionTex);
			Texture2D ukkoDancingLights = base.GetTexture("NPCs/Bosses/EaglecrestGolem/UkkoDancingLights");
			Redemption.PremultiplyTexture(ref ukkoDancingLights);
			Texture2D akkaPoisonBubble = base.GetTexture("NPCs/Bosses/Thorn/AkkaPoisonBubble");
			Redemption.PremultiplyTexture(ref akkaPoisonBubble);
			Texture2D akkaIslandWarning = base.GetTexture("NPCs/Bosses/Thorn/AkkaIslandWarning");
			Redemption.PremultiplyTexture(ref akkaIslandWarning);
			Texture2D akkaHealingSpirit = base.GetTexture("NPCs/Bosses/Thorn/AkkaHealingSpirit");
			Redemption.PremultiplyTexture(ref akkaHealingSpirit);
			Texture2D ukkoElectricBlast = base.GetTexture("NPCs/Bosses/EaglecrestGolem/UkkoElectricBlast");
			Redemption.PremultiplyTexture(ref ukkoElectricBlast);
			Texture2D ancientForcefieldPro = base.GetTexture("Projectiles/Druid/Stave/AncientForcefieldPro1");
			Redemption.PremultiplyTexture(ref ancientForcefieldPro);
			Texture2D ancientForcefieldPro2 = base.GetTexture("Projectiles/Druid/Stave/AncientForcefieldPro2");
			Redemption.PremultiplyTexture(ref ancientForcefieldPro2);
			Texture2D caveAura = base.GetTexture("Projectiles/Druid/Stave/CaveAura");
			Redemption.PremultiplyTexture(ref caveAura);
			Texture2D fireForcePro = base.GetTexture("Projectiles/Druid/Stave/FireForcePro1");
			Redemption.PremultiplyTexture(ref fireForcePro);
			Texture2D freezeForcePro = base.GetTexture("Projectiles/Druid/Stave/FreezeForcePro1");
			Redemption.PremultiplyTexture(ref freezeForcePro);
			Texture2D martianForcefieldPro = base.GetTexture("Projectiles/Druid/Stave/MartianForcefieldPro1");
			Redemption.PremultiplyTexture(ref martianForcefieldPro);
			Texture2D sunAura = base.GetTexture("Projectiles/Druid/Stave/SunAura");
			Redemption.PremultiplyTexture(ref sunAura);
			Texture2D darksideVortex = base.GetTexture("Projectiles/Druid/Stave/DarksideVortex");
			Redemption.PremultiplyTexture(ref darksideVortex);
			StarGodSky sSky = new StarGodSky();
			Filters.Scene["Redemption:NebP1"] = new Filter(new StarGodSkyData("FilterMiniTower").UseColor(0.3f, 0f, 0.4f).UseOpacity(0.5f), 4);
			SkyManager.Instance["Redemption:NebP1"] = sSky;
			StarGodSky2 sSky2 = new StarGodSky2();
			Filters.Scene["Redemption:NebP2"] = new Filter(new StarGodSkyData2("FilterMiniTower").UseColor(0.3f, 0f, 0.4f).UseOpacity(0.5f), 4);
			SkyManager.Instance["Redemption:NebP2"] = sSky2;
			UkkoClouds1 uSky = new UkkoClouds1();
			Filters.Scene["Redemption:Ukko"] = new Filter(new UkkoClouds1Data("FilterMiniTower").UseColor(0.1f, 0.1f, 0f).UseOpacity(0.65f), 4);
			SkyManager.Instance["Redemption:Ukko"] = uSky;
			Texture2D ukkoSkyBeam = base.GetTexture("Backgrounds/Skies/UkkoSkyBeam");
			Redemption.PremultiplyTexture(ref ukkoSkyBeam);
			Texture2D ukkoClouds = base.GetTexture("Backgrounds/Skies/UkkoClouds1");
			Redemption.PremultiplyTexture(ref ukkoClouds);
		}

		public static void PremultiplyTexture(ref Texture2D texture)
		{
			Color[] buffer = new Color[texture.Width * texture.Height];
			texture.GetData<Color>(buffer);
			for (int i = 0; i < buffer.Length; i++)
			{
				buffer[i] = Color.FromNonPremultiplied((int)buffer[i].R, (int)buffer[i].G, (int)buffer[i].B, (int)buffer[i].A);
			}
			texture.SetData<Color>(buffer);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			if (ChickWorld.chickArmy)
			{
				int index = layers.FindIndex((GameInterfaceLayer layer) => layer.Name.Equals("Vanilla: Inventory"));
				if (index >= 0)
				{
					LegacyGameInterfaceLayer ChickThing = new LegacyGameInterfaceLayer("Redemption: ChickBenis", delegate()
					{
						this.DrawChick(Main.spriteBatch);
						return true;
					}, 1);
					layers.Insert(index, ChickThing);
				}
			}
			if (Main.player[Main.myPlayer].GetModPlayer<InfectionTextPlayer>().text)
			{
				int textLayer = layers.FindIndex((GameInterfaceLayer layer) => layer.Name.Equals("Vanilla: Inventory"));
				LegacyGameInterfaceLayer computerState = new LegacyGameInterfaceLayer("Redemption: UI", delegate()
				{
					this.DrawInfectionText();
					return true;
				}, 1);
				layers.Insert(textLayer, computerState);
			}
			int MouseTextIndex = layers.FindIndex((GameInterfaceLayer layer) => layer.Name.Equals("Vanilla: Mouse Text"));
			if (MouseTextIndex != -1)
			{
				this.AddInterfaceLayer(layers, this.TitleUILayer, this.TitleCardUIElement, MouseTextIndex, TitleCard.Showing, "Title Card");
				this.AddInterfaceLayer(layers, this.DialogueUILayer, this.DialogueUIElement, MouseTextIndex + 1, MoRDialogueUI.Visible, "Dialogue");
				this.AddInterfaceLayer(layers, this.NukeUILayer, this.NukeUIElement, MouseTextIndex + 2, NukeDetonationUI.Visible, "Nuke");
				this.AddInterfaceLayer(layers, this.AMemoryUILayer, this.AMemoryUIElement, MouseTextIndex + 3, AMemoryUIState.Visible, "Lab Photo");
			}
			layers.Insert(layers.FindIndex((GameInterfaceLayer layer) => layer.Name.Equals("Vanilla: Mouse Text")), new LegacyGameInterfaceLayer("GUI Menus", delegate()
			{
				if (ManualGeneratorMenu.Visible)
				{
					this.GeneratorMenuUI.Update(Main._drawInterfaceGameTime);
					this.GeneratorMenu.Draw(Main.spriteBatch);
				}
				if (ChestCustomizerState.Visible)
				{
					this.ChestMenuUI.Update(Main._drawInterfaceGameTime);
					this.ChestCustomizer.Draw(Main.spriteBatch);
				}
				return true;
			}, 1));
		}

		public void AddInterfaceLayer(List<GameInterfaceLayer> layers, UserInterface userInterface, UIState state, int index, bool visible, string customName = null)
		{
			string name;
			if (customName == null)
			{
				name = state.ToString();
			}
			else
			{
				name = customName;
			}
			layers.Insert(index, new LegacyGameInterfaceLayer("Redemption: " + name, delegate()
			{
				if (visible)
				{
					userInterface.Update(Main._drawInterfaceGameTime);
					state.Draw(Main.spriteBatch);
				}
				return true;
			}, 1));
		}

		public override void PostDrawInterface(SpriteBatch spriteBatch)
		{
			if (Main.LocalPlayer.HeldItem.modItem is CopyWand)
			{
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);
				Texture2D tex = ModContent.GetTexture("Redemption/StructureHelper/corner");
				Texture2D tex2 = ModContent.GetTexture("Redemption/StructureHelper/box1");
				Point16 TopLeft = (Main.LocalPlayer.HeldItem.modItem as CopyWand).TopLeft;
				int Width = (Main.LocalPlayer.HeldItem.modItem as CopyWand).Width;
				int Height = (Main.LocalPlayer.HeldItem.modItem as CopyWand).Height;
				float tileScale = 16f * Main.GameViewMatrix.Zoom.Length() * 0.7071067f;
				Vector2 pos = Utils.ToVector2(Utils.ToPoint16(Main.MouseWorld / tileScale)) * tileScale - Main.screenPosition;
				pos = Vector2.Transform(pos, Matrix.Invert(Main.GameViewMatrix.ZoomMatrix));
				pos = Vector2.Transform(pos, Main.UIScaleMatrix);
				spriteBatch.Draw(tex, pos, new Rectangle?(Utils.Frame(tex, 1, 1, 0, 0)), Color.White * 0.5f, 0f, Utils.Size(Utils.Frame(tex, 1, 1, 0, 0)) / 2f, 1f, SpriteEffects.None, 0f);
				if (Width != 0)
				{
					spriteBatch.Draw(tex2, new Rectangle((int)((float)(TopLeft.X * 16) - Main.screenPosition.X), (int)((float)(TopLeft.Y * 16) - Main.screenPosition.Y), Width * 16 + 16, Height * 16 + 16), new Rectangle?(Utils.Frame(tex2, 1, 1, 0, 0)), Color.White * 0.15f);
					spriteBatch.Draw(tex, (Utils.ToVector2(TopLeft) + new Vector2((float)(Width + 1), (float)(Height + 1))) * 16f - Main.screenPosition, new Rectangle?(Utils.Frame(tex, 1, 1, 0, 0)), Color.Red, 0f, Utils.Size(Utils.Frame(tex, 1, 1, 0, 0)) / 2f, 1f, SpriteEffects.None, 0f);
				}
				spriteBatch.Draw(tex, Utils.ToVector2(TopLeft) * 16f - Main.screenPosition, new Rectangle?(Utils.Frame(tex, 1, 1, 0, 0)), Color.Cyan, 0f, Utils.Size(Utils.Frame(tex, 1, 1, 0, 0)) / 2f, 1f, SpriteEffects.None, 0f);
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
			}
			if (Main.LocalPlayer.HeldItem.modItem is MultiWand)
			{
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);
				Texture2D tex3 = ModContent.GetTexture("Redemption/StructureHelper/corner");
				Texture2D tex4 = ModContent.GetTexture("Redemption/StructureHelper/box1");
				Point16 TopLeft2 = (Main.LocalPlayer.HeldItem.modItem as MultiWand).TopLeft;
				int Width2 = (Main.LocalPlayer.HeldItem.modItem as MultiWand).Width;
				int Height2 = (Main.LocalPlayer.HeldItem.modItem as MultiWand).Height;
				int count = (Main.LocalPlayer.HeldItem.modItem as MultiWand).StructureCache.Count;
				float tileScale2 = 16f * Main.GameViewMatrix.Zoom.Length() * 0.7071067f;
				Vector2 pos2 = Utils.ToVector2(Utils.ToPoint16(Main.MouseWorld / tileScale2)) * tileScale2 - Main.screenPosition;
				pos2 = Vector2.Transform(pos2, Matrix.Invert(Main.GameViewMatrix.ZoomMatrix));
				pos2 = Vector2.Transform(pos2, Main.UIScaleMatrix);
				spriteBatch.Draw(tex3, pos2, new Rectangle?(Utils.Frame(tex3, 1, 1, 0, 0)), Color.White * 0.5f, 0f, Utils.Size(Utils.Frame(tex3, 1, 1, 0, 0)) / 2f, 1f, SpriteEffects.None, 0f);
				if (Width2 != 0)
				{
					spriteBatch.Draw(tex4, new Rectangle((int)((float)(TopLeft2.X * 16) - Main.screenPosition.X), (int)((float)(TopLeft2.Y * 16) - Main.screenPosition.Y), Width2 * 16 + 16, Height2 * 16 + 16), new Rectangle?(Utils.Frame(tex4, 1, 1, 0, 0)), Color.White * 0.15f);
					spriteBatch.Draw(tex3, (Utils.ToVector2(TopLeft2) + new Vector2((float)(Width2 + 1), (float)(Height2 + 1))) * 16f - Main.screenPosition, new Rectangle?(Utils.Frame(tex3, 1, 1, 0, 0)), Color.Yellow, 0f, Utils.Size(Utils.Frame(tex3, 1, 1, 0, 0)) / 2f, 1f, SpriteEffects.None, 0f);
				}
				spriteBatch.Draw(tex3, Utils.ToVector2(TopLeft2) * 16f - Main.screenPosition, new Rectangle?(Utils.Frame(tex3, 1, 1, 0, 0)), Color.LimeGreen, 0f, Utils.Size(Utils.Frame(tex3, 1, 1, 0, 0)) / 2f, 1f, SpriteEffects.None, 0f);
				spriteBatch.End();
				spriteBatch.Begin();
				Utils.DrawBorderString(spriteBatch, "Structures to save: " + count, Main.MouseScreen + new Vector2(0f, 30f), Color.White, 1f, 0f, 0f, -1);
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
			}
		}

		private void DrawInfectionText()
		{
			float alpha = Main.player[Main.myPlayer].GetModPlayer<InfectionTextPlayer>().alphaText;
			string text = "The Infection shall begin...";
			Vector2 textSize = Main.fontDeathText.MeasureString(text);
			float textPositionLeft = (float)(Main.screenWidth / 2) - textSize.X / 2f;
			DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, Main.fontDeathText, text, new Vector2(textPositionLeft, (float)(Main.screenHeight / 2 - 300)), Color.Green * ((255f - alpha) / 255f), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
		}

		public void DrawChick(SpriteBatch spriteBatch)
		{
			if (ChickWorld.chickArmy)
			{
				float alpha = 0.5f;
				Texture2D backGround = Main.colorBarTexture;
				Texture2D progressColor = Main.colorBarTexture;
				Texture2D ChickIcon = Redemption.Inst.GetTexture("Effects/InvasionIcons/ChickArmy_Icon");
				float scmp = 0.875f;
				Color descColor = new Color(77, 39, 135);
				Color waveColor = new Color(255, 241, 51);
				int width = (int)(200f * scmp);
				int height = (int)(46f * scmp);
				Rectangle waveBackground = Utils.CenteredRectangle(new Vector2((float)(Main.screenWidth - 20) - 100f, (float)(Main.screenHeight - 20) - 23f), new Vector2((float)width, (float)height));
				Utils.DrawInvBG(spriteBatch, waveBackground, new Color(63, 65, 151, 255) * 0.785f);
				float cleared = RedeWorld.downedPatientZero ? ((float)ChickWorld.ChickPoints / 200f) : ((float)ChickWorld.ChickPoints / 100f);
				string waveText = "Cleared " + Math.Round((double)(100f * cleared)) + "%";
				Utils.DrawBorderString(spriteBatch, waveText, new Vector2((float)(waveBackground.X + waveBackground.Width / 2), (float)(waveBackground.Y + 5)), Color.White, scmp * 0.8f, 0.5f, -0.1f, -1);
				Rectangle waveProgressBar = Utils.CenteredRectangle(new Vector2((float)waveBackground.X + (float)waveBackground.Width * 0.5f, (float)waveBackground.Y + (float)waveBackground.Height * 0.75f), new Vector2((float)progressColor.Width, (float)progressColor.Height));
				Rectangle waveProgressAmount = new Rectangle(0, 0, (int)((float)progressColor.Width * MathHelper.Clamp(cleared, 0f, 1f)), progressColor.Height);
				Vector2 offset = new Vector2((float)(waveProgressBar.Width - (int)((float)waveProgressBar.Width * scmp)) * 0.5f, (float)(waveProgressBar.Height - (int)((float)waveProgressBar.Height * scmp)) * 0.5f);
				spriteBatch.Draw(backGround, Utils.ToVector2(waveProgressBar.Location) + offset, null, Color.White * alpha, 0f, new Vector2(0f), scmp, SpriteEffects.None, 0f);
				spriteBatch.Draw(backGround, Utils.ToVector2(waveProgressBar.Location) + offset, new Rectangle?(waveProgressAmount), waveColor, 0f, new Vector2(0f), scmp, SpriteEffects.None, 0f);
				Vector2 descSize = new Vector2(154f, 40f) * scmp;
				Rectangle barrierBackground = Utils.CenteredRectangle(new Vector2((float)(Main.screenWidth - 20) - 100f, (float)(Main.screenHeight - 20) - 19f), new Vector2((float)width, (float)height));
				Rectangle descBackground = Utils.CenteredRectangle(new Vector2((float)barrierBackground.X + (float)barrierBackground.Width * 0.5f, (float)(barrierBackground.Y - 6) - descSize.Y * 0.5f), descSize * 0.8f);
				Utils.DrawInvBG(spriteBatch, descBackground, descColor * alpha);
				int descOffset = (descBackground.Height - (int)(32f * scmp)) / 2;
				Rectangle icon = new Rectangle(descBackground.X + descOffset + 7, descBackground.Y + descOffset, (int)(32f * scmp), (int)(32f * scmp));
				spriteBatch.Draw(ChickIcon, icon, Color.White);
				Utils.DrawBorderString(spriteBatch, "Chicken Army", new Vector2((float)barrierBackground.X + (float)barrierBackground.Width * 0.5f, (float)(barrierBackground.Y - 6) - descSize.Y * 0.5f), Color.White, 0.8f, 0.3f, 0.4f, -1);
			}
		}

		public override void PostSetupContent()
		{
			WeakReferences.PerformModSupport();
			Mod bossChecklist = ModLoader.GetMod("BossChecklist");
			Mod yabhb = ModLoader.GetMod("FKBossHealthBar");
			Mod Calamity = ModLoader.GetMod("CalamityMod");
			Mod Thorium = ModLoader.GetMod("ThoriumMod");
			Mod Spirit = ModLoader.GetMod("SpiritMod");
			Mod Fargos = ModLoader.GetMod("Fargowiltas");
			Mod GRealm = ModLoader.GetMod("GRealm");
			Mod SacredTools = ModLoader.GetMod("SacredTools");
			Mod Tremor = ModLoader.GetMod("Tremor");
			Mod CheatSheet = ModLoader.GetMod("CheatSheet");
			Mod HEROsMod = ModLoader.GetMod("HEROsMod");
			Mod AA = ModLoader.GetMod("AAMod");
			Mod fargos = ModLoader.GetMod("Fargowiltas");
			Mod musicMod = ModLoader.GetMod("RedemptionMusic");
			if (fargos != null)
			{
				Mod mod = fargos;
				object[] array = new object[6];
				array[0] = "AddSummon";
				array[1] = 0f;
				array[2] = "Redemption";
				array[3] = "EggCrown";
				array[4] = new Func<bool>(() => RedeWorld.downedKingChicken);
				array[5] = 40000;
				mod.Call(array);
				Mod mod2 = fargos;
				object[] array2 = new object[6];
				array2[0] = "AddSummon";
				array2[1] = 1.5f;
				array2[2] = "Redemption";
				array2[3] = "HeartOfTheThorns";
				array2[4] = new Func<bool>(() => RedeWorld.downedThorn);
				array2[5] = 60000;
				mod2.Call(array2);
				Mod mod3 = fargos;
				object[] array3 = new object[6];
				array3[0] = "AddSummon";
				array3[1] = 2.4f;
				array3[2] = "Redemption";
				array3[3] = "MysteriousTabletCorrupt";
				array3[4] = new Func<bool>(() => RedeWorld.downedKeeper && !WorldGen.crimson);
				array3[5] = 80000;
				mod3.Call(array3);
				Mod mod4 = fargos;
				object[] array4 = new object[6];
				array4[0] = "AddSummon";
				array4[1] = 2.4f;
				array4[2] = "Redemption";
				array4[3] = "MysteriousTabletCrimson";
				array4[4] = new Func<bool>(() => RedeWorld.downedKeeper && WorldGen.crimson);
				array4[5] = 80000;
				mod4.Call(array4);
				Mod mod5 = fargos;
				object[] array5 = new object[6];
				array5[0] = "AddSummon";
				array5[1] = 3.48f;
				array5[2] = "Redemption";
				array5[3] = "GeigerCounter";
				array5[4] = new Func<bool>(() => RedeWorld.downedSoI);
				array5[5] = 100000;
				mod5.Call(array5);
				Mod mod6 = fargos;
				object[] array6 = new object[6];
				array6[0] = "AddSummon";
				array6[1] = 9.99999f;
				array6[2] = "Redemption";
				array6[3] = "KingSummon";
				array6[4] = new Func<bool>(() => RedeWorld.downedSlayer);
				array6[5] = 400000;
				mod6.Call(array6);
				Mod mod7 = fargos;
				object[] array7 = new object[6];
				array7[0] = "AddSummon";
				array7[1] = 11.5f;
				array7[2] = "Redemption";
				array7[3] = "CorruptedHeroSword";
				array7[4] = new Func<bool>(() => RedeWorld.downedVlitch1);
				array7[5] = 600000;
				mod7.Call(array7);
				Mod mod8 = fargos;
				object[] array8 = new object[6];
				array8[0] = "AddSummon";
				array8[1] = 12.8f;
				array8[2] = "Redemption";
				array8[3] = "CorruptedWormMedallion";
				array8[4] = new Func<bool>(() => RedeWorld.downedVlitch2);
				array8[5] = 600000;
				mod8.Call(array8);
				Mod mod9 = fargos;
				object[] array9 = new object[6];
				array9[0] = "AddSummon";
				array9[1] = 14.05f;
				array9[2] = "Redemption";
				array9[3] = "OmegaRadar";
				array9[4] = new Func<bool>(() => RedeWorld.downedVlitch3);
				array9[5] = 1000000;
				mod9.Call(array9);
				Mod mod10 = fargos;
				object[] array10 = new object[6];
				array10[0] = "AddSummon";
				array10[1] = 14.7f;
				array10[2] = "Redemption";
				array10[3] = "SigilOfThorns";
				array10[4] = new Func<bool>(() => RedeWorld.downedEaglecrestGolemPZ && RedeWorld.downedThornPZ);
				array10[5] = 4000000;
				mod10.Call(array10);
				Mod mod11 = fargos;
				object[] array11 = new object[6];
				array11[0] = "AddSummon";
				array11[1] = 15.5f;
				array11[2] = "Redemption";
				array11[3] = "NebSummon";
				array11[4] = new Func<bool>(() => RedeWorld.downedNebuleus);
				array11[5] = 10000000;
				mod11.Call(array11);
				Mod mod12 = fargos;
				object[] array12 = new object[6];
				array12[0] = "AddEventSummon";
				array12[1] = 1f;
				array12[2] = "Redemption";
				array12[3] = "ChickenContract";
				array12[4] = new Func<bool>(() => RedeWorld.downedChickenInv);
				array12[5] = 20000;
				mod12.Call(array12);
			}
			if (Calamity != null)
			{
				Redemption.calamityLoaded = true;
			}
			if (Thorium != null)
			{
				Redemption.thoriumLoaded = true;
			}
			if (Spirit != null)
			{
				Redemption.spiritLoaded = true;
			}
			if (Fargos != null)
			{
				Redemption.fargoLoaded = true;
			}
			if (GRealm != null)
			{
				Redemption.grealmLoaded = true;
			}
			if (SacredTools != null)
			{
				Redemption.sacredToolsLoaded = true;
			}
			if (Tremor != null)
			{
				Redemption.tremorLoaded = true;
			}
			if (CheatSheet != null)
			{
				Redemption.cheatsheetLoaded = true;
			}
			if (HEROsMod != null)
			{
				Redemption.herosLoaded = true;
			}
			if (AA != null)
			{
				Redemption.AALoaded = true;
			}
			if (musicMod != null)
			{
				Redemption.musicPackLoaded = true;
			}
			if (bossChecklist != null)
			{
				Redemption.checklistLoaded = true;
			}
			if (yabhb != null)
			{
				this.Call(new object[]
				{
					"RegisterHealthBarMini",
					Redemption.Inst.NPCType("SunkenCaptain")
				});
				this.Call(new object[]
				{
					"RegisterHealthBarMini",
					Redemption.Inst.NPCType("StrangePortal")
				});
				this.Call(new object[]
				{
					"RegisterHealthBarMini",
					Redemption.Inst.NPCType("DarkSlime")
				});
				this.Call(new object[]
				{
					"RegisterHealthBarMini",
					Redemption.Inst.NPCType("SkullDigger")
				});
				this.Call(new object[]
				{
					"RegisterHealthBarMini",
					Redemption.Inst.NPCType("TrojanChicken")
				});
				this.Call(new object[]
				{
					"RegisterHealthBarMini",
					Redemption.Inst.NPCType("MansionWraith")
				});
			}
		}

		public override void ModifySunLightColor(ref Color tileColor, ref Color backgroundColor)
		{
			if (RedeWorld.xenoBiome > 0 || RedeWorld.evilXenoBiome > 0 || RedeWorld.evilXenoBiome2 > 0)
			{
				float exampleStrength = (float)RedeWorld.xenoBiome / 200f;
				exampleStrength = Math.Min(exampleStrength, 1f);
				int sunR = (int)backgroundColor.R;
				int sunG = (int)backgroundColor.G;
				int sunB = (int)backgroundColor.B;
				sunR -= (int)(200f * exampleStrength * ((float)backgroundColor.R / 255f));
				sunB -= (int)(200f * exampleStrength * ((float)backgroundColor.B / 255f));
				sunG -= (int)(170f * exampleStrength * ((float)backgroundColor.G / 255f));
				sunR = Utils.Clamp<int>(sunR, 15, 255);
				sunG = Utils.Clamp<int>(sunG, 15, 255);
				sunB = Utils.Clamp<int>(sunB, 15, 255);
				backgroundColor.R = (byte)sunR;
				backgroundColor.G = (byte)sunG;
				backgroundColor.B = (byte)sunB;
			}
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(this);
			modRecipe.AddIngredient(null, "CoastScarabShell", 1);
			modRecipe.AddTile(228);
			modRecipe.SetResult(1044, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(this);
			modRecipe2.AddIngredient(null, "TreeBugShell", 1);
			modRecipe2.AddTile(228);
			modRecipe2.SetResult(1011, 1);
			modRecipe2.AddRecipe();
			ModRecipe modRecipe3 = new ModRecipe(this);
			modRecipe3.AddIngredient(23, 40);
			modRecipe3.AddIngredient(null, "BlankGoldCrown", 1);
			modRecipe3.AddTile(26);
			modRecipe3.SetResult(560, 1);
			modRecipe3.AddRecipe();
			ModRecipe modRecipe4 = new ModRecipe(this);
			modRecipe4.AddIngredient(23, 40);
			modRecipe4.AddIngredient(null, "BlankPlatCrown", 1);
			modRecipe4.AddTile(26);
			modRecipe4.SetResult(560, 1);
			modRecipe4.AddRecipe();
			ModRecipe modRecipe5 = new ModRecipe(this);
			modRecipe5.AddIngredient(null, "DruidEmblem", 1);
			modRecipe5.AddIngredient(548, 5);
			modRecipe5.AddIngredient(549, 5);
			modRecipe5.AddIngredient(547, 5);
			modRecipe5.AddTile(114);
			modRecipe5.SetResult(935, 1);
			modRecipe5.AddRecipe();
			ModRecipe modRecipe6 = new ModRecipe(this);
			modRecipe6.AddIngredient(null, "LivingTwig", 10);
			modRecipe6.AddTile(18);
			modRecipe6.SetResult(2196, 1);
			modRecipe6.AddRecipe();
			ModRecipe modRecipe7 = new ModRecipe(this);
			modRecipe7.AddIngredient(null, "LivingTwig", 12);
			modRecipe7.AddTile(18);
			modRecipe7.SetResult(832, 1);
			modRecipe7.AddRecipe();
			ModRecipe modRecipe8 = new ModRecipe(this);
			modRecipe8.AddIngredient(null, "LivingTwig", 12);
			modRecipe8.AddTile(18);
			modRecipe8.SetResult(933, 1);
			modRecipe8.AddRecipe();
			ModRecipe modRecipe9 = new ModRecipe(this);
			modRecipe9.AddIngredient(null, "LivingTwig", 1);
			modRecipe9.AddTile(18);
			modRecipe9.SetResult(3584, 4);
			modRecipe9.AddRecipe();
			ModRecipe modRecipe10 = new ModRecipe(this);
			modRecipe10.AddIngredient(null, "LivingTwig", 1);
			modRecipe10.AddTile(18);
			modRecipe10.SetResult(1723, 4);
			modRecipe10.AddRecipe();
			ModRecipe modRecipe11 = new ModRecipe(this);
			modRecipe11.AddIngredient(null, "LivingTwig", 6);
			modRecipe11.AddTile(18);
			modRecipe11.SetResult(819, 1);
			modRecipe11.AddRecipe();
			ModRecipe modRecipe12 = new ModRecipe(this);
			modRecipe12.AddIngredient(null, "LivingTwig", 4);
			modRecipe12.AddTile(18);
			modRecipe12.SetResult(806, 1);
			modRecipe12.AddRecipe();
			ModRecipe modRecipe13 = new ModRecipe(this);
			modRecipe13.AddIngredient(null, "LivingTwig", 8);
			modRecipe13.AddTile(18);
			modRecipe13.SetResult(829, 1);
			modRecipe13.AddRecipe();
			ModRecipe modRecipe14 = new ModRecipe(this);
			modRecipe14.AddIngredient(null, "LivingTwig", 15);
			modRecipe14.AddIngredient(154, 4);
			modRecipe14.AddIngredient(149, 1);
			modRecipe14.AddTile(18);
			modRecipe14.SetResult(2245, 1);
			modRecipe14.AddRecipe();
			ModRecipe modRecipe15 = new ModRecipe(this);
			modRecipe15.AddIngredient(null, "LivingTwig", 20);
			modRecipe15.AddIngredient(149, 10);
			modRecipe15.AddTile(18);
			modRecipe15.SetResult(2135, 1);
			modRecipe15.AddRecipe();
			ModRecipe modRecipe16 = new ModRecipe(this);
			modRecipe16.AddIngredient(null, "LivingTwig", 15);
			modRecipe16.AddIngredient(225, 5);
			modRecipe16.AddTile(18);
			modRecipe16.SetResult(2139, 1);
			modRecipe16.AddRecipe();
			ModRecipe modRecipe17 = new ModRecipe(this);
			modRecipe17.AddIngredient(null, "LivingTwig", 5);
			modRecipe17.AddIngredient(225, 2);
			modRecipe17.AddTile(18);
			modRecipe17.SetResult(2636, 1);
			modRecipe17.AddRecipe();
			ModRecipe modRecipe18 = new ModRecipe(this);
			modRecipe18.AddIngredient(null, "LivingTwig", 14);
			modRecipe18.AddTile(18);
			modRecipe18.SetResult(2126, 1);
			modRecipe18.AddRecipe();
			ModRecipe modRecipe19 = new ModRecipe(this);
			modRecipe19.AddIngredient(null, "LivingTwig", 6);
			modRecipe19.AddIngredient(8, 1);
			modRecipe19.AddTile(18);
			modRecipe19.SetResult(2145, 1);
			modRecipe19.AddRecipe();
			ModRecipe modRecipe20 = new ModRecipe(this);
			modRecipe20.AddIngredient(null, "LivingTwig", 3);
			modRecipe20.AddIngredient(8, 1);
			modRecipe20.AddTile(18);
			modRecipe20.SetResult(2131, 1);
			modRecipe20.AddRecipe();
			ModRecipe modRecipe21 = new ModRecipe(this);
			modRecipe21.AddIngredient(null, "LivingTwig", 4);
			modRecipe21.AddIngredient(8, 1);
			modRecipe21.AddTile(18);
			modRecipe21.SetResult(2153, 1);
			modRecipe21.AddRecipe();
			ModRecipe modRecipe22 = new ModRecipe(this);
			modRecipe22.AddIngredient(null, "LivingTwig", 4);
			modRecipe22.AddIngredient(8, 4);
			modRecipe22.AddIngredient(85, 1);
			modRecipe22.AddTile(18);
			modRecipe22.SetResult(2141, 1);
			modRecipe22.AddRecipe();
			ModRecipe modRecipe23 = new ModRecipe(this);
			modRecipe23.AddIngredient(null, "LivingTwig", 5);
			modRecipe23.AddIngredient(8, 3);
			modRecipe23.AddTile(18);
			modRecipe23.SetResult(2149, 1);
			modRecipe23.AddRecipe();
			ModRecipe modRecipe24 = new ModRecipe(this);
			modRecipe24.AddIngredient(null, "LivingTwig", 10);
			modRecipe24.AddTile(18);
			modRecipe24.SetResult(2633, 1);
			modRecipe24.AddRecipe();
			ModRecipe modRecipe25 = new ModRecipe(this);
			modRecipe25.AddIngredient(null, "LivingTwig", 1);
			modRecipe25.AddTile(18);
			modRecipe25.SetResult(2629, 2);
			modRecipe25.AddRecipe();
			ModRecipe modRecipe26 = new ModRecipe(this);
			modRecipe26.AddIngredient(null, "LivingTwig", 10);
			modRecipe26.AddIngredient(22, 3);
			modRecipe26.AddIngredient(170, 6);
			modRecipe26.AddTile(18);
			modRecipe26.SetResult(2596, 1);
			modRecipe26.AddRecipe();
			ModRecipe modRecipe27 = new ModRecipe(this);
			modRecipe27.AddIngredient(null, "LivingTwig", 10);
			modRecipe27.AddIngredient(704, 3);
			modRecipe27.AddIngredient(170, 6);
			modRecipe27.AddTile(18);
			modRecipe27.SetResult(2596, 1);
			modRecipe27.AddRecipe();
			ModRecipe modRecipe28 = new ModRecipe(this);
			modRecipe28.AddIngredient(null, "LivingTwig", 6);
			modRecipe28.AddIngredient(206, 1);
			modRecipe28.AddTile(18);
			modRecipe28.SetResult(2833, 1);
			modRecipe28.AddRecipe();
			ModRecipe modRecipe29 = new ModRecipe(this);
			modRecipe29.AddIngredient(null, "AncientWood", 12);
			modRecipe29.AddIngredient(23, 25);
			modRecipe29.AddTile(16);
			modRecipe29.SetResult(1309, 1);
			modRecipe29.AddRecipe();
		}

		public ModPacket GetPacket(ModMessageType type, int capacity)
		{
			ModPacket packet = base.GetPacket(capacity + 1);
			packet.Write((byte)type);
			return packet;
		}

		public static ModPacket WriteToPacket(ModPacket packet, byte msg, params object[] param)
		{
			packet.Write(msg);
			foreach (object obj in param)
			{
				object obj2;
				if ((obj2 = obj) is bool)
				{
					bool boolean = (bool)obj2;
					packet.Write(boolean);
				}
				else if ((obj2 = obj) is byte)
				{
					byte @byte = (byte)obj2;
					packet.Write(@byte);
				}
				else if ((obj2 = obj) is int)
				{
					int @int = (int)obj2;
					packet.Write(@int);
				}
				else if ((obj2 = obj) is float)
				{
					float single = (float)obj2;
					packet.Write(single);
				}
			}
			return packet;
		}

		public override void HandlePacket(BinaryReader bb, int whoAmI)
		{
			switch (bb.ReadByte())
			{
			case 0:
				if (Main.netMode == 2)
				{
					int bossType = bb.ReadInt32();
					int npcCenterX = bb.ReadInt32();
					int npcCenterY = bb.ReadInt32();
					if (NPC.AnyNPCs(bossType))
					{
						return;
					}
					int npcID = NPC.NewNPC(npcCenterX, npcCenterY, bossType, 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[npcID].Center = new Vector2((float)npcCenterX, (float)npcCenterY);
					Main.npc[npcID].netUpdate2 = true;
					NetMessage.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasAwoken", new object[]
					{
						Main.npc[npcID].GetTypeNetName()
					}), new Color(175, 75, 255), -1);
					return;
				}
				break;
			case 1:
				if (Main.netMode == 2)
				{
					int NPCType = bb.ReadInt32();
					int npcCenterX2 = bb.ReadInt32();
					int npcCenterY2 = bb.ReadInt32();
					if (NPC.AnyNPCs(NPCType))
					{
						return;
					}
					int npcID2 = NPC.NewNPC(npcCenterX2, npcCenterY2, NPCType, 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[npcID2].Center = new Vector2((float)npcCenterX2, (float)npcCenterY2);
					Main.npc[npcID2].netUpdate2 = true;
					return;
				}
				break;
			case 2:
				if (Main.netMode == 2)
				{
					int NPCType2 = bb.ReadInt32();
					int npcCenterX3 = bb.ReadInt32();
					int npcCenterY3 = bb.ReadInt32();
					int npcID3 = NPC.NewNPC(npcCenterX3, npcCenterY3, NPCType2, 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[npcID3].Center = new Vector2((float)npcCenterX3, (float)npcCenterY3);
					Main.npc[npcID3].netUpdate2 = true;
					return;
				}
				break;
			case 3:
			{
				byte player = bb.ReadByte();
				DashType dash = (DashType)bb.ReadByte();
				sbyte dir = bb.ReadSByte();
				if (Main.netMode == 2)
				{
					ModPacket packet = this.GetPacket(ModMessageType.Dash, 3);
					packet.Write(player);
					packet.Write((byte)dash);
					packet.Write(dir);
					packet.Send(-1, whoAmI);
				}
				Main.player[(int)player].GetModPlayer<DashPlayer>().PerformDash(dash, dir, false);
				return;
			}
			case 4:
				ChickWorld.chickArmy = true;
				ChickWorld.ChickArmyStart();
				return;
			case 5:
				ChickWorld.HandlePacket(bb);
				break;
			default:
				return;
			}
		}

		public static void SpawnBoss(Player player, string type, bool spawnMessage = true, int overrideDirection = 0, int overrideDirectionY = 0, string overrideDisplayName = "", bool namePlural = false)
		{
			Mod mod = Redemption.Inst;
			Redemption.SpawnBoss(player, mod.NPCType(type), spawnMessage, overrideDirection, overrideDirectionY, overrideDisplayName, namePlural);
		}

		public static void SpawnBoss(Player player, int bossType, bool spawnMessage = true, int overrideDirection = 0, int overrideDirectionY = 0, string overrideDisplayName = "", bool namePlural = false)
		{
			if (overrideDirection == 0)
			{
				overrideDirection = ((Main.rand.Next(2) == 0) ? -1 : 1);
			}
			if (overrideDirectionY == 0)
			{
				overrideDirectionY = -1;
			}
			Vector2 npcCenter = player.Center + new Vector2(MathHelper.Lerp(500f, 800f, (float)Main.rand.NextDouble()) * (float)overrideDirection, 800f * (float)overrideDirectionY);
			Redemption.SpawnBoss(player, bossType, spawnMessage, npcCenter, overrideDisplayName, namePlural);
		}

		public static void SpawnBoss(Player player, string type, bool spawnMessage = true, Vector2 npcCenter = default(Vector2), string overrideDisplayName = "", bool namePlural = false)
		{
			Mod mod = Redemption.Inst;
			Redemption.SpawnBoss(player, mod.NPCType(type), spawnMessage, npcCenter, overrideDisplayName, namePlural);
		}

		public static void SpawnBoss(Player player, int bossType, bool spawnMessage = true, Vector2 npcCenter = default(Vector2), string overrideDisplayName = "", bool namePlural = false)
		{
			if (npcCenter == default(Vector2))
			{
				npcCenter = player.Center;
			}
			if (Main.netMode != 1)
			{
				if (NPC.AnyNPCs(bossType))
				{
					return;
				}
				int npcID = NPC.NewNPC((int)npcCenter.X, (int)npcCenter.Y, bossType, 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[npcID].Center = npcCenter;
				Main.npc[npcID].netUpdate2 = true;
				if (spawnMessage)
				{
					string npcName = (!string.IsNullOrEmpty(Main.npc[npcID].GivenName)) ? Main.npc[npcID].GivenName : overrideDisplayName;
					if ((npcName == null || npcName.Equals("")) && Main.npc[npcID].modNPC != null)
					{
						npcName = Main.npc[npcID].modNPC.DisplayName.GetDefault();
					}
					if (namePlural)
					{
						if (Main.netMode == 0)
						{
							if (Main.netMode != 1)
							{
								BaseUtility.Chat(npcName + " have awoken!", 175, 75, byte.MaxValue, false);
								return;
							}
						}
						else if (Main.netMode == 2)
						{
							NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(npcName + " have awoken!"), new Color(175, 75, 255), -1);
							return;
						}
					}
					else if (Main.netMode == 0)
					{
						if (Main.netMode != 1)
						{
							BaseUtility.Chat(Language.GetTextValue("Announcement.HasAwoken", npcName), 175, 75, byte.MaxValue, false);
							return;
						}
					}
					else if (Main.netMode == 2)
					{
						NetMessage.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasAwoken", new object[]
						{
							NetworkText.FromLiteral(npcName)
						}), new Color(175, 75, 255), -1);
						return;
					}
				}
			}
			else
			{
				RedeNet.SendNetMessage(0, new object[]
				{
					(byte)player.whoAmI,
					(short)bossType,
					spawnMessage,
					(int)npcCenter.X,
					(int)npcCenter.Y,
					overrideDisplayName,
					namePlural
				});
			}
		}

		public const string Abbreviation = "MoR";

		public const string EMPTY_TEXTURE = "Redemption/Empty";

		public const string customEventName = "Chicken Army";

		public static bool cachedata;

		public static int FaceCustomCurrencyID;

		public static bool GirusSilence;

		public UserInterface TitleUILayer;

		public TitleCard TitleCardUIElement;

		public UserInterface DialogueUILayer;

		public MoRDialogueUI DialogueUIElement;

		public UserInterface NukeUILayer;

		public NukeDetonationUI NukeUIElement;

		public UserInterface AMemoryUILayer;

		public AMemoryUIState AMemoryUIElement;

		public UserInterface StoneTabletUILayer;

		public Vector2 cameraOffset;

		public Rectangle currentScreen;

		public static TrailManager TrailManager;

		private UserInterface GeneratorMenuUI;

		internal ManualGeneratorMenu GeneratorMenu;

		private UserInterface ChestMenuUI;

		internal ChestCustomizerState ChestCustomizer;

		private List<ILoadable> _loadCache;

		public static bool fargoLoaded;

		public static bool calamityLoaded;

		public static bool grealmLoaded;

		public static bool sacredToolsLoaded;

		public static bool spiritLoaded;

		public static bool thoriumLoaded;

		public static bool tremorLoaded;

		public static bool cheatsheetLoaded;

		public static bool herosLoaded;

		public static bool AALoaded;

		public static bool GRealmLoaded;

		public static bool musicPackLoaded;

		public static bool checklistLoaded;
	}
}
