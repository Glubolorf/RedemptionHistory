using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Datalogs
{
	public class MemoryChip : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Memory Chip");
			base.Tooltip.SetDefault("Has no use to you, but it would be a good idea to keep it for now...");
		}

		public override void SetDefaults()
		{
			base.item.width = 14;
			base.item.height = 42;
			base.item.maxStack = 1;
			base.item.rare = 9;
			base.item.value = Item.sellPrice(0, 0, 0, 0);
		}
	}
}
