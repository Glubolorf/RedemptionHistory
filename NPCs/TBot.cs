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
			base.DisplayName.SetDefault("Friendly T-Bot");
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
			switch (WorldGen.genRand.Next(7))
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
			case 5:
				return "Adam";
			default:
				return "Tommy";
			}
		}

		public override string GetChat()
		{
			int GuideID = NPC.FindFirstNPC(22);
			if (GuideID >= 0 && Main.rand.Next(9) == 0)
			{
				return Main.npc[GuideID].GivenName + " is very interested in finding out the material my skeleton is made out of. Too bad taking the plates off could damage me. Even I am not sure what I am made out of.";
			}
			int MerchantID = NPC.FindFirstNPC(17);
			if (MerchantID >= 0 && Main.rand.Next(9) == 0)
			{
				return Main.npc[MerchantID].GivenName + " keeps trying to sell his stuff to me. I don't even have any money! I smelted all of it to bars. Not sure why.";
			}
			int DryadID = NPC.FindFirstNPC(20);
			if (DryadID >= 0 && Main.rand.Next(9) == 0)
			{
				return "Apparently " + Main.npc[DryadID].GivenName + " told me about the Corruption. I have compared the differences between The Corruption and The Girus Corruption. They are quite similar, but your corruption affects living matter, but mine only affects robotics.";
			}
			int NurseID = NPC.FindFirstNPC(18);
			if (NurseID >= 0 && Main.rand.Next(9) == 0)
			{
				return "No, " + Main.npc[NurseID].GivenName + ", I don't need healing, I am a robot. You don't know how I work.";
			}
			int ArmsDealerID = NPC.FindFirstNPC(19);
			if (ArmsDealerID >= 0 && Main.rand.Next(9) == 0)
			{
				return Main.npc[ArmsDealerID].GivenName + "'s weapons are useless to me. I haven't been programmed to fight against anything, as it would break the Laws of Robotics.";
			}
			int cyborgID = NPC.FindFirstNPC(209);
			if (cyborgID >= 0 && Main.rand.Next(9) == 0)
			{
				return "Everyone else looks at me in a weird way, but " + Main.npc[cyborgID].GivenName + " seems to be fine with me.";
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
			if ((RedeWorld.downedVlitch1 || RedeWorld.downedVlitch2) && Main.rand.Next(9) == 0)
			{
				return "You defeated a Vlitch Overlord?! Oh this can't be good...";
			}
			if ((RedeWorld.downedVlitch1 || RedeWorld.downedVlitch2) && Main.rand.Next(9) == 0)
			{
				return "Why am I concerned about the Overlords? Well, let's say I've had a close encounter with their leader. Not someone you'd like to mess with.";
			}
			if (RedeWorld.downedSlayer && Main.rand.Next(9) == 0)
			{
				return "Oh King Slayer? He's bit of... well...";
			}
			if (RedeWorld.tbotLabAccess && Main.rand.Next(9) == 0)
			{
				return "Hey! I noticed you were exploring the lab. I was the one who deactivated the lasers. I found something in the lab that could be useful to you, it's a Tesla Cannon!";
			}
			switch (Main.rand.Next(10))
			{
			case 0:
				return "I'm not sure how I got here, but for the time being I'll stay in this building you've made.";
			case 1:
				return "I hope you are protecting me, as I am utterly defenceless...";
			case 2:
				return "Why am I here?";
			case 3:
				return "Why do I have a limitless supply of robot parts? I dunno, maybe I craft them using the coins I've smelted?";
			case 4:
				return "The wasteland? It used to be a good place to live, but someone pressed the nuke button and initiated Mutual Nuclear Destruction. Only us androids survived the nuclear annihilation. That's where I'm from, too.";
			case 5:
				return "Oh this powercell in my chest? It's quite the marvel of technology, uses pure Xenomite. Sadly I can't explain how it works, I wasn't designed to be a factory machine.";
			case 6:
				return "You're interested in what the green crystals do? They technically aren't crystals, but sure. Its called Xenomite, and they're kind of like fungi, they consume flesh and ooze this green gloop that crystallizes overtime. Not sure of its origin, though. They do have high levels of Radium and some other radioactive materials, though.";
			case 7:
				return "If you ever explore the wasteland or abandoned lab, it's a good idea to carry a Geiger-M�ller with you - it'll warn you if you're near radioactive materials. However, the wasteland and laboratory seems to set off the M�ller even though the radiation is too minor to affect you.";
			case 8:
				return "Here's a good tip, DON'T go near uranium, plutonium, and ESPECIALLY corium. Those are radioactive, and going too close will give you a heavy dose of radiation.";
			case 9:
				return "The deadly thing with radiation is, at first, you won't even know you've got it. The first symptoms usually start minutes after, beginning with a headache most likely, then dizziness, fatigue, bleeding, skin burns, a fever, hair loss, and death.";
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
				shop.item[nextSlot].SetDefaults(base.mod.ItemType("GeigerMuller"), false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(base.mod.ItemType("RadiationPill"), false);
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
				shop.item[nextSlot].SetDefaults(base.mod.ItemType("HazmatSuit"), false);
				nextSlot++;
			}
			if (NPC.downedMoonlord)
			{
				shop.item[nextSlot].SetDefaults(base.mod.ItemType("TerraBombaPart1"), false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(base.mod.ItemType("TerraBombaPart2"), false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(base.mod.ItemType("TerraBombaPart3"), false);
				nextSlot++;
			}
			if (RedeWorld.tbotLabAccess)
			{
				shop.item[nextSlot].SetDefaults(base.mod.ItemType("TeslaCannon"), false);
				nextSlot++;
			}
			if ((RedeWorld.downedStage2Scientist || RedeWorld.downedJanitor) && !RedeWorld.labAccess1)
			{
				shop.item[nextSlot].SetDefaults(base.mod.ItemType("ZoneAccessPanel1"), false);
				nextSlot++;
			}
			if (RedeWorld.downedStage3Scientist && !RedeWorld.labAccess2)
			{
				shop.item[nextSlot].SetDefaults(base.mod.ItemType("ZoneAccessPanel2"), false);
				nextSlot++;
			}
			if (RedeWorld.downedIBehemoth && !RedeWorld.labAccess3)
			{
				shop.item[nextSlot].SetDefaults(base.mod.ItemType("ZoneAccessPanel3"), false);
				nextSlot++;
			}
			if (RedeWorld.downedBlisterface && !RedeWorld.labAccess4)
			{
				shop.item[nextSlot].SetDefaults(base.mod.ItemType("ZoneAccessPanel4"), false);
				nextSlot++;
			}
			if (RedeWorld.tbotLabAccess && !RedeWorld.labAccess5)
			{
				shop.item[nextSlot].SetDefaults(base.mod.ItemType("ZoneAccessPanel5"), false);
				nextSlot++;
			}
			if (RedeWorld.downedMACE && !RedeWorld.labAccess6)
			{
				shop.item[nextSlot].SetDefaults(base.mod.ItemType("ZoneAccessPanel6"), false);
				nextSlot++;
			}
			if (RedeWorld.downedPatientZero && !RedeWorld.labAccess7)
			{
				shop.item[nextSlot].SetDefaults(base.mod.ItemType("ZoneAccessPanel7"), false);
				nextSlot++;
			}
		}
	}
}
