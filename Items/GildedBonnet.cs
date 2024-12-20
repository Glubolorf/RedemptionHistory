using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class GildedBonnet : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Gilded Bonnet");
			base.Tooltip.SetDefault("Summons Nebby to give you moral support!");
		}

		public override void SetDefaults()
		{
			base.item.CloneDefaults(669);
			base.item.rare = 0;
			base.item.shoot = base.mod.ProjectileType("NebPet");
			base.item.buffType = base.mod.BuffType("NebPetBuff");
		}

		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.overrideColor = new Color?(RedeColor.NebColour);
				}
			}
		}

		public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(base.item.buffType, 3600, true);
			}
		}
	}
}
