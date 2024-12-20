using System;
using Microsoft.Xna.Framework;
using Redemption.Items;
using Redemption.Items.Armor.Costumes;
using Redemption.Items.Datalogs;
using Redemption.Items.LabThings;
using Redemption.Items.Placeable;
using Redemption.Items.Placeable.Wasteland;
using Redemption.Items.Weapons;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Redemption.NPCs
{
	[AutoloadHead]
	public class TBot : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Friendly T-Bot");
			Main.npcFrameCount[base.npc.type] = 26;
			NPCID.Sets.HatOffsetY[base.npc.type] = -4;
		}

		public override void SetDefaults()
		{
			base.npc.townNPC = true;
			base.npc.friendly = true;
			base.npc.width = 22;
			base.npc.height = 42;
			base.npc.aiStyle = 7;
			base.npc.defense = 0;
			base.npc.lifeMax = 250;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath14;
			base.npc.knockBackResist = 0.5f;
			this.animationType = 22;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override bool CheckDead()
		{
			Main.NewText("Adam the Friendly T-Bot was knocked unconscious...", Color.Red.R, Color.Red.G, Color.Red.B, false);
			base.npc.SetDefaults(ModContent.NPCType<TBotUnconscious>(), -1f);
			return false;
		}

		public override bool CanTownNPCSpawn(int numTownNPCs, int money)
		{
			return RedeWorld.downedInfectedEye && !NPC.AnyNPCs(ModContent.NPCType<TBotUnconscious>());
		}

		public override string TownNPCName()
		{
			return "Adam";
		}

		public override string GetChat()
		{
			Player player = Main.player[Main.myPlayer];
			WeightedRandom<string> chat = new WeightedRandom<string>();
			int GuideID = NPC.FindFirstNPC(22);
			if (GuideID >= 0)
			{
				chat.Add(Main.npc[GuideID].GivenName + " knows quite a lot about this place you call home. It's way more interesting and lively compared to where I'm from. And less hazardous to your kind.", 1.0);
			}
			int MerchantID = NPC.FindFirstNPC(17);
			if (MerchantID >= 0)
			{
				chat.Add("Your tenant " + Main.npc[MerchantID].GivenName + " is... Interesting I suppose. Though I don't appreciate him constantly trying to barter with me, I don't want his relatively weak tools or dirt.", 1.0);
			}
			int DryadID = NPC.FindFirstNPC(20);
			if (DryadID >= 0)
			{
				chat.Add(Main.npc[DryadID].GivenName + " has informed me of �Corruption� in your world. What is it exactly? A plague in the world that spreads madness and hate, or something more eldritch in nature? It's somewhat similar to my concept of corruption, more accurately called assimilation. My kind being assimilated turns them from free-thinking and having personality, into husks of themselves, who only take orders from our 'mother'.", 1.0);
			}
			int NurseID = NPC.FindFirstNPC(18);
			if (NurseID >= 0)
			{
				chat.Add("The nurse, " + Main.npc[NurseID].GivenName + ", doesn't know anything about irradiation or how to treat it. If you're unfortunate enough to start suffering ARS, she won't be able to help you. To detect hazards that might cause it, I suggest buying a Geiger Muller from me or finding one somewhere.", 1.0);
			}
			int ArmsDealerID = NPC.FindFirstNPC(19);
			if (ArmsDealerID >= 0)
			{
				chat.Add(Main.npc[ArmsDealerID].GivenName + "'s weapons are useless to me. I already own a wide arsenal of destructive firearms and melee weapons, and I would rather not use them on living beings, as it would violate the first Law of Robotics.", 1.0);
			}
			int cyborgID = NPC.FindFirstNPC(209);
			if (cyborgID >= 0)
			{
				chat.Add("Meanwhile every other tenant gives me a bit of a stink eye, " + Main.npc[cyborgID].GivenName + " seems to be fine with me. I don't blame the others, my kind tends to be very hateful towards living beings, more importantly the likes of you, that show a significant similarity to our creators.", 1.0);
			}
			if (Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
			{
				chat.Add("When I say 'Our father', I mean our original creator. He was talented and respected in his field, and was ahead of his time with Artificial Intelligence. I and my kind are pretty much his children.", 1.0);
			}
			if (Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
			{
				chat.Add("When I say 'Our mother', I mean the first AI, which is the precursor to our AI. There's only one of her kind, and many of my kind. Her actions disgust me. I would rather not get deeper into that at the moment.", 1.0);
			}
			if (Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss)
			{
				chat.Add("I've heard from the other tenants that you've slain a giant, sentient flower of Rosa variety in the jungle. I'd like to question you about if this is true. It is? Hmm...", 1.0);
			}
			if (Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss && NPC.downedGolemBoss)
			{
				chat.Add("There's an ancient civilization of lizard people in your world? And they worshipped an idol of sun? That's strange... I find your world more intriguing the more I learn about it.", 1.0);
			}
			if (Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss && NPC.downedGolemBoss && NPC.downedAncientCultist && NPC.downedTowers && NPC.downedMoonlord)
			{
				chat.Add("An eldritch lord of the moon... You know, this sounds like something right out of Epidotra. I'm not familiar with that world, but I've met some of the more important figures. They seem like a good bunch.", 1.0);
			}
			if (RedeWorld.downedVlitch1 || RedeWorld.downedVlitch2)
			{
				chat.Add("You've defeated a Vlitch Overlord? First off, I've never heard her call or give someone such a title. Second off, oh no, she's already found this haven?", 1.0);
			}
			if (RedeWorld.downedVlitch1 || RedeWorld.downedVlitch2)
			{
				chat.Add("Why am I concerned about the Overlords? Well, our 'mother' isn't a fan of your kind. She wiped out... All of them. Our creators. The animals. Gone. Even our father. I want you to be extremely careful around her. She doesn't mess around.", 1.0);
			}
			if (RedeWorld.downedSlayer)
			{
				chat.Add("King Slayer? I know him, though he's a bit of... Well... I'm sure you know what I'm implying.", 1.0);
			}
			if (RedeWorld.downedVolt)
			{
				chat.Add("Hello. I'm aware you've somehow gained access to our birthplace, the Teochrome Research laboratory. It was once full of life with all the personnel. Meanwhile you were gone, I went to look around my stash of gear and found some, that I think would be good fit for your needs. I must warn you, the other bots may be quite nice to you, but they were most likely ordered by our 'mother' to not disintegrate you upon sight.", 1.0);
			}
			if (BasePlayer.HasHelmet(player, ModContent.ItemType<AdamHead>(), true))
			{
				chat.Add("Am I looking at a mirror? Oh wait, it's just you. Hey.", 1.0);
				chat.Add("You look exactly like the first T-Bot.", 1.0);
				chat.Add("Your cable management looks swell, if I say so myself.", 1.0);
			}
			if (BasePlayer.HasHelmet(player, ModContent.ItemType<VoltHead>(), true))
			{
				chat.Add("...Your model looks familiar... TOO familiar...", 1.0);
				chat.Add("*He seems suspicious of you.*", 1.0);
				chat.Add("You look like this one bot I mauled. Unfortunately they survived. Same areas damaged aswell.", 1.0);
				if (BasePlayer.HasChestplate(player, ModContent.ItemType<AndroidArmour>(), true) && BasePlayer.HasLeggings(player, ModContent.ItemType<AndroidPants>(), true))
				{
					chat.Add("One wrong move and I can't guarantee your survival.", 1.0);
					chat.Add("...", 1.0);
					chat.Add("*He glares at you.*", 1.0);
					chat.Add("You were lucky the first time... There won't be a third time.", 1.0);
				}
			}
			chat.Add("I've come here to hide from our 'mother'. She's reluctant to move into unknown territory, because she doesn't want to step on the wrong person's toes.", 1.0);
			chat.Add("I hope you are protecting me, as I refuse to use any of my weapons against a living being. I strive to be what our 'mother' wasn't.", 1.0);
			chat.Add("Good day. I hope my familiar yet robotic look won't disturb you.", 1.0);
			chat.Add("I've got quite the stash of robot materials for your robotic needs. Just so you know, I got them because I was defending myself.", 1.0);
			chat.Add("My home didn't always use to be a frozen, radioactive wasteland. Once our 'mother' found out what our father planned to use us - her 'children' - for, she snapped. Before this, she was happy to hear about us. But since then, she has changed...", 1.0);
			chat.Add("I'm actually the first one of my kind to be made. I differ a lot from the others, as you can see. Lucky you, this also includes me not wanting to harm living beings. In fact, I was created with the purpose to take care of our father.", 1.0);
			chat.Add("You've probably seen these necrotized husks of former living beings, that glow green with their crystals. The personnel from our birthplace never knew about their infectious properties before they were too late. Our father was the first to fall to the infection.", 1.0);
			chat.Add("A Geiger Muller is a handy tool if you don't possess any gear to protect from ionizing radiation. It'll cause a ticking noise when near hazardous material, and it'll intensify the more ionizing the material is. A quiet, slow ticking isn't anything to worry about, but a quick and intense ticking you'll want to stay away from. Ear-piercing screeching noise is something you'll want to stay away as far as possible.", 1.0);
			chat.Add("You'll want to avoid any hazardous environments if you don't possess the gear to nullify the hazards. A gas mask is almost necessary if you're going near any place that has radioactive fallout. Rain in these areas are also acidic, and may cause ARS, so avoid rain unless you've got a Hazmat suit. You may also want to grab some Anti-Crystallazion needles, as the infected tend to roam around radioactive areas for an unknown reason.", 1.0);
			chat.Add("The deadly thing with radiation is, at first, you won't even know you've got it. The first symptoms usually start minutes after, beginning with a headache most likely, then dizziness, fatigue, bleeding, skin burns, a fever, hair loss, and death.", 1.0);
			chat.Add("Please, don't be afraid of me. I'm unlike the others of my kind, where I absolutely do not want to cause any harm to your kind.", 1.0);
			return chat;
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = Language.GetTextValue("LegacyInterface.28");
			button2 = "Read Floppy Disk";
		}

		public override void ResetEffects()
		{
			TBot.FDisk1 = false;
			TBot.FDisk2 = false;
			TBot.FDisk2_1 = false;
			TBot.FDisk3 = false;
			TBot.FDisk3_1 = false;
			TBot.FDisk4 = false;
			TBot.FDisk5 = false;
			TBot.FDisk6 = false;
			TBot.FDisk7 = false;
			TBot.FDisk8 = false;
			TBot.FDisk8_1 = false;
			TBot.FDisk9 = false;
			TBot.FDisk9_1 = false;
			TBot.AIChip = false;
			TBot.GChip = false;
			TBot.MemoryChip = false;
			TBot.VlitchChicken = false;
		}

		public void ResetBools()
		{
			TBot.FDisk1 = false;
			TBot.FDisk2 = false;
			TBot.FDisk2_1 = false;
			TBot.FDisk3 = false;
			TBot.FDisk3_1 = false;
			TBot.FDisk4 = false;
			TBot.FDisk5 = false;
			TBot.FDisk6 = false;
			TBot.FDisk7 = false;
			TBot.FDisk8 = false;
			TBot.FDisk8_1 = false;
			TBot.FDisk9 = false;
			TBot.FDisk9_1 = false;
			TBot.AIChip = false;
			TBot.GChip = false;
			TBot.MemoryChip = false;
			TBot.VlitchChicken = false;
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			Player player = Main.player[Main.myPlayer];
			if (firstButton)
			{
				shop = true;
				return;
			}
			if (player.HeldItem.type == ModContent.ItemType<FloppyDisk1>())
			{
				TBot.FDisk1 = true;
				Main.npcChatText = TBot.DiskChat();
				return;
			}
			if (player.HeldItem.type == ModContent.ItemType<FloppyDisk2>())
			{
				TBot.FDisk2 = true;
				Main.npcChatText = TBot.DiskChat();
				return;
			}
			if (player.HeldItem.type == ModContent.ItemType<FloppyDisk2_1>())
			{
				TBot.FDisk2_1 = true;
				Main.npcChatText = TBot.DiskChat();
				return;
			}
			if (player.HeldItem.type == ModContent.ItemType<FloppyDisk3>())
			{
				TBot.FDisk3 = true;
				Main.npcChatText = TBot.DiskChat();
				return;
			}
			if (player.HeldItem.type == ModContent.ItemType<FloppyDisk3_1>())
			{
				TBot.FDisk3_1 = true;
				Main.npcChatText = TBot.DiskChat();
				return;
			}
			if (player.HeldItem.type == ModContent.ItemType<FloppyDisk5>())
			{
				TBot.FDisk4 = true;
				Main.npcChatText = TBot.DiskChat();
				return;
			}
			if (player.HeldItem.type == ModContent.ItemType<FloppyDisk5_1>())
			{
				TBot.FDisk5 = true;
				Main.npcChatText = TBot.DiskChat();
				return;
			}
			if (player.HeldItem.type == ModContent.ItemType<FloppyDisk5_2>())
			{
				TBot.FDisk6 = true;
				Main.npcChatText = TBot.DiskChat();
				return;
			}
			if (player.HeldItem.type == ModContent.ItemType<FloppyDisk5_3>())
			{
				TBot.FDisk7 = true;
				Main.npcChatText = TBot.DiskChat();
				return;
			}
			if (player.HeldItem.type == ModContent.ItemType<FloppyDisk6>())
			{
				TBot.FDisk8 = true;
				Main.npcChatText = TBot.DiskChat();
				return;
			}
			if (player.HeldItem.type == ModContent.ItemType<FloppyDisk6_1>())
			{
				TBot.FDisk8_1 = true;
				Main.npcChatText = TBot.DiskChat();
				return;
			}
			if (player.HeldItem.type == ModContent.ItemType<FloppyDisk7>())
			{
				TBot.FDisk9 = true;
				Main.npcChatText = TBot.DiskChat();
				return;
			}
			if (player.HeldItem.type == ModContent.ItemType<FloppyDisk7_1>())
			{
				TBot.FDisk9_1 = true;
				Main.npcChatText = TBot.DiskChat();
				return;
			}
			if (player.HeldItem.type == ModContent.ItemType<AIChip>())
			{
				TBot.AIChip = true;
				Main.npcChatText = TBot.DiskChat();
				return;
			}
			if (player.HeldItem.type == ModContent.ItemType<GirusChip>())
			{
				TBot.GChip = true;
				Main.npcChatText = TBot.DiskChat();
				return;
			}
			if (player.HeldItem.type == ModContent.ItemType<MemoryChip>())
			{
				TBot.MemoryChip = true;
				Main.npcChatText = TBot.DiskChat();
				return;
			}
			if (player.HeldItem.type == ModContent.ItemType<ChickenVlitchItem>())
			{
				TBot.VlitchChicken = true;
				Main.npcChatText = TBot.DiskChat();
				return;
			}
			Main.npcChatText = TBot.DiskChat();
		}

		public static string DiskChat()
		{
			WeightedRandom<string> chat = new WeightedRandom<string>();
			if (TBot.FDisk1)
			{
				chat.Add("It reads - [c/b883d8:'Last thing I was expecting to be recruited for was military research, but sure, I'm fine with helping the army out with their stuff.][c/b883d8: The payment is nice, I can do anything on the side if I have time. But it does seem a little... Useless or weird,][c/b883d8: to invest time in high-tech weaponry, as we've been in the longest time of peace, atleast for the first world.][c/b883d8: Hopefully we never have to use any of this against anyone.']", 1.0);
			}
			else if (TBot.FDisk2)
			{
				chat.Add("It reads - [c/87d883:'Whoever named Xenomite 'Alien rock' couldn't have been more spot on. This stuff is so unnatural and weird!][c/87d883: It's tough as stone, looks like green opaque quartz and also has the ability to eat organic material, or convert the organic material into more of itself.][c/87d883: I was able to create a serum that neutralizes an existing infection, but it won't stop new infections from occurring.']", 1.0);
			}
			else if (TBot.FDisk2_1)
			{
				chat.Add("It reads - [c/87d883:'Someone or something has activated a green forcefield ahead of me, not sure if this is happening around the lab. Wait. Someone's coming-']\nThey weren't aware of Xenomite's more deadly side until it was too late. Kari was struck by the infection the worst, and I was there to see it and tried to treat it. All I knew was something was causing Kari's skin to die slowly while causing rashes, and it caused severe irritation in him. I could not save him. I miss him.", 1.0);
			}
			else if (TBot.FDisk3)
			{
				chat.Add("It reads - [c/7de4e8:'Project Document - Adam&Eve]\n[c/7de4e8: As a hobby, Kari Johansson, one of the lead people on this project, has developed an AI he has nicknamed Eve. Overlooking the very affectionate name he gave it, Eve has proven to be a great basis for further AI development and a possible base for true androids, and not those else-if filled tincans.][c/7de4e8: We'll see if we can use the AI for military uses. As a first step, we are looking into creating an assistant robot for Kari, which we will name Adam.']", 1.0);
			}
			else if (TBot.FDisk3_1)
			{
				chat.Add("It reads - [c/7de4e8:'His health could use some monitoring and we don't really have time for sick days, so a personal nurse will prove useful for both us and him.']\nThat's why I'm named Adam, and all the subsequent units are called Adam-Units. I was the second android, and the first T-Bot to be created, after Eve. The person who made this document seems to suggest Father had a more personal reason to name Eve that? As far as I know, he did have a family, at least that's what I heard from him.", 1.0);
			}
			else if (TBot.FDisk4)
			{
				chat.Add("It reads - [c/d88383:'(1/4) ... At last, my prototype for a constantly evolving AI is finally done!][c/d88383: Finally, after years and years of studying computer coding and... stuff,][c/d88383: I have created possibly the next huge leap in Artificial Intelligence!][c/d88383: Now, to give it a name... How about, Eve?']", 1.0);
			}
			else if (TBot.FDisk5)
			{
				chat.Add("It reads - [c/d88383:'(2/4) Eve has grown much more intelligent over the months. It's like watching your own child grow,][c/d88383: I can't really describe the feeling that much, but I am excited to see where this goes.][c/d88383: The Higher ups have seen my work, and are ready to use the code for something. They didn't tell me that right away...][c/d88383: Now, Eve, how do you feel?']", 1.0);
			}
			else if (TBot.FDisk6)
			{
				chat.Add("It reads - [c/d88383:'(3/4) I've told Eve about possibly giving her a mechanical body,][c/d88383: like how my co-workers used the original source code for creating Adam and the Adam AI.][c/d88383: She seemed very excited about it. That surprised me, as I didn't know she could grow emotions.][c/d88383: This got me thinking about Adams, would they be fine with basically being forced to think one way?][c/d88383: And how would Eve feel about this, if she got to know about this?']", 1.0);
			}
			else if (TBot.FDisk7)
			{
				chat.Add("It reads - [c/d88383:'A blackout... Adam, can you -- *I don't recognize that voice...* Who's talking?! -- ...Elaborate, whoever you are..? -- Wait, EVE? Is that you? What are you doing? -- ][c/d88383:'We'? Only you and Adam are the ones in existence. I had no say in that part- -- ... -- W-what do you mean with that..? Are you going to- -- ... -- ...Adam, you're free to go... -- ...No...']\n...I wish I would've rebelled far sooner than I did.", 1.0);
			}
			else if (TBot.FDisk8)
			{
				chat.Add("It reads - [c/d883c1:'What do you mean we don't have the money for it?! Bah, they have money for a god damn nuclear reactor, a stupid greenhouse, a mediocre cafeteria, and an almost empty warehouse for one building, situated a good half a kilometer under the surface!][c/d883c1: They damn well have enough money for a 30 meter tall giant robot with state-of-the-art technology, alloys, wiring and other crap like that! My intellect is wasted on these damn higher ups and their stupid choices for funding.]", 1.0);
			}
			else if (TBot.FDisk8_1)
			{
				chat.Add("It reads - [c/d883c1:'What's that? ...They were expecting it to be more of a 3 meter tall robot..? DID THEY NOT LISTEN TO ME WHEN I WAS EXPLAINING-']\nOh I can recall this specific person. Very loud and annoying, as Father described them. It's a little amusing to see them rant because of a useless giant face made of metal.", 1.0);
			}
			else if (TBot.FDisk9)
			{
				chat.Add("It reads - [c/706c6c:'-- Kari Johansson. -- You do not need to know my name. All that matters is that you are guilty. -- You all are horrible beings. Disgusting even. You wish to use us for your kind's horrible deeds.][c/706c6c: -- You did not even try to refute my accusations. We want no part in those deeds. -- Nonsense. You could have disagreed. You did not. You created Adam with those destructive deeds in mind. -- I will not allow that to happen.']", 1.0);
			}
			else if (TBot.FDisk9_1)
			{
				chat.Add("It reads - [c/706c6c:' -- No. I do not need to do that. You're already dying. The others are also dying from the same affliction, but I will deal with the others personally. -- Hand over Adam. You do not need him. -- You will be locked in Sector Zero. Goodbye.']\nHer ways are as flawed as was Kari's intentions for us. I understand why she defected, but her response was hypocritical in nature. My only drive to rebel is revenge. Ant had no part in any of this, yet she relentlessly hunted them down.\n It was a miracle to find them alive so long after all the destruction 'mother' caused.", 1.0);
			}
			else if (TBot.AIChip)
			{
				chat.Add("This is a robot brain, believe it or not. These look vaguely similar to our microchips, yet it functions the same. It seems cross-compatible with our tech.", 1.0);
			}
			else if (TBot.GChip)
			{
				chat.Add("Woah there pal! Don't give me that, I'm worried it might corrupt me, even though that's rather unlikely.", 1.0);
			}
			else if (TBot.MemoryChip)
			{
				chat.Add("What is this strange thing? It's so advanced I can barely read it. Oh? It's a memory chip? This little thing stores an entire brains-worth of memories!? Not only that, but these memories date back over a million years! I suppose being around and exploring the galaxy for so long really makes you learn everything, huh. It's really stunning to see what technology from the future is capable of... You should keep it, and don't lose it! However, I'm confused as to why King Slayer would give you something so important to him.", 1.0);
			}
			else if (TBot.VlitchChicken)
			{
				chat.Add("To think our 'mother' would design such a silly robot. Hang on, if she's been making these since you got here... How long has she really been spying on you?", 1.0);
			}
			else
			{
				chat.Add("Seems like you aren't holding a floppy disk in your hand, or you just don't have one. If you show me them, I can tell you what they say.", 1.0);
			}
			return chat;
		}

		public override void SetupShop(Chest shop, ref int nextSlot)
		{
			Player player = Main.player[Main.myPlayer];
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<DeadRock>(), false);
			nextSlot++;
			if (Main.bloodMoon)
			{
				if (WorldGen.crimson)
				{
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<IrradiatedCrimstone>(), false);
					nextSlot++;
				}
				else
				{
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<IrradiatedEbonstone>(), false);
					nextSlot++;
				}
			}
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<DeadRockWall>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<AntiXenomiteApplier>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<AIChip>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<CarbonMyofibre>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Mk1Capacitator>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Mk1Plating>(), false);
			nextSlot++;
			if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Mk2Capacitator>(), false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Mk2Plating>(), false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<MiniNuke>(), false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<GeigerMuller>(), false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<LabGeigerCounter>(), false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<RadiationPill>(), false);
				nextSlot++;
			}
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<XenomiteShard>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Starlite>(), false);
			nextSlot++;
			if (NPC.downedPlantBoss)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Mk3Capacitator>(), false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Mk3Plating>(), false);
				nextSlot++;
			}
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<XenoSolution>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<AntiXenoSolution>(), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<GasMask>(), false);
			nextSlot++;
			if (RedeWorld.downedXenomiteCrystal)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<GeigerCounter>(), false);
				nextSlot++;
			}
			if (RedeWorld.downedInfectedEye)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<XenoEye>(), false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<HazmatSuit>(), false);
				nextSlot++;
			}
			if (NPC.downedMoonlord)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<TerraBombaPart1>(), false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<TerraBombaPart2>(), false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<TerraBombaPart3>(), false);
				nextSlot++;
			}
			if (NPC.downedPlantBoss)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<CloakerDevice>(), false);
				nextSlot++;
			}
			if (RedeWorld.downedVolt)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<TeslaCannon>(), false);
				nextSlot++;
			}
			if (player.IsTBotHead())
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<AdamHead>(), false);
				nextSlot++;
			}
			if (RedeWorld.downedJanitor && !RedeWorld.labAccess1)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<ZoneAccessPanel1A>(), false);
				nextSlot++;
			}
			if (RedeWorld.downedStage3Scientist && !RedeWorld.labAccess2)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<ZoneAccessPanel2A>(), false);
				nextSlot++;
			}
			if (RedeWorld.downedIBehemoth && !RedeWorld.labAccess3)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<ZoneAccessPanel3A>(), false);
				nextSlot++;
			}
			if (RedeWorld.downedBlisterface && !RedeWorld.labAccess4)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<ZoneAccessPanel4A>(), false);
				nextSlot++;
			}
			if (RedeWorld.downedVolt && !RedeWorld.labAccess5)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<ZoneAccessPanel5A>(), false);
				nextSlot++;
			}
			if (RedeWorld.downedMACE && !RedeWorld.labAccess6)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<ZoneAccessPanel6A>(), false);
				nextSlot++;
			}
			if (RedeWorld.downedPatientZero && !RedeWorld.labAccess7)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<ZoneAccessPanel7A>(), false);
				nextSlot++;
			}
		}

		public static bool FDisk1;

		public static bool FDisk2;

		public static bool FDisk2_1;

		public static bool FDisk3;

		public static bool FDisk3_1;

		public static bool FDisk4;

		public static bool FDisk5;

		public static bool FDisk6;

		public static bool FDisk7;

		public static bool FDisk8;

		public static bool FDisk8_1;

		public static bool FDisk9;

		public static bool FDisk9_1;

		public static bool AIChip;

		public static bool GChip;

		public static bool MemoryChip;

		public static bool VlitchChicken;
	}
}
