using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PreHM
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class NightshadeBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nightshade Katabira");
			base.Tooltip.SetDefault("Increased life regen at night");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 20;
			base.item.value = Item.sellPrice(0, 0, 15, 0);
			base.item.rare = 1;
			base.item.defense = 6;
		}

		public override void UpdateEquip(Player player)
		{
			if (!Main.dayTime)
			{
				player.lifeRegen++;
			}
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(83, 1);
			modRecipe.AddIngredient(null, "Nightshade", 8);
			modRecipe.AddIngredient(null, "SmallLostSoul", 3);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(697, 1);
			modRecipe2.AddIngredient(null, "Nightshade", 8);
			modRecipe2.AddIngredient(null, "SmallLostSoul", 3);
			modRecipe2.AddTile(16);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
