﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class MysteriousArtifact : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mysterious Artifact");
			base.Tooltip.SetDefault("'Seems to have some weird carvings in it, looks like an alien alphabet...'\n[c/aa00ff:Epic]");
		}

		public override void SetDefaults()
		{
			base.item.CloneDefaults(561);
			base.item.shootSpeed *= 1.25f;
			base.item.useTime = 6;
			base.item.value = Item.sellPrice(0, 20, 0, 0);
			base.item.useAnimation = 6;
			base.item.damage = (int)((double)base.item.damage * 2.0);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			type = base.mod.ProjectileType("MysteriousArtifactPro");
			return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			Color transparent = Color.Transparent;
			if (base.item.modItem != null && base.item.modItem.mod == ModLoader.GetMod("Redemption"))
			{
				TooltipLine tooltipLine = Enumerable.First<TooltipLine>(tooltips, (TooltipLine v) => v.Name.Equals("ItemName"));
				tooltipLine.overrideColor = new Color?(new Color(170, 0, 255));
			}
		}
	}
}