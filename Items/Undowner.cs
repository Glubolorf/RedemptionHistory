using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class Undowner : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Undowner");
			base.Tooltip.SetDefault("Undowns all bosses.\r\nNon-Consumable");
		}

		public override void SetDefaults()
		{
			base.item.width = 16;
			base.item.height = 16;
			base.item.rare = 2;
			base.item.value = Item.sellPrice(0, 0, 0, 0);
			base.item.useAnimation = 45;
			base.item.useTime = 45;
			base.item.useStyle = 4;
		}

		public override bool UseItem(Player player)
		{
			NPC.downedAncientCultist = false;
			NPC.downedBoss1 = false;
			NPC.downedBoss2 = false;
			NPC.downedBoss3 = false;
			NPC.downedChristmasIceQueen = false;
			NPC.downedChristmasSantank = false;
			NPC.downedChristmasTree = false;
			NPC.downedClown = false;
			NPC.downedFishron = false;
			NPC.downedFrost = false;
			NPC.downedGoblins = false;
			NPC.downedGolemBoss = false;
			NPC.downedHalloweenKing = false;
			NPC.downedHalloweenTree = false;
			NPC.downedMartians = false;
			NPC.downedMechBoss1 = false;
			NPC.downedMechBoss2 = false;
			NPC.downedMechBoss3 = false;
			NPC.downedMechBossAny = false;
			NPC.downedMoonlord = false;
			NPC.downedPirates = false;
			NPC.downedPlantBoss = false;
			NPC.downedQueenBee = false;
			NPC.downedSlimeKing = false;
			NPC.downedTowerNebula = false;
			NPC.downedTowerSolar = false;
			NPC.downedTowerStardust = false;
			NPC.downedTowerVortex = false;
			RedeWorld.downedBlisterface = false;
			RedeWorld.downedChickenInv = false;
			RedeWorld.downedChickenInvPZ = false;
			RedeWorld.downedDarkSlime = false;
			RedeWorld.downedEaglecrestGolem = false;
			RedeWorld.downedEaglecrestGolemPZ = false;
			RedeWorld.downedIBehemoth = false;
			RedeWorld.nukeDropped = false;
			RedeWorld.downedKingChicken = false;
			RedeWorld.downedMACE = false;
			RedeWorld.downedNebuleus = false;
			RedeWorld.downedPatientZero = false;
			RedeWorld.downedSkullDigger = false;
			RedeWorld.downedSlayer = false;
			RedeWorld.downedStage3Scientist = false;
			RedeWorld.downedStrangePortal = false;
			RedeWorld.downedSunkenCaptain = false;
			RedeWorld.downedKeeper = false;
			RedeWorld.downedVlitch1 = false;
			RedeWorld.downedVlitch2 = false;
			RedeWorld.downedVlitch3 = false;
			RedeWorld.downedSoI = false;
			RedeWorld.girusTalk1 = false;
			RedeWorld.girusTalk2 = false;
			RedeWorld.girusTalk3 = false;
			RedeWorld.keeperSaved = false;
			RedeWorld.downedThorn = false;
			RedeWorld.downedThornPZ = false;
			RedeWorld.downedJanitor = false;
			RedeWorld.downedVolt = false;
			RedeWorld.KSRajahInteraction = false;
			RedeWorld.voltBegin = false;
			RedeWorld.downedTheWarden = false;
			RedeWorld.downedMansionWraith = false;
			RedeWorld.oblitDeath = 0;
			RedeWorld.nebDeath = 0;
			RedeWorld.slayerDeath = 0;
			return true;
		}
	}
}
