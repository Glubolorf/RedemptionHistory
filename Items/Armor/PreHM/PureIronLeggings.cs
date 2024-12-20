using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PreHM
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class PureIronLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Pure-Iron Leggings");
			base.Tooltip.SetDefault("Increases melee damage by 6%\nIncreases movement speed by 25%");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 18;
			base.item.value = Item.sellPrice(0, 7, 50, 0);
			base.item.rare = 4;
			base.item.defense = 8;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage *= 1.06f;
			player.moveSpeed += 0.25f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "PureIron", 15);
			modRecipe.AddTile(null, "GathicCryoFurnaceTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
