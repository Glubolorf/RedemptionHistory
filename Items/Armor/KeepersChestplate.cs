using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class KeepersChestplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Breastplate of the Fallen");
			base.Tooltip.SetDefault("Increases life regen\nIncreases max life by 50");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 20;
			base.item.value = Item.sellPrice(0, 1, 0, 0);
			base.item.rare = 3;
			base.item.defense = 5;
		}

		public override void UpdateEquip(Player player)
		{
			player.lifeRegen += 3;
			player.statLifeMax2 += 25;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DarkShard", 1);
			modRecipe.AddIngredient(null, "SmallLostSoul", 6);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
