using System;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class DruidNote : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("A Note on Druid Class");
			base.Tooltip.SetDefault("It reads - 'The druid class is unfinished, if you decide to try it out,\nyou will find hardmode to be impossible, as there are very few druid items for it.\nWhen you have finished reading, you have permission to trash this. :)'");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 22;
			base.item.maxStack = 1;
			base.item.value = 0;
			base.item.rare = 0;
		}
	}
}
