using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class IcarsElements : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Icar's Elements");
			base.Tooltip.SetDefault("'Blesses you with Icar's divine protection...'\nGreatly increases damage, defence, life regen and mana regen");
		}

		public override void SetDefaults()
		{
			base.item.width = 32;
			base.item.height = 38;
			base.item.value = 100000;
			base.item.rare = 8;
			base.item.accessory = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "IcarsFire", 1);
			modRecipe.AddIngredient(null, "IcarsFrost", 1);
			modRecipe.AddTile(114);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.AddBuff(base.mod.BuffType("ElementBlessed"), Main.rand.Next(5, 10), true);
		}
	}
}
