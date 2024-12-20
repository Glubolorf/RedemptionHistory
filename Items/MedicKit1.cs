﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class MedicKit1 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Medic Kit");
			base.Tooltip.SetDefault("Permanently increases maximum life by 50\nCan only be used if the max amount of life fruit has been consumed");
		}

		public override void SetDefaults()
		{
			base.item.width = 32;
			base.item.height = 30;
			base.item.useAnimation = 30;
			base.item.useTime = 30;
			base.item.useStyle = 4;
			base.item.UseSound = SoundID.Item4;
			base.item.consumable = true;
		}

		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.overrideColor = new Color?(new Color(0, 255, 200));
				}
			}
		}

		public override bool CanUseItem(Player player)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>(base.mod);
			return !modPlayer.medKit && player.statLifeMax >= 500;
		}

		public override bool UseItem(Player player)
		{
			if (player.itemAnimation > 0 && player.itemTime == 0)
			{
				player.itemTime = base.item.useTime;
				if (Main.myPlayer == player.whoAmI)
				{
					player.HealEffect(50, true);
				}
				RedePlayer modPlayer = player.GetModPlayer<RedePlayer>(base.mod);
				modPlayer.medKit = true;
			}
			return true;
		}
	}
}