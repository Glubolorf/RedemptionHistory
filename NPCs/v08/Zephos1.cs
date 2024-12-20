using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs;
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
	public class Zephos1 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Wayfarer");
			Main.npcFrameCount[base.npc.type] = 25;
			NPCID.Sets.ExtraFramesCount[base.npc.type] = 5;
			NPCID.Sets.AttackFrameCount[base.npc.type] = 5;
			NPCID.Sets.DangerDetectRange[base.npc.type] = 80;
			NPCID.Sets.AttackType[base.npc.type] = 3;
			NPCID.Sets.AttackTime[base.npc.type] = 30;
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
			if (RedeQuests.zephosQuests > 4 || RedeQuests.daerelQuests > 4)
			{
				base.npc.Transform(ModContent.NPCType<Zephos2>());
			}
		}

		public override bool CheckDead()
		{
			Main.NewText("Zephos the Wayfarer was knocked unconscious...", Color.Red.R, Color.Red.G, Color.Red.B, false);
			base.npc.SetDefaults(ModContent.NPCType<ZepUnconscious>(), -1f);
			return false;
		}

		public override bool CanTownNPCSpawn(int numTownNPCs, int money)
		{
			return !WorldGen.crimson && !NPC.AnyNPCs(ModContent.NPCType<ZepUnconscious>()) && !NPC.AnyNPCs(ModContent.NPCType<Zep2Unconscious>()) && !NPC.AnyNPCs(ModContent.NPCType<Zep3Unconscious>()) && !NPC.AnyNPCs(ModContent.NPCType<Zephos2>()) && !NPC.AnyNPCs(ModContent.NPCType<Zephos3>());
		}

		public override string TownNPCName()
		{
			return "Zephos";
		}

		public override void FindFrame(int frameHeight)
		{
		}

		public override string GetChat()
		{
			int DryadID = NPC.FindFirstNPC(20);
			if (DryadID >= 0 && Main.rand.Next(8) == 0)
			{
				return "Doesn't " + Main.npc[DryadID].GivenName + " know how to put clothes on? Whatever, I like it!";
			}
			switch (Main.rand.Next(8))
			{
			case 0:
				return "How's it goin' bro!";
			case 1:
				return "Hey I was washed ashore on this island, but you don't mind me staying here, right?";
			case 2:
				return "Yo, I have some pretty cool things, you can have them if you got the money.";
			case 3:
				return "My favourite colour is orange! Donno why I'm tellin' ya though...";
			case 4:
				return "I don't know what the deal with cats are. Dogs are definitely better!";
			case 5:
				return "I've been travelling this land for a while, but staying in a house is nice.";
			case 6:
				return "Have you seen a guy in a cloak, he carries a bow around. I've lost him while travelling to this island, hope he's alright.";
			default:
				return "'Ey bro!";
			}
		}

		public override void ResetEffects()
		{
			Zephos1.SwitchInfo = false;
			Zephos1.Shop = false;
			Zephos1.Talk = false;
			Zephos1.Sharpen = false;
			Zephos1.ShineArmour = false;
			Zephos1.Quest1 = false;
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			string SwitchInfoT = "Cycle Options";
			string ShopT = "Shop";
			string TalkT = "Talk";
			string SharpenT = "Sharpen (5 silver)";
			string ShineArmourT = "Shine Armor (15 silver)";
			string QuestT = "Quest";
			button = SwitchInfoT;
			if (Zephos1.ChatNumber == 0)
			{
				button2 = ShopT;
				Zephos1.Shop = true;
				return;
			}
			if (Zephos1.ChatNumber == 1)
			{
				button2 = TalkT;
				Zephos1.Talk = true;
				return;
			}
			if (Zephos1.ChatNumber == 2)
			{
				button2 = SharpenT;
				Zephos1.Sharpen = true;
				return;
			}
			if (Zephos1.ChatNumber == 3)
			{
				button2 = ShineArmourT;
				Zephos1.ShineArmour = true;
				return;
			}
			if (Zephos1.ChatNumber == 4)
			{
				button2 = QuestT;
				Zephos1.Quest1 = true;
				return;
			}
			Zephos1.ChatNumber = 0;
			button2 = TalkT;
			Zephos1.Talk = true;
		}

		public void ResetBools()
		{
			Zephos1.Shop = false;
			Zephos1.Talk = false;
			Zephos1.Sharpen = false;
			Zephos1.ShineArmour = false;
			Zephos1.Quest1 = false;
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			Player player = Main.LocalPlayer;
			player.GetModPlayer<RedePlayer>();
			if (firstButton)
			{
				this.ResetBools();
				Zephos1.ChatNumber++;
				if (Zephos1.ChatNumber > 4)
				{
					Zephos1.ChatNumber = 0;
					return;
				}
			}
			else
			{
				if (Zephos1.Talk)
				{
					Main.npcChatText = Zephos1.ChitChat();
					return;
				}
				if (Zephos1.ChatNumber == 0)
				{
					shop = true;
				}
				if (Zephos1.ChatNumber == 1)
				{
					Main.npcChatText = Zephos1.ChitChat();
					return;
				}
				if (Zephos1.ChatNumber == 2)
				{
					if (Main.LocalPlayer.BuyItemOld(500))
					{
						Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 37, 1f, 0f);
						Main.LocalPlayer.AddBuff(159, 36000, true);
						return;
					}
					Main.npcChatText = Zephos1.NoCoinsChat();
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					return;
				}
				else if (Zephos1.ChatNumber == 3)
				{
					if (Main.LocalPlayer.BuyItemOld(1500))
					{
						Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 37, 1f, 0f);
						Main.LocalPlayer.AddBuff(ModContent.BuffType<ShineArmourBuff>(), 36000, true);
						return;
					}
					Main.npcChatText = Zephos1.NoCoinsChat();
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					return;
				}
				else if (Zephos1.ChatNumber == 4)
				{
					if (RedeQuests.zephosQuests == 0)
					{
						Main.npcChatCornerItem = ModContent.ItemType<BucketOfChicken>();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						int ChickenBucket = player.FindItem(ModContent.ItemType<BucketOfChicken>());
						if (ChickenBucket >= 0)
						{
							player.inventory[ChickenBucket].stack--;
							if (player.inventory[ChickenBucket].stack <= 0)
							{
								player.inventory[ChickenBucket] = new Item();
							}
							Main.npcChatCornerItem = ModContent.ItemType<ZephosRewardBag>();
							Main.npcChatText = this.Give1Chat();
							player.QuickSpawnItem(ModContent.ItemType<ZephosRewardBag>(), 1);
							player.QuickSpawnItem(ModContent.ItemType<FriedChicken>(), 1);
							RedeQuests.zephosQuests++;
							RedeQuests.ZchickenQuest = true;
							Main.PlaySound(24, -1, -1, 1, 1f, 0f);
							return;
						}
						Main.npcChatText = this.QuestChat();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						return;
					}
					else if (RedeQuests.zephosQuests == 1)
					{
						Main.npcChatCornerItem = ModContent.ItemType<RubySkull>();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						int RedSkull = player.FindItem(ModContent.ItemType<RubySkull>());
						if (RedSkull >= 0)
						{
							player.inventory[RedSkull].stack--;
							if (player.inventory[RedSkull].stack <= 0)
							{
								player.inventory[RedSkull] = new Item();
							}
							Main.npcChatCornerItem = ModContent.ItemType<ZephosRewardBag>();
							Main.npcChatText = this.Give2Chat();
							player.QuickSpawnItem(ModContent.ItemType<ZephosRewardBag>(), 1);
							RedeQuests.zephosQuests++;
							RedeQuests.ZskullQuest = true;
							Main.PlaySound(24, -1, -1, 1, 1f, 0f);
							return;
						}
						Main.npcChatText = this.QuestChat();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						return;
					}
					else if (RedeQuests.zephosQuests == 2)
					{
						Main.npcChatCornerItem = ModContent.ItemType<UltimateDish>();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						int Dish = player.FindItem(ModContent.ItemType<UltimateDish>());
						if (Dish >= 0)
						{
							player.inventory[Dish].stack--;
							if (player.inventory[Dish].stack <= 0)
							{
								player.inventory[Dish] = new Item();
							}
							Main.npcChatCornerItem = ModContent.ItemType<ZephosRewardBag>();
							Main.npcChatText = this.Give3Chat();
							player.QuickSpawnItem(ModContent.ItemType<ZephosRewardBag>(), 1);
							RedeQuests.zephosQuests++;
							RedeQuests.ZdishQuest = true;
							Main.PlaySound(24, -1, -1, 1, 1f, 0f);
							return;
						}
						Main.npcChatText = this.QuestChat();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						return;
					}
					else if (RedeQuests.zephosQuests == 3)
					{
						Main.npcChatCornerItem = ModContent.ItemType<VepdorHat>();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						int VepdorGear = player.FindItem(ModContent.ItemType<VepdorHat>());
						if (VepdorGear >= 0)
						{
							player.inventory[VepdorGear].stack--;
							if (player.inventory[VepdorGear].stack <= 0)
							{
								player.inventory[VepdorGear] = new Item();
							}
							Main.npcChatCornerItem = ModContent.ItemType<ZephosRewardBag>();
							Main.npcChatText = this.Give4Chat();
							player.QuickSpawnItem(ModContent.ItemType<ZephosRewardBag>(), 1);
							RedeQuests.zephosQuests++;
							RedeQuests.ZvepdorQuest = true;
							Main.PlaySound(24, -1, -1, 1, 1f, 0f);
							return;
						}
						Main.npcChatText = this.QuestChat();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						return;
					}
					else if (RedeQuests.zephosQuests == 4)
					{
						Main.npcChatCornerItem = ModContent.ItemType<SwordSlicer>();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						int Sword = player.FindItem(ModContent.ItemType<SwordSlicer>());
						if (Sword >= 0)
						{
							player.inventory[Sword].stack--;
							if (player.inventory[Sword].stack <= 0)
							{
								player.inventory[Sword] = new Item();
							}
							Main.npcChatCornerItem = ModContent.ItemType<ZephosRewardBag>();
							Main.npcChatText = this.Give5Chat();
							player.QuickSpawnItem(ModContent.ItemType<ZephosRewardBag>(), 3);
							RedeQuests.zephosQuests++;
							RedeQuests.ZswordQuest = true;
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
			if (RedeQuests.zephosQuests == 0)
			{
				chat.Add("Quest? Normally I'm the one who needs quests, cos' I'm as poor as a chicken! Oh! That gave me an idea, give me a bucket of chicken! When a chicken dies while they're on fire, they'll drop Fried Chicken! You'll also need some wood for the bucket.", 1.0);
			}
			else if (RedeQuests.zephosQuests == 1)
			{
				chat.Add("Alright so, I asked this travelling merchant if I could do any quests for some coin, 'cos he looked rich, and he was like 'You could find me a relic known as the Skull of Bloodstone.' Apparently it's some enchanted skull with ruby eyes, and he'd give me a giant reward if I found it. So, since I got no idea where it is, just find me a skull and put 2 rubies in it's eyes. That could fool him, and I get lots-a-money!", 1.0);
			}
			else if (RedeQuests.zephosQuests == 2)
			{
				chat.Add("That bucket of chicken was great, could you get me another one? Wait, no, that's not enough. Oh how about this, Make me something big! Like the ultimate dish! Cooked Shrimp, Sashimi, Pho, and Pad Thai all combined into one! ... What are you looking at? I'm hungry alright!", 1.0);
			}
			else if (RedeQuests.zephosQuests == 3)
			{
				chat.Add("I got this contract to kill some undead dude called 'Vepdor', apparently the person knew him before he died, and wants his headgear as a thing to remember. Why don't I do it for once? Well, it's for... personal reasons. I don't want to face another skeleton for a while. So when you're in the caverns, look out for a skeleton wearing strange headgear and black & purple clothing. Cheers.\n\n(Completable after The Keeper is defeated)", 1.0);
			}
			else if (RedeQuests.zephosQuests == 4)
			{
				chat.Add("I recently got a cool new sword, I call it the Slicer, but unfortunately it broke while fighting a great beast! The fragments of it are still stuck in it's extra thick hide. I'm asking for a lot, but without my cool sword I'm basically useless, could you find it and slay it? It'll be in the jungle area. It's big so won't be too hard to find.\n\n(Completable after The Keeper is defeated)", 1.0);
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
			weightedRandom.Add("HECK YEAH, CHICKEN! Thank ya, bro! Here's something for ya. It's a goodie bag with random stuff I found on my adventures. Oh, you want some chicken too? I'll give you a piece!", 1.0);
			return weightedRandom;
		}

		public string Give2Chat()
		{
			WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
			weightedRandom.Add("That looks like an 'enchanted skull' to me! Thanks! I got something for you too. I'm giving you a reward for making me get a reward, reward-ception.", 1.0);
			return weightedRandom;
		}

		public string Give3Chat()
		{
			WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
			weightedRandom.Add("Woah! You actually did it. Thank ya, bro! Travelling around can be tiring so I need big meals like these.", 1.0);
			return weightedRandom;
		}

		public string Give4Chat()
		{
			WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
			weightedRandom.Add("Thanks dude, I'll give the person the headgear and I can get some sweet money!", 1.0);
			return weightedRandom;
		}

		public string Give5Chat()
		{
			WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
			weightedRandom.Add("You did it! Nice, I got my sword back. Now I can kill things without your help!", 1.0);
			return weightedRandom;
		}

		public static string ChitChat()
		{
			WeightedRandom<string> chat = new WeightedRandom<string>();
			chat.Add("How about I tell you the time I was a pirate, sailing abroad the vast ocean with fellow pirate people... Actually, I don't remeber a lot about being a pirate. I was very young at the time.", 1.0);
			chat.Add("I'm doin' good, although I've lost someone, his name is Daerel and wears a cloak. I'm sure I'll find him eventually.", 1.0);
			chat.Add("Did I ever tell you about my victory against a powerful undead druid? It was a close match, it was giant, and its magic was insane! But yeah, I beat it, pretty cool huh? It had flowers growing everywhere on it!", 1.0);
			chat.Add("How did I get here, I hear you asking? Me and Daerel were on a boat looking for cool islands, but then a storm came! We must've drifted here, don't know where Daerel is.", 1.0);
			if (Main.moonPhase == 0 && !Main.dayTime)
			{
				chat.Add("It was midnight when I woke up on this island's beach. Full moon, mist, an eerie feeling, all the things for a spooky night. When I looked towards the sea, I swear I could see a ghost! It was a dude with a soggy beard and pale-green skin, he also had a cutlass. Did I fight him? No, ghosts are... uh... not my type of opponent! Yeah, that.", 1.0);
			}
			chat.Add("This island's gotta lotta chickens! Ever wonder where they came from? Back in Anglon, there are way deadlier chickens, called Anglonic Forest Hens. Funny story, I was with Daerel on one of his walks through the forest, then out of nowhere a giant hen charges through the bushes straight at him! I've never seen him run so fast!", 1.0);
			chat.Add("I swear I saw a Blobble around here. I didn't expect them to be here, they're native to, uh, Ithon I think. Don't quote me on that though, Daerel's a lot better at remembering useless info than I.", 1.0);
			if (!Main.dayTime)
			{
				chat.Add("You never told me there'd be undead here! What, they're called zombies? Well where I'm from they're called undead. There's also a few skeletons out here, normally they like to stay underground. This island is pretty weird. How do you live here?", 1.0);
			}
			if (!Main.dayTime && !RedeConfigClient.Instance.NoSpidersInMyTerrariaMod)
			{
				chat.Add("I've seen some forest spiders here, but never any Greater Forest Spiders, although that's probably a good thing. Greater Forest Spiders are like normal forest spiders, but they're massive! Thank Oysus they aren't on this island.", 1.0);
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
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<NoblesSword>(), false);
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
			damage = 28;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 30;
			randExtraCooldown = 30;
		}

		public override void DrawTownAttackSwing(ref Texture2D item, ref int itemSize, ref float scale, ref Vector2 offset)
		{
			scale = 1f;
			item = Main.itemTexture[ModContent.ItemType<NoblesSword>()];
			itemSize = 36;
		}

		public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight)
		{
			itemWidth = 36;
			itemHeight = 34;
		}

		public static bool SwitchInfo;

		public static bool Shop;

		public static bool Talk;

		public static bool Sharpen;

		public static bool ShineArmour;

		public static bool Quest1;

		public static int ChatNumber;
	}
}
