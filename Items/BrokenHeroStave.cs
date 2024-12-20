using System;
using Redemption.Items.DruidDamageClass;
using Terraria;

namespace Redemption.Items
{
	public class BrokenHeroStave : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Broken Hero Stave");
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 28;
			base.item.height = 28;
			base.item.maxStack = 99;
			base.item.value = Item.buyPrice(0, 2, 0, 0);
			base.item.rare = 8;
		}
	}
}
