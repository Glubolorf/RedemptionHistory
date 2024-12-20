using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PreHM
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class PureIronArmour : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Pure-Iron Breastplate");
			base.Tooltip.SetDefault("Increases melee damage by 6%\nIncreases melee speed by 5%");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 20;
			base.item.value = Item.sellPrice(0, 8, 0, 0);
			base.item.rare = 4;
			base.item.defense = 10;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage *= 1.06f;
			player.meleeSpeed *= 1.05f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "PureIron", 20);
			modRecipe.AddTile(null, "GathicCryoFurnaceTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
