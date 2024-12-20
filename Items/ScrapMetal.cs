using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class ScrapMetal : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Scrap Metal");
			base.Tooltip.SetDefault("'Surely I can get something useful from this scrap...'\n{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			base.item.maxStack = 999;
			base.item.consumable = true;
			base.item.width = 42;
			base.item.height = 28;
			base.item.rare = -1;
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
			if (Main.rand.Next(2) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<Mk1Plating>(), Main.rand.Next(1, 3));
			}
			if (Main.rand.Next(2) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<Mk1Capacitator>(), Main.rand.Next(1, 2));
			}
			if (Main.rand.Next(75) == 0)
			{
				player.QuickSpawnItem(205, 1);
			}
			if (Main.rand.Next(75) == 0)
			{
				player.QuickSpawnItem(324, 1);
			}
			if (Main.rand.Next(75) == 0)
			{
				player.QuickSpawnItem(164, 1);
			}
			if (Main.rand.Next(75) == 0)
			{
				player.QuickSpawnItem(434, 1);
			}
			if (Main.rand.Next(75) == 0)
			{
				player.QuickSpawnItem(426, 1);
			}
			if (Main.rand.Next(75) == 0)
			{
				player.QuickSpawnItem(3012, 1);
			}
			if (Main.rand.Next(100) == 0)
			{
				player.QuickSpawnItem(930, 1);
			}
			if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
			{
				if (Main.rand.Next(75) == 0)
				{
					player.QuickSpawnItem(760, 1);
				}
				if (Main.rand.Next(2) == 0)
				{
					player.QuickSpawnItem(ModContent.ItemType<Mk2Plating>(), Main.rand.Next(1, 3));
				}
				if (Main.rand.Next(2) == 0)
				{
					player.QuickSpawnItem(ModContent.ItemType<Mk2Capacitator>(), Main.rand.Next(1, 2));
				}
			}
			if (NPC.downedGolemBoss)
			{
				if (Main.rand.Next(2) == 0)
				{
					player.QuickSpawnItem(ModContent.ItemType<Mk3Plating>(), Main.rand.Next(1, 3));
				}
				if (Main.rand.Next(2) == 0)
				{
					player.QuickSpawnItem(ModContent.ItemType<Mk3Capacitator>(), Main.rand.Next(1, 2));
				}
				if (Main.rand.Next(75) == 0)
				{
					player.QuickSpawnItem(2798, 1);
				}
				if (Main.rand.Next(75) == 0)
				{
					player.QuickSpawnItem(2880, 1);
				}
				if (Main.rand.Next(75) == 0)
				{
					player.QuickSpawnItem(2882, 1);
				}
				if (Main.rand.Next(75) == 0)
				{
					player.QuickSpawnItem(2795, 1);
				}
				if (Main.rand.Next(75) == 0)
				{
					player.QuickSpawnItem(3249, 1);
				}
			}
			if (NPC.downedMechBossAny)
			{
				if (Main.rand.Next(75) == 0)
				{
					player.QuickSpawnItem(3623, 1);
				}
				if (Main.rand.Next(75) == 0)
				{
					player.QuickSpawnItem(779, 1);
				}
				player.QuickSpawnItem(1344, Main.rand.Next(4, 18));
				if (Main.rand.Next(2) == 0)
				{
					player.QuickSpawnItem(391, Main.rand.Next(2, 6));
				}
				if (Main.rand.Next(2) == 0)
				{
					player.QuickSpawnItem(1198, Main.rand.Next(2, 6));
				}
			}
			int num = Main.rand.Next(2);
			if (num == 0)
			{
				player.QuickSpawnItem(22, Main.rand.Next(2, 6));
			}
			if (num == 1)
			{
				player.QuickSpawnItem(704, Main.rand.Next(2, 6));
			}
		}
	}
}
