using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class GoldTribalLegs : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Gold Tribal Loincloth");
		}

		public override void SetDefaults()
		{
			base.item.width = 14;
			base.item.height = 10;
			base.item.rare = 3;
			base.item.vanity = true;
		}
	}
}
