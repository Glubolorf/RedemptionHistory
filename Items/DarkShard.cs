﻿using System;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class DarkShard : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dark Shard");
			base.Tooltip.SetDefault("'Gives the Undead power'");
		}

		public override void SetDefaults()
		{
			base.item.width = 12;
			base.item.height = 24;
			base.item.maxStack = 99;
			base.item.value = 0;
			base.item.rare = 4;
		}
	}
}
