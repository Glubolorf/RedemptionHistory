using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Costumes
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class AndroidPants : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Android Pants");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 16;
			base.item.rare = 1;
			base.item.value = Item.buyPrice(0, 3, 0, 0);
			base.item.vanity = true;
		}

		public override bool DrawLegs()
		{
			return false;
		}
	}
}
