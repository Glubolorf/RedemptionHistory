using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class KingChickenBag : ModItem
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
			this.bossBagNPC = base.mod.NPCType("KingChicken");
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
		{
			if (Main.rand.Next(7) == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("KingChickenMask"), 1);
			}
			if (Main.rand.Next(3) == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("EggStaff"), 1);
			}
			player.QuickSpawnItem(base.mod.ItemType("SpiritOfLife"), 1);
			player.QuickSpawnItem(base.mod.ItemType("Grain"), 1);
			player.QuickSpawnItem(264, 1);
		}
	}
}
