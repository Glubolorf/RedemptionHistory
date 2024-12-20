using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class SlayerBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Treasure Bag");
			base.Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			base.item.maxStack = 999;
			base.item.consumable = true;
			base.item.width = 32;
			base.item.height = 32;
			base.item.rare = 9;
			base.item.expert = true;
			this.bossBagNPC = base.mod.NPCType("KSEntrance");
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
		{
			if (Main.rand.Next(7) == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("KingSlayerMask"), 1);
			}
			int num = Main.rand.Next(4);
			if (num == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("SlayerFlamethrower"), 1);
			}
			if (num == 1)
			{
				player.QuickSpawnItem(base.mod.ItemType("SlayerNanogun"), 1);
			}
			if (num == 2)
			{
				player.QuickSpawnItem(base.mod.ItemType("SlayerFist"), 1);
			}
			if (num == 3)
			{
				player.QuickSpawnItem(base.mod.ItemType("SlayerGun"), 1);
			}
			player.QuickSpawnItem(base.mod.ItemType("KingCore"), 1);
			player.QuickSpawnItem(base.mod.ItemType("SlayerMedal"), 1);
			player.QuickSpawnItem(base.mod.ItemType("CyberPlating"), Main.rand.Next(12, 16));
		}
	}
}
