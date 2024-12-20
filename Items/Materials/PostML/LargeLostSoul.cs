﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.PostML
{
	public class LargeLostSoul : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Large Lost Soul");
			base.Tooltip.SetDefault("'The soul of a strong creature'");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(5, 4));
			ItemID.Sets.AnimatesAsSoul[base.item.type] = true;
			ItemID.Sets.ItemIconPulse[base.item.type] = true;
			ItemID.Sets.ItemNoGravity[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			Item refItem = new Item();
			refItem.SetDefaults(549, false);
			base.item.width = refItem.width;
			base.item.height = refItem.height;
			base.item.maxStack = 999;
			base.item.value = 50;
			base.item.rare = 1;
		}

		public override void PostUpdate()
		{
			Lighting.AddLight(base.item.Center, Color.WhiteSmoke.ToVector3() * 0.55f * Main.essScale);
		}
	}
}