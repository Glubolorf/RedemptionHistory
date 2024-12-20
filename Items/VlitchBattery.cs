using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class VlitchBattery : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Vlitch Battery");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(4, 3));
		}

		public override void SetDefaults()
		{
			base.item.width = 18;
			base.item.height = 46;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 20, 0);
			base.item.rare = 10;
		}
	}
}
