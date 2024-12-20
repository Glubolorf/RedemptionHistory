using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.PreHM
{
	public class AncientBrassChunk : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Brass Chunk");
			base.Tooltip.SetDefault("'A chunk of old brass, Gathuram used brass for weapons before they discovered Pure-Iron'");
		}

		public override void SetDefaults()
		{
			base.item.width = 16;
			base.item.height = 16;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 0, 0);
			base.item.rare = -1;
		}
	}
}
