using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.HM
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class XenomiteBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenomite Breastplate");
			base.Tooltip.SetDefault("+15 max mana\n10% increased magic damage");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 20;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.rare = 7;
			base.item.defense = 12;
		}

		public override void UpdateEquip(Player player)
		{
			player.statManaMax2 += 15;
			player.magicDamage *= 1.1f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Xenomite", 20);
			modRecipe.AddIngredient(null, "StarliteBar", 10);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
