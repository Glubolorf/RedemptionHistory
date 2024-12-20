using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.ChickenArmy;
using Redemption.Items;
using Redemption.Items.Armor.Costumes;
using Redemption.Items.Cores;
using Redemption.NPCs.Bosses.Nebuleus;
using Terraria;
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
		public static Texture2D EmptyTexture { get; private set; }

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
			Player localPlayer = Main.LocalPlayer;
			if (!localPlayer.active)
			{
				return;
			}
			if (Main.myPlayer != -1 && !Main.gameMenu)
			{
				if (ChickWorld.chickArmy)
				{
					if (RedeWorld.downedPatientZero)
					{
						music = base.GetSoundSlot(51, "Sounds/Music/ChickenInvasion1");
						priority = 5;
					}
					else
					{
						music = base.GetSoundSlot(51, "Sounds/Music/BossKingChicken");
						priority = 5;
					}
				}
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
				base.ItemType("DeathsGraspBag"),
				base.ItemType("GloomShroomBag"),
				base.ItemType("GlowingMBag"),
				base.ItemType("GoldenOrangeBag"),
				base.ItemType("HealShroomBag"),
				base.ItemType("NightshadeSeedbag"),
				base.ItemType("BloodRootSeedbag"),
				base.ItemType("ViciousShroomBag"),
				base.ItemType("VileShroomBag")
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
			RecipeGroup recipeGroup7 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Xenium Autoturret", new int[]
			{
				base.ItemType("XeniumDrone1"),
				base.ItemType("XeniumDrone2"),
				base.ItemType("XeniumDrone3"),
				base.ItemType("XeniumDrone4"),
				base.ItemType("XeniumDrone5"),
				base.ItemType("XeniumDrone6"),
				base.ItemType("XeniumDrone7"),
				base.ItemType("XeniumDrone8"),
				base.ItemType("XeniumDrone9"),
				base.ItemType("XeniumDrone10"),
				base.ItemType("XeniumDrone11"),
				base.ItemType("XeniumDrone12")
			});
			RecipeGroup.RegisterGroup("Redemption:XeniumTurret", recipeGroup7);
			RecipeGroup recipeGroup8 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Chicken", new int[]
			{
				base.ItemType("ChickenItem"),
				base.ItemType("ChickenLeghornItem"),
				base.ItemType("ChickenRedItem")
			});
			RecipeGroup.RegisterGroup("Redemption:Chicken", recipeGroup8);
			if (RecipeGroup.recipeGroupIDs.ContainsKey("Wood"))
			{
				int key = RecipeGroup.recipeGroupIDs["Wood"];
				RecipeGroup recipeGroup9 = RecipeGroup.recipeGroups[key];
				recipeGroup9.ValidItems.Add(base.ItemType("DeadWood"));
				recipeGroup9.ValidItems.Add(base.ItemType("AncientWood"));
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
				Redemption.EmptyTexture = base.GetTexture("Empty");
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
				base.AddEquipTexture(new HEVHead(), null, 0, "HEVHead", "Redemption/Items/Armor/Costumes/HEVSuit_Head", "", "");
				base.AddEquipTexture(new HEVBody(), null, 1, "HEVBody", "Redemption/Items/Armor/Costumes/HEVSuit_Body", "Redemption/Items/Armor/Costumes/HEVSuit_Arms", "");
				base.AddEquipTexture(new HEVLegs(), null, 2, "HEVLegs", "Redemption/Items/Armor/Costumes/HEVSuit_Legs", "", "");
				base.AddMusicBox(base.GetSoundSlot(51, "Sounds/Music/XenoCaves"), base.ItemType("WastelandBox"), base.TileType("WastelandBoxTile"), 0);
				base.AddMusicBox(base.GetSoundSlot(51, "Sounds/Music/BossXeno1"), base.ItemType("XenomiteCrystalBox"), base.TileType("XenomiteCrystalBoxTile"), 0);
				base.AddMusicBox(base.GetSoundSlot(51, "Sounds/Music/BossXeno2"), base.ItemType("InfectedEyeBox"), base.TileType("InfectedEyeBoxTile"), 0);
				base.AddMusicBox(base.GetSoundSlot(51, "Sounds/Music/BossSlayer"), base.ItemType("KSBox"), base.TileType("KSBoxTile"), 0);
				base.AddMusicBox(base.GetSoundSlot(51, "Sounds/Music/BossVlitch1"), base.ItemType("VlitchBox"), base.TileType("VlitchBoxTile"), 0);
				base.AddMusicBox(base.GetSoundSlot(51, "Sounds/Music/BossKeeper"), base.ItemType("KeeperBox"), base.TileType("KeeperBoxTile"), 0);
				base.AddMusicBox(base.GetSoundSlot(51, "Sounds/Music/BossVlitch2"), base.ItemType("VlitchBox2"), base.TileType("VlitchBoxTile2"), 0);
				base.AddMusicBox(base.GetSoundSlot(51, "Sounds/Music/LabMusic"), base.ItemType("LabMusicBox"), base.TileType("LabMusicBoxTile"), 0);
				base.AddMusicBox(base.GetSoundSlot(51, "Sounds/Music/LabBossMusic"), base.ItemType("LabBossMusicBox"), base.TileType("LabBossMusicBoxTile"), 0);
				base.AddMusicBox(base.GetSoundSlot(51, "Sounds/Music/LabBossMusic2"), base.ItemType("PZMusicBox"), base.TileType("PZMusicBoxTile"), 0);
				base.AddMusicBox(base.GetSoundSlot(51, "Sounds/Music/HallofHeroes"), base.ItemType("HallOfHeroesBox"), base.TileType("HallOfHeroesBoxTile"), 0);
				base.AddMusicBox(base.GetSoundSlot(51, "Sounds/Music/BossStarGod1"), base.ItemType("NebBox"), base.TileType("NebBoxTile"), 0);
				base.AddMusicBox(base.GetSoundSlot(51, "Sounds/Music/BossStarGod2"), base.ItemType("NebBox2"), base.TileType("NebBox2Tile"), 0);
				base.AddMusicBox(base.GetSoundSlot(51, "Sounds/Music/BossForest1"), base.ItemType("ForestBossBox"), base.TileType("ForestBossBoxTile"), 0);
				Redemption.PremultiplyTexture(base.GetTexture("Backgrounds/fog"));
				Filters.Scene["Redemption:Nebuleus"] = new Filter(new StarGodSkyData("FilterMiniTower").UseColor(0.3f, 0f, 0.4f).UseOpacity(0.5f), 4);
				SkyManager.Instance["Redemption:Nebuleus"] = new StarGodSky();
				Filters.Scene["Redemption:BigNebuleus"] = new Filter(new StarGodSkyData2("FilterMiniTower").UseColor(0.3f, 0f, 0.4f).UseOpacity(0.5f), 4);
				SkyManager.Instance["Redemption:BigNebuleus"] = new StarGodSky2();
				StarGodSky.SkyTex = base.GetTexture("NPCs/Bosses/Nebuleus/StarGodSky");
				StarGodSky2.SkyTex = base.GetTexture("NPCs/Bosses/Nebuleus/StarGodSky2");
			}
			Redemption.FaceCustomCurrencyID = CustomCurrencyManager.RegisterCurrency(new CustomCurrency(base.ItemType<AncientGoldCoin>(), 999L));
			Filters.Scene["Redemption:XenoSky"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0f, 0.2f, 0f).UseOpacity(0.4f), 3);
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
			modTranslation.SetDefault("Laser Systems #7 has been deactivated...");
			base.AddTranslation(modTranslation);
			modTranslation = base.CreateTranslation("PatientZeroMessage1");
			modTranslation.SetDefault("As the Infection halts, strong creatures of Epidotra return...");
			base.AddTranslation(modTranslation);
			modTranslation = base.CreateTranslation("PatientZeroMessage2");
			modTranslation.SetDefault("Cursed rocks of crimson form in the ash of the Underworld...");
			base.AddTranslation(modTranslation);
			modTranslation = base.CreateTranslation("PatientZeroMessage3");
			modTranslation.SetDefault("Cosmic creatures fill the skies...");
			base.AddTranslation(modTranslation);
			modTranslation = base.CreateTranslation("PatientZeroMessage4");
			modTranslation.SetDefault("The forest's curse awakens...");
			base.AddTranslation(modTranslation);
			modTranslation = base.CreateTranslation("PatientZeroMessage5");
			modTranslation.SetDefault("Beings of the Ancient Times roam the caverns...");
			base.AddTranslation(modTranslation);
		}

		public override void Unload()
		{
			this.CleanupStaticArrays();
			Redemption.inst = null;
		}

		public static void PremultiplyTexture(Texture2D texture)
		{
			Color[] array = new Color[texture.Width * texture.Height];
			texture.GetData<Color>(array);
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Color.FromNonPremultiplied((int)array[i].R, (int)array[i].G, (int)array[i].B, (int)array[i].A);
			}
			texture.SetData<Color>(array);
		}

		public void CleanupStaticArrays()
		{
			if (Main.netMode != 2)
			{
				Redemption.precachedTextures.Clear();
				StarGodSky.SkyTex = null;
				StarGodSky2.SkyTex = null;
			}
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			Main.player[Main.myPlayer].GetModPlayer<ChickPlayer>();
			if (ChickWorld.chickArmy)
			{
				int num = layers.FindIndex((GameInterfaceLayer layer) => layer.Name.Equals("Vanilla: Inventory"));
				if (num >= 0)
				{
					LegacyGameInterfaceLayer item = new LegacyGameInterfaceLayer("Redemption: ChickBenis", delegate()
					{
						this.DrawChick(Main.spriteBatch);
						return true;
					}, 1);
					layers.Insert(num, item);
				}
			}
		}

		public void DrawChick(SpriteBatch spriteBatch)
		{
			if (ChickWorld.chickArmy)
			{
				if (RedeWorld.downedPatientZero)
				{
					float num = 0.5f;
					Texture2D colorBarTexture = Main.colorBarTexture;
					Texture2D colorBarTexture2 = Main.colorBarTexture;
					Texture2D texture = Redemption.inst.GetTexture("Effects/InvasionIcons/ChickArmy_Icon");
					float num2 = 0.875f;
					Color color;
					color..ctor(77, 39, 135);
					Color color2;
					color2..ctor(255, 241, 51);
					new Color(255, 241, 51);
					int num3 = (int)(200f * num2);
					int num4 = (int)(46f * num2);
					Rectangle rectangle = Utils.CenteredRectangle(new Vector2((float)(Main.screenWidth - 20) - 100f, (float)(Main.screenHeight - 20) - 23f), new Vector2((float)num3, (float)num4));
					Utils.DrawInvBG(spriteBatch, rectangle, new Color(63, 65, 151, 255) * 0.785f);
					float num5 = (float)ChickWorld.ChickPoints2 / 180f;
					string text = "Cleared " + Math.Round((double)(100f * num5)) + "%";
					Utils.DrawBorderString(spriteBatch, text, new Vector2((float)(rectangle.X + rectangle.Width / 2), (float)(rectangle.Y + 5)), Color.White, num2 * 0.8f, 0.5f, -0.1f, -1);
					Rectangle rectangle2 = Utils.CenteredRectangle(new Vector2((float)rectangle.X + (float)rectangle.Width * 0.5f, (float)rectangle.Y + (float)rectangle.Height * 0.75f), new Vector2((float)colorBarTexture2.Width, (float)colorBarTexture2.Height));
					Rectangle value;
					value..ctor(0, 0, (int)((float)colorBarTexture2.Width * MathHelper.Clamp(num5, 0f, 1f)), colorBarTexture2.Height);
					Vector2 vector;
					vector..ctor((float)(rectangle2.Width - (int)((float)rectangle2.Width * num2)) * 0.5f, (float)(rectangle2.Height - (int)((float)rectangle2.Height * num2)) * 0.5f);
					spriteBatch.Draw(colorBarTexture, Utils.ToVector2(rectangle2.Location) + vector, null, Color.White * num, 0f, new Vector2(0f), num2, 0, 0f);
					spriteBatch.Draw(colorBarTexture, Utils.ToVector2(rectangle2.Location) + vector, new Rectangle?(value), color2, 0f, new Vector2(0f), num2, 0, 0f);
					Vector2 vector2 = new Vector2(154f, 40f) * num2;
					Rectangle rectangle3 = Utils.CenteredRectangle(new Vector2((float)(Main.screenWidth - 20) - 100f, (float)(Main.screenHeight - 20) - 19f), new Vector2((float)num3, (float)num4));
					Rectangle rectangle4 = Utils.CenteredRectangle(new Vector2((float)rectangle3.X + (float)rectangle3.Width * 0.5f, (float)(rectangle3.Y - 6) - vector2.Y * 0.5f), vector2 * 0.8f);
					Utils.DrawInvBG(spriteBatch, rectangle4, color * num);
					int num6 = (rectangle4.Height - (int)(32f * num2)) / 2;
					Rectangle rectangle5;
					rectangle5..ctor(rectangle4.X + num6 + 7, rectangle4.Y + num6, (int)(32f * num2), (int)(32f * num2));
					spriteBatch.Draw(texture, rectangle5, Color.White);
					Utils.DrawBorderString(spriteBatch, "Chicken Army", new Vector2((float)rectangle3.X + (float)rectangle3.Width * 0.5f, (float)(rectangle3.Y - 6) - vector2.Y * 0.5f), Color.White, 0.8f, 0.3f, 0.4f, -1);
					return;
				}
				float num7 = 0.5f;
				Texture2D colorBarTexture3 = Main.colorBarTexture;
				Texture2D colorBarTexture4 = Main.colorBarTexture;
				Texture2D texture2 = Redemption.inst.GetTexture("Effects/InvasionIcons/ChickArmy_Icon");
				float num8 = 0.875f;
				Color color3;
				color3..ctor(77, 39, 135);
				Color color4;
				color4..ctor(255, 241, 51);
				new Color(255, 241, 51);
				int num9 = (int)(200f * num8);
				int num10 = (int)(46f * num8);
				Rectangle rectangle6 = Utils.CenteredRectangle(new Vector2((float)(Main.screenWidth - 20) - 100f, (float)(Main.screenHeight - 20) - 23f), new Vector2((float)num9, (float)num10));
				Utils.DrawInvBG(spriteBatch, rectangle6, new Color(63, 65, 151, 255) * 0.785f);
				float num11 = (float)ChickWorld.ChickPoints2 / 80f;
				string text2 = "Cleared " + Math.Round((double)(100f * num11)) + "%";
				Utils.DrawBorderString(spriteBatch, text2, new Vector2((float)(rectangle6.X + rectangle6.Width / 2), (float)(rectangle6.Y + 5)), Color.White, num8 * 0.8f, 0.5f, -0.1f, -1);
				Rectangle rectangle7 = Utils.CenteredRectangle(new Vector2((float)rectangle6.X + (float)rectangle6.Width * 0.5f, (float)rectangle6.Y + (float)rectangle6.Height * 0.75f), new Vector2((float)colorBarTexture4.Width, (float)colorBarTexture4.Height));
				Rectangle value2;
				value2..ctor(0, 0, (int)((float)colorBarTexture4.Width * MathHelper.Clamp(num11, 0f, 1f)), colorBarTexture4.Height);
				Vector2 vector3;
				vector3..ctor((float)(rectangle7.Width - (int)((float)rectangle7.Width * num8)) * 0.5f, (float)(rectangle7.Height - (int)((float)rectangle7.Height * num8)) * 0.5f);
				spriteBatch.Draw(colorBarTexture3, Utils.ToVector2(rectangle7.Location) + vector3, null, Color.White * num7, 0f, new Vector2(0f), num8, 0, 0f);
				spriteBatch.Draw(colorBarTexture3, Utils.ToVector2(rectangle7.Location) + vector3, new Rectangle?(value2), color4, 0f, new Vector2(0f), num8, 0, 0f);
				Vector2 vector4 = new Vector2(154f, 40f) * num8;
				Rectangle rectangle8 = Utils.CenteredRectangle(new Vector2((float)(Main.screenWidth - 20) - 100f, (float)(Main.screenHeight - 20) - 19f), new Vector2((float)num9, (float)num10));
				Rectangle rectangle9 = Utils.CenteredRectangle(new Vector2((float)rectangle8.X + (float)rectangle8.Width * 0.5f, (float)(rectangle8.Y - 6) - vector4.Y * 0.5f), vector4 * 0.8f);
				Utils.DrawInvBG(spriteBatch, rectangle9, color3 * num7);
				int num12 = (rectangle9.Height - (int)(32f * num8)) / 2;
				Rectangle rectangle10;
				rectangle10..ctor(rectangle9.X + num12 + 7, rectangle9.Y + num12, (int)(32f * num8), (int)(32f * num8));
				spriteBatch.Draw(texture2, rectangle10, Color.White);
				Utils.DrawBorderString(spriteBatch, "Chicken Army", new Vector2((float)rectangle8.X + (float)rectangle8.Width * 0.5f, (float)(rectangle8.Y - 6) - vector4.Y * 0.5f), Color.White, 0.8f, 0.3f, 0.4f, -1);
			}
		}

		public override void PostSetupContent()
		{
			Mod mod = ModLoader.GetMod("BossChecklist");
			Mod mod2 = ModLoader.GetMod("FKBossHealthBar");
			Mod mod3 = ModLoader.GetMod("CalamityMod");
			Mod mod4 = ModLoader.GetMod("ThoriumMod");
			Mod mod5 = ModLoader.GetMod("SpiritMod");
			Mod mod6 = ModLoader.GetMod("Fargowiltas");
			Mod mod7 = ModLoader.GetMod("GRealm");
			Mod mod8 = ModLoader.GetMod("SacredTools");
			Mod mod9 = ModLoader.GetMod("Tremor");
			Mod mod10 = ModLoader.GetMod("CheatSheet");
			Mod mod11 = ModLoader.GetMod("HEROsMod");
			Mod mod12 = ModLoader.GetMod("AAMod");
			if (mod3 != null)
			{
				Redemption.calamityLoaded = true;
			}
			if (mod4 != null)
			{
				Redemption.thoriumLoaded = true;
			}
			if (mod5 != null)
			{
				Redemption.spiritLoaded = true;
			}
			if (mod6 != null)
			{
				Redemption.fargoLoaded = true;
			}
			if (mod7 != null)
			{
				Redemption.grealmLoaded = true;
			}
			if (mod8 != null)
			{
				Redemption.sacredToolsLoaded = true;
			}
			if (mod9 != null)
			{
				Redemption.tremorLoaded = true;
			}
			if (mod10 != null)
			{
				Redemption.cheatsheetLoaded = true;
			}
			if (mod11 != null)
			{
				Redemption.herosLoaded = true;
			}
			if (mod12 != null)
			{
				Redemption.AALoaded = true;
			}
			if (mod2 != null)
			{
				this.Call(new object[]
				{
					"RegisterHealthBarMini",
					Redemption.inst.NPCType("SunkenCaptain")
				});
				this.Call(new object[]
				{
					"RegisterHealthBarMini",
					Redemption.inst.NPCType("StrangePortal")
				});
				this.Call(new object[]
				{
					"RegisterHealthBarMini",
					Redemption.inst.NPCType("DarkSlime")
				});
				this.Call(new object[]
				{
					"RegisterHealthBarMini",
					Redemption.inst.NPCType("SkullDigger")
				});
				this.Call(new object[]
				{
					"RegisterHealthBarMini",
					Redemption.inst.NPCType("TrojanChicken")
				});
			}
			if (mod != null)
			{
				Mod mod13 = mod;
				object[] array = new object[5];
				array[0] = "AddMiniBossWithInfo";
				array[1] = "The Mighty King Chicken";
				array[2] = 0f;
				array[3] = new Func<bool>(() => RedeWorld.downedKingChicken);
				array[4] = "Use an [i:" + base.ItemType<EggCrown>() + "] at day";
				mod13.Call(array);
				Mod mod14 = mod;
				object[] array2 = new object[6];
				array2[0] = "AddMiniBossWithInfo";
				array2[1] = "Sunken Captain";
				array2[2] = 0.3f;
				array2[3] = new Func<bool>(() => RedeWorld.downedSunkenCaptain);
				array2[4] = "Wait by the sea on a full moon.";
				array2[5] = new Func<bool>(() => RedeWorld.downedSunkenCaptain);
				mod14.Call(array2);
				Mod mod15 = mod;
				object[] array3 = new object[5];
				array3[0] = "AddEventWithInfo";
				array3[1] = "Chicken Invasion";
				array3[2] = 0.5f;
				array3[3] = new Func<bool>(() => RedeWorld.downedChickenInvPZ);
				array3[4] = "Use a [i:" + base.ItemType<ChickenContract>() + "] at day.";
				mod15.Call(array3);
				Mod mod16 = mod;
				object[] array4 = new object[5];
				array4[0] = "AddBossWithInfo";
				array4[1] = "Thorn, Bane of the Forest";
				array4[2] = 1.5f;
				array4[3] = new Func<bool>(() => RedeWorld.downedThorn);
				array4[4] = "Use a [i:" + base.ItemType<HeartOfTheThorns>() + "] at day";
				mod16.Call(array4);
				Mod mod17 = mod;
				object[] array5 = new object[5];
				array5[0] = "AddBossWithInfo";
				array5[1] = "The Keeper";
				array5[2] = 2.4f;
				array5[3] = new Func<bool>(() => RedeWorld.downedTheKeeper);
				array5[4] = string.Concat(new object[]
				{
					"Use a [i:",
					base.ItemType<MysteriousTabletCorrupt>(),
					"] or [i:",
					base.ItemType<MysteriousTabletCrimson>(),
					"] at night"
				});
				mod17.Call(array5);
				Mod mod18 = mod;
				object[] array6 = new object[6];
				array6[0] = "AddMiniBossWithInfo";
				array6[1] = "Skull Digger";
				array6[2] = 2.41f;
				array6[3] = new Func<bool>(() => RedeWorld.downedSkullDigger);
				array6[4] = "Roams the caverns, seeking revenge...";
				array6[5] = new Func<bool>(() => RedeWorld.downedSkullDigger);
				mod18.Call(array6);
				Mod mod19 = mod;
				object[] array7 = new object[5];
				array7[0] = "AddMiniBossWithInfo";
				array7[1] = "Strange Portal";
				array7[2] = 3.48f;
				array7[3] = new Func<bool>(() => RedeWorld.downedStrangePortal);
				array7[4] = "Use an [i:" + base.ItemType<UnstableCrystal>() + "]";
				mod19.Call(array7);
				Mod mod20 = mod;
				object[] array8 = new object[5];
				array8[0] = "AddBossWithInfo";
				array8[1] = "Xenomite Crystal";
				array8[2] = 3.481f;
				array8[3] = new Func<bool>(() => RedeWorld.downedXenomiteCrystal);
				array8[4] = "Use a [i:" + base.ItemType<GeigerCounter>() + "], dropped by the Strange Portal. Begins the Infection storyline";
				mod20.Call(array8);
				Mod mod21 = mod;
				object[] array9 = new object[5];
				array9[0] = "AddMiniBossWithInfo";
				array9[1] = "Eaglecrest Golem";
				array9[2] = 4.1f;
				array9[3] = new Func<bool>(() => RedeWorld.downedEaglecrestGolem);
				array9[4] = "Naturally spawns at day after Eater of Worlds/Brain of Cthulhu is defeated";
				mod21.Call(array9);
				Mod mod22 = mod;
				object[] array10 = new object[6];
				array10[0] = "AddBossWithInfo";
				array10[1] = "Infected Eye";
				array10[2] = 6.25f;
				array10[3] = new Func<bool>(() => RedeWorld.downedInfectedEye);
				array10[4] = "Use a [i:" + base.ItemType<XenoEye>() + "] at night, requires the Xenomite Crystal to be defeated";
				array10[5] = new Func<bool>(() => RedeWorld.downedXenomiteCrystal);
				mod22.Call(array10);
				Mod mod23 = mod;
				object[] array11 = new object[5];
				array11[0] = "AddBossWithInfo";
				array11[1] = "The Abandoned Lab";
				array11[2] = 9.1f;
				array11[3] = new Func<bool>(() => RedeWorld.downedIBehemoth);
				array11[4] = "Find the Abandoned Lab far below the surface, defeat the first 3 minibosses within. Requires all mech bosses to be defeated. [i:" + base.ItemType<LabHelpMessage>() + "]";
				mod23.Call(array11);
				Mod mod24 = mod;
				object[] array12 = new object[5];
				array12[0] = "AddBossWithInfo";
				array12[1] = "King Slayer III";
				array12[2] = 9.99999f;
				array12[3] = new Func<bool>(() => RedeWorld.downedSlayer);
				array12[4] = "Use a [i:" + base.ItemType<KingSummon>() + "] at day. (Although I would recommend fighting at a later point in the game)";
				mod24.Call(array12);
				Mod mod25 = mod;
				object[] array13 = new object[6];
				array13[0] = "AddBossWithInfo";
				array13[1] = "1st Vlitch Overlord";
				array13[2] = 11.5f;
				array13[3] = new Func<bool>(() => RedeWorld.downedVlitch1);
				array13[4] = "Use a [i:" + base.ItemType<CorruptedHeroSword>() + "] at night";
				array13[5] = new Func<bool>(() => RedeWorld.downedXenomiteCrystal);
				mod25.Call(array13);
				Mod mod26 = mod;
				object[] array14 = new object[6];
				array14[0] = "AddBossWithInfo";
				array14[1] = "2nd Vlitch Overlord";
				array14[2] = 13.5f;
				array14[3] = new Func<bool>(() => RedeWorld.downedVlitch2);
				array14[4] = "Use a [i:" + base.ItemType<CorruptedWormMedallion>() + "] at night";
				array14[5] = new Func<bool>(() => RedeWorld.downedXenomiteCrystal);
				mod26.Call(array14);
				Mod mod27 = mod;
				object[] array15 = new object[6];
				array15[0] = "AddBossWithInfo";
				array15[1] = "3rd Vlitch Overlord";
				array15[2] = 14.05f;
				array15[3] = new Func<bool>(() => RedeWorld.downedVlitch3);
				array15[4] = "Use an [i:" + base.ItemType<OmegaRadar>() + "] at night";
				array15[5] = new Func<bool>(() => RedeWorld.downedXenomiteCrystal);
				mod27.Call(array15);
				Mod mod28 = mod;
				object[] array16 = new object[5];
				array16[0] = "AddBossWithInfo";
				array16[1] = "Patient Zero";
				array16[2] = 14.5f;
				array16[3] = new Func<bool>(() => RedeWorld.downedPatientZero);
				array16[4] = "Use a lunar pickaxe to mine the hardened sludge in the Abandoned Lab to explore further. Beware what awaits beyond.";
				mod28.Call(array16);
				Mod mod29 = mod;
				object[] array17 = new object[5];
				array17[0] = "AddEventWithInfo";
				array17[1] = "King Chicken's Royal Army";
				array17[2] = 14.55f;
				array17[3] = new Func<bool>(() => RedeWorld.downedChickenInvPZ);
				array17[4] = "Use a [i:" + base.ItemType<ChickenContract>() + "] at day after Patient Zero is defeated.";
				mod29.Call(array17);
				Mod mod30 = mod;
				object[] array18 = new object[5];
				array18[0] = "AddBossWithInfo";
				array18[1] = "Thorn & Eaglecrest Rematch";
				array18[2] = 14.7f;
				array18[3] = new Func<bool>(() => RedeWorld.downedThornPZ && RedeWorld.downedEaglecrestGolemPZ);
				array18[4] = string.Concat(new object[]
				{
					"Use an [i:",
					base.ItemType<AncientSigil>(),
					"] & [i:",
					base.ItemType<LifeFruitOfThorns>(),
					"] at day."
				});
				mod30.Call(array18);
				Mod mod31 = mod;
				object[] array19 = new object[5];
				array19[0] = "AddBossWithInfo";
				array19[1] = "Nebuleus, Angel of the Cosmos";
				array19[2] = 15.5f;
				array19[3] = new Func<bool>(() => RedeWorld.downedNebuleus);
				array19[4] = "Use an [i:" + base.ItemType<NebSummon>() + "] at night";
				mod31.Call(array19);
			}
			Mod mod32 = ModLoader.GetMod("Census");
			if (mod32 != null)
			{
				mod32.Call(new object[]
				{
					"TownNPCCondition",
					base.NPCType("Squire"),
					"Have a suitable house"
				});
				mod32.Call(new object[]
				{
					"TownNPCCondition",
					base.NPCType("Fallen"),
					"Defeat the Keeper and have a suitable house"
				});
				mod32.Call(new object[]
				{
					"TownNPCCondition",
					base.NPCType("Newb"),
					"Find the Suspicious Dirt Pile in the caverns"
				});
				mod32.Call(new object[]
				{
					"TownNPCCondition",
					base.NPCType("TBot"),
					"Defeat the Infected Eye and have a suitable house"
				});
				mod32.Call(new object[]
				{
					"TownNPCCondition",
					base.NPCType("Slicer"),
					"Defeat the Dark Slime miniboss, which spawns very rarely on the surface after all mech bosses are defeated"
				});
				mod32.Call(new object[]
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
			switch (bb.ReadByte())
			{
			case 0:
			{
				Vector2 vector = Utils.ReadVector2(bb);
				NPC.NewNPC((int)vector.X, (int)vector.Y, base.NPCType("Stage2ScientistBoss"), 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			case 1:
			{
				Vector2 vector2 = Utils.ReadVector2(bb);
				NPC.NewNPC((int)vector2.X, (int)vector2.Y, base.NPCType("Stage3Scientist"), 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			case 2:
			{
				Vector2 vector3 = Utils.ReadVector2(bb);
				NPC.NewNPC((int)vector3.X, (int)vector3.Y, base.NPCType("IrradiatedBehemoth"), 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			case 3:
			{
				Vector2 vector4 = Utils.ReadVector2(bb);
				NPC.NewNPC((int)vector4.X, (int)vector4.Y, base.NPCType("Blisterface"), 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			case 4:
			{
				Vector2 vector5 = Utils.ReadVector2(bb);
				NPC.NewNPC((int)vector5.X, (int)vector5.Y, base.NPCType("TBotHolo"), 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			case 5:
			{
				Vector2 vector6 = Utils.ReadVector2(bb);
				NPC.NewNPC((int)vector6.X, (int)vector6.Y, base.NPCType("MACEProjectHead"), 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			case 6:
			{
				Vector2 vector7 = Utils.ReadVector2(bb);
				NPC.NewNPC((int)vector7.X, (int)vector7.Y, base.NPCType("PatientZero"), 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			}
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

		public static void SpawnBoss(Player player, string type, bool spawnMessage = true, int overrideDirection = 0, int overrideDirectionY = 0, string overrideDisplayName = "", bool namePlural = false)
		{
			Mod mod = Redemption.inst;
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
			Mod mod = Redemption.inst;
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
				int num = NPC.NewNPC((int)npcCenter.X, (int)npcCenter.Y, bossType, 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[num].Center = npcCenter;
				Main.npc[num].netUpdate = true;
				if (spawnMessage)
				{
					string text = (!string.IsNullOrEmpty(Main.npc[num].GivenName)) ? Main.npc[num].GivenName : overrideDisplayName;
					if ((text == null || text.Equals("")) && Main.npc[num].modNPC != null)
					{
						text = Main.npc[num].modNPC.DisplayName.GetDefault();
					}
					if (namePlural)
					{
						if (Main.netMode == 0)
						{
							Main.NewText(text + " have awoken!", 175, 75, byte.MaxValue, false);
							return;
						}
						if (Main.netMode == 2)
						{
							NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(text + " have awoken!"), new Color(175, 75, 255), -1);
							return;
						}
					}
					else
					{
						if (Main.netMode == 0)
						{
							Main.NewText(Language.GetTextValue("Announcement.HasAwoken", text), 175, 75, byte.MaxValue, false);
							return;
						}
						if (Main.netMode == 2)
						{
							NetMessage.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasAwoken", new object[]
							{
								NetworkText.FromLiteral(text)
							}), new Color(175, 75, 255), -1);
							return;
						}
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

		public const string EMPTY_TEXTURE = "Redemption/Empty";

		public const string customEventName = "Chicken Army";

		public static int customEvent;

		public static int FaceCustomCurrencyID;

		public static bool GirusSilence;

		public static Redemption inst = null;

		public static bool templeOfHeroes;

		public static IDictionary<string, Texture2D> Textures = null;

		public static Dictionary<string, Texture2D> precachedTextures = new Dictionary<string, Texture2D>();

		public static bool fargoLoaded = false;

		public static bool calamityLoaded = false;

		public static bool grealmLoaded = false;

		public static bool sacredToolsLoaded = false;

		public static bool spiritLoaded = false;

		public static bool thoriumLoaded = false;

		public static bool tremorLoaded = false;

		public static bool cheatsheetLoaded = false;

		public static bool herosLoaded = false;

		public static bool AALoaded = false;

		public static bool GRealmLoaded = false;
	}
}
