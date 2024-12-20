using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Furniture.Misc
{
	public class KSStatue : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Statue of the Slayer");
			base.Tooltip.SetDefault("[c/ffea9b:'Ashes to ashes; Dust to dust]\n[c/ffea9b:Honor to glory; And iron to rust]\n[c/ffea9b:Hate to bloodshed; From rise to fall]\n[c/ffea9b:If I never have to die,]\n[c/ffea9b:Am I alive at all?']\n[c/ff0000:Unbreakable (500% Pickaxe Power)]");
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
			base.item.createTile = base.mod.TileType("KSStatueTile");
			base.item.GetGlobalItem<RedeItem>().redeRarity = 7;
		}
	}
}
