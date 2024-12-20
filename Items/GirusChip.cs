using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class GirusChip : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Girus Chip");
			base.Tooltip.SetDefault("'What is this?'");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(10, 10));
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 26;
			base.item.maxStack = 99;
			base.item.value = 10000;
			base.item.rare = 10;
		}
	}
}
