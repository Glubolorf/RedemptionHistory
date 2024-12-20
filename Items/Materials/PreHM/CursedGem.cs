using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.PreHM
{
	public class CursedGem : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Gem");
			base.Tooltip.SetDefault("'A gem... or an eye?'");
		}

		public override void SetDefaults()
		{
			base.item.width = 18;
			base.item.height = 18;
			base.item.maxStack = 1;
			base.item.value = Item.sellPrice(0, 5, 0, 0);
			base.item.rare = 7;
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(33, 10, true);
		}
	}
}
