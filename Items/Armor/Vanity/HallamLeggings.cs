using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Vanity
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class HallamLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hallam's Casual Trousers");
			base.Tooltip.SetDefault("'Great for impersonating devs!'");
		}

		public override void SetDefaults()
		{
			base.item.width = 18;
			base.item.height = 14;
			base.item.rare = 9;
			base.item.expert = true;
			base.item.vanity = true;
		}
	}
}
