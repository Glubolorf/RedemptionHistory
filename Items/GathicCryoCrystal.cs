using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class GathicCryoCrystal : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Gathic Cryo-Crystal");
			base.Tooltip.SetDefault("'A freezing cold crystal Ragnos discovered, might come in handy...'\nMakes the player Chilled when held\nSold by Ragnos after Skeletron is defeated");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 24;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 25, 0);
			base.item.rare = 4;
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(46, 60, true);
		}
	}
}
