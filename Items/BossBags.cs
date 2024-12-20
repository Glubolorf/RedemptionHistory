using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class BossBags : GlobalItem
	{
		public override void OpenVanillaBag(string context, Player player, int arg)
		{
			if (context == "bossBag" && arg == 3319 && Main.rand.Next(3) == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("EyeStalkBag"), 1);
			}
			if (context == "bossBag" && arg == 3329 && Main.rand.Next(8) == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("LihzWorldStave"), 1);
			}
			if (context == "bossBag" && arg == 3329 && Main.rand.Next(8) == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("LihzWorldStave"), 1);
			}
			if (context == "bossBag" && arg == 3328)
			{
				player.QuickSpawnItem(base.mod.ItemType("SoulOfBloom"), Main.rand.Next(120, 180));
				if (Main.rand.Next(7) == 0)
				{
					int num = Main.rand.Next(2);
					if (num == 0)
					{
						player.QuickSpawnItem(base.mod.ItemType("PlanterasStave1"), 1);
					}
					if (num == 1)
					{
						player.QuickSpawnItem(base.mod.ItemType("PlanterasStave2"), 1);
					}
				}
			}
			if (context == "bossBag" && arg == 3324)
			{
				if (Main.rand.Next(8) == 0)
				{
					player.QuickSpawnItem(base.mod.ItemType("DruidEmblem"), 1);
				}
				if (Main.rand.Next(6) == 0)
				{
					player.QuickSpawnItem(base.mod.ItemType("WallsClaw"), 1);
				}
			}
		}
	}
}
