using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class BlackenedHeart : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Blackened Heart");
			base.Tooltip.SetDefault("'May cause instant death'");
			ItemID.Sets.ItemIconPulse[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 18;
			base.item.maxStack = 99;
			base.item.value = 3000;
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item3;
			base.item.useStyle = 2;
			base.item.useTurn = true;
			base.item.useAnimation = 17;
			base.item.useTime = 17;
			base.item.consumable = true;
		}

		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.overrideColor = new Color?(RedeColor.SoullessColour);
				}
			}
		}

		public override bool UseItem(Player player)
		{
			int num = Main.rand.Next(2);
			if (num == 0)
			{
				base.item.buffType = base.mod.BuffType("BlackenedHeartBuff");
				base.item.buffTime = 18000;
			}
			if (num == 1)
			{
				base.item.buffType = base.mod.BuffType("BlackenedHeartDebuff");
				base.item.buffTime = 18000;
			}
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			return !player.HasBuff(base.mod.BuffType("BlackenedHeartBuff")) && !player.HasBuff(base.mod.BuffType("BlackenedHeartDebuff"));
		}
	}
}
