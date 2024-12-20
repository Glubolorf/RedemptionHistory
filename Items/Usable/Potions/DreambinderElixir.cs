using System;
using Redemption.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Usable.Potions
{
	public class DreambinderElixir : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dreambinder Elixir");
			base.Tooltip.SetDefault("'Remembering the warmth of a much brighter day.'\nIncreases length of invincibility after taking damage\nNot consumable");
		}

		public override void SetDefaults()
		{
			base.item.width = 28;
			base.item.height = 42;
			base.item.useTurn = true;
			base.item.maxStack = 1;
			base.item.healLife = 250;
			base.item.useAnimation = 17;
			base.item.useTime = 17;
			base.item.useStyle = 2;
			base.item.UseSound = SoundID.Item3;
			base.item.consumable = false;
			base.item.potion = true;
			base.item.buffType = ModContent.BuffType<DreambinderBuff>();
			base.item.buffTime = 1200;
			base.item.value = Item.sellPrice(0, 15, 50, 0);
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override bool ConsumeItem(Player player)
		{
			return false;
		}
	}
}
