using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class GirusHeavyBody2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Girus Tactical Breastplate");
			base.Tooltip.SetDefault("16% increased ranged damage\n6% increased ranged crit");
		}

		public override void SetDefaults()
		{
			base.item.width = 42;
			base.item.height = 26;
			base.item.value = Item.sellPrice(0, 65, 0, 0);
			base.item.rare = 10;
			base.item.defense = 26;
		}

		public override void UpdateEquip(Player player)
		{
			player.rangedDamage *= 1.16f;
			player.rangedCrit += 6;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CorruptedXenomiteBody", 1);
			modRecipe.AddIngredient(null, "VlitchScale", 20);
			modRecipe.AddIngredient(null, "GirusChip", 1);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
