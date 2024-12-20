using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class CharasKnife : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Chara's Knife");
			base.Tooltip.SetDefault("True Knife\n'What? An Undertale reference?'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 300;
			base.item.melee = true;
			base.item.width = 42;
			base.item.height = 46;
			base.item.useTime = 10;
			base.item.useAnimation = 10;
			base.item.useStyle = 1;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 66, 6, 6);
			base.item.rare = 10;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(3106, 1);
			modRecipe.AddIngredient(3467, 15);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
