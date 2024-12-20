using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PreHM
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class KaniteLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Kanite Greaves");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 18;
			base.item.value = Item.sellPrice(0, 0, 5, 50);
			base.item.rare = 0;
			base.item.defense = 2;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "KaniteBar", 25);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
