using System;
using Redemption.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class InfectedLens : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Infected Lens");
			base.Tooltip.SetDefault("'Will NOT improve your eyesight!'\nDropped by Infected Demon Eyes in Hardmode");
		}

		public override void SetDefaults()
		{
			base.item.width = 16;
			base.item.height = 22;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 10, 0);
			base.item.rare = 2;
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(ModContent.BuffType<XenomiteDebuff>(), Main.rand.Next(10, 20), true);
		}
	}
}
