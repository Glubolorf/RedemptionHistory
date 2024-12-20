using System;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.HM
{
	public class CarbonMyofibre : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Carbon Myofibre");
			base.Tooltip.SetDefault("'Elastic and strong...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 26;
			base.item.maxStack = 999;
			base.item.value = 5000;
			base.item.rare = 5;
		}
	}
}
