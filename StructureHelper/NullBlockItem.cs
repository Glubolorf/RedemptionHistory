using System;
using Terraria.ModLoader;

namespace Redemption.StructureHelper
{
	internal class NullBlockItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Null Block");
			base.Tooltip.SetDefault("Use these in a structure to indicate where the generator\n should leave whatever already exists in the world untouched\n ignores walls, use null walls for that :3");
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
			base.item.createTile = ModContent.TileType<NullBlock>();
		}
	}
}
