using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	[AutoloadEquip(new EquipType[]
	{
		10
	})]
	public class ShieldRusty : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Rusty Shield");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 26;
			base.item.value = Item.buyPrice(0, 0, 1, 0);
			base.item.rare = -1;
			base.item.accessory = true;
			base.item.defense = 1;
		}
	}
}
