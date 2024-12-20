using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items;
using Redemption.Items.DruidDamageClass;
using Redemption.Items.Placeable;
using Redemption.Items.Quest;
using Redemption.Items.Weapons;
using Redemption.Items.Weapons.v08;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Redemption.NPCs
{
	[AutoloadHead]
	public class Fallen : ModNPC
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Fallen";
			}
		}

		public override bool Autoload(ref string name)
		{
			name = "Fallen";
			return base.mod.Properties.Autoload;
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Fallen");
			Main.npcFrameCount[base.npc.type] = 25;
			NPCID.Sets.ExtraFramesCount[base.npc.type] = 9;
			NPCID.Sets.AttackFrameCount[base.npc.type] = 4;
			NPCID.Sets.DangerDetectRange[base.npc.type] = 100;
			NPCID.Sets.AttackType[base.npc.type] = 3;
			NPCID.Sets.AttackTime[base.npc.type] = 40;
			NPCID.Sets.AttackAverageChance[base.npc.type] = 30;
			NPCID.Sets.HatOffsetY[base.npc.type] = 14;
		}

		public override void SetDefaults()
		{
			base.npc.townNPC = true;
			base.npc.friendly = true;
			base.npc.width = 40;
			base.npc.height = 54;
			base.npc.aiStyle = 7;
			base.npc.damage = 15;
			base.npc.defense = 5;
			base.npc.lifeMax = 250;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.knockBackResist = 0.5f;
			this.animationType = 22;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/FallenGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/FallenGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/FallenGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/FallenGore4"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/FallenGore5"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/FallenGore6"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/FallenGore7"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/FallenGore8"), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			if (!RedeWorld.keeperSaved && Main.netMode != 1 && base.npc.life <= 0)
			{
				NPC.NewNPC((int)base.npc.position.X + 20, (int)base.npc.position.Y + 20, ModContent.NPCType<AAAA>(), 0, 0f, 0f, 0f, 0f, 255);
				Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Shriek").WithVolume(0.4f).WithPitchVariance(0.1f), -1, -1);
			}
		}

		public override bool CanTownNPCSpawn(int numTownNPCs, int money)
		{
			return RedeWorld.downedTheKeeper;
		}

		public override string TownNPCName()
		{
			int num = WorldGen.genRand.Next(3);
			if (num == 0)
			{
				return "Happins";
			}
			if (num != 1)
			{
				return "Okvot";
			}
			return "Tenvon";
		}

		public override string GetChat()
		{
			for (int i = 0; i < 255; i++)
			{
				Player player = Main.player[i];
				if (player.active)
				{
					for (int j = 0; j < player.inventory.Length; j++)
					{
						if (player.inventory[j].type == ModContent.ItemType<BlackenedHeart>() && Main.rand.Next(3) == 0)
						{
							return "I wouldn't consume that Blackened Heart if I were you. Only bad things will come of it.";
						}
					}
				}
			}
			int FallenID = NPC.FindFirstNPC(ModContent.NPCType<Fallen>());
			if (Main.npc[FallenID].GivenName == "Okvot")
			{
				if (RedeWorld.keeperSaved && Main.rand.Next(3) == 0)
				{
					return "You saved the Keeper? Thank you for that, I can't imagine the pain she was feeling. If you need Dark Fragments, I'll sell them now for you. However, I doubt this is over... Her husband died too, apparently of depression. But he didn't become an undead, his inverted soul made him into something worse.";
				}
				switch (Main.rand.Next(9))
				{
				case 0:
					return "I may be undead, but I hope for a world where no tears are shed, and no pain is spread. A world of peace. That is who I was before dying, and despite the undead tendency to be more aggressive, I feel the same as I always have.";
				case 1:
					return "Do not worry, human. I bring no hatred where I go, despite my undead look, I won't harm you. And I hope you won't harm me either.";
				case 2:
					return "Times may come when you have hardship, maybe you struggle fighting a tough enemy, maybe you feel alone on this island with only enemies everywhere you go, but I'll still be here. Don't let my rotten looks deceive you, young one. I will help you.";
				case 3:
					return "The wind sings the longest tune... Do you hear it?";
				case 4:
					return "Wanna know what lost souls are? When a living thing dies, it's soul leaves the body, these are Lost Souls. Lost Souls search around the world to look for corpses to infuse with. To ordinary people, Lost Souls are invisible, but some who use magic can see them fade in and out of the Spirit Realm.";
				case 5:
					return "Y'know, planting saplings on Ancient Dirt will make Ancient Trees grow! It's like magic!";
				case 6:
					return "The size of the Lost Souls depends on the original individual's power, the stronger the Will of the being, the bigger the soul. Once a soul leaves the body, they cannot infuse with it again. On most occasions where a soul fuses, a skeleton will be created. However, if the soul is strong enough, it will form pale-brown flesh on the skeleton, creating a Fallen.";
				case 7:
					return "In the lowest level of the Catacombs of Gathuram, the floor is littered with broken bones and puddles of water. Dim blue lights often were visible in this level, and nothing is alive there. So if any were to survive the fall, they would be alone forever.";
				default:
					return "My name's Okvot, I usually sell equipment to other Fallen to help them survive in the Catacombs of Gathuram, however, destiny has brought me into the outside world. I hope my junk can be of use to you.";
				}
			}
			else if (Main.npc[FallenID].GivenName == "Happins")
			{
				if (RedeWorld.keeperSaved && Main.rand.Next(3) == 0)
				{
					return "You saved the Keeper? That's great to hear, she gave me trouble when she had escaped the catacombs. If you need Dark Fragments, I'll sell them now for you.";
				}
				switch (Main.rand.Next(9))
				{
				case 0:
					return "In my first few days of becoming Fallen, every other undead tried to kill me. It was a very scary experience, but I escaped the catacombs and now I'm here.";
				case 1:
					return "Do you want to know about Pure-Iron? It's an extremely durable metal only found in the southern region of the world, in Gathuram. I am wearing a Pure-Iron helmet right now in fact.";
				case 2:
					return "At the start, this whole 'Fallen' thing was a little overwhelming. I didn't want to 'live' with the fact that I had died, even though I'm not 'living', haha. But I'm more accepting of this now, I'm undead, humans hate me, deal with it. Actually, you're a human, right? Why aren't you attacking me?";
				case 3:
					return "You want to know about the Warriors of the Iron Realm? They are the domain of Gathuram's primary warriors. They normally wear Pure-Iron armour.";
				case 4:
					return "Wanna know what lost souls are? When a living thing dies, it's soul leaves the body, these are Lost Souls. Lost Souls search around the world to look for corpses to infuse with. To ordinary people, Lost Souls are invisible, but some who use magic can see them fade in and out of the Spirit Realm.";
				case 5:
					return "Planting saplings on Ancient Dirt will make Ancient Trees grow. Just a tip.";
				case 6:
					return "The size of the Lost Souls depends on the original individual's power, the stronger the Will of the being, the bigger the soul. Once a soul leaves the body, they cannot infuse with it again. When the soul has found a worthy vessel, it fuses with it. On most occasions, a skeleton will be created. However, if the soul is strong enough, it will form pale-brown flesh on the skeleton, creating a Fallen.";
				case 7:
					return "The Catacombs of Gathuram - where my soul found a vessel - are a seemingly endless network of underground tunnels, crypts, and dungeons spanning all across the Iron Realm. It is considered the largest underground structure in Epidotra.";
				default:
					return "Hello, the name's Happins. I used to be a Warrior of the Iron Realm... Until I was killed of course.";
				}
			}
			else
			{
				if (RedeWorld.keeperSaved && Main.rand.Next(3) == 0)
				{
					return "You saved the Keeper? Bah, I guess that's nice of ya. If you need Dark Fragments, I'll sell them now for you.";
				}
				switch (Main.rand.Next(9))
				{
				case 0:
					return "I'm not very interested in talking, what ya want?";
				case 1:
					return "Darkness... Ha! What a strange term. You humans fear it more than death itself. You cower in the face of the overwhelming shadow of the night. Pitiful creature! It is not the darkness you should fear, but what lurks within it. So, did I spook ya?! Hahaha!";
				case 2:
					return "Ya know what Willpower is, right? It's the essence of yer soul! The stronger yer will to live, the bigger the soul. Did you know you can die of depression? Apparently if you have no will to live, your soul can invert! I got a ton'a willpower! I ain't dying anytime soon... Again.";
				case 3:
					return "Give me some of ya sweet coins! I got some sweet junk.";
				case 4:
					return "Wanna know what lost souls are? When a living thing dies, it's soul leaves the body. Lost Souls look around for corpses to infuse with. For normal people, Lost Souls are invisible, but some who use magic can see them fade in and out of the Spirit Realm.";
				case 5:
					return "Did ya know planting saplings on Ancient Dirt will make Ancient Trees grow. How do trees so old even grow? Crazy.";
				case 6:
					return "Once a soul leaves the body, they cannot infuse with it again. When the soul has found a worthy vessel, it fuses with it. Most of the time, a skeleton will be made. However, if the soul is strong enough, it will form pale-brown flesh on the skeleton, creating a Fallen. Like me!";
				case 7:
					return "Ever been to Spiritpeak Forest? A quarter of the forest is a giant graveyard, meaning there be a staggerin' number of Skeletons, Wandering Souls and Spirits. I used to take walks there when I was still alive.";
				default:
					return "M'name is Tenvon, I'm a blacksmith, but I guess I'm here now. I also should mention, I got some junk for sale, if ya interested.";
				}
			}
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = Language.GetTextValue("LegacyInterface.28");
			button2 = "Repair Fragments";
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			Player player = Main.LocalPlayer;
			player.GetModPlayer<RedePlayer>();
			if (firstButton)
			{
				shop = true;
				return;
			}
			int Frag = player.FindItem(ModContent.ItemType<SwordSlicerFrag1>());
			int Frag2 = player.FindItem(ModContent.ItemType<SwordSlicerFrag2>());
			int Frag3 = player.FindItem(ModContent.ItemType<AkisClawFrag1>());
			int Frag4 = player.FindItem(ModContent.ItemType<AkisClawFrag2>());
			int Frag5 = player.FindItem(ModContent.ItemType<DeathsClawFrag1>());
			int Frag6 = player.FindItem(ModContent.ItemType<DeathsClawFrag2>());
			int Frag7 = player.FindItem(ModContent.ItemType<FalconFrag1>());
			int Frag8 = player.FindItem(ModContent.ItemType<FalconFrag2>());
			int Frag9 = player.FindItem(ModContent.ItemType<ForestNymphsSickleFrag1>());
			int Frag10 = player.FindItem(ModContent.ItemType<ForestNymphsSickleFrag2>());
			int Frag11 = player.FindItem(ModContent.ItemType<GoldenEdgeFrag1>());
			int Frag12 = player.FindItem(ModContent.ItemType<GoldenEdgeFrag2>());
			int Frag13 = player.FindItem(ModContent.ItemType<LightbaneFrag1>());
			int Frag14 = player.FindItem(ModContent.ItemType<LightbaneFrag2>());
			int Frag15 = player.FindItem(ModContent.ItemType<LivingWoodRapierFrag1>());
			int Frag16 = player.FindItem(ModContent.ItemType<LivingWoodRapierFrag2>());
			int Frag17 = player.FindItem(ModContent.ItemType<PeacekeeperFrag1>());
			int Frag18 = player.FindItem(ModContent.ItemType<PeacekeeperFrag2>());
			int Frag19 = player.FindItem(ModContent.ItemType<SilverRapierFrag1>());
			int Frag20 = player.FindItem(ModContent.ItemType<SilverRapierFrag2>());
			int Frag21 = player.FindItem(ModContent.ItemType<VictorBattletomeFrag1>());
			int Frag22 = player.FindItem(ModContent.ItemType<VictorBattletomeFrag2>());
			int Frag23 = player.FindItem(ModContent.ItemType<SilverwoodBowFrag1>());
			int Frag24 = player.FindItem(ModContent.ItemType<SilverwoodBowFrag2>());
			int Frag25 = player.FindItem(ModContent.ItemType<HonorsReachFrag1>());
			int Frag26 = player.FindItem(ModContent.ItemType<HonorsReachFrag2>());
			int Frag27 = player.FindItem(ModContent.ItemType<MidnightFrag1>());
			int Frag28 = player.FindItem(ModContent.ItemType<MidnightFrag2>());
			int Frag29 = player.FindItem(ModContent.ItemType<SpellsongFrag1>());
			int Frag30 = player.FindItem(ModContent.ItemType<SpellsongFrag2>());
			int Frag31 = player.FindItem(ModContent.ItemType<GodslayerFrag1>());
			int Frag32 = player.FindItem(ModContent.ItemType<GodslayerFrag2>());
			int Frag33 = player.FindItem(ModContent.ItemType<WhisperwindFrag1>());
			int Frag34 = player.FindItem(ModContent.ItemType<WhisperwindFrag2>());
			int Frag35 = player.FindItem(ModContent.ItemType<BlindJusticeFrag1>());
			int Frag36 = player.FindItem(ModContent.ItemType<BlindJusticeFrag2>());
			int Frag37 = player.FindItem(ModContent.ItemType<DusksongFrag1>());
			int Frag38 = player.FindItem(ModContent.ItemType<DusksongFrag2>());
			int Frag39 = player.FindItem(ModContent.ItemType<SteelSwordFragment1>());
			int Frag40 = player.FindItem(ModContent.ItemType<SteelSwordFragment2>());
			int Frag41 = player.FindItem(ModContent.ItemType<TiedsRapierFrag1>());
			int Frag42 = player.FindItem(ModContent.ItemType<TiedsRapierFrag2>());
			if (Frag >= 0 && Frag2 >= 0)
			{
				player.inventory[Frag].stack--;
				player.inventory[Frag2].stack--;
				if (player.inventory[Frag].stack <= 0)
				{
					player.inventory[Frag] = new Item();
				}
				if (player.inventory[Frag2].stack <= 0)
				{
					player.inventory[Frag2] = new Item();
				}
				Main.npcChatCornerItem = ModContent.ItemType<SwordSlicer>();
				Main.npcChatText = Fallen.GiveChat();
				player.QuickSpawnItem(ModContent.ItemType<SwordSlicer>(), 1);
				Main.PlaySound(24, -1, -1, 1, 1f, 0f);
				return;
			}
			if (Frag3 >= 0 && Frag4 >= 0)
			{
				player.inventory[Frag3].stack--;
				player.inventory[Frag4].stack--;
				if (player.inventory[Frag3].stack <= 0)
				{
					player.inventory[Frag3] = new Item();
				}
				if (player.inventory[Frag4].stack <= 0)
				{
					player.inventory[Frag4] = new Item();
				}
				Main.npcChatCornerItem = ModContent.ItemType<AkisClaws>();
				Main.npcChatText = Fallen.GiveChat();
				player.QuickSpawnItem(ModContent.ItemType<AkisClaws>(), 1);
				Main.PlaySound(24, -1, -1, 1, 1f, 0f);
				return;
			}
			if (Frag5 >= 0 && Frag6 >= 0)
			{
				player.inventory[Frag5].stack--;
				player.inventory[Frag6].stack--;
				if (player.inventory[Frag5].stack <= 0)
				{
					player.inventory[Frag5] = new Item();
				}
				if (player.inventory[Frag6].stack <= 0)
				{
					player.inventory[Frag6] = new Item();
				}
				Main.npcChatCornerItem = ModContent.ItemType<DeathsClaw>();
				Main.npcChatText = Fallen.GiveChat();
				player.QuickSpawnItem(ModContent.ItemType<DeathsClaw>(), 1);
				Main.PlaySound(24, -1, -1, 1, 1f, 0f);
				return;
			}
			if (Frag7 >= 0 && Frag8 >= 0)
			{
				player.inventory[Frag7].stack--;
				player.inventory[Frag8].stack--;
				if (player.inventory[Frag7].stack <= 0)
				{
					player.inventory[Frag7] = new Item();
				}
				if (player.inventory[Frag8].stack <= 0)
				{
					player.inventory[Frag8] = new Item();
				}
				Main.npcChatCornerItem = ModContent.ItemType<Falcon>();
				Main.npcChatText = Fallen.GiveChat();
				player.QuickSpawnItem(ModContent.ItemType<Falcon>(), 1);
				Main.PlaySound(24, -1, -1, 1, 1f, 0f);
				return;
			}
			if (Frag9 >= 0 && Frag10 >= 0)
			{
				player.inventory[Frag9].stack--;
				player.inventory[Frag10].stack--;
				if (player.inventory[Frag9].stack <= 0)
				{
					player.inventory[Frag9] = new Item();
				}
				if (player.inventory[Frag10].stack <= 0)
				{
					player.inventory[Frag10] = new Item();
				}
				Main.npcChatCornerItem = ModContent.ItemType<ForestNymphsSickle>();
				Main.npcChatText = Fallen.GiveChat();
				player.QuickSpawnItem(ModContent.ItemType<ForestNymphsSickle>(), 1);
				Main.PlaySound(24, -1, -1, 1, 1f, 0f);
				return;
			}
			if (Frag11 >= 0 && Frag12 >= 0)
			{
				player.inventory[Frag11].stack--;
				player.inventory[Frag12].stack--;
				if (player.inventory[Frag11].stack <= 0)
				{
					player.inventory[Frag11] = new Item();
				}
				if (player.inventory[Frag12].stack <= 0)
				{
					player.inventory[Frag12] = new Item();
				}
				Main.npcChatCornerItem = ModContent.ItemType<GoldenEdge>();
				Main.npcChatText = Fallen.GiveChat();
				player.QuickSpawnItem(ModContent.ItemType<GoldenEdge>(), 1);
				Main.PlaySound(24, -1, -1, 1, 1f, 0f);
				return;
			}
			if (Frag13 >= 0 && Frag14 >= 0)
			{
				player.inventory[Frag13].stack--;
				player.inventory[Frag14].stack--;
				if (player.inventory[Frag13].stack <= 0)
				{
					player.inventory[Frag13] = new Item();
				}
				if (player.inventory[Frag14].stack <= 0)
				{
					player.inventory[Frag14] = new Item();
				}
				Main.npcChatCornerItem = ModContent.ItemType<Lightbane>();
				Main.npcChatText = Fallen.GiveChat();
				player.QuickSpawnItem(ModContent.ItemType<Lightbane>(), 1);
				Main.PlaySound(24, -1, -1, 1, 1f, 0f);
				return;
			}
			if (Frag15 >= 0 && Frag16 >= 0)
			{
				player.inventory[Frag15].stack--;
				player.inventory[Frag16].stack--;
				if (player.inventory[Frag15].stack <= 0)
				{
					player.inventory[Frag15] = new Item();
				}
				if (player.inventory[Frag16].stack <= 0)
				{
					player.inventory[Frag16] = new Item();
				}
				Main.npcChatCornerItem = ModContent.ItemType<LivingWoodRapier>();
				Main.npcChatText = Fallen.GiveChat();
				player.QuickSpawnItem(ModContent.ItemType<LivingWoodRapier>(), 1);
				Main.PlaySound(24, -1, -1, 1, 1f, 0f);
				return;
			}
			if (Frag17 >= 0 && Frag18 >= 0)
			{
				player.inventory[Frag17].stack--;
				player.inventory[Frag18].stack--;
				if (player.inventory[Frag17].stack <= 0)
				{
					player.inventory[Frag17] = new Item();
				}
				if (player.inventory[Frag18].stack <= 0)
				{
					player.inventory[Frag18] = new Item();
				}
				Main.npcChatCornerItem = ModContent.ItemType<PeaceKeeper>();
				Main.npcChatText = Fallen.GiveChat();
				player.QuickSpawnItem(ModContent.ItemType<PeaceKeeper>(), 1);
				Main.PlaySound(24, -1, -1, 1, 1f, 0f);
				return;
			}
			if (Frag19 >= 0 && Frag20 >= 0)
			{
				player.inventory[Frag19].stack--;
				player.inventory[Frag20].stack--;
				if (player.inventory[Frag19].stack <= 0)
				{
					player.inventory[Frag19] = new Item();
				}
				if (player.inventory[Frag20].stack <= 0)
				{
					player.inventory[Frag20] = new Item();
				}
				Main.npcChatCornerItem = ModContent.ItemType<SilverRapier>();
				Main.npcChatText = Fallen.GiveChat();
				player.QuickSpawnItem(ModContent.ItemType<SilverRapier>(), 1);
				Main.PlaySound(24, -1, -1, 1, 1f, 0f);
				return;
			}
			if (Frag21 >= 0 && Frag22 >= 0)
			{
				player.inventory[Frag21].stack--;
				player.inventory[Frag22].stack--;
				if (player.inventory[Frag21].stack <= 0)
				{
					player.inventory[Frag21] = new Item();
				}
				if (player.inventory[Frag22].stack <= 0)
				{
					player.inventory[Frag22] = new Item();
				}
				Main.npcChatCornerItem = ModContent.ItemType<VictorBattletome>();
				Main.npcChatText = Fallen.GiveChat();
				player.QuickSpawnItem(ModContent.ItemType<VictorBattletome>(), 1);
				Main.PlaySound(24, -1, -1, 1, 1f, 0f);
				return;
			}
			if (Frag23 >= 0 && Frag24 >= 0)
			{
				player.inventory[Frag23].stack--;
				player.inventory[Frag24].stack--;
				if (player.inventory[Frag23].stack <= 0)
				{
					player.inventory[Frag23] = new Item();
				}
				if (player.inventory[Frag24].stack <= 0)
				{
					player.inventory[Frag24] = new Item();
				}
				Main.npcChatCornerItem = ModContent.ItemType<SilverwoodBow>();
				Main.npcChatText = Fallen.GiveChat();
				player.QuickSpawnItem(ModContent.ItemType<SilverwoodBow>(), 1);
				Main.PlaySound(24, -1, -1, 1, 1f, 0f);
				return;
			}
			if (Frag25 >= 0 && Frag26 >= 0)
			{
				player.inventory[Frag25].stack--;
				player.inventory[Frag26].stack--;
				if (player.inventory[Frag25].stack <= 0)
				{
					player.inventory[Frag25] = new Item();
				}
				if (player.inventory[Frag26].stack <= 0)
				{
					player.inventory[Frag26] = new Item();
				}
				Main.npcChatCornerItem = ModContent.ItemType<HonorsReach>();
				Main.npcChatText = Fallen.GiveChat();
				player.QuickSpawnItem(ModContent.ItemType<HonorsReach>(), 1);
				Main.PlaySound(24, -1, -1, 1, 1f, 0f);
				return;
			}
			if (Frag27 >= 0 && Frag28 >= 0)
			{
				player.inventory[Frag27].stack--;
				player.inventory[Frag28].stack--;
				if (player.inventory[Frag27].stack <= 0)
				{
					player.inventory[Frag27] = new Item();
				}
				if (player.inventory[Frag28].stack <= 0)
				{
					player.inventory[Frag28] = new Item();
				}
				Main.npcChatCornerItem = ModContent.ItemType<Midnight>();
				Main.npcChatText = Fallen.GiveChat();
				player.QuickSpawnItem(ModContent.ItemType<Midnight>(), 1);
				Main.PlaySound(24, -1, -1, 1, 1f, 0f);
				return;
			}
			if (Frag29 >= 0 && Frag30 >= 0)
			{
				player.inventory[Frag29].stack--;
				player.inventory[Frag30].stack--;
				if (player.inventory[Frag29].stack <= 0)
				{
					player.inventory[Frag29] = new Item();
				}
				if (player.inventory[Frag30].stack <= 0)
				{
					player.inventory[Frag30] = new Item();
				}
				Main.npcChatCornerItem = ModContent.ItemType<Spellsong>();
				Main.npcChatText = Fallen.GiveChat();
				player.QuickSpawnItem(ModContent.ItemType<Spellsong>(), 1);
				Main.PlaySound(24, -1, -1, 1, 1f, 0f);
				return;
			}
			if (Frag31 >= 0 && Frag32 >= 0)
			{
				player.inventory[Frag31].stack--;
				player.inventory[Frag32].stack--;
				if (player.inventory[Frag31].stack <= 0)
				{
					player.inventory[Frag31] = new Item();
				}
				if (player.inventory[Frag32].stack <= 0)
				{
					player.inventory[Frag32] = new Item();
				}
				Main.npcChatCornerItem = ModContent.ItemType<Godslayer>();
				Main.npcChatText = Fallen.GiveChat();
				player.QuickSpawnItem(ModContent.ItemType<Godslayer>(), 1);
				Main.PlaySound(24, -1, -1, 1, 1f, 0f);
				return;
			}
			if (Frag33 >= 0 && Frag34 >= 0)
			{
				player.inventory[Frag33].stack--;
				player.inventory[Frag34].stack--;
				if (player.inventory[Frag33].stack <= 0)
				{
					player.inventory[Frag33] = new Item();
				}
				if (player.inventory[Frag34].stack <= 0)
				{
					player.inventory[Frag34] = new Item();
				}
				Main.npcChatCornerItem = ModContent.ItemType<Whisperwind>();
				Main.npcChatText = Fallen.GiveChat();
				player.QuickSpawnItem(ModContent.ItemType<Whisperwind>(), 1);
				Main.PlaySound(24, -1, -1, 1, 1f, 0f);
				return;
			}
			if (Frag35 >= 0 && Frag36 >= 0)
			{
				player.inventory[Frag35].stack--;
				player.inventory[Frag36].stack--;
				if (player.inventory[Frag35].stack <= 0)
				{
					player.inventory[Frag35] = new Item();
				}
				if (player.inventory[Frag36].stack <= 0)
				{
					player.inventory[Frag36] = new Item();
				}
				Main.npcChatCornerItem = ModContent.ItemType<BlindJustice>();
				Main.npcChatText = Fallen.GiveChat();
				player.QuickSpawnItem(ModContent.ItemType<BlindJustice>(), 1);
				Main.PlaySound(24, -1, -1, 1, 1f, 0f);
				return;
			}
			if (Frag37 >= 0 && Frag38 >= 0)
			{
				player.inventory[Frag37].stack--;
				player.inventory[Frag38].stack--;
				if (player.inventory[Frag37].stack <= 0)
				{
					player.inventory[Frag37] = new Item();
				}
				if (player.inventory[Frag38].stack <= 0)
				{
					player.inventory[Frag38] = new Item();
				}
				Main.npcChatCornerItem = ModContent.ItemType<Dusksong>();
				Main.npcChatText = Fallen.GiveChat();
				player.QuickSpawnItem(ModContent.ItemType<Dusksong>(), 1);
				Main.PlaySound(24, -1, -1, 1, 1f, 0f);
				return;
			}
			if (Frag39 >= 0 && Frag40 >= 0)
			{
				player.inventory[Frag39].stack--;
				player.inventory[Frag40].stack--;
				if (player.inventory[Frag39].stack <= 0)
				{
					player.inventory[Frag39] = new Item();
				}
				if (player.inventory[Frag40].stack <= 0)
				{
					player.inventory[Frag40] = new Item();
				}
				Main.npcChatCornerItem = ModContent.ItemType<HallamSword>();
				Main.npcChatText = Fallen.GiveChat();
				player.QuickSpawnItem(ModContent.ItemType<HallamSword>(), 1);
				Main.PlaySound(24, -1, -1, 1, 1f, 0f);
				return;
			}
			if (Frag41 >= 0 && Frag42 >= 0)
			{
				player.inventory[Frag41].stack--;
				player.inventory[Frag42].stack--;
				if (player.inventory[Frag41].stack <= 0)
				{
					player.inventory[Frag41] = new Item();
				}
				if (player.inventory[Frag42].stack <= 0)
				{
					player.inventory[Frag42] = new Item();
				}
				Main.npcChatCornerItem = ModContent.ItemType<TiedRapier>();
				Main.npcChatText = Fallen.GiveChat();
				player.QuickSpawnItem(ModContent.ItemType<TiedRapier>(), 1);
				Main.PlaySound(24, -1, -1, 1, 1f, 0f);
				return;
			}
			Main.npcChatText = Fallen.NoChat();
			Main.PlaySound(12, -1, -1, 1, 1f, 0f);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			int FallenID = NPC.FindFirstNPC(ModContent.NPCType<Fallen>());
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D TenvonAni = base.mod.GetTexture("NPCs/Fallen2");
			Texture2D HappinsAni = base.mod.GetTexture("NPCs/Fallen3");
			int spriteDirection = base.npc.spriteDirection;
			if (Main.npc[FallenID].GivenName == "Okvot")
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y + 4f);
				spriteBatch.Draw(texture, drawCenter - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			else if (Main.npc[FallenID].GivenName == "Tenvon")
			{
				Vector2 drawCenter2 = new Vector2(base.npc.Center.X, base.npc.Center.Y + 4f);
				Main.spriteBatch.Draw(TenvonAni, drawCenter2 - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			else
			{
				Vector2 drawCenter3 = new Vector2(base.npc.Center.X, base.npc.Center.Y + 4f);
				Main.spriteBatch.Draw(HappinsAni, drawCenter3 - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		public static string GiveChat()
		{
			WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
			weightedRandom.Add("All done and repaired, here you go.", 1.0);
			return weightedRandom;
		}

		public static string NoChat()
		{
			WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
			weightedRandom.Add("You don't seem to have any fragments on your possession. Either that or the fragments you may own aren't for the same weapon.", 1.0);
			return weightedRandom;
		}

		public override void SetupShop(Chest shop, ref int nextSlot)
		{
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<OldGathicWaraxe>(), false);
			shop.item[nextSlot].shopCustomPrice = new int?(30);
			shop.item[nextSlot].shopSpecialCurrency = Redemption.FaceCustomCurrencyID;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<BronzeGreatsword>(), false);
			shop.item[nextSlot].shopCustomPrice = new int?(25);
			shop.item[nextSlot].shopSpecialCurrency = Redemption.FaceCustomCurrencyID;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<BronzeStaff>(), false);
			shop.item[nextSlot].shopCustomPrice = new int?(30);
			shop.item[nextSlot].shopSpecialCurrency = Redemption.FaceCustomCurrencyID;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<AncientDirt>(), false);
			shop.item[nextSlot].shopCustomPrice = new int?(1);
			shop.item[nextSlot].shopSpecialCurrency = Redemption.FaceCustomCurrencyID;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<AncientWood>(), false);
			shop.item[nextSlot].shopCustomPrice = new int?(2);
			shop.item[nextSlot].shopSpecialCurrency = Redemption.FaceCustomCurrencyID;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<AncientStone>(), false);
			shop.item[nextSlot].shopCustomPrice = new int?(3);
			shop.item[nextSlot].shopSpecialCurrency = Redemption.FaceCustomCurrencyID;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<MysteriousTabletCorrupt>(), false);
			shop.item[nextSlot].shopCustomPrice = new int?(15);
			shop.item[nextSlot].shopSpecialCurrency = Redemption.FaceCustomCurrencyID;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<MysteriousTabletCrimson>(), false);
			shop.item[nextSlot].shopCustomPrice = new int?(15);
			shop.item[nextSlot].shopSpecialCurrency = Redemption.FaceCustomCurrencyID;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<SmallLostSoul>(), false);
			shop.item[nextSlot].shopCustomPrice = new int?(3);
			shop.item[nextSlot].shopSpecialCurrency = Redemption.FaceCustomCurrencyID;
			nextSlot++;
			if (Main.hardMode)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<LostSoul>(), false);
				shop.item[nextSlot].shopCustomPrice = new int?(6);
				shop.item[nextSlot].shopSpecialCurrency = Redemption.FaceCustomCurrencyID;
				nextSlot++;
			}
			if (Main.hardMode && NPC.downedMoonlord)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<LargeLostSoul>(), false);
				shop.item[nextSlot].shopCustomPrice = new int?(9);
				shop.item[nextSlot].shopSpecialCurrency = Redemption.FaceCustomCurrencyID;
				nextSlot++;
			}
			if (Main.hardMode && RedeWorld.downedPatientZero)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<SmallShadesoul>(), false);
				shop.item[nextSlot].shopCustomPrice = new int?(6);
				shop.item[nextSlot].shopSpecialCurrency = Redemption.FaceCustomCurrencyID;
				nextSlot++;
			}
			if (NPC.downedPlantBoss && !NPC.downedGolemBoss)
			{
				shop.item[nextSlot].SetDefaults(1508, false);
				shop.item[nextSlot].shopCustomPrice = new int?(10);
				shop.item[nextSlot].shopSpecialCurrency = Redemption.FaceCustomCurrencyID;
				nextSlot++;
			}
			if (NPC.downedGolemBoss)
			{
				shop.item[nextSlot].SetDefaults(1508, false);
				shop.item[nextSlot].shopCustomPrice = new int?(6);
				shop.item[nextSlot].shopSpecialCurrency = Redemption.FaceCustomCurrencyID;
				nextSlot++;
			}
			if (RedeWorld.keeperSaved)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<DarkShard>(), false);
				shop.item[nextSlot].shopCustomPrice = new int?(4);
				shop.item[nextSlot].shopSpecialCurrency = Redemption.FaceCustomCurrencyID;
				nextSlot++;
				if (Main.expertMode)
				{
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<HeartEmblem>(), false);
					shop.item[nextSlot].shopCustomPrice = new int?(30);
					shop.item[nextSlot].shopSpecialCurrency = Redemption.FaceCustomCurrencyID;
					nextSlot++;
				}
			}
		}

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 46;
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
			item = Main.itemTexture[ModContent.ItemType<OldGathicWaraxe>()];
			itemSize = 60;
		}

		public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight)
		{
			itemWidth = 60;
			itemHeight = 60;
		}
	}
}
