using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.ChickenArmy;
using Redemption.CrossMod;
using Redemption.Items;
using Redemption.Items.Armor.Costumes;
using Redemption.Items.Cores;
using Redemption.NPCs.Bosses.EaglecrestGolem;
using Redemption.NPCs.Bosses.Nebuleus;
using Redemption.NPCs.Bosses.SeedOfInfection;
using ReLogic.Graphics;
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
				if (Redemption.emptyHallActive)
				{
					music = base.GetSoundSlot(51, "Sounds/Music/Empty");
					priority = 4;
				}
				if (Redemption.soullessBiomeActive)
				{
					music = base.GetSoundSlot(51, "Sounds/Music/Soulless");
					priority = 4;
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
				base.AddMusicBox(base.GetSoundSlot(51, "Sounds/Music/HallofHeroes"), base.ItemType("HallOfHeroesBox"), base.TileType("HallOfHeroesBoxTile"), 0);
				Redemption.EmptyTexture = base.GetTexture("Empty");
				Redemption.PremultiplyTexture(base.GetTexture("Backgrounds/fog"));
				Redemption.boom = base.GetTexture("TransitionTex");
				Redemption.PremultiplyTexture(Redemption.boom);
				Redemption.dancingLight = base.GetTexture("NPCs/Bosses/EaglecrestGolem/UkkoDancingLights");
				Redemption.PremultiplyTexture(Redemption.dancingLight);
				Redemption.warningTex1 = base.GetTexture("NPCs/Bosses/Thorn/AkkaIslandWarning");
				Redemption.PremultiplyTexture(Redemption.warningTex1);
				Redemption.healingSpiritTex = base.GetTexture("NPCs/Bosses/Thorn/AkkaHealingSpirit");
				Redemption.PremultiplyTexture(Redemption.healingSpiritTex);
				Redemption.ukkoBlast = base.GetTexture("NPCs/Bosses/EaglecrestGolem/UkkoElectricBlast");
				Redemption.PremultiplyTexture(Redemption.ukkoBlast);
				Redemption.forcefield1 = base.GetTexture("Projectiles/DruidProjectiles/WorldStave/AncientForcefieldPro1");
				Redemption.PremultiplyTexture(Redemption.forcefield1);
				Redemption.forcefield2 = base.GetTexture("Projectiles/DruidProjectiles/WorldStave/AncientForcefieldPro2");
				Redemption.PremultiplyTexture(Redemption.forcefield2);
				Redemption.forcefield3 = base.GetTexture("Projectiles/DruidProjectiles/WorldStave/CaveAura");
				Redemption.PremultiplyTexture(Redemption.forcefield3);
				Redemption.forcefield4 = base.GetTexture("Projectiles/DruidProjectiles/WorldStave/FireForcePro1");
				Redemption.PremultiplyTexture(Redemption.forcefield4);
				Redemption.forcefield5 = base.GetTexture("Projectiles/DruidProjectiles/WorldStave/FreezeForcePro1");
				Redemption.PremultiplyTexture(Redemption.forcefield5);
				Redemption.forcefield6 = base.GetTexture("Projectiles/DruidProjectiles/WorldStave/MartianForcefieldPro1");
				Redemption.PremultiplyTexture(Redemption.forcefield6);
				Redemption.forcefield7 = base.GetTexture("Projectiles/DruidProjectiles/WorldStave/SunAura");
				Redemption.PremultiplyTexture(Redemption.forcefield7);
				Filters.Scene["Redemption:Nebuleus"] = new Filter(new StarGodSkyData("FilterMiniTower").UseColor(0.3f, 0f, 0.4f).UseOpacity(0.5f), 4);
				SkyManager.Instance["Redemption:Nebuleus"] = new StarGodSky();
				Filters.Scene["Redemption:BigNebuleus"] = new Filter(new StarGodSkyData2("FilterMiniTower").UseColor(0.3f, 0f, 0.4f).UseOpacity(0.5f), 4);
				SkyManager.Instance["Redemption:BigNebuleus"] = new StarGodSky2();
				Filters.Scene["Redemption:Ukko"] = new Filter(new UkkoClouds1Data("FilterMiniTower").UseColor(0.1f, 0.1f, 0f).UseOpacity(0.65f), 4);
				SkyManager.Instance["Redemption:Ukko"] = new UkkoClouds1();
				UkkoClouds1.boltTexture = ModLoader.GetMod("Redemption").GetTexture("NPCs/Bosses/EaglecrestGolem/UkkoSkyBolt");
				UkkoClouds1.flashTexture = ModLoader.GetMod("Redemption").GetTexture("NPCs/Bosses/EaglecrestGolem/UkkoSkyFlash");
				UkkoClouds1.BeamTexture = ModLoader.GetMod("Redemption").GetTexture("NPCs/Bosses/EaglecrestGolem/UkkoSkyBeam");
				Redemption.PremultiplyTexture(base.GetTexture("NPCs/Bosses/EaglecrestGolem/UkkoSkyBeam"));
				Redemption.PremultiplyTexture(base.GetTexture("NPCs/Bosses/EaglecrestGolem/UkkoClouds1"));
				StarGodSky.SkyTex = ModLoader.GetMod("Redemption").GetTexture("NPCs/Bosses/Nebuleus/StarGodSky");
				StarGodSky2.SkyTex = ModLoader.GetMod("Redemption").GetTexture("NPCs/Bosses/Nebuleus/StarGodSky2");
				UkkoClouds1.CloudTex = ModLoader.GetMod("Redemption").GetTexture("NPCs/Bosses/EaglecrestGolem/UkkoClouds1");
				Ref<Effect> screenRef = new Ref<Effect>(base.GetEffect("Effects/Shockwave"));
				Filters.Scene["Shockwave"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), 4);
				Filters.Scene["Shockwave"].Load();
			}
			Redemption.FaceCustomCurrencyID = CustomCurrencyManager.RegisterCurrency(new CustomCurrency(ModContent.ItemType<AncientGoldCoin>(), 999L));
			Filters.Scene["Redemption:XenoSky"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0f, 0.2f, 0f).UseOpacity(0.4f), 3);
			Filters.Scene["Redemption:SoullessSky"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0f, 0f, 0f).UseOpacity(0.6f), 3);
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
			text.SetDefault("Powerful creatures roam the forests, caverns, and skies...");
			base.AddTranslation(text);
			text = base.CreateTranslation("GirusHide");
			text.SetDefault("Thought you could hide from me?");
			base.AddTranslation(text);
		}

		public override void Unload()
		{
			this.CleanupStaticArrays();
			Redemption.boom = null;
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
				UkkoClouds1.CloudTex = null;
				UkkoClouds1.boltTexture = null;
				UkkoClouds1.flashTexture = null;
				UkkoClouds1.BeamTexture = null;
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
		}

		private void DrawInfectionText()
		{
			float alpha = Main.player[Main.myPlayer].GetModPlayer<InfectionTextPlayer>().alphaText;
			string text = "The Infection shall begin...";
			Vector2 textSize = Main.fontDeathText.MeasureString(text);
			float textPositionLeft = (float)(Main.screenWidth / 2) - textSize.X / 2f;
			DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, Main.fontDeathText, text, new Vector2(textPositionLeft, (float)(Main.screenHeight / 2 - 300)), Color.Green * ((float)Color.Green.A / alpha), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
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
			WeakReferences.PerformModSupport();
			ModLoader.GetMod("BossChecklist");
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
				array3[4] = new Func<bool>(() => RedeWorld.downedTheKeeper && !WorldGen.crimson);
				array3[5] = 80000;
				mod3.Call(array3);
				Mod mod4 = fargos;
				object[] array4 = new object[6];
				array4[0] = "AddSummon";
				array4[1] = 2.4f;
				array4[2] = "Redemption";
				array4[3] = "MysteriousTabletCrimson";
				array4[4] = new Func<bool>(() => RedeWorld.downedTheKeeper && WorldGen.crimson);
				array4[5] = 80000;
				mod4.Call(array4);
				Mod mod5 = fargos;
				object[] array5 = new object[6];
				array5[0] = "AddSummon";
				array5[1] = 3.48f;
				array5[2] = "Redemption";
				array5[3] = "GeigerCounter";
				array5[4] = new Func<bool>(() => RedeWorld.downedXenomiteCrystal);
				array5[5] = 100000;
				mod5.Call(array5);
				Mod mod6 = fargos;
				object[] array6 = new object[6];
				array6[0] = "AddSummon";
				array6[1] = 6.25f;
				array6[2] = "Redemption";
				array6[3] = "XenoEye";
				array6[4] = new Func<bool>(() => RedeWorld.downedInfectedEye);
				array6[5] = 250000;
				mod6.Call(array6);
				Mod mod7 = fargos;
				object[] array7 = new object[6];
				array7[0] = "AddSummon";
				array7[1] = 9.99999f;
				array7[2] = "Redemption";
				array7[3] = "KingSummon";
				array7[4] = new Func<bool>(() => RedeWorld.downedSlayer);
				array7[5] = 400000;
				mod7.Call(array7);
				Mod mod8 = fargos;
				object[] array8 = new object[6];
				array8[0] = "AddSummon";
				array8[1] = 11.5f;
				array8[2] = "Redemption";
				array8[3] = "CorruptedHeroSword";
				array8[4] = new Func<bool>(() => RedeWorld.downedVlitch1);
				array8[5] = 600000;
				mod8.Call(array8);
				Mod mod9 = fargos;
				object[] array9 = new object[6];
				array9[0] = "AddSummon";
				array9[1] = 12.8f;
				array9[2] = "Redemption";
				array9[3] = "CorruptedWormMedallion";
				array9[4] = new Func<bool>(() => RedeWorld.downedVlitch2);
				array9[5] = 600000;
				mod9.Call(array9);
				Mod mod10 = fargos;
				object[] array10 = new object[6];
				array10[0] = "AddSummon";
				array10[1] = 14.05f;
				array10[2] = "Redemption";
				array10[3] = "OmegaRadar";
				array10[4] = new Func<bool>(() => RedeWorld.downedVlitch3);
				array10[5] = 1000000;
				mod10.Call(array10);
				Mod mod11 = fargos;
				object[] array11 = new object[6];
				array11[0] = "AddSummon";
				array11[1] = 14.7f;
				array11[2] = "Redemption";
				array11[3] = "SigilOfThorns";
				array11[4] = new Func<bool>(() => RedeWorld.downedEaglecrestGolemPZ && RedeWorld.downedThornPZ);
				array11[5] = 4000000;
				mod11.Call(array11);
				Mod mod12 = fargos;
				object[] array12 = new object[6];
				array12[0] = "AddSummon";
				array12[1] = 15.5f;
				array12[2] = "Redemption";
				array12[3] = "NebSummon";
				array12[4] = new Func<bool>(() => RedeWorld.downedNebuleus);
				array12[5] = 10000000;
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
			if (yabhb != null)
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

		public const string EMPTY_TEXTURE = "Redemption/Empty";

		public const string customEventName = "Chicken Army";

		public static Texture2D boom;

		public static Texture2D dancingLight;

		public static Texture2D warningTex1;

		public static Texture2D healingSpiritTex;

		public static Texture2D ukkoBlast;

		public static Texture2D forcefield1;

		public static Texture2D forcefield2;

		public static Texture2D forcefield3;

		public static Texture2D forcefield4;

		public static Texture2D forcefield5;

		public static Texture2D forcefield6;

		public static Texture2D forcefield7;

		public static int customEvent;

		public static int FaceCustomCurrencyID;

		public static bool GirusSilence;

		public static Redemption inst = null;

		public static bool templeOfHeroes;

		public static bool emptyHallActive;

		public static bool soullessBiomeActive;

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
