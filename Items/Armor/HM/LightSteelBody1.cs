using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.HM
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class LightSteelBody1 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hikarite Breastplate");
			base.Tooltip.SetDefault("4% increased melee and ranged damage\n4% increased melee crit");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 24;
			base.item.value = Item.sellPrice(0, 50, 0, 0);
			base.item.rare = 8;
			base.item.defense = 32;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage *= 1.04f;
			player.rangedDamage *= 1.04f;
			player.meleeCrit += 4;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LightSteel", 30);
			modRecipe.AddIngredient(182, 5);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
