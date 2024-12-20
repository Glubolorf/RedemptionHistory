using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Melee
{
	public class KaniteSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Kanite Broadsword");
		}

		public override void SetDefaults()
		{
			base.item.damage = 9;
			base.item.melee = true;
			base.item.width = 30;
			base.item.height = 30;
			base.item.useTime = 19;
			base.item.useAnimation = 19;
			base.item.useStyle = 1;
			base.item.knockBack = 5f;
			base.item.value = 1750;
			base.item.rare = 0;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "KaniteBar", 8);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
