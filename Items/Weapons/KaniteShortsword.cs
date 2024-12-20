using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class KaniteShortsword : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Kanite Shortsword");
		}

		public override void SetDefaults()
		{
			base.item.damage = 7;
			base.item.melee = true;
			base.item.width = 30;
			base.item.height = 30;
			base.item.useTime = 10;
			base.item.useAnimation = 10;
			base.item.useStyle = 3;
			base.item.knockBack = 4f;
			base.item.value = 1350;
			base.item.rare = 0;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "KaniteBar", 7);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
