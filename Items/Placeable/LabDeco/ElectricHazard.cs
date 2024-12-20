using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.LabDeco
{
	public class ElectricHazard : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Electric Hazard");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(4, 3));
		}

		public override void SetDefaults()
		{
			base.item.width = 32;
			base.item.height = 16;
			base.item.maxStack = 999;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.noUseGraphic = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.value = Item.buyPrice(0, 0, 15, 0);
			base.item.consumable = true;
			base.item.rare = 6;
			base.item.createTile = base.mod.TileType("ElectricHazardTile");
		}
	}
}
