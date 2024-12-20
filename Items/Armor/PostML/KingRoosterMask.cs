﻿using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PostML
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class KingRoosterMask : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Rooster King Mask");
			base.Tooltip.SetDefault("'... UH...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 14;
			base.item.height = 20;
			base.item.vanity = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override bool DrawHead()
		{
			return false;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}
	}
}
