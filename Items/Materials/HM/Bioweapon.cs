using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.HM
{
	public class Bioweapon : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bio-Weapon");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 22;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 8, 0);
			base.item.rare = 7;
		}
	}
}
