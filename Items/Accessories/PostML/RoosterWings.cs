﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.PostML
{
	[AutoloadEquip(new EquipType[]
	{
		9
	})]
	public class RoosterWings : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Rooster Wings");
			base.Tooltip.SetDefault("Allows flight and slow fall");
		}

		public override void SetDefaults()
		{
			base.item.width = 28;
			base.item.height = 30;
			base.item.value = Item.sellPrice(0, 20, 0, 0);
			base.item.accessory = true;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.wingTimeMax = 200;
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 0.95f;
			ascentWhenRising = 0.15f;
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 4f;
			constantAscend = 0.17f;
		}

		public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
		{
			speed = 12f;
			acceleration *= 3f;
		}
	}
}
