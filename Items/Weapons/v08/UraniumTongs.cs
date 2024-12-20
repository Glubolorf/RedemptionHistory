using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class UraniumTongs : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Uranium Tongs");
			base.Tooltip.SetDefault("'Because you can't just hold uranium with your hands.'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 80;
			base.item.melee = true;
			base.item.width = 60;
			base.item.height = 60;
			base.item.useTime = 21;
			base.item.useAnimation = 21;
			base.item.useStyle = 1;
			base.item.knockBack = 4f;
			base.item.value = Item.sellPrice(0, 3, 55, 0);
			base.item.UseSound = SoundID.Item15;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.rare = 7;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Uranium", 8);
			modRecipe.AddRecipeGroup("Redemption:Plating", 1);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
