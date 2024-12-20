using System;
using Terraria.ModLoader;

namespace Redemption.Walls
{
	public class LeadFenceBlackWallItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Lead Fence (Black Background)");
		}

		public override void SetDefaults()
		{
			base.item.width = 16;
			base.item.height = 16;
			base.item.maxStack = 999;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 7;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.createWall = ModContent.WallType<LeadFenceBlackWall>();
		}
	}
}
