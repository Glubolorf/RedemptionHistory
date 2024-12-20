using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class Zweihander : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Zweihander");
			base.Tooltip.SetDefault("'Nice and shiny...'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 120;
			base.item.melee = true;
			base.item.width = 74;
			base.item.height = 74;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.useStyle = 1;
			base.item.knockBack = 7f;
			base.item.value = Item.buyPrice(0, 5, 0, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item7;
			base.item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "RustedZweihander", 1);
			modRecipe.AddIngredient(null, "MagicMetalPolish", 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
