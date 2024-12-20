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
	public class Zephos2 : ModNPC
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
			base.npc.damage = 26;
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
				base.npc.Transform(ModContent.NPCType<Zephos3>());
			}
		}

		public override bool CheckDead()
		{
			Main.NewText("Zephos the Wayfarer was knocked unconscious...", Color.Red.R, Color.Red.G, Color.Red.B, false);
			base.npc.SetDefaults(ModContent.NPCType<Zep2Unconscious>(), -1f);
			return false;
		}

		public override bool CanTownNPCSpawn(int numTownNPCs, int money)
		{
			return false;
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
				return "Hey! I got some new armour, how do I look?";
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
			Zephos2.SwitchInfo = false;
			Zephos2.Shop = false;
			Zephos2.Talk = false;
			Zephos2.Sharpen = false;
			Zephos2.ShineArmour = false;
			Zephos2.Quest1 = false;
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			string SwitchInfoT = "Cycle Options";
			string ShopT = "Shop";
			string TalkT = "Talk";
			string SharpenT = "Sharpen II (25 silver)";
			string ShineArmourT = "Shine Armor II (80 silver)";
			string QuestT = "Quest";
			button = SwitchInfoT;
			if (Zephos2.ChatNumber == 0)
			{
				button2 = ShopT;
				Zephos2.Shop = true;
				return;
			}
			if (Zephos2.ChatNumber == 1)
			{
				button2 = TalkT;
				Zephos2.Talk = true;
				return;
			}
			if (Zephos2.ChatNumber == 2)
			{
				button2 = SharpenT;
				Zephos2.Sharpen = true;
				return;
			}
			if (Zephos2.ChatNumber == 3)
			{
				button2 = ShineArmourT;
				Zephos2.ShineArmour = true;
				return;
			}
			if (Zephos2.ChatNumber == 4)
			{
				button2 = QuestT;
				Zephos2.Quest1 = true;
				return;
			}
			Zephos2.ChatNumber = 0;
			button2 = TalkT;
			Zephos2.Talk = true;
		}

		public void ResetBools()
		{
			Zephos2.Shop = false;
			Zephos2.Talk = false;
			Zephos2.Sharpen = false;
			Zephos2.ShineArmour = false;
			Zephos2.Quest1 = false;
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			Player player = Main.LocalPlayer;
			player.GetModPlayer<RedePlayer>();
			if (firstButton)
			{
				this.ResetBools();
				Zephos2.ChatNumber++;
				if (Zephos2.ChatNumber > 4)
				{
					Zephos2.ChatNumber = 0;
					return;
				}
			}
			else
			{
				if (Zephos2.Talk)
				{
					Main.npcChatText = Zephos2.ChitChat();
					return;
				}
				if (Zephos2.ChatNumber == 0)
				{
					shop = true;
				}
				if (Zephos2.ChatNumber == 1)
				{
					Main.npcChatText = Zephos2.ChitChat();
					return;
				}
				if (Zephos2.ChatNumber == 2)
				{
					if (Main.LocalPlayer.BuyItemOld(2500))
					{
						Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 37, 1f, 0f);
						Main.LocalPlayer.AddBuff(ModContent.BuffType<Sharpen2Buff>(), 36000, true);
						return;
					}
					Main.npcChatText = Zephos2.NoCoinsChat();
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					return;
				}
				else if (Zephos2.ChatNumber == 3)
				{
					if (Main.LocalPlayer.BuyItemOld(8000))
					{
						Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 37, 1f, 0f);
						Main.LocalPlayer.AddBuff(ModContent.BuffType<ShineArmour2Buff>(), 36000, true);
						return;
					}
					Main.npcChatText = Zephos2.NoCoinsChat();
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					return;
				}
				else if (Zephos2.ChatNumber == 4)
				{
					if (RedeQuests.zephosQuests == 5 && Main.hardMode)
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
							Main.npcChatCornerItem = ModContent.ItemType<ZephosRewardBag>();
							Main.npcChatText = this.Give1Chat();
							player.QuickSpawnItem(ModContent.ItemType<ZephosRewardBag>(), 1);
							RedeQuests.zephosQuests++;
							RedeQuests.ZsoulQuest = true;
							Main.PlaySound(24, -1, -1, 1, 1f, 0f);
							return;
						}
						Main.npcChatText = this.QuestChat();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						return;
					}
					else if (RedeQuests.zephosQuests == 6 && Main.hardMode)
					{
						Main.npcChatCornerItem = ModContent.ItemType<AdamantiteSheath>();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						int Sheath = player.FindItem(ModContent.ItemType<AdamantiteSheath>());
						int Sheath2 = player.FindItem(ModContent.ItemType<TitaniumSwordSheath>());
						if (Sheath >= 0 || Sheath2 >= 0)
						{
							if (Sheath >= 0)
							{
								player.inventory[Sheath].stack--;
								if (player.inventory[Sheath].stack <= 0)
								{
									player.inventory[Sheath] = new Item();
								}
							}
							if (Sheath2 >= 0)
							{
								player.inventory[Sheath2].stack--;
								if (player.inventory[Sheath2].stack <= 0)
								{
									player.inventory[Sheath2] = new Item();
								}
							}
							Main.npcChatCornerItem = ModContent.ItemType<ZephosRewardBag>();
							Main.npcChatText = this.Give2Chat();
							player.QuickSpawnItem(ModContent.ItemType<ZephosRewardBag>(), 1);
							RedeQuests.zephosQuests++;
							RedeQuests.ZsheathQuest = true;
							Main.PlaySound(24, -1, -1, 1, 1f, 0f);
							return;
						}
						Main.npcChatText = this.QuestChat();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						return;
					}
					else if (RedeQuests.zephosQuests == 7 && Main.hardMode)
					{
						Main.npcChatCornerItem = ModContent.ItemType<Zweihander>();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						int Zweihander = player.FindItem(ModContent.ItemType<Zweihander>());
						if (Zweihander >= 0)
						{
							player.inventory[Zweihander].stack--;
							if (player.inventory[Zweihander].stack <= 0)
							{
								player.inventory[Zweihander] = new Item();
							}
							Main.npcChatCornerItem = ModContent.ItemType<ZephosRewardBag>();
							Main.npcChatText = this.Give3Chat();
							player.QuickSpawnItem(ModContent.ItemType<ZephosRewardBag>(), 1);
							RedeQuests.zephosQuests++;
							RedeQuests.ZzweiQuest = true;
							Main.PlaySound(24, -1, -1, 1, 1f, 0f);
							return;
						}
						Main.npcChatText = this.QuestChat();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						return;
					}
					else if (RedeQuests.zephosQuests == 8 && Main.hardMode)
					{
						Main.npcChatCornerItem = ModContent.ItemType<CouragePotion>();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						int Potion = player.FindItem(ModContent.ItemType<CouragePotion>());
						if (Potion >= 0)
						{
							player.inventory[Potion].stack--;
							if (player.inventory[Potion].stack <= 0)
							{
								player.inventory[Potion] = new Item();
							}
							Main.npcChatCornerItem = ModContent.ItemType<ZephosRewardBag>();
							Main.npcChatText = this.Give4Chat();
							player.QuickSpawnItem(ModContent.ItemType<ZephosRewardBag>(), 1);
							RedeQuests.zephosQuests++;
							RedeQuests.ZpotionQuest = true;
							Main.PlaySound(24, -1, -1, 1, 1f, 0f);
							return;
						}
						Main.npcChatText = this.QuestChat();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						return;
					}
					else if (RedeQuests.zephosQuests == 9 && Main.hardMode)
					{
						Main.npcChatCornerItem = ModContent.ItemType<MythrilsBane>();
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						int Bane = player.FindItem(ModContent.ItemType<MythrilsBane>());
						if (Bane >= 0)
						{
							player.inventory[Bane].stack--;
							if (player.inventory[Bane].stack <= 0)
							{
								player.inventory[Bane] = new Item();
							}
							Main.npcChatCornerItem = ModContent.ItemType<ZephosRewardBag>();
							Main.npcChatText = this.Give5Chat();
							player.QuickSpawnItem(ModContent.ItemType<ZephosRewardBag>(), 3);
							RedeQuests.zephosQuests++;
							RedeQuests.DslimeQuest = true;
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
			if (RedeQuests.zephosQuests == 5 && Main.hardMode)
			{
				chat.Add("I've recently started seeing spooky lost souls roaming about, no idea why though. As for a job for you, I think it'd be pretty epic if you caught one for me and put it in a bottle for me to show off! I'm selling a vacuum-sealed bottle for 100% free! Get one and catch a Lost Soul with a Bug Net while holding it!", 1.0);
			}
			else if (RedeQuests.zephosQuests == 6 && Main.hardMode)
			{
				chat.Add("Hey okay so you know how I use a sword? Well the sheath for my sword is pretty wack now. So yo bro, basically my sheath broke, can you get me one what won't break as easily? Maybe an adamantite or titanium one is fine.", 1.0);
			}
			else if (RedeQuests.zephosQuests == 7 && Main.hardMode)
			{
				chat.Add("Before me and Daerel got seperated, he told me about some strange sword from his world, it's called the Zwyhander... Zihander? Zyhander? Zweihander? Whatever the name is, do you think you can craft that for me? I'm sure this cool sword will make all the ladies fall for me!", 1.0);
			}
			else if (RedeQuests.zephosQuests == 8 && Main.hardMode)
			{
				chat.Add("Now as you'll know, I'm a very courageous, handsome, and cool dude, but whenever I try to talk to a lady I always have trouble, ya know? I really could use something to help me, like, I donno... A potion of courage? Yeah, that'll do the trick.", 1.0);
			}
			else if (RedeQuests.zephosQuests == 9 && Main.hardMode)
			{
				chat.Add("Yo bro! I found Daerel! One problem, he's stuck in a giant slime creature! I tried to get 'im out, but I lose my new sword in there, so I had to make a rightous retreat. You can take it down, right? I found it in the corruption or crimson or whatever that evil land is called, I'm also selling an item to lure it easier, so go get it!", 1.0);
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
			weightedRandom.Add("Look at the little thing, it's beautiful! Nicely done, brochacho!", 1.0);
			return weightedRandom;
		}

		public string Give2Chat()
		{
			WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
			weightedRandom.Add("Perfect! A sheath fit for a handsome and strong swordsdude as myself!", 1.0);
			return weightedRandom;
		}

		public string Give3Chat()
		{
			WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
			weightedRandom.Add("Never expected it to be this big! It's glorious! You've done it again, bro. Great crafting.", 1.0);
			return weightedRandom;
		}

		public string Give4Chat()
		{
			WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
			weightedRandom.Add("Now I can really shine with this, all the ladies will fall for me once I talk to them! Woo!", 1.0);
			return weightedRandom;
		}

		public string Give5Chat()
		{
			WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
			weightedRandom.Add("Daerel is saved? You got my spicy sword back? Nice job, we'll be unstoppable now!", 1.0);
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
			if (RedeQuests.zephosQuests == 9 && Main.hardMode)
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
			if (RedeQuests.zephosQuests == 5 && Main.hardMode)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<LostSoulCatcher>(), false);
				nextSlot++;
			}
		}

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 32;
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
			item = Main.itemTexture[ModContent.ItemType<SwordSlicer>()];
			itemSize = 42;
		}

		public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight)
		{
			itemWidth = 42;
			itemHeight = 42;
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
