using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class CyberPlating : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cyber Plating");
			base.Tooltip.SetDefault("'Resistant to everything'");
		}

		public override void SetDefaults()
		{
			base.item.width = 28;
			base.item.height = 32;
			base.item.maxStack = 30;
			base.item.value = Item.sellPrice(0, 75, 0, 0);
			base.item.rare = 9;
		}
	}
}
