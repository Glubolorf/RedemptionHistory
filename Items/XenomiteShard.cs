using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class XenomiteShard : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenomite Shard");
			base.Tooltip.SetDefault("'Holding this may infect you...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 12;
			base.item.height = 12;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 0, 75);
			base.item.rare = 2;
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(base.mod.BuffType("XenomiteDebuff"), Main.rand.Next(10, 20), true);
		}
	}
}
