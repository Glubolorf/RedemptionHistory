﻿using System;
using Terraria.ModLoader;

namespace Redemption.Items.Quest
{
	public class ForestNymphsSickleFrag1 : ModItem
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/Quest/ForestNymphsSickleFrag1";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Rare Fragment");
			base.Tooltip.SetDefault("'A bulb... ?'\nLooks like a certain Undead can repair this, if you have both pieces");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 24;
			base.item.maxStack = 1;
			base.item.value = 5000;
			base.item.rare = 1;
		}
	}
}