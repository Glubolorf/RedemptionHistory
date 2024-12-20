using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	[AutoloadEquip(new EquipType[]
	{
		11
	})]
	public class PrismiteHNecklace : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Prismite Heart Necklace");
			base.Tooltip.SetDefault("+600 max health\nDecreases defence by 999");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 34;
			base.item.value = Item.buyPrice(0, 4, 0, 0);
			base.item.rare = 5;
			base.item.accessory = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(29, 1);
			modRecipe.AddIngredient(85, 6);
			modRecipe.AddIngredient(2310, 1);
			modRecipe.AddTile(114);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statDefense -= 999;
			player.statLifeMax2 += 600;
		}
	}
}
