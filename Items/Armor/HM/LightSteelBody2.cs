using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.HM
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class LightSteelBody2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shining Hikarite Breastplate");
			base.Tooltip.SetDefault("12% increased magic and minion damage\n4% increased magic crit");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 22;
			base.item.value = Item.sellPrice(0, 50, 0, 0);
			base.item.rare = 8;
			base.item.defense = 24;
		}

		public override void UpdateEquip(Player player)
		{
			player.magicDamage *= 1.12f;
			player.minionDamage *= 1.12f;
			player.magicCrit += 4;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LightSteel", 25);
			modRecipe.AddIngredient(182, 10);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
