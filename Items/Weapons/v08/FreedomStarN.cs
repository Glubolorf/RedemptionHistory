using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class FreedomStarN : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Freedom Star");
			base.Tooltip.SetDefault("'Wait, I don't remember adding this...'\nHold the use button to charge, and then release a powerful Charged Shot!\nKept you waiting, huh?");
		}

		public override void SetDefaults()
		{
			base.item.width = 74;
			base.item.height = 34;
			base.item.ranged = true;
			base.item.damage = 720;
			base.item.shoot = base.mod.ProjectileType("FreedomStarN");
			base.item.useTime = 10;
			base.item.useAnimation = 10;
			base.item.useStyle = 5;
			base.item.channel = true;
			Item.sellPrice(1, 0, 0, 0);
			base.item.noMelee = true;
			base.item.shootSpeed = 12f;
			base.item.noUseGraphic = true;
		}

		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine line2 in list)
			{
				if (line2.mod == "Terraria" && line2.Name == "ItemName")
				{
					line2.overrideColor = new Color?(RedeColor.NebColour);
				}
			}
		}
	}
}
