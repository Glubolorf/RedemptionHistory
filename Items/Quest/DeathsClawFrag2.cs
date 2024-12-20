﻿using System;
using Terraria.ModLoader;

namespace Redemption.Items.Quest
{
	public class DeathsClawFrag2 : ModItem
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/Quest/DeathsClawFrag2";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Rare Fragment");
			base.Tooltip.SetDefault("'A demon face... ?'\nLooks like a certain Undead can repair this, if you have both pieces");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 26;
			base.item.maxStack = 1;
			base.item.value = 50000;
			base.item.rare = 1;
		}
	}
}