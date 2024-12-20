using System;
using Microsoft.Xna.Framework;
using Redemption.Items;
using Redemption.Items.Placeable;
using Redemption.Items.Quest;
using Redemption.Items.Weapons;
using Redemption.Items.Weapons.v08;
using Redemption.NPCs.Minibosses.MossyGoliath;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Redemption.NPCs.v08
{
	[AutoloadHead]
	public class Daerel1 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Wayfarer");
			Main.npcFrameCount[base.npc.type] = 26;
			NPCID.Sets.ExtraFramesCount[base.npc.type] = 5;
			NPCID.Sets.AttackFrameCount[base.npc.type] = 5;
			NPCID.Sets.DangerDetectRange[base.npc.type] = 1000;
			NPCID.Sets.AttackType[base.npc.type] = 1;
			NPCID.Sets.AttackTime[base.npc.type] = 20;
			NPCID.Sets.AttackAverageChance[base.npc.type] = 30;
			NPCID.Sets.HatOffsetY[base.npc.type] = 8;
		}

		public override void SetDefaults()
		{
			base.npc.townNPC = true;
			base.npc.friendly = true;
			base.npc.width = 24;
			base.npc.height = 48;
			base.npc.aiStyle = 7;
			base.npc.damage = 10;
			base.npc.defense = 5;
			base.npc.lifeMax = 250;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.knockBackResist = 0.5f;
			this.animationType = 22;
		}

		public override void AI()
		{
			if (RedeQuests.daerelQuests > 4 || RedeQuests.zephosQuests > 4)
			{
				base.npc.Transform(ModContent.NPCType<Daerel2>());
			}
		}

		public override bool CheckDead()
		{
			Main.NewText("Daerel the Wayfarer was knocked unconscious...", Color.Red.R, Color.Red.G, Color.Red.B, false);
			base.npc.SetDefaults(ModContent.NPCType<DaerelUnconscious>(), -1f);
			return false;
		}

		public override bool CanTownNPCSpawn(int numTownNPCs, int money)
		{
			return WorldGen.crimson && !NPC.AnyNPCs(ModContent.NPCType<DaerelUnconscious>()) && !NPC.AnyNPCs(ModContent.NPCType<Daerel2Unconscious>()) && !NPC.AnyNPCs(ModContent.NPCType<Daerel3Unconscious>()) && !NPC.AnyNPCs(ModContent.NPCType<Daerel2>()) && !NPC.AnyNPCs(ModContent.NPCType<Daerel3>());
		}

		public override string TownNPCName()
		{
			return "Daerel";
		}

		public override void FindFrame(int frameHeight)
		{
		}

		public override string GetChat()
		{
			int DryadID = NPC.FindFirstNPC(20);
			if (DryadID >= 0 && Main.rand.Next(8) == 0)
			{
				return "Is " + Main.npc[DryadID].GivenName + " a half-Nymph? Or just a weirdo who doesn't wear actual clothes?";
			}
			int PartyGirlID = NPC.FindFirstNPC(208);
			if (PartyGirlID >= 0 && Main.rand.Next(15) == 0)
			{
				return "I swear " + Main.npc[PartyGirlID].GivenName + " reminds me of a technicoloured pony from another universe...";
			}
			switch (Main.rand.Next(8))
			{
			case 0:
				return "Need anything? I can restring your bow, or poison your weapon. It'll cost you though.";
			case 1:
				return "You don't mind me staying here, right?";
			case 2:
				return "I got some pretty nice loot I can sell you, I kinda need money right now.";
			case 3:
				return "My favourite colour is green, not sure why I'm telling you though...";
			case 4:
				return "Cats are obviously superior than dogs.";
			case 5:
				return "I've been travelling this land for a while, but staying in a house is nice.";
			case 6:
				return "Have you seen a guy with slicked back, hazel hair? He carries a sword and wears a green tunic last I saw. I've lost him while travelling to this island, hope he's doing alright.";
			default:
				return "Hello there.";
			}
		}

		public override void ResetEffects()
		{
			Daerel1.SwitchInfo = false;
			Daerel1.Shop = false;
			Daerel1.Talk = false;
			Daerel1.RestringBow = false;
			Daerel1.PoisonWeapon = false;
			Daerel1.Quest1 = false;
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			string SwitchInfoT = "Cycle Options";
			string ShopT = "Shop";
			string TalkT = "Talk";
			string RestringT = "Restring Bow (10 silver)";
			string PoisonT = "Poison Weapon (25 silver)";
			string QuestT = "Quest";
			button = SwitchInfoT;
			if (Daerel1.ChatNumber == 0)
			{
				button2 = ShopT;
				Daerel1.Shop = true;
				return;
			}
			if (Daerel1.ChatNumber == 1)
			{
				button2 = TalkT;
				Daerel1.Talk = true;
				return;
			}
			if (Daerel1.ChatNumber == 2)
			{
				button2 = RestringT;
				Daerel1.RestringBow = true;
				return;
			}
			if (Daerel1.ChatNumber == 3)
			{
				button2 = PoisonT;
				Daerel1.PoisonWeapon = true;
				return;
			}
			if (Daerel1.ChatNumber == 4)
			{
				button2 = QuestT;
				Daerel1.Quest1 = true;
				return;
			}
			Daerel1.ChatNumber = 0;
			button2 = TalkT;
			Daerel1.Talk = true;
		}

		public void ResetBools()
		{
			Daerel1.Shop = false;
			Daerel1.Talk = false;
			Daerel1.RestringBow = false;
			Daerel1.PoisonWeapon = false;
			Daerel1.Quest1 = false;
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			Player player = Main.LocalPlayer;
			player.GetModPlayer<RedePlayer>();
			if (firstButton)
			{
				this.ResetBools();
				Daerel1.ChatNumber++;
				if (Daerel1.ChatNumber > 4)
				{
					Daerel1.ChatNumber = 0;
					return;
				}
			}
			else
			{
				if (Daerel1.Talk)
				{
					Main.npcChatText = Daerel1.ChitChat();
					return;
				}
				if (Daerel1.ChatNumber == 0)
				{
					shop = true;
				}
				if (Daerel1.ChatNumber == 1)
				{
					Main.npcChatText = Daerel1.ChitChat();
					return;
				}
				if (Daerel1.ChatNumber == 2)
				{
					if (Main.LocalPlayer.BuyItemOld(1000))
					{
						Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 37, 1f, 0f);
						Main.LocalPlayer.AddBuff(16, 36000, true);
						return;
					}
					Main.npcChatText = Daerel1.NoCoinsChat();
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					return;
				}
				else if (Daerel1.ChatNumber == 3)
				{
					if (Main.LocalPlayer.BuyItemOld(2500))
					{
						Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 37, 1f, 0f);
						Main.LocalPlayer.AddBuff(79, 36000, true);
						return;
					}
					Main.npcChatText = Daerel1.NoCoinsChat();
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					return;
				}
				else if (Daerel1.ChatNumber == 4)
				{
					if (RedeQuests.daerelQuests == 0)
					{
						Main.npcChatCornerItem = ModContent.ItemType<ShellNecklaceQuest>();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						int Necklace = player.FindItem(ModContent.ItemType<ShellNecklaceQuest>());
						if (Necklace >= 0)
						{
							player.inventory[Necklace].stack--;
							if (player.inventory[Necklace].stack <= 0)
							{
								player.inventory[Necklace] = new Item();
							}
							Main.npcChatCornerItem = ModContent.ItemType<DaerelRewardBag>();
							Main.npcChatText = this.Give1Chat();
							player.QuickSpawnItem(ModContent.ItemType<DaerelRewardBag>(), 1);
							RedeQuests.daerelQuests++;
							RedeQuests.DnecklaceQuest = true;
							Main.PlaySound(24, -1, -1, 1, 1f, 0f);
							return;
						}
						Main.npcChatText = this.QuestChat();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					}
					if (RedeQuests.daerelQuests == 1)
					{
						Main.npcChatCornerItem = ModContent.ItemType<HikersBackpack>();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						int Backpack = player.FindItem(ModContent.ItemType<HikersBackpack>());
						if (Backpack >= 0)
						{
							player.inventory[Backpack].stack--;
							if (player.inventory[Backpack].stack <= 0)
							{
								player.inventory[Backpack] = new Item();
							}
							Main.npcChatCornerItem = ModContent.ItemType<DaerelRewardBag>();
							Main.npcChatText = this.Give2Chat();
							player.QuickSpawnItem(ModContent.ItemType<DaerelRewardBag>(), 1);
							RedeQuests.daerelQuests++;
							RedeQuests.DbackpackQuest = true;
							Main.PlaySound(24, -1, -1, 1, 1f, 0f);
							return;
						}
						Main.npcChatText = this.QuestChat();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					}
					if (RedeQuests.daerelQuests == 2)
					{
						Main.npcChatCornerItem = ModContent.ItemType<PatchingKit>();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						int Kit = player.FindItem(ModContent.ItemType<PatchingKit>());
						if (Kit >= 0)
						{
							player.inventory[Kit].stack--;
							if (player.inventory[Kit].stack <= 0)
							{
								player.inventory[Kit] = new Item();
							}
							Main.npcChatCornerItem = ModContent.ItemType<DaerelRewardBag>();
							Main.npcChatText = this.Give3Chat();
							player.QuickSpawnItem(ModContent.ItemType<DaerelRewardBag>(), 1);
							RedeQuests.daerelQuests++;
							RedeQuests.DkitQuest = true;
							Main.PlaySound(24, -1, -1, 1, 1f, 0f);
							return;
						}
						Main.npcChatText = this.QuestChat();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					}
					if (RedeQuests.daerelQuests == 3)
					{
						Main.npcChatCornerItem = ModContent.ItemType<UltimatePotionIcon>();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						int Potion = player.FindItem(ModContent.ItemType<UltimatePotion>());
						if (Potion >= 0)
						{
							player.inventory[Potion].stack--;
							if (player.inventory[Potion].stack <= 0)
							{
								player.inventory[Potion] = new Item();
							}
							Main.npcChatCornerItem = ModContent.ItemType<DaerelRewardBag>();
							Main.npcChatText = this.Give4Chat();
							player.QuickSpawnItem(ModContent.ItemType<DaerelRewardBag>(), 2);
							RedeQuests.daerelQuests++;
							RedeQuests.DpotionQuest = true;
							Main.PlaySound(24, -1, -1, 1, 1f, 0f);
							return;
						}
						Main.npcChatText = this.QuestChat();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					}
					if (RedeQuests.daerelQuests == 4)
					{
						Main.npcChatCornerItem = ModContent.ItemType<SilverwoodBow>();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						int Bow = player.FindItem(ModContent.ItemType<SilverwoodBow>());
						if (Bow >= 0)
						{
							player.inventory[Bow].stack--;
							if (player.inventory[Bow].stack <= 0)
							{
								player.inventory[Bow] = new Item();
							}
							Main.npcChatCornerItem = ModContent.ItemType<DaerelRewardBag>();
							Main.npcChatText = this.Give5Chat();
							player.QuickSpawnItem(ModContent.ItemType<DaerelRewardBag>(), 3);
							RedeQuests.daerelQuests++;
							RedeQuests.DpotionQuest = true;
							Main.PlaySound(24, -1, -1, 1, 1f, 0f);
							return;
						}
						Main.npcChatText = this.QuestChat();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						return;
					}
					else
					{
						Main.npcChatText = this.QuestChat();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					}
				}
			}
		}

		public static string NoCoinsChat()
		{
			WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
			weightedRandom.Add("You're as poor as me?", 1.0);
			weightedRandom.Add("You really don't have enough money? Ah whatever, not like I can complain.", 1.0);
			return weightedRandom;
		}

		public string QuestChat()
		{
			Main.LocalPlayer.GetModPlayer<RedePlayer>();
			WeightedRandom<string> chat = new WeightedRandom<string>();
			if (RedeQuests.daerelQuests == 0)
			{
				chat.Add("You asking me for quests? I won't be giving you any money for them, cos I need money just as much as you do. I can give you a goodie bag with random junk I find on my adventures. Anyway, could you craft me a necklace out of Tree Bug and Coast Scarab Shells? No, it isn't for me. I just want something to sell to a merchant.", 1.0);
			}
			else if (RedeQuests.daerelQuests == 1)
			{
				chat.Add("Exploring this strange island can be a hassle, so could you make some equipment that'll help me? A rope hook, an axe, a fishing pole, some throwing knives, and a backpack could be very useful.", 1.0);
			}
			else if (RedeQuests.daerelQuests == 2)
			{
				chat.Add("My cloak will inevitably get torn while roaming around this land, there are a lot of monsters here, so maybe give me a Patching Kit. It needs tattered cloth, silk, and some black thread. Thanks.", 1.0);
			}
			else if (RedeQuests.daerelQuests == 3)
			{
				chat.Add("This one will take some time, unless you have someone who sells everything, but I've been thinking... Carrying a bunch of potions can be a hassle, so what if you combined all of them together into one ultimate potion cocktail! I'd like potions of Nightshade, Restoration, Regeneration, Hunter, Dangersense, Archery, Endurance, Ironskin, Nightowl, and Swiftness in the potion.", 1.0);
			}
			else if (RedeQuests.daerelQuests == 4)
			{
				chat.Add("I recently was given a new bow, but I lost it while running away from a jungle beast! I've read about them, they are known as Mossy Goliaths. Sorry if this is asking a lot, but could you kill it and get my bow back? You should find it in the jungle, presumably sleeping. Thanks in advance.\n\n(Completable after The Keeper is defeated)", 1.0);
			}
			else
			{
				chat.Add("I can't think of any quests right now.", 1.0);
			}
			return chat;
		}

		public string Give1Chat()
		{
			WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
			weightedRandom.Add("Thanks, I'm sure this'll sell for good money, it's even made with silver! Or is that tungsten? I don't know my metals.", 1.0);
			return weightedRandom;
		}

		public string Give2Chat()
		{
			WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
			weightedRandom.Add("Nice, this will come in handy. Here's your reward.", 1.0);
			return weightedRandom;
		}

		public string Give3Chat()
		{
			WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
			weightedRandom.Add("You sure are a master of crafting. In fact, you are a master of a lot of things, combat, crafting, mining, exploring. Where did you come from actually? Were you born here?", 1.0);
			return weightedRandom;
		}

		public string Give4Chat()
		{
			WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
			weightedRandom.Add("Wow! It's very colourful. I wonder how it tastes... Only one way to find out! Oh, and here is your 2 reward bags.", 1.0);
			return weightedRandom;
		}

		public string Give5Chat()
		{
			WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
			weightedRandom.Add("You killed it and repaired my bow? Oh, the Fallen repaired it? Hey, you killed that beast so thank you, here are not 1, not 2, but 3 reward bags!", 1.0);
			return weightedRandom;
		}

		public static string ChitChat()
		{
			WeightedRandom<string> chat = new WeightedRandom<string>();
			chat.Add("I've lost someone, his name is Zephos and wears a green tunic. I'm sure I'll find him eventually.", 1.0);
			chat.Add("One time me and Zephos were in a cave, and then a skeleton with flowers stuck in its ribcage appeared. Zephos thought it was a powerful druid skeleton. He likes to exaggerate. It didn't have any magic, it was just a normal skeleton.", 1.0);
			chat.Add("If you wanna find Leaf Beetles, or Tree Bugs as they're called here, then chop down some trees. They live on tree tops, their leaf-green shell camouflaging them in the foliage. They eat the bark off of trees, and if their tree is destroyed or rotted, it will climb down and find another suitable tree to live on.", 1.0);
			chat.Add("Cool Bug Fact: Coast Scarabs are small beetles that live on sandy beaches and eat grains of sand as their primary diet. When wet, their cyan shells will shine. Their shell is normally used to make cyan dyes.", 1.0);
			chat.Add("Cool Bug Fact: Sandskin Spiders live in deserts, roaming around at night when other tiny insects come out to eat. When the hot day arrives, the spider will borrow a feet under the sand to sleep. Yes, I like bugs.", 1.0);
			chat.Add("How did I get here? Me and Zephos were on a boat looking for interesting islands, but then a storm came to ruin our day. We must've drifted here, but I don't know where Zephos is.", 1.0);
			if (Main.moonPhase == 0 && !Main.dayTime)
			{
				chat.Add("It was midnight when I woke up on this island's beach. 'Twas a full moon, and when I looked towards the sea, I saw a ghost. It was an old man with a soggy beard and pale-green skin, he also had a cutlass. I ran away, which is a normal human reaction when seeing a ghost.", 1.0);
			}
			chat.Add("Moonflare Bats have thin wings, causing moonlight to pass through, creating the illusion that they glow. They store the light of the moon within them and convert it to weak energy. They are relatively harmless. Cool Mammal Fact of the day. Yes, bats are mammals.", 1.0);
			chat.Add("Living Blooms roam this island? They are native to Anglon's lush forests. Living Blooms are more plant than animal, it doesn't eat, it photosynthesises sunlight.", 1.0);
			if (!Main.dayTime)
			{
				chat.Add("There are zombies here? Not that I'm surprised, there are many types of undead on the mainland too.", 1.0);
			}
			if (!Main.dayTime && !RedeConfigClient.Instance.NoSpidersInMyTerrariaMod)
			{
				chat.Add("This island has Forest Spiders? They're most commonly found in Anglon, but can be found in neighboring domains, such as Ithon and Gathuram. They like to live within small caves in forests and woodlands, normally coming out at night.", 1.0);
			}
			if (RedeQuests.DnecklaceQuest)
			{
				chat.Add("Those shell necklaces are selling nicely.", 1.0);
			}
			return chat;
		}

		public override void SetupShop(Chest shop, ref int nextSlot)
		{
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<LivingTwig>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(259, false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<LeatherPouch>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<WoodenBuckler>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<FlintAndSteel>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<BeardedHatchet>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<AncientNovicesStaff>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<WardensBow>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<IronfurAmulet>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Archcloth>(), false);
			nextSlot++;
			if (NPC.downedBoss1)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<ForestGolemPainting>(), false);
				nextSlot++;
			}
			if (NPC.downedBoss2)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<EaglecrestSpelltome>(), false);
				nextSlot++;
			}
			if (NPC.downedBoss3)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<GathicCryoCrystal>(), false);
				nextSlot++;
			}
			if (RedeWorld.downedEaglecrestGolem)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<GolemEye>(), false);
				nextSlot++;
			}
			if (RedeWorld.downedMossyGoliath)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<MossyWimpGun>(), false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<MudMace>(), false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<TastySteak>(), false);
				nextSlot++;
			}
		}

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 13;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 20;
			randExtraCooldown = 10;
		}

		public override void DrawTownAttackGun(ref float scale, ref int item, ref int closeness)
		{
			scale = 1f;
			item = ModContent.ItemType<WardensBow>();
			closeness = 20;
		}

		public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
		{
			projType = 1;
			attackDelay = 1;
		}

		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
		{
			multiplier = 10f;
		}

		public static bool SwitchInfo;

		public static bool Shop;

		public static bool Talk;

		public static bool RestringBow;

		public static bool PoisonWeapon;

		public static bool Quest1;

		public static int ChatNumber;
	}
}
