using System;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class BlackGloop : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Black Gloop");
			base.Tooltip.SetDefault("'Sticky...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 22;
			base.item.maxStack = 999;
			base.item.value = 0;
			base.item.rare = 10;
		}
	}
}
