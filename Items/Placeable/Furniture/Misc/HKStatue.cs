using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Furniture.Misc
{
	public class HKStatue : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Statue of the Demigod... ?");
			base.Tooltip.SetDefault("[c/ffea9b:'Spent years waiting for the time,]\n[c/ffea9b:I'd come to see the truth behind,]\n[c/ffea9b:The darkness that we face in our own lives,]\n[c/ffea9b:Cast aside your fear,]\n[c/ffea9b:We're strong in numbers,]\n[c/ffea9b:Holding back a fate of endless slumber,]\n[c/ffea9b:I want to stay to keep you strong,]\n[c/ffea9b:Side by side is where we belong...']\n[c/ff0000:Unbreakable (500% Pickaxe Power)]");
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
			base.item.createTile = base.mod.TileType("HKStatueTile");
			base.item.GetGlobalItem<RedeItem>().redeRarity = 7;
		}
	}
}
