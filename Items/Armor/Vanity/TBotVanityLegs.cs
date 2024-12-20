using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Vanity
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class TBotVanityLegs : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("T-Bot Legs");
		}

		public override void SetDefaults()
		{
			base.item.width = 18;
			base.item.height = 22;
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
