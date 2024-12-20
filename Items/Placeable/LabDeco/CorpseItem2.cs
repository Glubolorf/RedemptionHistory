using System;
using Redemption.Tiles.LabDeco;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.LabDeco
{
	public class CorpseItem2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corpse (Laying on Back)");
		}

		public override void SetDefaults()
		{
			base.item.width = 32;
			base.item.height = 28;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 0;
			base.item.rare = 6;
			base.item.createTile = ModContent.TileType<Corpse2>();
			base.item.placeStyle = 0;
		}
	}
}
