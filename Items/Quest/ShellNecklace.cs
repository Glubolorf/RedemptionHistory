using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Quest
{
	[AutoloadEquip(new EquipType[]
	{
		11
	})]
	public class ShellNecklace : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shell Necklace");
			base.Tooltip.SetDefault("'Makes you feel one with nature'");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 26;
			base.item.value = Item.buyPrice(0, 1, 0, 0);
			base.item.vanity = true;
			base.item.rare = 2;
			base.item.accessory = true;
		}
	}
}
