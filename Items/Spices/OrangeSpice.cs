using System;
using Redemption.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Spices
{
	public class OrangeSpice : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Orange Spices");
			base.Tooltip.SetDefault("'Tastes a bit spicy, could be white pepper'\nYour weapon will ignite enemies");
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
			base.item.value = Item.sellPrice(0, 0, 7, 50);
			base.item.rare = 3;
			base.item.buffType = ModContent.BuffType<SpiceOrangeBuff>();
			base.item.buffTime = 3600;
		}
	}
}
