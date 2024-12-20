using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PreHM
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class NightshadeLegs : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nightshade Sabatons");
			base.Tooltip.SetDefault("Increases movement speed by 10%\nIncreased jump height");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 18;
			base.item.value = Item.sellPrice(0, 0, 12, 0);
			base.item.rare = 1;
			base.item.defense = 5;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.1f;
			player.jumpBoost = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(79, 1);
			modRecipe.AddIngredient(null, "Nightshade", 6);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(698, 1);
			modRecipe2.AddIngredient(null, "Nightshade", 6);
			modRecipe2.AddTile(16);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
