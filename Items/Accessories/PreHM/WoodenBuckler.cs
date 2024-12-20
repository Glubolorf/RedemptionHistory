using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.PreHM
{
	[AutoloadEquip(new EquipType[]
	{
		10
	})]
	public class WoodenBuckler : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Wooden Buckler");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 22;
			base.item.value = Item.buyPrice(0, 0, 25, 0);
			base.item.rare = 1;
			base.item.accessory = true;
			base.item.defense = 1;
		}
	}
}
