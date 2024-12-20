using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.HM
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class CorruptedXenomiteBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corrupted Xenomite Breastplate");
			base.Tooltip.SetDefault("12% increased magic and minion damage\n6% increased magic crit");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 24;
			base.item.value = Item.sellPrice(0, 60, 0, 0);
			base.item.rare = 10;
			base.item.defense = 18;
		}

		public override void UpdateEquip(Player player)
		{
			player.magicDamage *= 1.12f;
			player.minionDamage *= 1.12f;
			player.magicCrit += 6;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CorruptedXenomite", 20);
			modRecipe.AddIngredient(null, "CorruptedStarliteBar", 10);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "XenomiteBody", 1);
			modRecipe2.AddIngredient(null, "GirusChip", 2);
			modRecipe2.AddIngredient(1508, 15);
			modRecipe2.AddTile(null, "CorruptorTile");
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
