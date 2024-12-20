using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class AncientPowerCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Power Core");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(3, 7));
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 30;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 10, 0);
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}
	}
}
