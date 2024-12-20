using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class KaniteHammer : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Kanite Hammer");
		}

		public override void SetDefaults()
		{
			base.item.damage = 6;
			base.item.melee = true;
			base.item.width = 32;
			base.item.height = 32;
			base.item.useTime = 19;
			base.item.useAnimation = 28;
			base.item.hammer = 40;
			base.item.useStyle = 1;
			base.item.knockBack = 5.5f;
			base.item.value = 1550;
			base.item.rare = 0;
			base.item.UseSound = SoundID.Item1;
			base.item.useTurn = true;
			base.item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.anyWood = true;
			modRecipe.AddIngredient(null, "KaniteBar", 10);
			modRecipe.AddIngredient(9, 3);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
