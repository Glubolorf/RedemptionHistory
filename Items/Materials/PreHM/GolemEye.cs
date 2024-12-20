using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.PreHM
{
	public class GolemEye : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Eye of the Eaglecrest Golem");
		}

		public override void SetDefaults()
		{
			base.item.width = 14;
			base.item.height = 14;
			base.item.maxStack = 1;
			base.item.value = Item.sellPrice(0, 5, 0, 0);
			base.item.rare = 3;
		}
	}
}
