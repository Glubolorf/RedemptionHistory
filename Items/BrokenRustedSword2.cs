using System;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class BrokenRustedSword2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Rusted Sword Fragment");
			base.Tooltip.SetDefault("'A piece of a strange weapon...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 38;
			base.item.height = 38;
			base.item.maxStack = 1;
			base.item.value = 0;
			base.item.rare = -1;
		}
	}
}
