using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
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
			base.Tooltip.SetDefault("18% increased magic and summon damage\n6% increased magic crit");
		}

		public override void SetDefaults()
		{
			base.item.width = 36;
			base.item.height = 22;
			base.item.value = Item.sellPrice(0, 60, 0, 0);
			base.item.rare = 10;
			base.item.defense = 18;
		}

		public override void UpdateEquip(Player player)
		{
			player.magicDamage *= 1.18f;
			player.minionDamage *= 1.18f;
			player.magicCrit += 6;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CorruptedXenomite", 20);
			modRecipe.AddIngredient(null, "CorruptedStarliteBar", 10);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
