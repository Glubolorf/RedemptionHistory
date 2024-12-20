using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PreHM
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class IthonicGreaves : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sky Squire's Greaves");
			base.Tooltip.SetDefault("Increases movement speed by 10%");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 18;
			base.item.value = Item.sellPrice(0, 0, 59, 0);
			base.item.rare = 1;
			base.item.defense = 5;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.1f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "KaniteBar", 25);
			modRecipe.AddIngredient(225, 12);
			modRecipe.AddIngredient(751, 14);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
