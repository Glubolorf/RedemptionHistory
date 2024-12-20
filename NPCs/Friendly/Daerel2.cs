using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Accessories.PreHM;
using Redemption.Items.Materials.PreHM;
using Redemption.Items.Placeable.Furniture.Misc;
using Redemption.Items.Quest;
using Redemption.Items.Quest.Daerel;
using Redemption.Items.Usable.Summons;
using Redemption.Items.Weapons.HM.Ranged;
using Redemption.Items.Weapons.PreHM.Magic;
using Redemption.Items.Weapons.PreHM.Melee;
using Redemption.Items.Weapons.PreHM.Ranged;
using Redemption.NPCs.Minibosses.MossyGoliath;
using Redemption.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Redemption.NPCs.Friendly
{
	[AutoloadHead]
	public class Daerel2 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Wayfarer");
			Main.npcFrameCount[base.npc.type] = 26;
			NPCID.Sets.ExtraFramesCount[base.npc.type] = 5;
			NPCID.Sets.AttackFrameCount[base.npc.type] = 5;
			NPCID.Sets.DangerDetectRange[base.npc.type] = 1000;
			NPCID.Sets.AttackType[base.npc.type] = 1;
			NPCID.Sets.AttackTime[base.npc.type] = 6;
			NPCID.Sets.AttackAverageChance[base.npc.type] = 20;
			NPCID.Sets.HatOffsetY[base.npc.type] = 8;
		}

		public override void SetDefaults()
		{
			base.npc.townNPC = true;
			base.npc.friendly = true;
			base.npc.width = 24;
			base.npc.height = 48;
			base.npc.aiStyle = 7;
			base.npc.damage = 11;
			base.npc.defense = 20;
			base.npc.lifeMax = 250;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.knockBackResist = 0.3f;
			this.animationType = 22;
		}

		public override void AI()
		{
			if (RedeQuests.daerelQuests > 9 || RedeQuests.zephosQuests > 9 || (RedeWorld.downedDarkSlime && (RedeQuests.daerelQuests == 9 || RedeQuests.zephosQuests == 9)))
			{
				base.npc.Transform(ModContent.NPCType<Daerel3>());
			}
		}

		public override bool CheckDead()
		{
			Main.NewText("Daerel the Wayfarer was knocked unconscious...", Color.Red.R, Color.Red.G, Color.Red.B, false);
			base.npc.SetDefaults(ModContent.NPCType<Daerel2Unconscious>(), -1f);
			return false;
		}

		public override bool CanTownNPCSpawn(int numTownNPCs, int money)
		{
			return false;
		}

		public override string TownNPCName()
		{
			return "Daerel";
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
			Daerel2.SwitchInfo = false;
			Daerel2.Shop = false;
			Daerel2.Talk = false;
			Daerel2.RestringBow = false;
			Daerel2.PoisonWeapon = false;
			Daerel2.Quest1 = false;
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
			if (Daerel2.ChatNumber == 0)
			{
				button2 = ShopT;
				Daerel2.Shop = true;
				return;
			}
			if (Daerel2.ChatNumber == 1)
			{
				button2 = TalkT;
				Daerel2.Talk = true;
				return;
			}
			if (Daerel2.ChatNumber == 2)
			{
				button2 = RestringT;
				Daerel2.RestringBow = true;
				return;
			}
			if (Daerel2.ChatNumber == 3)
			{
				button2 = PoisonT;
				Daerel2.PoisonWeapon = true;
				return;
			}
			if (Daerel2.ChatNumber == 4)
			{
				button2 = QuestT;
				Daerel2.Quest1 = true;
				return;
			}
			Daerel2.ChatNumber = 0;
			button2 = TalkT;
			Daerel2.Talk = true;
		}

		public void ResetBools()
		{
			Daerel2.Shop = false;
			Daerel2.Talk = false;
			Daerel2.RestringBow = false;
			Daerel2.PoisonWeapon = false;
			Daerel2.Quest1 = false;
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			Player player = Main.LocalPlayer;
			player.GetModPlayer<RedePlayer>();
			if (firstButton)
			{
				this.ResetBools();
				Daerel2.ChatNumber++;
				if (Daerel2.ChatNumber > 4)
				{
					Daerel2.ChatNumber = 0;
					return;
				}
			}
			else
			{
				if (Daerel2.Talk)
				{
					Main.npcChatText = Daerel2.ChitChat();
					return;
				}
				if (Daerel2.ChatNumber == 0)
				{
					shop = true;
				}
				if (Daerel2.ChatNumber == 1)
				{
					Main.npcChatText = Daerel2.ChitChat();
					return;
				}
				if (Daerel2.ChatNumber == 2)
				{
					if (Main.LocalPlayer.BuyItem(1000, -1))
					{
						Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 37, 1f, 0f);
						Main.LocalPlayer.AddBuff(16, 36000, true);
						return;
					}
					Main.npcChatText = Daerel2.NoCoinsChat();
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					return;
				}
				else if (Daerel2.ChatNumber == 3)
				{
					if (Main.LocalPlayer.BuyItem(2500, -1))
					{
						Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 37, 1f, 0f);
						Main.LocalPlayer.AddBuff(79, 36000, true);
						return;
					}
					Main.npcChatText = Daerel2.NoCoinsChat();
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					return;
				}
				else if (Daerel2.ChatNumber == 4)
				{
					if (RedeQuests.daerelQuests == 5 && Main.hardMode)
					{
						Main.npcChatCornerItem = ModContent.ItemType<BottledLostSoulIcon>();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						int Bottle = player.FindItem(ModContent.ItemType<BottledLostSoul>());
						int Vacuum = player.FindItem(ModContent.ItemType<LostSoulCatcher>());
						if (Bottle >= 0)
						{
							player.inventory[Bottle].stack--;
							if (player.inventory[Bottle].stack <= 0)
							{
								player.inventory[Bottle] = new Item();
							}
							if (Vacuum >= 0)
							{
								player.inventory[Vacuum].stack--;
								if (player.inventory[Vacuum].stack <= 0)
								{
									player.inventory[Vacuum] = new Item();
								}
							}
							Main.npcChatCornerItem = ModContent.ItemType<DaerelRewardBag>();
							Main.npcChatText = this.Give1Chat();
							player.QuickSpawnItem(ModContent.ItemType<DaerelRewardBag>(), 1);
							RedeQuests.daerelQuests++;
							RedeQuests.DsoulQuest = true;
							if (Main.netMode != 0)
							{
								NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
							}
							Main.PlaySound(24, -1, -1, 1, 1f, 0f);
							return;
						}
						Main.npcChatText = this.QuestChat();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						return;
					}
					else if (RedeQuests.daerelQuests == 6 && Main.hardMode)
					{
						Main.npcChatCornerItem = ModContent.ItemType<InvisibleCloak>();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						int Cloak = player.FindItem(ModContent.ItemType<InvisibleCloak>());
						if (Cloak >= 0)
						{
							player.inventory[Cloak].stack--;
							if (player.inventory[Cloak].stack <= 0)
							{
								player.inventory[Cloak] = new Item();
							}
							Main.npcChatCornerItem = ModContent.ItemType<DaerelRewardBag>();
							Main.npcChatText = this.Give2Chat();
							player.QuickSpawnItem(ModContent.ItemType<DaerelRewardBag>(), 1);
							RedeQuests.daerelQuests++;
							RedeQuests.DcloakQuest = true;
							if (Main.netMode != 0)
							{
								NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
							}
							Main.PlaySound(24, -1, -1, 1, 1f, 0f);
							return;
						}
						Main.npcChatText = this.QuestChat();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						return;
					}
					else if (RedeQuests.daerelQuests == 7 && Main.hardMode)
					{
						Main.npcChatCornerItem = ModContent.ItemType<Parthius>();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						int ParthiusBow = player.FindItem(ModContent.ItemType<Parthius>());
						if (ParthiusBow >= 0)
						{
							player.inventory[ParthiusBow].stack--;
							if (player.inventory[ParthiusBow].stack <= 0)
							{
								player.inventory[ParthiusBow] = new Item();
							}
							Main.npcChatCornerItem = ModContent.ItemType<DaerelRewardBag>();
							Main.npcChatText = this.Give3Chat();
							player.QuickSpawnItem(ModContent.ItemType<DaerelRewardBag>(), 1);
							RedeQuests.daerelQuests++;
							RedeQuests.DparthQuest = true;
							if (Main.netMode != 0)
							{
								NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
							}
							Main.PlaySound(24, -1, -1, 1, 1f, 0f);
							return;
						}
						Main.npcChatText = this.QuestChat();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						return;
					}
					else if (RedeQuests.daerelQuests == 8 && Main.hardMode)
					{
						Main.npcChatCornerItem = ModContent.ItemType<EnchantedMap>();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						int Map = player.FindItem(ModContent.ItemType<EnchantedMap>());
						if (Map >= 0)
						{
							player.inventory[Map].stack--;
							if (player.inventory[Map].stack <= 0)
							{
								player.inventory[Map] = new Item();
							}
							Main.npcChatCornerItem = ModContent.ItemType<DaerelRewardBag>();
							Main.npcChatText = this.Give4Chat();
							player.QuickSpawnItem(ModContent.ItemType<DaerelRewardBag>(), 1);
							RedeQuests.daerelQuests++;
							RedeQuests.DmapQuest = true;
							if (Main.netMode != 0)
							{
								NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
							}
							Main.PlaySound(24, -1, -1, 1, 1f, 0f);
							return;
						}
						Main.npcChatText = this.QuestChat();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						return;
					}
					else if (RedeQuests.daerelQuests == 9 && Main.hardMode)
					{
						Main.npcChatCornerItem = ModContent.ItemType<DarkSteelBow>();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						int DarkBow = player.FindItem(ModContent.ItemType<DarkSteelBow>());
						if (DarkBow >= 0)
						{
							player.inventory[DarkBow].stack--;
							if (player.inventory[DarkBow].stack <= 0)
							{
								player.inventory[DarkBow] = new Item();
							}
							Main.npcChatCornerItem = ModContent.ItemType<DaerelRewardBag>();
							Main.npcChatText = this.Give5Chat();
							player.QuickSpawnItem(ModContent.ItemType<DaerelRewardBag>(), 3);
							RedeQuests.daerelQuests++;
							RedeQuests.DslimeQuest = true;
							if (Main.netMode != 0)
							{
								NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
							}
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
			if (RedeQuests.daerelQuests == 5 && Main.hardMode)
			{
				chat.Add("I've started seeing lost souls out and about, I'm not sure what this means but could you catch one for me to keep? I'm selling a vacuum-sealed bottle for free so have that with you when catching a Lost Soul with a Bug Net.", 1.0);
			}
			else if (RedeQuests.daerelQuests == 6 && Main.hardMode)
			{
				chat.Add("I've been starting to learn magic, and I've recently discovered I can only use Shadow Magic. But I got no idea how magic works, like I know magic is an extension to ones soul, but how would I, say, become invisible? Anyway, while I'm learning, could you make me an invisible cloak? It will be useful for sneaking around, and other reasons.", 1.0);
			}
			else if (RedeQuests.daerelQuests == 7 && Main.hardMode)
			{
				chat.Add("I'm not too sure what to make you do, maybe something you could craft for me to sell for money. Actually, how about a fancy golden bow with gems in it? I can sell it or use it, so I see no reason not to make it.", 1.0);
			}
			else if (RedeQuests.daerelQuests == 8 && Main.hardMode)
			{
				chat.Add("I've been exploring this land for a while now, but I often get lost. There are no maps of this island from what I've seen, so how about making a map for me? Not just any map, but an Enchanted Map, one that gets bigger the more land I discover! Imagine that.", 1.0);
			}
			else if (RedeQuests.daerelQuests == 9 && Main.hardMode)
			{
				chat.Add("I got good news and bad news, good news is I finally found Zephos, bad news is he's trapped in a giant slime... I found it in the corruption... or was it crimson? Uh, I'll just say evil biome. I need you to find it and kill it so Zephos can escape, also because it ate my new bow. I'll sell an item to lure it to you quicker.", 1.0);
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
			weightedRandom.Add("Interesting, I'll keep this on me and study it.", 1.0);
			return weightedRandom;
		}

		public string Give2Chat()
		{
			WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
			weightedRandom.Add("Oh cool you actually made one. By the way, when I said 'other reasons', it's not what you think I swear. I meant like pranking Zephos when I see him again, not anything bad!", 1.0);
			return weightedRandom;
		}

		public string Give3Chat()
		{
			WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
			weightedRandom.Add("That is certainly a fancy bow, and now it's mine, haha. Thanks for making it.", 1.0);
			return weightedRandom;
		}

		public string Give4Chat()
		{
			WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
			weightedRandom.Add("Thanks for the map, I'll use it to help me find Zephos, he's gotta be around here somewhere...", 1.0);
			return weightedRandom;
		}

		public string Give5Chat()
		{
			WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
			weightedRandom.Add("Zephos is saved? Good job! And you got my bow back? Nice, here are 3 reward bags.", 1.0);
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
			if (RedeQuests.daerelQuests == 9 && Main.hardMode)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<EvilJelly>(), false);
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
			if (RedeQuests.daerelQuests == 5 && Main.hardMode)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<LostSoulCatcher>(), false);
				nextSlot++;
			}
		}

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 11;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 5;
			randExtraCooldown = 5;
		}

		public override void DrawTownAttackGun(ref float scale, ref int item, ref int closeness)
		{
			scale = 1f;
			item = ModContent.ItemType<SilverwoodBow>();
			closeness = 20;
		}

		public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
		{
			projType = ModContent.ProjectileType<SilverwoodArrowPro>();
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
