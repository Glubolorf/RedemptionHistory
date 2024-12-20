using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class GirusHeavyBody4 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Girus Overseer Plate");
			base.Tooltip.SetDefault("16% increased minion damage\nIncreases your max number of minions by 2");
		}

		public override void SetDefaults()
		{
			base.item.width = 44;
			base.item.height = 30;
			base.item.value = Item.sellPrice(0, 65, 0, 0);
			base.item.rare = 10;
			base.item.defense = 18;
		}

		public override void UpdateEquip(Player player)
		{
			player.minionDamage *= 1.16f;
			player.maxMinions += 2;
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
