using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.HM
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class LightSteelLeggings1 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hikarite Leggings");
			base.Tooltip.SetDefault("12% increased melee speed\n15% increased movement speed");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 18;
			base.item.value = Item.sellPrice(0, 25, 0, 0);
			base.item.rare = 8;
			base.item.defense = 22;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.15f;
			player.meleeSpeed *= 1.12f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LightSteel", 25);
			modRecipe.AddIngredient(182, 5);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
