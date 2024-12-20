﻿using System;
using Terraria.ModLoader;

namespace Redemption.Items.Quest
{
	public class PeacekeeperFrag2 : ModItem
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/Quest/PeacekeeperFrag2";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Rare Fragment");
			base.Tooltip.SetDefault("'A trident... ?'\nLooks like a certain Undead can repair this, if you have both pieces");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 30;
			base.item.maxStack = 1;
			base.item.value = 5000;
			base.item.rare = 1;
		}
	}
}
