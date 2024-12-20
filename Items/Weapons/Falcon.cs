﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class Falcon : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Falcon");
			base.Tooltip.SetDefault("'A warhammer that can create earthquakes... Very tiny earthquakes...'\n[c/1c4dff:Rare]");
		}

		public override void SetDefaults()
		{
			base.item.damage = 34;
			base.item.melee = true;
			base.item.width = 48;
			base.item.height = 48;
			base.item.useTime = 32;
			base.item.useAnimation = 32;
			base.item.hammer = 70;
			base.item.useStyle = 1;
			base.item.knockBack = 8f;
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.rare = 9;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			Color transparent = Color.Transparent;
			if (base.item.modItem != null && base.item.modItem.mod == ModLoader.GetMod("Redemption"))
			{
				TooltipLine tooltipLine = Enumerable.First<TooltipLine>(tooltips, (TooltipLine v) => v.Name.Equals("ItemName"));
				tooltipLine.overrideColor = new Color?(new Color(0, 120, 255));
			}
		}
	}
}