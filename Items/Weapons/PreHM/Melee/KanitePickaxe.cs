using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Melee
{
	public class KanitePickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Kanite Pickaxe");
		}

		public override void SetDefaults()
		{
			base.item.damage = 4;
			base.item.melee = true;
			base.item.width = 34;
			base.item.height = 34;
			base.item.useTime = 18;
			base.item.useAnimation = 18;
			base.item.pick = 50;
			base.item.useStyle = 1;
			base.item.knockBack = 2f;
			base.item.value = 1950;
			base.item.rare = 0;
			base.item.UseSound = SoundID.Item1;
			base.item.useTurn = true;
			base.item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.anyWood = true;
			modRecipe.AddIngredient(null, "KaniteBar", 12);
			modRecipe.AddIngredient(9, 3);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
