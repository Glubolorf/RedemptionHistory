using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class UkkoBag : ModItem
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
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override int BossBagNPC
		{
			get
			{
				return base.mod.NPCType("Ukko");
			}
		}

		public override void OpenBossBag(Player player)
		{
			if (Main.rand.Next(7) == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("UkkoMask"), 1);
			}
			int num = Main.rand.Next(3);
			if (num == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("StonePuppet"), 1);
			}
			if (num == 1)
			{
				player.QuickSpawnItem(base.mod.ItemType("EaglecrestGlove"), 1);
			}
			if (num == 2)
			{
				player.QuickSpawnItem(base.mod.ItemType("AncientPowerStave"), 1);
			}
			player.QuickSpawnItem(base.mod.ItemType("ViisaanKantele"), 1);
			player.QuickSpawnItem(base.mod.ItemType("UkonRuno"), 1);
			player.QuickSpawnItem(base.mod.ItemType("AncientPowerCore"), Main.rand.Next(9, 18));
		}
	}
}
