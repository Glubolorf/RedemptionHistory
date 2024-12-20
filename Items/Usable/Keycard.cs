using System;
using Terraria.ModLoader;

namespace Redemption.Items.Usable
{
	public class Keycard : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Keycard");
			base.Tooltip.SetDefault("'Unlocks Lab Chests and Doors'\nOnly one is needed");
		}

		public override void SetDefaults()
		{
			base.item.width = 44;
			base.item.height = 30;
			base.item.rare = 9;
			base.item.maxStack = 1;
			base.item.value = 0;
		}
	}
}
