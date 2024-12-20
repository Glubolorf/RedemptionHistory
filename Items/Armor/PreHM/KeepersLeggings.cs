using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PreHM
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class KeepersLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Leggings of the Fallen");
			base.Tooltip.SetDefault("Increases movement speed by 10%");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 18;
			base.item.value = Item.sellPrice(0, 0, 75, 0);
			base.item.rare = 3;
			base.item.defense = 4;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 1.1f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DarkShard", 1);
			modRecipe.AddIngredient(null, "SmallLostSoul", 4);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
