using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.HM
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class GirusHeavyLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Girus Leggings");
			base.Tooltip.SetDefault("Hold DOWN to increase fall speed");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 18;
			base.item.value = Item.sellPrice(0, 40, 0, 0);
			base.item.rare = 10;
			base.item.defense = 20;
		}

		public override void UpdateEquip(Player player)
		{
			if (player.controlDown)
			{
				player.maxFallSpeed += 10f;
			}
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CorruptedXenomiteLeggings", 1);
			modRecipe.AddIngredient(null, "VlitchScale", 15);
			modRecipe.AddIngredient(null, "GirusChip", 1);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
