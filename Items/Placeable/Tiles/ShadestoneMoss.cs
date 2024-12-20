using System;
using Redemption.Tiles.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Tiles
{
	public class ShadestoneMoss : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Black Moss");
			base.Tooltip.SetDefault("Plants moss on Shadestone and Shadestone Bricks");
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 18;
			base.item.maxStack = 999;
			base.item.rare = 11;
			base.item.useStyle = 1;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.createTile = ModContent.TileType<ShadestoneMossyTile>();
			base.item.consumable = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override bool CanUseItem(Player p)
		{
			Tile tile = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY);
			if (tile != null && tile.active() && (int)tile.type == ModContent.TileType<ShadestoneTile>())
			{
				base.item.createTile = ModContent.TileType<ShadestoneMossyTile>();
				WorldGen.destroyObject = true;
				TileID.Sets.BreakableWhenPlacing[ModContent.TileType<ShadestoneTile>()] = true;
				return base.CanUseItem(p);
			}
			if (tile != null && tile.active() && (int)tile.type == ModContent.TileType<ShadestoneBrickTile>())
			{
				base.item.createTile = ModContent.TileType<ShadestoneBrickMossyTile>();
				WorldGen.destroyObject = true;
				TileID.Sets.BreakableWhenPlacing[ModContent.TileType<ShadestoneBrickTile>()] = true;
				return base.CanUseItem(p);
			}
			return false;
		}

		public override bool UseItem(Player p)
		{
			WorldGen.destroyObject = false;
			TileID.Sets.BreakableWhenPlacing[ModContent.TileType<ShadestoneTile>()] = false;
			TileID.Sets.BreakableWhenPlacing[ModContent.TileType<ShadestoneBrickTile>()] = false;
			return base.UseItem(p);
		}
	}
}
