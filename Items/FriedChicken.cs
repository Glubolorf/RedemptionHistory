using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class FriedChicken : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Fried Chicken");
			base.Tooltip.SetDefault("'I'm lovin' it'\nMinor improvements to all stats");
		}

		public override void SetDefaults()
		{
			base.item.UseSound = SoundID.Item2;
			base.item.useStyle = 2;
			base.item.useTurn = true;
			base.item.useAnimation = 14;
			base.item.useTime = 14;
			base.item.maxStack = 999;
			base.item.consumable = true;
			base.item.width = 12;
			base.item.height = 38;
			base.item.value = 200;
			base.item.rare = 1;
			base.item.buffType = 26;
			base.item.buffTime = 20000;
		}
	}
}
