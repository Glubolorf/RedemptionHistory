using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class AncientDirt : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Dirt");
			base.Tooltip.SetDefault("Can grow Ancient Trees");
			ItemID.Sets.ExtractinatorMode[base.item.type] = base.item.type;
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
			base.item.consumable = true;
			base.item.createTile = base.mod.TileType("AncientDirtTile");
		}

		public override void ExtractinatorUse(ref int resultType, ref int resultStack)
		{
			if (Main.rand.Next(5) == 0)
			{
				resultType = base.mod.ItemType("AncientWood");
				resultStack = 14;
				return;
			}
			resultType = 172;
			resultStack = 1;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(2, 1);
			modRecipe.AddIngredient(172, 5);
			modRecipe.AddTile(220);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
