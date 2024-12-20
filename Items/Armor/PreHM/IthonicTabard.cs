using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PreHM
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class IthonicTabard : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sky Squire's Tabard");
			base.Tooltip.SetDefault("4% damage reduction");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 20;
			base.item.value = Item.sellPrice(0, 0, 74, 0);
			base.item.rare = 1;
			base.item.defense = 6;
		}

		public override void UpdateEquip(Player player)
		{
			player.endurance += 0.04f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "KaniteBar", 30);
			modRecipe.AddIngredient(225, 15);
			modRecipe.AddIngredient(751, 18);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
