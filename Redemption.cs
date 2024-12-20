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
			if (!Main.LocalPlayer.active)
			{
				return;
			}
			if (Main.myPlayer != -1 && !Main.gameMenu)
			{
				if (ChickWorld.chickArmy)
				{
					if (RedeWorld.downedPatientZero)
					{
						if (RedeConfigClient.Instance.AntiAntti)
						{
							music = 35;
							priority = 5;
						}
						else
						{
							music = base.GetSoundSlot(51, "Sounds/Music/ChickenInvasion1");
							priority = 5;
						}
					}
					else
					{
						music = base.GetSoundSlot(51, "Sounds/Music/BossKingChicken");
						priority = 5;
					}
				}
				if (Main.player[Main.myPlayer].active && (Main.player[Main.myPlayer].GetModPlayer<RedePlayer>().ZoneXeno || Main.player[Main.myPlayer].GetModPlayer<RedePlayer>().ZoneEvilXeno))
				{
					music = base.GetSoundSlot(51, "Sounds/Music/XenoCaves");
					priority = 3;
				}
				if (Main.player[Main.myPlayer].active && Main.player[Main.myPlayer].GetModPlayer<RedePlayer>().ZoneLab)
				{
					music = base.GetSoundSlot(51, "Sounds/Music/LabMusic");
					priority = 3;
				}
				if (Main.player[Main.myPlayer].active && Main.player[Main.myPlayer].GetModPlayer<RedePlayer>().ZoneSlayer && RedeWorld.downedSlayer)
				{
					music = base.GetSoundSlot(51, "Sounds/Music/SlayerShipMusic");
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
				base.ItemType("Mk1Capacitator"),
				base.ItemType("Mk2Capacitator"),
				base.ItemType("Mk3Capacitator")
			});
			RecipeGroup.RegisterGroup("Redemption:Capacitators", group5);
			RecipeGroup group6 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Plating", new int[]
			{
				base.ItemType("Mk1Plating"),
				base.ItemType("Mk2Plating"),
				base.ItemType("Mk3Plating")
			});
			RecipeGroup.RegisterGroup("Redemption:Plating", group6);
			RecipeGroup group7 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Xenium Autoturret", new int[]
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
			RecipeGroup.RegisterGroup("Redemption:XeniumTurret", group7);
			RecipeGroup group8 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Chicken", new int[]
			{
				base.ItemType("ChickenItem"),
				base.ItemType("ChickenLeghornItem"),
				base.ItemType("ChickenRedItem")
			});
			RecipeGroup.RegisterGroup("Redemption:Chicken", group8);
			if (RecipeGroup.recipeGroupIDs.ContainsKey("Wood"))
			{
				int index = RecipeGroup.recipeGroupIDs["Wood"];
				RecipeGroup recipeGroup = RecipeGroup.recipeGroups[index];
				recipeGroup.ValidItems.Add(base.ItemType("DeadWood"));
				recipeGroup.ValidItems.Add(base.ItemType("AncientWood"));
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
				base.AddEquipTexture(null, 2, "NebuleusVanity_Legs", "Redemption/Items/Armor/PostML/NebuleusVanity_Legs", "", "");
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
				base.AddMusicBox(base.GetSoundSlot(51, "Sounds/Music/ChickenInvasion1"), base.ItemType("ChickenInvasionBox"), base.TileType("ChickenInvasionBoxTile"), 0);
				Redemption.PremultiplyTexture(base.GetTexture("Backgrounds/fog"));
				Filters.Scene["Redemption:Nebuleus"] = new Filter(new StarGodSkyData("FilterMiniTower").UseColor(0.3f, 0f, 0.4f).UseOpacity(0.5f), 4);
				SkyManager.Instance["Redemption:Nebuleus"] = new StarGodSky();
				Filters.Scene["Redemption:BigNebuleus"] = new Filter(new StarGodSkyData2("FilterMiniTower").UseColor(0.3f, 0f, 0.4f).UseOpacity(0.5f), 4);
				SkyManager.Instance["Redemption:BigNebuleus"] = new StarGodSky2();
				StarGodSky.SkyTex = ModLoader.GetMod("Redemption").GetTexture("NPCs/Bosses/Nebuleus/StarGodSky");
				StarGodSky2.SkyTex = ModLoader.GetMod("Redemption").GetTexture("NPCs/Bosses/Nebuleus/StarGodSky2");
				Ref<Effect> screenRef = new Ref<Effect>(base.GetEffect("Effects/Shockwave"));
				Filters.Scene["Shockwave"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), 4);
				Filters.Scene["Shockwave"].Load();
			}
			Redemption.FaceCustomCurrencyID = CustomCurrencyManager.RegisterCurrency(new CustomCurrency(ModContent.ItemType<AncientGoldCoin>(), 999L));
			Filters.Scene["Redemption:XenoSky"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0f, 0.2f, 0f).UseOpacity(0.4f), 3);
			ModTranslation text = base.CreateTranslation("DruidicOre");
			text.SetDefault("Druidic energy courses through the world's ore...");
			base.AddTranslation(text);
			text = base.CreateTranslation("DragonLeadMessage");
			text.SetDefault("The caverns are heated with dragon bone...");
			base.AddTranslation(text);
			text = base.CreateTranslation("InfectionMessage1");
			text.SetDefault("Creatures start getting infected...");
			base.AddTranslation(text);
			text = base.CreateTranslation("LabIsSafe");
			text.SetDefault("The lab's defence systems have malfunctioned...");
			base.AddTranslation(text);
			text = base.CreateTranslation("GrowingInfection");
			text.SetDefault("A nuke has been dropped!");
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
			text.SetDefault("Cosmic creatures fill the skies...");
			base.AddTranslation(text);
			text = base.CreateTranslation("PatientZeroMessage4");
			text.SetDefault("The forest's curse awakens...");
			base.AddTranslation(text);
			text = base.CreateTranslation("PatientZeroMessage5");
			text.SetDefault("Beings of the Ancient Times roam the caverns...");
			base.AddTranslation(text);
			text = base.CreateTranslation("GirusHide");
			text.SetDefault("Thought you could hide from me?");
			base.AddTranslation(text);
		}

		public override void Unload()
		{
			this.CleanupStaticArrays();
			Redemption.inst = null;
		}

		public static void PremultiplyTexture(Texture2D texture)
		{
			Color[] buffer = new Color[texture.Width * texture.Height];
			texture.GetData<Color>(buffer);
			for (int i = 0; i < buffer.Length; i++)
			{
				buffer[i] = Color.FromNonPremultiplied((int)buffer[i].R, (int)buffer[i].G, (int)buffer[i].B, (int)buffer[i].A);
			}
			texture.SetData<Color>(buffer);
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
		}

		public void DrawChick(SpriteBatch spriteBatch)
		{
			if (ChickWorld.chickArmy)
			{
				if (RedeWorld.downedPatientZero)
				{
					float alpha = 0.5f;
					Texture2D backGround = Main.colorBarTexture;
					Texture2D progressColor = Main.colorBarTexture;
					Texture2D ChickIcon = Redemption.inst.GetTexture("Effects/InvasionIcons/ChickArmy_Icon");
					float scmp = 0.875f;
					Color descColor = new Color(77, 39, 135);
					Color waveColor = new Color(255, 241, 51);
					new Color(255, 241, 51);
					int width = (int)(200f * scmp);
					int height = (int)(46f * scmp);
					Rectangle waveBackground = Utils.CenteredRectangle(new Vector2((float)(Main.screenWidth - 20) - 100f, (float)(Main.screenHeight - 20) - 23f), new Vector2((float)width, (float)height));
					Utils.DrawInvBG(spriteBatch, waveBackground, new Color(63, 65, 151, 255) * 0.785f);
					float cleared = (float)ChickWorld.ChickPoints2 / 200f;
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
					return;
				}
				float alpha2 = 0.5f;
				Texture2D backGround2 = Main.colorBarTexture;
				Texture2D progressColor2 = Main.colorBarTexture;
				Texture2D ChickIcon2 = Redemption.inst.GetTexture("Effects/InvasionIcons/ChickArmy_Icon");
				float scmp2 = 0.875f;
				Color descColor2 = new Color(77, 39, 135);
				Color waveColor2 = new Color(255, 241, 51);
				new Color(255, 241, 51);
				int width2 = (int)(200f * scmp2);
				int height2 = (int)(46f * scmp2);
				Rectangle waveBackground2 = Utils.CenteredRectangle(new Vector2((float)(Main.screenWidth - 20) - 100f, (float)(Main.screenHeight - 20) - 23f), new Vector2((float)width2, (float)height2));
				Utils.DrawInvBG(spriteBatch, waveBackground2, new Color(63, 65, 151, 255) * 0.785f);
				float cleared2 = (float)ChickWorld.ChickPoints2 / 100f;
				string waveText2 = "Cleared " + Math.Round((double)(100f * cleared2)) + "%";
				Utils.DrawBorderString(spriteBatch, waveText2, new Vector2((float)(waveBackground2.X + waveBackground2.Width / 2), (float)(waveBackground2.Y + 5)), Color.White, scmp2 * 0.8f, 0.5f, -0.1f, -1);
				Rectangle waveProgressBar2 = Utils.CenteredRectangle(new Vector2((float)waveBackground2.X + (float)waveBackground2.Width * 0.5f, (float)waveBackground2.Y + (float)waveBackground2.Height * 0.75f), new Vector2((float)progressColor2.Width, (float)progressColor2.Height));
				Rectangle waveProgressAmount2 = new Rectangle(0, 0, (int)((float)progressColor2.Width * MathHelper.Clamp(cleared2, 0f, 1f)), progressColor2.Height);
				Vector2 offset2 = new Vector2((float)(waveProgressBar2.Width - (int)((float)waveProgressBar2.Width * scmp2)) * 0.5f, (float)(waveProgressBar2.Height - (int)((float)waveProgressBar2.Height * scmp2)) * 0.5f);
				spriteBatch.Draw(backGround2, Utils.ToVector2(waveProgressBar2.Location) + offset2, null, Color.White * alpha2, 0f, new Vector2(0f), scmp2, SpriteEffects.None, 0f);
				spriteBatch.Draw(backGround2, Utils.ToVector2(waveProgressBar2.Location) + offset2, new Rectangle?(waveProgressAmount2), waveColor2, 0f, new Vector2(0f), scmp2, SpriteEffects.None, 0f);
				Vector2 descSize2 = new Vector2(154f, 40f) * scmp2;
				Rectangle barrierBackground2 = Utils.CenteredRectangle(new Vector2((float)(Main.screenWidth - 20) - 100f, (float)(Main.screenHeight - 20) - 19f), new Vector2((float)width2, (float)height2));
				Rectangle descBackground2 = Utils.CenteredRectangle(new Vector2((float)barrierBackground2.X + (float)barrierBackground2.Width * 0.5f, (float)(barrierBackground2.Y - 6) - descSize2.Y * 0.5f), descSize2 * 0.8f);
				Utils.DrawInvBG(spriteBatch, descBackground2, descColor2 * alpha2);
				int descOffset2 = (descBackground2.Height - (int)(32f * scmp2)) / 2;
				Rectangle icon2 = new Rectangle(descBackground2.X + descOffset2 + 7, descBackground2.Y + descOffset2, (int)(32f * scmp2), (int)(32f * scmp2));
				spriteBatch.Draw(ChickIcon2, icon2, Color.White);
				Utils.DrawBorderString(spriteBatch, "Chicken Army", new Vector2((float)barrierBackground2.X + (float)barrierBackground2.Width * 0.5f, (float)(barrierBackground2.Y - 6) - descSize2.Y * 0.5f), Color.White, 0.8f, 0.3f, 0.4f, -1);
			}
		}

		public override void PostSetupContent()
		{
			Mod bossChecklist = ModLoader.GetMod("BossChecklist");
			bool mod = ModLoader.GetMod("FKBossHealthBar") != null;
			bool mod2 = ModLoader.GetMod("CalamityMod") != null;
			Mod Thorium = ModLoader.GetMod("ThoriumMod");
			Mod Spirit = ModLoader.GetMod("SpiritMod");
			Mod Fargos = ModLoader.GetMod("Fargowiltas");
			Mod GRealm = ModLoader.GetMod("GRealm");
			Mod SacredTools = ModLoader.GetMod("SacredTools");
			Mod Tremor = ModLoader.GetMod("Tremor");
			Mod CheatSheet = ModLoader.GetMod("CheatSheet");
			Mod HEROsMod = ModLoader.GetMod("HEROsMod");
			Mod AA = ModLoader.GetMod("AAMod");
			if (mod2)
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
			if (mod)
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
			if (bossChecklist != null)
			{
				Mod mod3 = bossChecklist;
				object[] array = new object[5];
				array[0] = "AddMiniBossWithInfo";
				array[1] = "The Mighty King Chicken";
				array[2] = 0f;
				array[3] = new Func<bool>(() => RedeWorld.downedKingChicken);
				array[4] = "Use an [i:" + ModContent.ItemType<EggCrown>() + "] at day";
				mod3.Call(array);
				Mod mod4 = bossChecklist;
				object[] array2 = new object[6];
				array2[0] = "AddMiniBossWithInfo";
				array2[1] = "Sunken Captain";
				array2[2] = 0.3f;
				array2[3] = new Func<bool>(() => RedeWorld.downedSunkenCaptain);
				array2[4] = "Wait by the sea on a full moon.";
				array2[5] = new Func<bool>(() => RedeWorld.downedSunkenCaptain);
				mod4.Call(array2);
				Mod mod5 = bossChecklist;
				object[] array3 = new object[6];
				array3[0] = "AddEventWithInfo";
				array3[1] = "Chicken Invasion";
				array3[2] = 0.5f;
				array3[3] = new Func<bool>(() => RedeWorld.downedChickenInv);
				array3[4] = "Use a [i:" + ModContent.ItemType<ChickenContract>() + "] at day.";
				array3[5] = new Func<bool>(() => RedeWorld.downedKingChicken);
				mod5.Call(array3);
				Mod mod6 = bossChecklist;
				object[] array4 = new object[5];
				array4[0] = "AddBossWithInfo";
				array4[1] = "Thorn, Bane of the Forest";
				array4[2] = 1.5f;
				array4[3] = new Func<bool>(() => RedeWorld.downedThorn);
				array4[4] = "Use a [i:" + ModContent.ItemType<HeartOfTheThorns>() + "] at day";
				mod6.Call(array4);
				Mod mod7 = bossChecklist;
				object[] array5 = new object[5];
				array5[0] = "AddBossWithInfo";
				array5[1] = "The Keeper";
				array5[2] = 2.4f;
				array5[3] = new Func<bool>(() => RedeWorld.downedTheKeeper);
				array5[4] = string.Concat(new object[]
				{
					"Use a [i:",
					ModContent.ItemType<MysteriousTabletCorrupt>(),
					"] or [i:",
					ModContent.ItemType<MysteriousTabletCrimson>(),
					"] at night"
				});
				mod7.Call(array5);
				Mod mod8 = bossChecklist;
				object[] array6 = new object[6];
				array6[0] = "AddMiniBossWithInfo";
				array6[1] = "Skull Digger";
				array6[2] = 2.41f;
				array6[3] = new Func<bool>(() => RedeWorld.downedSkullDigger);
				array6[4] = "Roams the caverns, seeking revenge...";
				array6[5] = new Func<bool>(() => RedeWorld.downedSkullDigger);
				mod8.Call(array6);
				Mod mod9 = bossChecklist;
				object[] array7 = new object[5];
				array7[0] = "AddMiniBossWithInfo";
				array7[1] = "Strange Portal";
				array7[2] = 3.48f;
				array7[3] = new Func<bool>(() => RedeWorld.downedStrangePortal);
				array7[4] = "Use an [i:" + ModContent.ItemType<UnstableCrystal>() + "]";
				mod9.Call(array7);
				Mod mod10 = bossChecklist;
				object[] array8 = new object[5];
				array8[0] = "AddBossWithInfo";
				array8[1] = "Xenomite Crystal";
				array8[2] = 3.481f;
				array8[3] = new Func<bool>(() => RedeWorld.downedXenomiteCrystal);
				array8[4] = "Use a [i:" + ModContent.ItemType<GeigerCounter>() + "], dropped by the Strange Portal. Begins the Infection storyline";
				mod10.Call(array8);
				Mod mod11 = bossChecklist;
				object[] array9 = new object[5];
				array9[0] = "AddMiniBossWithInfo";
				array9[1] = "Eaglecrest Golem";
				array9[2] = 4.1f;
				array9[3] = new Func<bool>(() => RedeWorld.downedEaglecrestGolem);
				array9[4] = "Naturally spawns at day after Eater of Worlds/Brain of Cthulhu is defeated";
				mod11.Call(array9);
				Mod mod12 = bossChecklist;
				object[] array10 = new object[6];
				array10[0] = "AddBossWithInfo";
				array10[1] = "Infected Eye";
				array10[2] = 6.25f;
				array10[3] = new Func<bool>(() => RedeWorld.downedInfectedEye);
				array10[4] = "Use a [i:" + ModContent.ItemType<XenoEye>() + "] at night, requires the Xenomite Crystal to be defeated";
				array10[5] = new Func<bool>(() => RedeWorld.downedXenomiteCrystal);
				mod12.Call(array10);
				Mod mod13 = bossChecklist;
				object[] array11 = new object[5];
				array11[0] = "AddBossWithInfo";
				array11[1] = "The Abandoned Lab";
				array11[2] = 9.1f;
				array11[3] = new Func<bool>(() => RedeWorld.downedIBehemoth);
				array11[4] = "Find the Abandoned Lab far below the surface, defeat the first 3 minibosses within. Requires all mech bosses to be defeated. [i:" + ModContent.ItemType<LabHelpMessage>() + "]";
				mod13.Call(array11);
				Mod mod14 = bossChecklist;
				object[] array12 = new object[5];
				array12[0] = "AddBossWithInfo";
				array12[1] = "King Slayer III";
				array12[2] = 9.99999f;
				array12[3] = new Func<bool>(() => RedeWorld.downedSlayer);
				array12[4] = "Use a [i:" + ModContent.ItemType<KingSummon>() + "] at day. (Although I would recommend fighting at a later point in the game)";
				mod14.Call(array12);
				Mod mod15 = bossChecklist;
				object[] array13 = new object[6];
				array13[0] = "AddBossWithInfo";
				array13[1] = "1st Vlitch Overlord";
				array13[2] = 11.5f;
				array13[3] = new Func<bool>(() => RedeWorld.downedVlitch1);
				array13[4] = "Use a [i:" + ModContent.ItemType<CorruptedHeroSword>() + "] at night";
				array13[5] = new Func<bool>(() => RedeWorld.downedXenomiteCrystal);
				mod15.Call(array13);
				Mod mod16 = bossChecklist;
				object[] array14 = new object[6];
				array14[0] = "AddBossWithInfo";
				array14[1] = "2nd Vlitch Overlord";
				array14[2] = 13.5f;
				array14[3] = new Func<bool>(() => RedeWorld.downedVlitch2);
				array14[4] = "Use a [i:" + ModContent.ItemType<CorruptedWormMedallion>() + "] at night";
				array14[5] = new Func<bool>(() => RedeWorld.downedXenomiteCrystal);
				mod16.Call(array14);
				Mod mod17 = bossChecklist;
				object[] array15 = new object[6];
				array15[0] = "AddBossWithInfo";
				array15[1] = "3rd Vlitch Overlord";
				array15[2] = 14.05f;
				array15[3] = new Func<bool>(() => RedeWorld.downedVlitch3);
				array15[4] = "Use an [i:" + ModContent.ItemType<OmegaRadar>() + "] at night";
				array15[5] = new Func<bool>(() => RedeWorld.downedXenomiteCrystal);
				mod17.Call(array15);
				Mod mod18 = bossChecklist;
				object[] array16 = new object[5];
				array16[0] = "AddBossWithInfo";
				array16[1] = "Patient Zero";
				array16[2] = 14.5f;
				array16[3] = new Func<bool>(() => RedeWorld.downedPatientZero);
				array16[4] = "Use a lunar pickaxe to mine the hardened sludge in the Abandoned Lab to explore further. Beware what awaits beyond.";
				mod18.Call(array16);
				Mod mod19 = bossChecklist;
				object[] array17 = new object[6];
				array17[0] = "AddEventWithInfo";
				array17[1] = "King Chicken's Royal Army";
				array17[2] = 14.55f;
				array17[3] = new Func<bool>(() => RedeWorld.downedChickenInvPZ);
				array17[4] = "Use a [i:" + ModContent.ItemType<ChickenContract>() + "] at day after Patient Zero is defeated.";
				array17[5] = new Func<bool>(() => RedeWorld.downedKingChicken);
				mod19.Call(array17);
				Mod mod20 = bossChecklist;
				object[] array18 = new object[5];
				array18[0] = "AddBossWithInfo";
				array18[1] = "Thorn & Eaglecrest Rematch";
				array18[2] = 14.7f;
				array18[3] = new Func<bool>(() => RedeWorld.downedThornPZ && RedeWorld.downedEaglecrestGolemPZ);
				array18[4] = string.Concat(new object[]
				{
					"Use an [i:",
					ModContent.ItemType<AncientSigil>(),
					"] & [i:",
					ModContent.ItemType<LifeFruitOfThorns>(),
					"] at day."
				});
				mod20.Call(array18);
				Mod mod21 = bossChecklist;
				object[] array19 = new object[5];
				array19[0] = "AddBossWithInfo";
				array19[1] = "Nebuleus, Angel of the Cosmos";
				array19[2] = 15.5f;
				array19[3] = new Func<bool>(() => RedeWorld.downedNebuleus);
				array19[4] = "Use an [i:" + ModContent.ItemType<NebSummon>() + "] at night";
				mod21.Call(array19);
			}
			Mod censusMod = ModLoader.GetMod("Census");
			if (censusMod != null)
			{
				censusMod.Call(new object[]
				{
					"TownNPCCondition",
					base.NPCType("Squire"),
					"Have a suitable house"
				});
				censusMod.Call(new object[]
				{
					"TownNPCCondition",
					base.NPCType("Fallen"),
					"Defeat the Keeper and have a suitable house"
				});
				censusMod.Call(new object[]
				{
					"TownNPCCondition",
					base.NPCType("Newb"),
					"Find the Suspicious Dirt Pile in the caverns"
				});
				censusMod.Call(new object[]
				{
					"TownNPCCondition",
					base.NPCType("TBot"),
					"Defeat the Infected Eye and have a suitable house"
				});
				censusMod.Call(new object[]
				{
					"TownNPCCondition",
					base.NPCType("Slicer"),
					"Defeat the Dark Slime miniboss, which spawns very rarely on the surface after all mech bosses are defeated"
				});
				censusMod.Call(new object[]
				{
					"TownNPCCondition",
					base.NPCType("DHunter"),
					"Defeat the Dark Slime and Plantera, and have a suitable house"
				});
			}
		}

		public override void ModifySunLightColor(ref Color tileColor, ref Color backgroundColor)
		{
			if (RedeWorld.xenoBiome > 0 && RedeWorld.evilXenoBiome > 0)
			{
				float exampleStrength = (float)RedeWorld.xenoBiome / 200f;
				float exampleStrength2 = (float)RedeWorld.evilXenoBiome / 200f;
				exampleStrength = Math.Min(exampleStrength, 1f);
				exampleStrength2 = Math.Min(exampleStrength, 1f);
				int sunR = (int)backgroundColor.R;
				int sunG = (int)backgroundColor.G;
				int sunB = (int)backgroundColor.B;
				sunR -= (int)(200f * exampleStrength * ((float)backgroundColor.R / 255f));
				sunB -= (int)(200f * exampleStrength * ((float)backgroundColor.B / 255f));
				sunG -= (int)(170f * exampleStrength * ((float)backgroundColor.G / 255f));
				sunR -= (int)(200f * exampleStrength2 * ((float)backgroundColor.R / 255f));
				sunB -= (int)(200f * exampleStrength2 * ((float)backgroundColor.B / 255f));
				sunG -= (int)(170f * exampleStrength2 * ((float)backgroundColor.G / 255f));
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
		}

		public override void HandlePacket(BinaryReader bb, int whoAmI)
		{
			switch (bb.ReadByte())
			{
			case 0:
			{
				Vector2 summonAt = Utils.ReadVector2(bb);
				NPC.NewNPC((int)summonAt.X, (int)summonAt.Y, base.NPCType("Stage2ScientistBoss"), 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			case 1:
			{
				Vector2 summonAt2 = Utils.ReadVector2(bb);
				NPC.NewNPC((int)summonAt2.X, (int)summonAt2.Y, base.NPCType("Stage3Scientist"), 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			case 2:
			{
				Vector2 summonAt2A = Utils.ReadVector2(bb);
				NPC.NewNPC((int)summonAt2A.X, (int)summonAt2A.Y, base.NPCType("Stage3Scientist2"), 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			case 3:
			{
				Vector2 summonAt3 = Utils.ReadVector2(bb);
				NPC.NewNPC((int)summonAt3.X, (int)summonAt3.Y, base.NPCType("IrradiatedBehemoth"), 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			case 4:
			{
				Vector2 summonAt3A = Utils.ReadVector2(bb);
				NPC.NewNPC((int)summonAt3A.X, (int)summonAt3A.Y, base.NPCType("IrradiatedBehemoth2"), 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			case 5:
			{
				Vector2 summonAt4 = Utils.ReadVector2(bb);
				NPC.NewNPC((int)summonAt4.X, (int)summonAt4.Y, base.NPCType("Blisterface"), 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			case 6:
			{
				Vector2 summonAt4A = Utils.ReadVector2(bb);
				NPC.NewNPC((int)summonAt4A.X, (int)summonAt4A.Y, base.NPCType("Blisterface2"), 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			case 7:
			{
				Vector2 summonAt5 = Utils.ReadVector2(bb);
				NPC.NewNPC((int)summonAt5.X, (int)summonAt5.Y, base.NPCType("TBotHolo"), 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			case 8:
			{
				Vector2 summonAt5A = Utils.ReadVector2(bb);
				NPC.NewNPC((int)summonAt5A.X, (int)summonAt5A.Y, base.NPCType("TbotMinibossStart"), 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			case 9:
			{
				Vector2 summonAt5B = Utils.ReadVector2(bb);
				NPC.NewNPC((int)summonAt5B.X, (int)summonAt5B.Y, base.NPCType("TbotMiniboss"), 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			case 10:
			{
				Vector2 summonAt6 = Utils.ReadVector2(bb);
				NPC.NewNPC((int)summonAt6.X, (int)summonAt6.Y, base.NPCType("MACEProjectHead"), 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			case 11:
			{
				Vector2 summonAt6A = Utils.ReadVector2(bb);
				NPC.NewNPC((int)summonAt6A.X, (int)summonAt6A.Y, base.NPCType("MACEProjectOffA"), 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			case 12:
			{
				Vector2 summonAt6B = Utils.ReadVector2(bb);
				NPC.NewNPC((int)summonAt6B.X, (int)summonAt6B.Y, base.NPCType("MACEProjectHeadA"), 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			case 13:
			{
				Vector2 summonAt7 = Utils.ReadVector2(bb);
				NPC.NewNPC((int)summonAt7.X, (int)summonAt7.Y, base.NPCType("PatientZero"), 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			case 14:
			{
				Vector2 summonAt7A = Utils.ReadVector2(bb);
				NPC.NewNPC((int)summonAt7A.X, (int)summonAt7A.Y, base.NPCType("PZ2Eyelid"), 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			case 15:
			{
				Vector2 summonAt7B = Utils.ReadVector2(bb);
				NPC.NewNPC((int)summonAt7B.X, (int)summonAt7B.Y, base.NPCType("PZ2Fight"), 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			case 16:
			{
				Vector2 summonAt8 = Utils.ReadVector2(bb);
				NPC.NewNPC((int)summonAt8.X, (int)summonAt8.Y, base.NPCType("KS3Sitting"), 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			case 17:
			{
				Vector2 summonAt9 = Utils.ReadVector2(bb);
				NPC.NewNPC((int)summonAt9.X, (int)summonAt9.Y, base.NPCType("JanitorBotCleaning"), 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			case 18:
			{
				Vector2 summonAt10 = Utils.ReadVector2(bb);
				NPC.NewNPC((int)summonAt10.X, (int)summonAt10.Y, base.NPCType("JanitorBotNPC"), 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			case 19:
			{
				Vector2 summonAt11 = Utils.ReadVector2(bb);
				NPC.NewNPC((int)summonAt11.X, (int)summonAt11.Y, base.NPCType("ProtectorVoltNPC"), 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			case 20:
			{
				Vector2 summonAt12 = Utils.ReadVector2(bb);
				NPC.NewNPC((int)summonAt12.X, (int)summonAt12.Y, base.NPCType("LabSentryTurretLeg"), 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			case 21:
			{
				Vector2 summonAt13 = Utils.ReadVector2(bb);
				NPC.NewNPC((int)summonAt13.X, (int)summonAt13.Y, base.NPCType("MACEControllerIdle"), 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			}
			MsgType msg = (MsgType)bb.ReadByte();
			if (msg == MsgType.ProjectileHostility)
			{
				int owner = bb.ReadInt32();
				int projID = bb.ReadInt32();
				bool friendly = bb.ReadBoolean();
				bool hostile = bb.ReadBoolean();
				if (Main.projectile[projID] != null)
				{
					Main.projectile[projID].owner = owner;
					Main.projectile[projID].friendly = friendly;
					Main.projectile[projID].hostile = hostile;
				}
				if (Main.netMode == 2)
				{
					MNet.SendBaseNetMessage(0, new object[]
					{
						owner,
						projID,
						friendly,
						hostile
					});
					return;
				}
			}
			else if (msg == MsgType.SyncAI)
			{
				int classID = (int)bb.ReadByte();
				int id = (int)bb.ReadInt16();
				int aitype = (int)bb.ReadByte();
				int arrayLength = (int)bb.ReadByte();
				float[] newAI = new float[arrayLength];
				for (int i = 0; i < arrayLength; i++)
				{
					newAI[i] = bb.ReadSingle();
				}
				if (classID == 0 && Main.npc[id] != null && Main.npc[id].active && Main.npc[id].modNPC != null && Main.npc[id].modNPC is ParentNPC)
				{
					((ParentNPC)Main.npc[id].modNPC).SetAI(newAI, aitype);
				}
				else if (classID == 1 && Main.projectile[id] != null && Main.projectile[id].active && Main.projectile[id].modProjectile != null && Main.projectile[id].modProjectile is ParentProjectile)
				{
					((ParentProjectile)Main.projectile[id].modProjectile).SetAI(newAI, aitype);
				}
				if (Main.netMode == 2)
				{
					BaseNet.SyncAI(classID, id, newAI, aitype);
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
				int npcID = NPC.NewNPC((int)npcCenter.X, (int)npcCenter.Y, bossType, 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[npcID].Center = npcCenter;
				Main.npc[npcID].netUpdate = true;
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
							Main.NewText(npcName + " have awoken!", 175, 75, byte.MaxValue, false);
							return;
						}
						if (Main.netMode == 2)
						{
							NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(npcName + " have awoken!"), new Color(175, 75, 255), -1);
							return;
						}
					}
					else
					{
						if (Main.netMode == 0)
						{
							Main.NewText(Language.GetTextValue("Announcement.HasAwoken", npcName), 175, 75, byte.MaxValue, false);
							return;
						}
						if (Main.netMode == 2)
						{
							NetMessage.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasAwoken", new object[]
							{
								NetworkText.FromLiteral(npcName)
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
