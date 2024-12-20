using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class PlutoniumTongs : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Plutonium Tongs");
			base.Tooltip.SetDefault("'Because you can't just hold plutonium with your hands.'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 125;
			base.item.melee = true;
			base.item.width = 70;
			base.item.height = 70;
			base.item.useTime = 18;
			base.item.useAnimation = 18;
			base.item.useStyle = 1;
			base.item.knockBack = 4f;
			base.item.value = Item.sellPrice(0, 6, 55, 0);
			base.item.UseSound = SoundID.Item15;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.rare = 9;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Plutonium", 8);
			modRecipe.AddRecipeGroup("Redemption:Plating", 1);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
