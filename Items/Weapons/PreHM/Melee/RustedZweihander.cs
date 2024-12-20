using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Melee
{
	public class RustedZweihander : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Rusty Zweihander");
			base.Tooltip.SetDefault("'Needs a bit of a polish...'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 20;
			base.item.melee = true;
			base.item.width = 74;
			base.item.height = 74;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.useStyle = 1;
			base.item.knockBack = 6f;
			base.item.value = Item.buyPrice(0, 0, 1, 0);
			base.item.rare = 0;
			base.item.UseSound = SoundID.Item7;
			base.item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "BrokenRustedSword1", 1);
			modRecipe.AddIngredient(null, "BrokenRustedSword2", 1);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
