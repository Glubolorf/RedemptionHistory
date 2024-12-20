using System;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class ChickenContract : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Strange Contract");
			base.Tooltip.SetDefault("It reads - [c/ff4c4c:'You fool...']");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 22;
			base.item.maxStack = 999;
			base.item.value = 0;
			base.item.rare = 0;
		}
	}
}
