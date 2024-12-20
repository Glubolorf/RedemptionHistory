using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Vanity
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class HalDevLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Servant's Godly Leggings");
			base.Tooltip.SetDefault("'Boondoggling!'");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 18;
			base.item.rare = 11;
			base.item.vanity = true;
		}
	}
}
