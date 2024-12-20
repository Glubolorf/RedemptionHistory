using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class TheKeeperBag : ModItem
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
			base.item.width = 24;
			base.item.height = 24;
			base.item.rare = 9;
			base.item.expert = true;
			this.bossBagNPC = base.mod.NPCType("TheKeeper");
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
		{
			if (Main.rand.Next(7) == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("TheKeeperMask"), 1);
			}
			if (Main.rand.Next(3) == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("OldGathicWaraxe"), 1);
			}
			player.QuickSpawnItem(base.mod.ItemType("DarkShard"), 1);
			player.QuickSpawnItem(base.mod.ItemType("TheKeepersCrown"), 1);
		}
	}
}
