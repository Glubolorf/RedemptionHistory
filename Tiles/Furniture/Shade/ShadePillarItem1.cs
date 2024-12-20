using System;
using Terraria.ModLoader;

namespace Redemption.Tiles.Furniture.Shade
{
	public class ShadePillarItem1 : ModItem
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Tiles/Furniture/Shade/ShadePillarItem";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shade Pillar");
			base.Tooltip.SetDefault("4x20\n[c/ff0000:Unbreakable (500% Pickaxe Power)]");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 48;
			base.item.maxStack = 999;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<ShadePillar1>();
		}
	}
}
