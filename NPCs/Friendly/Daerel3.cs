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
	public class Daerel3 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dark Hunter");
			Main.npcFrameCount[base.npc.type] = 26;
			NPCID.Sets.ExtraFramesCount[base.npc.type] = 5;
			NPCID.Sets.AttackFrameCount[base.npc.type] = 5;
			NPCID.Sets.DangerDetectRange[base.npc.type] = 1400;
			NPCID.Sets.AttackType[base.npc.type] = 1;
			NPCID.Sets.AttackTime[base.npc.type] = 30;
			NPCID.Sets.AttackAverageChance[base.npc.type] = 30;
			NPCID.Sets.HatOffsetY[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.townNPC = true;
			base.npc.friendly = true;
			base.npc.width = 26;
			base.npc.height = 48;
			base.npc.aiStyle = 7;
			base.npc.damage = 140;
			base.npc.defense = 15;
			base.npc.lifeMax = 500;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.knockBackResist = 0.4f;
			this.animationType = 22;
		}

		public override bool CheckDead()
		{
			Main.NewText("Daerel the Dark Hunter was knocked unconscious...", Color.Red.R, Color.Red.G, Color.Red.B, false);
			base.npc.SetDefaults(ModContent.NPCType<Daerel3Unconscious>(), -1f);
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
			switch (Main.rand.Next(7))
			{
			case 0:
				return "Need anything? I can restring your bow, or envenom your weapon. It'll cost you though.";
			case 1:
				return "You don't mind me staying here, right?";
			case 2:
				return "I got some pretty nice loot I can sell you.";
			case 3:
				return "My favourite colour is green, not sure why I'm telling you though...";
			case 4:
				return "Cats are obviously superior than dogs.";
			case 5:
				return "I've been travelling this land for a while, but staying in a house is nice.";
			default:
				return "Hello there.";
			}
		}

		public override void ResetEffects()
		{
			Daerel3.SwitchInfo = false;
			Daerel3.Shop = false;
			Daerel3.Talk = false;
			Daerel3.RestringBow = false;
			Daerel3.VenomWeapon = false;
			Daerel3.Quest1 = false;
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			string SwitchInfoT = "Cycle Options";
			string ShopT = "Shop";
			string TalkT = "Talk";
			string RestringT = "Restring Bow (10 silver)";
			string VenomT = "Envenom Weapon (50 silver)";
			string QuestT = "Quest";
			button = SwitchInfoT;
			if (Daerel3.ChatNumber == 0)
			{
				button2 = ShopT;
				Daerel3.Shop = true;
				return;
			}
			if (Daerel3.ChatNumber == 1)
			{
				button2 = TalkT;
				Daerel3.Talk = true;
				return;
			}
			if (Daerel3.ChatNumber == 2)
			{
				button2 = RestringT;
				Daerel3.RestringBow = true;
				return;
			}
			if (Daerel3.ChatNumber == 3)
			{
				button2 = VenomT;
				Daerel3.VenomWeapon = true;
				return;
			}
			if (Daerel3.ChatNumber == 4)
			{
				button2 = QuestT;
				Daerel3.Quest1 = true;
				return;
			}
			Daerel3.ChatNumber = 0;
			button2 = TalkT;
			Daerel3.Talk = true;
		}

		public void ResetBools()
		{
			Daerel3.Shop = false;
			Daerel3.Talk = false;
			Daerel3.RestringBow = false;
			Daerel3.VenomWeapon = false;
			Daerel3.Quest1 = false;
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			Player player = Main.LocalPlayer;
			player.GetModPlayer<RedePlayer>();
			if (firstButton)
			{
				this.ResetBools();
				Daerel3.ChatNumber++;
				if (Daerel3.ChatNumber > 4)
				{
					Daerel3.ChatNumber = 0;
					return;
				}
			}
			else
			{
				if (Daerel3.Talk)
				{
					Main.npcChatText = Daerel3.ChitChat();
					return;
				}
				if (Daerel3.ChatNumber == 0)
				{
					shop = true;
				}
				if (Daerel3.ChatNumber == 1)
				{
					Main.npcChatText = Daerel3.ChitChat();
					return;
				}
				if (Daerel3.ChatNumber == 2)
				{
					if (Main.LocalPlayer.BuyItem(1000, -1))
					{
						Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 37, 1f, 0f);
						Main.LocalPlayer.AddBuff(16, 36000, true);
						return;
					}
					Main.npcChatText = Daerel3.NoCoinsChat();
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					return;
				}
				else if (Daerel3.ChatNumber == 3)
				{
					if (Main.LocalPlayer.BuyItem(5000, -1))
					{
						Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 37, 1f, 0f);
						Main.LocalPlayer.AddBuff(71, 36000, true);
						return;
					}
					Main.npcChatText = Daerel3.NoCoinsChat();
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					return;
				}
				else if (Daerel3.ChatNumber == 4 && RedeQuests.daerelQuests == 9 && Main.hardMode)
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
						Main.npcChatText = this.GiveSChat();
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
				}
			}
		}

		public static string NoCoinsChat()
		{
			WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
			weightedRandom.Add("You still poor?", 1.0);
			weightedRandom.Add("You really don't have enough money? Understandable.", 1.0);
			return weightedRandom;
		}

		public string QuestChat()
		{
			Main.LocalPlayer.GetModPlayer<RedePlayer>();
			WeightedRandom<string> chat = new WeightedRandom<string>();
			if (RedeQuests.daerelQuests == 9 && Main.hardMode)
			{
				chat.Add("I got good news and bad news, good news is I finally found Zephos, bad news is he's trapped in a giant slime... I found it in the corruption... or was it crimson? Uh, I'll just say evil biome. I need you to find it and kill it so Zephos can escape, also because it ate my new bow. I'll sell an item to lure it to you quicker.", 1.0);
			}
			else
			{
				chat.Add("I can't think of any quests right now.", 1.0);
			}
			return chat;
		}

		public string GiveSChat()
		{
			WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
			weightedRandom.Add("Zephos is saved? Good job! And you got my bow back? Nice, here are 3 reward bags.", 1.0);
			return weightedRandom;
		}

		public static string ChitChat()
		{
			WeightedRandom<string> chat = new WeightedRandom<string>();
			chat.Add("One time me and Zephos were in a cave, and then a skeleton with flowers stuck in its ribcage appeared. Zephos thought it was a powerful druid skeleton. He likes to exaggerate. It didn't have any magic, it was just a normal skeleton.", 1.0);
			chat.Add("If you wanna find Leaf Beetles, or Tree Bugs as they're called here, then chop down some trees. They live on tree tops, their leaf-green shell camouflaging them in the foliage. They eat the bark off of trees, and if their tree is destroyed or rotted, it will climb down and find another suitable tree to live on.", 1.0);
			chat.Add("Cool Bug Fact: Coast Scarabs are small beetles that live on sandy beaches and eat grains of sand as their primary diet. When wet, their cyan shells will shine. Their shell is normally used to make cyan dyes.", 1.0);
			chat.Add("Cool Bug Fact: Sandskin Spiders live in deserts, roaming around at night when other tiny insects come out to eat. When the hot day arrives, the spider will borrow a feet under the sand to sleep. Yes, I like bugs.", 1.0);
			chat.Add("How did I get here? Me and Zephos were on a boat looking for interesting islands, but then a storm came to ruin our day. We must've drifted here, but I don't know where Zephos is.", 1.0);
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
			if (RedeQuests.DcloakQuest)
			{
				chat.Add("So Zephos noticed my invisible cloak you gave me, and now he wants to 'borrow' it for 'important reasons'. I think you and I know why he wants it.", 1.0);
			}
			if (RedeQuests.DpotionQuest)
			{
				chat.Add("I tried that Ultimate Potion you gave me, tasted disgusting. I think I'll just stick to regular potions.", 1.0);
			}
			chat.Add("Have you ever been to the jungles of Erellon? There are some ferocious creatures there, everything wants to eat anything that moves, even plants can kill you! This island's jungle is a lot more tame. Apparently there's a species of half-cat humans called Nekums in Erellon's jungles. Knowing that kinda makes me wanna explore there...", 1.0);
			chat.Add("Do you know about Wigglefloofs? They are like really REALLY fluffy cats, practically shaped like a ball. I want one, but they are only found in Ithon.", 1.0);
			chat.Add("My Dark-Steel Bow was crafted by Raiktu Shadeheart, he's a legendary smith in Erellon. I won it once in a competition and thats how I got it.", 1.0);
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
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<SilverwoodBow>(), false);
			nextSlot++;
			if (NPC.downedMoonlord)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Whisperwind>(), false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(3540, false);
				nextSlot++;
			}
			if (NPC.downedFishron)
			{
				shop.item[nextSlot].SetDefaults(2624, false);
				nextSlot++;
			}
			shop.item[nextSlot].SetDefaults(1802, false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(1321, false);
			nextSlot++;
		}

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 120;
			knockback = 5f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 20;
			randExtraCooldown = 10;
		}

		public override void DrawTownAttackGun(ref float scale, ref int item, ref int closeness)
		{
			scale = 1f;
			item = ModContent.ItemType<DarkSteelBow>();
			closeness = 20;
		}

		public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
		{
			projType = ModContent.ProjectileType<DarkSteelArrow>();
			attackDelay = 1;
		}

		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
		{
			multiplier = 12f;
		}

		public static bool SwitchInfo;

		public static bool Shop;

		public static bool Talk;

		public static bool RestringBow;

		public static bool VenomWeapon;

		public static bool Quest1;

		public static int ChatNumber;
	}
}
