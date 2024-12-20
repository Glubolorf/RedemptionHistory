using System;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.PreHM
{
	public class BrokenRustedSword1 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Rusted Sword Fragment");
			base.Tooltip.SetDefault("'A piece of a strange weapon...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 42;
			base.item.height = 42;
			base.item.maxStack = 1;
			base.item.value = 0;
			base.item.rare = -1;
		}
	}
}
