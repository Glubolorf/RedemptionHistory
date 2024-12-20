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
	public class Zephos3 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Slicer");
			Main.npcFrameCount[base.npc.type] = 25;
			NPCID.Sets.ExtraFramesCount[base.npc.type] = 5;
			NPCID.Sets.AttackFrameCount[base.npc.type] = 5;
			NPCID.Sets.DangerDetectRange[base.npc.type] = 100;
			NPCID.Sets.AttackType[base.npc.type] = 3;
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
			base.npc.defense = 25;
			base.npc.lifeMax = 500;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.knockBackResist = 0.3f;
			this.animationType = 22;
		}

		public override bool CheckDead()
		{
			Main.NewText("Zephos the Slicer was knocked unconscious...", Color.Red.R, Color.Red.G, Color.Red.B, false);
			base.npc.SetDefaults(ModContent.NPCType<Zep3Unconscious>(), -1f);
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

		public override string GetChat()
		{
			if (NPC.downedMoonlord && Main.hardMode && Main.rand.Next(12) == 0)
			{
				return "I wonder how the demi-dude is doing... Wait, you don't know who that is.";
			}
			int DryadID = NPC.FindFirstNPC(20);
			if (DryadID >= 0 && Main.rand.Next(8) == 0)
			{
				return "Doesn't " + Main.npc[DryadID].GivenName + " know how to put clothes on? Whatever, I like it!";
			}
			switch (Main.rand.Next(7))
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
			default:
				return "'Ey bro!";
			}
		}

		public override void ResetEffects()
		{
			Zephos3.SwitchInfo = false;
			Zephos3.Shop = false;
			Zephos3.Talk = false;
			Zephos3.Sharpen = false;
			Zephos3.ShineArmour = false;
			Zephos3.Quest1 = false;
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			string SwitchInfoT = "Cycle Options";
			string ShopT = "Shop";
			string TalkT = "Talk";
			string SharpenT = "Sharpen III (2 gold 50 silver)";
			string ShineArmourT = "Shine Armor III (5 gold)";
			string QuestT = "Quest";
			button = SwitchInfoT;
			if (Zephos3.ChatNumber == 0)
			{
				button2 = ShopT;
				Zephos3.Shop = true;
				return;
			}
			if (Zephos3.ChatNumber == 1)
			{
				button2 = TalkT;
				Zephos3.Talk = true;
				return;
			}
			if (Zephos3.ChatNumber == 2)
			{
				button2 = SharpenT;
				Zephos3.Sharpen = true;
				return;
			}
			if (Zephos3.ChatNumber == 3)
			{
				button2 = ShineArmourT;
				Zephos3.ShineArmour = true;
				return;
			}
			if (Zephos3.ChatNumber == 4)
			{
				button2 = QuestT;
				Zephos3.Quest1 = true;
				return;
			}
			Zephos3.ChatNumber = 0;
			button2 = TalkT;
			Zephos3.Talk = true;
		}

		public void ResetBools()
		{
			Zephos3.Shop = false;
			Zephos3.Talk = false;
			Zephos3.Sharpen = false;
			Zephos3.ShineArmour = false;
			Zephos3.Quest1 = false;
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			Player player = Main.LocalPlayer;
			player.GetModPlayer<RedePlayer>();
			if (firstButton)
			{
				this.ResetBools();
				Zephos3.ChatNumber++;
				if (Zephos3.ChatNumber > 4)
				{
					Zephos3.ChatNumber = 0;
					return;
				}
			}
			else
			{
				if (Zephos3.Talk)
				{
					Main.npcChatText = Zephos3.ChitChat();
					return;
				}
				if (Zephos3.ChatNumber == 0)
				{
					shop = true;
				}
				if (Zephos3.ChatNumber == 1)
				{
					Main.npcChatText = Zephos3.ChitChat();
					return;
				}
				if (Zephos3.ChatNumber == 2)
				{
					if (Main.LocalPlayer.BuyItemOld(25000))
					{
						Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 37, 1f, 0f);
						Main.LocalPlayer.AddBuff(ModContent.BuffType<Sharpen3Buff>(), 36000, true);
						return;
					}
					Main.npcChatText = Zephos3.NoCoinsChat();
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					return;
				}
				else if (Zephos3.ChatNumber == 3)
				{
					if (Main.LocalPlayer.BuyItemOld(50000))
					{
						Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 37, 1f, 0f);
						Main.LocalPlayer.AddBuff(ModContent.BuffType<ShineArmour3Buff>(), 36000, true);
						return;
					}
					Main.npcChatText = Zephos3.NoCoinsChat();
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					return;
				}
				else if (Zephos3.ChatNumber == 4)
				{
					if (RedeQuests.zephosQuests == 9 && Main.hardMode)
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
							Main.npcChatText = this.GiveSChat();
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
			weightedRandom.Add("You're poor? Hahahahaha! Just kidding.", 1.0);
			weightedRandom.Add("You really don't have enough money? Understandable.", 1.0);
			return weightedRandom;
		}

		public string QuestChat()
		{
			Main.LocalPlayer.GetModPlayer<RedePlayer>();
			WeightedRandom<string> chat = new WeightedRandom<string>();
			if (RedeQuests.zephosQuests == 9 && Main.hardMode)
			{
				chat.Add("Yo bro! I found Daerel! One problem, he's stuck in a giant slime creature! I tried to get 'im out, but I lose my new sword in there, so I had to make a rightous retreat. You can take it down, right? I found it in the corruption or crimson or whatever that evil land is called, I'm also selling an item to lure it easier, so go get it!", 1.0);
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
			weightedRandom.Add("Daerel is saved? You got my spicy sword back? Nice job, we'll be unstoppable now!", 1.0);
			return weightedRandom;
		}

		public static string ChitChat()
		{
			WeightedRandom<string> chat = new WeightedRandom<string>();
			chat.Add("How about I tell you the time I was a pirate, sailing abroad the vast ocean with fellow pirate people... Actually, I don't remeber a lot about being a pirate. I was very young at the time.", 1.0);
			chat.Add("Did I ever tell you about my victory against a powerful undead druid? It was a close match, it was giant, and its magic was insane! But yeah, I beat it, pretty cool huh? It had flowers growing everywhere on it!", 1.0);
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
			chat.Add("Did you know I'm a left-handed swordsman? I was right-handed, but some clown called Reyner cut it off. He was a criminal in Ricusa called the Twilight Slasher. As you can see, my right hand is still here, 'cos healing magic, but it took weeks to heal. Despite all the healing, it hurts to swing it about still.", 1.0);
			chat.Add("In Anglon there's this hotty called Dragonslayer Gwyn, as the name implies, she slays dragons. Where did dragons come from anyway? Daerel told me they all come from Thamor. He also told me about Goliathon, apparently it was a dragon the size of a country! It's dead now obviously.", 1.0);
			chat.Add("Back when I was a pirate lad, I'd constantly hear tales of Naakseth, Lord of the Sea. I don't remember any of them though, I'm sure he won't be important anyway.", 1.0);
			chat.Add("Back when I got my arm cut off by the Twilight Slasher, I woke up in a mansion with an old lady healing me, that's when I met Asteria, she's certainly an interesting character... She's a Royal Knight - Anglon's strongest unit of knights. She also has blonde-white hair, orange eyes, oh, and did I tell you she's literally double the size of a normal person!? No wonder Anglon made her a Royal Knight, I've heard she's extremely strong too!", 1.0);
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
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<SwordSlicer>(), false);
			nextSlot++;
			if (NPC.downedMoonlord)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Godslayer>(), false);
				nextSlot++;
			}
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Zweihander>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(426, false);
			nextSlot++;
			if (NPC.downedFrost)
			{
				shop.item[nextSlot].SetDefaults(676, false);
				nextSlot++;
			}
			if (NPC.downedPlantBoss)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<LightSteel>(), false);
				nextSlot++;
			}
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<MagicMetalPolish>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(901, false);
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

		public override void DrawTownAttackSwing(ref Texture2D item, ref int itemSize, ref float scale, ref Vector2 offset)
		{
			scale = 1f;
			item = Main.itemTexture[ModContent.ItemType<MythrilsBane>()];
			itemSize = 58;
		}

		public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight)
		{
			itemWidth = 58;
			itemHeight = 58;
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
