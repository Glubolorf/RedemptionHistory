﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class NoblesSwordIron : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Iron Noble's Sword");
		}

		public override void SetDefaults()
		{
			base.item.damage = 15;
			base.item.melee = true;
			base.item.width = 32;
			base.item.height = 30;
			base.item.useTime = 27;
			base.item.useAnimation = 27;
			base.item.useStyle = 1;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 0, 40, 0);
			base.item.rare = 0;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
		}
	}
}
