using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class AkkaBag : ModItem
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
				return base.mod.NPCType("Akka");
			}
		}

		public override void OpenBossBag(Player player)
		{
			if (Main.rand.Next(7) == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("AkkaMask"), 1);
			}
			int num = Main.rand.Next(2);
			if (num == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("CursedThornBow2"), 1);
			}
			if (num == 1)
			{
				player.QuickSpawnItem(base.mod.ItemType("CursedThornFlail"), 1);
			}
			player.QuickSpawnItem(base.mod.ItemType("TuhonAura"), 1);
			player.QuickSpawnItem(base.mod.ItemType("Verenhimo"), 1);
			player.QuickSpawnItem(base.mod.ItemType("CursedThorns"), Main.rand.Next(9, 18));
			player.QuickSpawnItem(base.mod.ItemType("CrownOfThorns"), 1);
		}
	}
}
