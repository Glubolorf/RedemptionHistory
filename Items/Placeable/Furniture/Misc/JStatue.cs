using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Furniture.Misc
{
	public class JStatue : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Statue of ???");
			base.Tooltip.SetDefault("[c/ffea9b:'I've made mistakes that devastated,]\n[c/ffea9b:Too many battles lost to tell,]\n[c/ffea9b:If I could turn back to time to find you,]\n[c/ffea9b:I'd find our confidence as well']\n[c/ff0000:Unbreakable (500% Pickaxe Power)]");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 44;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.rare = 8;
			base.item.consumable = true;
			base.item.value = Item.sellPrice(5, 0, 0, 0);
			base.item.createTile = base.mod.TileType("JStatueTile");
			base.item.GetGlobalItem<RedeItem>().redeRarity = 7;
		}
	}
}
