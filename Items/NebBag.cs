using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class NebBag : ModItem
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
			this.bossBagNPC = base.mod.NPCType("BigNebuleus");
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
		{
			if (Main.rand.Next(7) == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("NebuleusMask"), 1);
			}
			if (Main.rand.Next(4) == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("GildedBonnet"), 1);
			}
			if (Main.rand.Next(2) == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("FreedomStarN"), 1);
			}
			if (Main.rand.Next(2) == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("NebulaStarFlail"), 1);
			}
			if (Main.rand.Next(2) == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("ConstellationsBook"), 1);
			}
			if (Main.rand.Next(2) == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("StarfruitSeedbag"), 1);
			}
		}
	}
}
