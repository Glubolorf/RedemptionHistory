using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.HM
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class LightSteelLeggings2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shining Hikarite Leggings");
			base.Tooltip.SetDefault("+1 max minions\n15% increased movement speed");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 18;
			base.item.value = Item.sellPrice(0, 25, 0, 0);
			base.item.rare = 8;
			base.item.defense = 20;
		}

		public override void UpdateEquip(Player player)
		{
			player.maxMinions++;
			player.moveSpeed += 0.15f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LightSteel", 20);
			modRecipe.AddIngredient(182, 10);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
