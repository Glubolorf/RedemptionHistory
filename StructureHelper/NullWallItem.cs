using System;
using Terraria.ModLoader;

namespace Redemption.StructureHelper
{
	internal class NullWallItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Null Wall");
			base.Tooltip.SetDefault("Use these in a structure to indicate where the generator\n should leave walls that already exists in the world untouched\n for walls only, use null blocks for other things");
		}

		public override void SetDefaults()
		{
			base.item.width = 16;
			base.item.height = 16;
			base.item.maxStack = 1;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 2;
			base.item.useTime = 2;
			base.item.useStyle = 1;
			base.item.rare = 8;
			base.item.createWall = ModContent.WallType<NullWall>();
		}
	}
}
