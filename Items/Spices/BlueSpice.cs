using System;
using Redemption.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Spices
{
	public class BlueSpice : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Blue Spices");
			base.Tooltip.SetDefault("'Tastes very flowery, perhaps moonglow petals'\nYou emit light");
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
			base.item.value = Item.sellPrice(0, 0, 5, 0);
			base.item.rare = 1;
			base.item.buffType = ModContent.BuffType<SpiceBlueBuff>();
			base.item.buffTime = 3600;
		}
	}
}
