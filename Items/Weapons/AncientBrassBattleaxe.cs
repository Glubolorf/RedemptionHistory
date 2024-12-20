using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class AncientBrassBattleaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Brass Battleaxe");
			base.Tooltip.SetDefault("'A battleaxe completely made of old brass'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 16;
			base.item.melee = true;
			base.item.width = 42;
			base.item.height = 34;
			base.item.useTime = 15;
			base.item.axe = 20;
			base.item.useAnimation = 30;
			base.item.useStyle = 1;
			base.item.knockBack = 5f;
			base.item.value = 1100;
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientBrassIngot", 12);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
