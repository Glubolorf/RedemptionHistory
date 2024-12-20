using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Cores
{
	public class EmptyCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Empty Core");
			base.Tooltip.SetDefault("'Fill with the essence of Epidotra'\nCraft a Challenge Core to do certain challenges\nNot recommended if you want to do a normal playthrough\n(Will be reworked)");
		}

		public override void SetDefaults()
		{
			base.item.width = 18;
			base.item.height = 18;
			base.item.value = Item.buyPrice(0, 0, 0, 0);
			base.item.rare = -1;
		}
	}
}
