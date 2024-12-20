using System;
using Redemption.Tiles.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Tiles
{
	public class DangerTape : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Danger Tape");
			base.Tooltip.SetDefault("[c/ff0000:Unbreakable (500% Pickaxe Power after Patient Zero)]");
		}

		public override void SetDefaults()
		{
			base.item.width = 16;
			base.item.height = 16;
			base.item.maxStack = 999;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.value = Item.buyPrice(0, 0, 1, 25);
			base.item.consumable = true;
			base.item.rare = 6;
			base.item.createTile = ModContent.TileType<DangerTapeTile>();
		}
	}
}
