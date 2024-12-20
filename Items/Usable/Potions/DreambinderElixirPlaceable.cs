using System;
using Redemption.Tiles.Furniture.Shade;
using Terraria.ModLoader;

namespace Redemption.Items.Usable.Potions
{
	public class DreambinderElixirPlaceable : ModItem
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/Usable/Potions/DreambinderElixir";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dreambinder Elixir (Placeable)");
		}

		public override void SetDefaults()
		{
			base.item.width = 28;
			base.item.height = 42;
			base.item.maxStack = 1;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.rare = 11;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<DreambinderElixirTile>();
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}
	}
}
