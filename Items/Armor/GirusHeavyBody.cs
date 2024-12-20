using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class GirusHeavyBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Girus Heavy Breastplate");
			base.Tooltip.SetDefault("16% increased melee and ranged damage\n6% increased melee crit");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 20;
			base.item.value = Item.sellPrice(0, 65, 0, 0);
			base.item.rare = 10;
			base.item.defense = 34;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage *= 1.16f;
			player.rangedDamage *= 1.16f;
			player.meleeCrit += 6;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CorruptedXenomiteBody", 1);
			modRecipe.AddIngredient(null, "GirusChip", 1);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
