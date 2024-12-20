using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.HM
{
	public class AIChip : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("AI Chip");
			base.Tooltip.SetDefault("'Filled with code...'");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(10, 10));
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 26;
			base.item.maxStack = 99;
			base.item.value = 250000;
			base.item.rare = 5;
		}
	}
}
