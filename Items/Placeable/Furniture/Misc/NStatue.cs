using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Furniture.Misc
{
	public class NStatue : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Statue of the Protector");
			base.Tooltip.SetDefault("[c/ffea9b:We're burdened with purpose,]\n[c/ffea9b:So here we remain,]\n[c/ffea9b:We're incandescent,]\n[c/ffea9b:A glorious flame...]\n[c/ff0000:Unbreakable (500% Pickaxe Power)]");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 36;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.rare = 8;
			base.item.consumable = true;
			base.item.value = Item.sellPrice(5, 0, 0, 0);
			base.item.createTile = base.mod.TileType("NStatueTile");
			base.item.GetGlobalItem<RedeItem>().redeRarity = 7;
		}
	}
}
