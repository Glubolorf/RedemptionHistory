using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	[AutoloadHead]
	public class TBot : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Friendy T-Bot");
			Main.npcFrameCount[base.npc.type] = 26;
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
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/TBotGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/TBotGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/TBotGore3"), 1f);
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

		public override bool CanTownNPCSpawn(int numTownNPCs, int money)
		{
			return RedeWorld.downedInfectedEye;
		}

		public override string TownNPCName()
		{
			switch (WorldGen.genRand.Next(6))
			{
			case 0:
				return "Tom";
			case 1:
				return "Tim";
			case 2:
				return "Thomas";
			case 3:
				return "Timmy";
			case 4:
				return "Tied";
			default:
				return "Tommy";
			}
		}

		public override string GetChat()
		{
			int num = NPC.FindFirstNPC(22);
			if (num >= 0 && Main.rand.Next(9) == 0)
			{
				return Main.npc[num].GivenName + " is very interested in finding out the material my skeleton is made out of. Too bad taking the plates off could damage me. Even I am not sure what I am made out of.";
			}
			int num2 = NPC.FindFirstNPC(17);
			if (num2 >= 0 && Main.rand.Next(9) == 0)
			{
				return Main.npc[num2].GivenName + " keeps trying to sell his stuff to me. I don't even have any money! I smelted all of it to bars. Not sure why.";
			}
			int num3 = NPC.FindFirstNPC(20);
			if (num3 >= 0 && Main.rand.Next(9) == 0)
			{
				return "Apparently " + Main.npc[num3].GivenName + " told me about the Corruption. I have compared the differences between The Corruption and The Girus Corruption. They are quite similar, but your corruption affects living matter, but mine only affects robotics.";
			}
			int num4 = NPC.FindFirstNPC(18);
			if (num4 >= 0 && Main.rand.Next(9) == 0)
			{
				return "No, " + Main.npc[num4].GivenName + ", I don't need healing, I am a robot. You don't know how I work.";
			}
			int num5 = NPC.FindFirstNPC(19);
			if (num5 >= 0 && Main.rand.Next(9) == 0)
			{
				return Main.npc[num5].GivenName + "'s weapons are useless to me. I haven't been programmed to fight against anything, as it would break the Laws of Robotics.";
			}
			int num6 = NPC.FindFirstNPC(209);
			if (num6 >= 0 && Main.rand.Next(9) == 0)
			{
				return "Everyone else looks at me in a weird way, but " + Main.npc[num6].GivenName + " seems to be fine with me.";
			}
			if (Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss && Main.rand.Next(9) == 0)
			{
				return "You killed a giant tulip? What? That doesn't make any sense.";
			}
			if (Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss && NPC.downedGolemBoss && Main.rand.Next(9) == 0)
			{
				return "There was a giant brown robot in the jungle? I am starting to think you're telling me lies.";
			}
			if (Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss && NPC.downedGolemBoss && NPC.downedAncientCultist && NPC.downedTowers && NPC.downedMoonlord && Main.rand.Next(9) == 0)
			{
				return "You destroyed a thing that shouldn't even be able to be killed, as Cthulhu is an entity you humans cannot overwhelm. Neither can I, it'd crash my system... Oh wait, that was Cthulhu's brother.";
			}
			if (RedeWorld.downedVlitch1 || (RedeWorld.downedVlitch2 && Main.rand.Next(9) == 0))
			{
				return "You defeated a Vlitch Overlord?! Oh this can't be good...";
			}
			if (RedeWorld.downedVlitch1 || (RedeWorld.downedVlitch2 && Main.rand.Next(9) == 0))
			{
				return "Why am i concerned about the Overlords? Well, let's say I've had a close encounter with their leader. Not someone you'd like to mess with.";
			}
			if (RedeWorld.downedSlayer && Main.rand.Next(9) == 0)
			{
				return "Oh King Slayer? He's bit of... well...";
			}
			switch (Main.rand.Next(9))
			{
			case 0:
				return "Have you seen a boy with glasses? I've lost him, and i need to find him.";
			case 1:
				return "I hope you are protecting me, as I am utterly defenceless...";
			case 2:
				return "Why am I here?";
			case 3:
				return "Why do I have a limitless supply of robot parts? I dunno, maybe I craft them using the coins I've smelted?";
			case 4:
				return "I once painted a painting. Some guy from the corner of the room said 'Oh my god...' to himself.";
			case 5:
				return "The wasteland? It used to be a good place to live, but someone pressed the nuke button and initiated Mutual Nuclear Destruction. Only us androids survived the nuclear annihilation. That's where I'm from, too.";
			case 6:
				return "Oh this powercell in my chest? It's quite the marvel of technology, uses pure Xenomite. Sadly I can't explain how it works, I wasn't designed to be a factory machine.";
			case 7:
				return "You're interested in what the green crystals do? They technically aren't crystals, but sure. Its called Xenomite, and they're kind of like fungi, they consume flesh and ooze this green gloop that crystallizes overtime. Not sure of its origin, though. They do have high levels of Radium and some other radioactive materials, though.";
			default:
				return "Hey, why are you giving me a glare? Never seen a T-Bot before?";
			}
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = Language.GetTextValue("LegacyInterface.28");
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			if (firstButton)
			{
				shop = true;
			}
		}

		public override void SetupShop(Chest shop, ref int nextSlot)
		{
			shop.item[nextSlot].SetDefaults(base.mod.ItemType("DeadRock"), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(base.mod.ItemType("DeadRockWall"), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(base.mod.ItemType("AntiXenomiteApplier"), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(base.mod.ItemType("AIChip"), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(base.mod.ItemType("CarbonMyofibre"), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(base.mod.ItemType("Mk1Capacitator"), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(base.mod.ItemType("Mk1Plating"), false);
			nextSlot++;
			if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
			{
				shop.item[nextSlot].SetDefaults(base.mod.ItemType("Mk2Capacitator"), false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(base.mod.ItemType("Mk2Plating"), false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(base.mod.ItemType("MiniNuke"), false);
				nextSlot++;
			}
			shop.item[nextSlot].SetDefaults(base.mod.ItemType("XenomiteShard"), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(base.mod.ItemType("Starlite"), false);
			nextSlot++;
			if (NPC.downedPlantBoss)
			{
				shop.item[nextSlot].SetDefaults(base.mod.ItemType("Mk3Capacitator"), false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(base.mod.ItemType("Mk3Plating"), false);
				nextSlot++;
			}
			shop.item[nextSlot].SetDefaults(base.mod.ItemType("XenoSolution"), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(base.mod.ItemType("AntiXenoSolution"), false);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(base.mod.ItemType("GasMask"), false);
			nextSlot++;
			if (RedeWorld.downedXenomiteCrystal)
			{
				shop.item[nextSlot].SetDefaults(base.mod.ItemType("GeigerCounter"), false);
				nextSlot++;
			}
			if (RedeWorld.downedInfectedEye)
			{
				shop.item[nextSlot].SetDefaults(base.mod.ItemType("XenoEye"), false);
				nextSlot++;
			}
		}
	}
}
