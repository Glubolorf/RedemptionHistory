using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.HM
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class GirusHeavyBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Girus Breastplate");
			base.Tooltip.SetDefault("16% increased damage\n6% increased critical strike chance");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 26;
			base.item.value = Item.sellPrice(0, 65, 0, 0);
			base.item.rare = 10;
			base.item.defense = 30;
		}

		public override void UpdateEquip(Player player)
		{
			player.allDamage *= 1.16f;
			player.magicCrit += 6;
			player.meleeCrit += 6;
			player.rangedCrit += 6;
			player.thrownCrit += 6;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CorruptedXenomiteBody", 1);
			modRecipe.AddIngredient(null, "VlitchScale", 20);
			modRecipe.AddIngredient(null, "GirusChip", 1);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
