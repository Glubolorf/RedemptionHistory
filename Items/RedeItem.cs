using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class RedeItem : GlobalItem
	{
		public override bool CanUseItem(Item item, Player player)
		{
			if (item.type == 1326)
			{
				int num = (int)Main.MouseWorld.X / 16;
				int num2 = (int)Main.MouseWorld.Y / 16;
				if ((int)Main.tile[num, num2].wall == base.mod.WallType("LabWallTileUnsafe"))
				{
					return false;
				}
				if ((int)Main.tile[num, num2].wall == base.mod.WallType("HardenedSludgeWallTile"))
				{
					return false;
				}
				if ((int)Main.tile[num, num2].wall == base.mod.WallType("HardenedyHardenedSludgeWallTile"))
				{
					return false;
				}
			}
			return true;
		}
	}
}
