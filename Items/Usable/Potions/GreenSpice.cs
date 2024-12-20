using System;
using Redemption.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Usable.Potions
{
	public class GreenSpice : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Green Spices");
			base.Tooltip.SetDefault("'Tastes quite earthly, maybe basil'\nSlightly increased druid damaged and crit chance");
		}

		public override void SetDefaults()
		{
			base.item.UseSound = SoundID.Item2;
			base.item.useStyle = 2;
			base.item.useTurn = true;
			base.item.useAnimation = 13;
			base.item.useTime = 13;
			base.item.maxStack = 30;
			base.item.consumable = true;
			base.item.width = 26;
			base.item.height = 26;
			base.item.value = Item.sellPrice(0, 0, 2, 50);
			base.item.rare = 2;
			base.item.buffType = ModContent.BuffType<SpiceGreenBuff>();
			base.item.buffTime = 3600;
		}
	}
}
