using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.HM
{
	public class BrokenHeroStave : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Broken Hero Stave");
		}

		public override void SetDefaults()
		{
			base.item.width = 28;
			base.item.height = 28;
			base.item.maxStack = 99;
			base.item.value = Item.buyPrice(0, 2, 0, 0);
			base.item.rare = 8;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}
	}
}
