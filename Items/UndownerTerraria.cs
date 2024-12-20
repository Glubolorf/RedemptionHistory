using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class UndownerTerraria : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Terraria Undowner");
			base.Tooltip.SetDefault("Undowns all Vanilla bosses.\r\nNon-Consumable");
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
			return true;
		}
	}
}
