using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class UndownerRede : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Redemption Undowner");
			base.Tooltip.SetDefault("Undowns all Redemption bosses.\r\nNon-Consumable");
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
			RedeWorld.downedMossyGoliath = false;
			RedeWorld.downedTheWarden = false;
			RedeWorld.downedMansionWraith = false;
			RedeWorld.oblitDeath = 0;
			RedeWorld.nebDeath = 0;
			RedeWorld.slayerDeath = 0;
			RedeWorld.nebRep = 0;
			return true;
		}
	}
}
