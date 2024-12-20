using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class InfectedEyeBag : ModItem
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
			this.bossBagNPC = base.mod.NPCType("InfectedEye");
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
		{
			player.QuickSpawnItem(base.mod.ItemType("Xenomite"), Main.rand.Next(4, 8));
			if (Main.rand.Next(20) == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("TiedsMask"), 1);
				player.QuickSpawnItem(base.mod.ItemType("TiedsSuit"), 1);
				player.QuickSpawnItem(base.mod.ItemType("TiedsLeggings"), 1);
			}
			if (Main.rand.Next(2) == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("XenomiteStaff"), 1);
			}
			if (Main.rand.Next(3) == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("TheInfectedEye"), 1);
			}
			if (Main.rand.Next(3) == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("InfectousJavelin"), 1);
			}
			player.QuickSpawnItem(base.mod.ItemType("AntiCrystallizer"), 1);
			player.QuickSpawnItem(base.mod.ItemType("AntiXenomiteApplier"), Main.rand.Next(2, 6));
		}
	}
}
