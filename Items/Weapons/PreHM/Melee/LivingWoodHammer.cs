using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Melee
{
	public class LivingWoodHammer : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Living Wood Hammer");
		}

		public override void SetDefaults()
		{
			base.item.damage = 2;
			base.item.melee = true;
			base.item.width = 32;
			base.item.height = 32;
			base.item.useTime = 24;
			base.item.useAnimation = 29;
			base.item.hammer = 25;
			base.item.useStyle = 1;
			base.item.knockBack = 5.5f;
			base.item.value = 0;
			base.item.rare = 0;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LivingTwig", 12);
			modRecipe.AddTile(304);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
