using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class AncientWoodBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Wood Breastplate");
			base.Tooltip.SetDefault("Increases your max number of minions\nIncreases minion damage by 6%");
		}

		public override void SetDefaults()
		{
			base.item.width = 32;
			base.item.height = 34;
			base.item.value = 100;
			base.item.rare = 1;
			base.item.defense = 5;
		}

		public override void UpdateEquip(Player player)
		{
			player.maxMinions++;
			player.minionDamage *= 1.06f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientWood", 30);
			modRecipe.AddTile(null, "AncientWoodWorkbenchTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
