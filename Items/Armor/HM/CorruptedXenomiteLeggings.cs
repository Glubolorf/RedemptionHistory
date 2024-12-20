using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.HM
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class CorruptedXenomiteLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corrupted Xenomite Leggings");
			base.Tooltip.SetDefault("+1 max minions\n50% increased movement speed");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 18;
			base.item.value = Item.sellPrice(0, 35, 0, 0);
			base.item.rare = 10;
			base.item.defense = 14;
		}

		public override void UpdateEquip(Player player)
		{
			player.maxMinions++;
			player.moveSpeed += 0.5f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CorruptedXenomite", 15);
			modRecipe.AddIngredient(null, "CorruptedStarliteBar", 5);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "XenomiteLeggings", 1);
			modRecipe2.AddIngredient(null, "GirusChip", 1);
			modRecipe2.AddIngredient(1508, 10);
			modRecipe2.AddTile(null, "CorruptorTile");
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
