using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Vanity
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class IntruderPants : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Intruder's Armored Pants");
		}

		public override void SetDefaults()
		{
			base.item.width = 18;
			base.item.height = 22;
			base.item.rare = 4;
			base.item.vanity = true;
		}

		public override bool DrawLegs()
		{
			return false;
		}
	}
}
