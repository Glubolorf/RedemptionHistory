using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Melee
{
	public class LivingWoodSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Living Wood Sword");
		}

		public override void SetDefaults()
		{
			base.item.damage = 7;
			base.item.melee = true;
			base.item.width = 32;
			base.item.height = 32;
			base.item.useTime = 18;
			base.item.useAnimation = 18;
			base.item.useStyle = 1;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 0, 0, 0);
			base.item.rare = 0;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LivingTwig", 11);
			modRecipe.AddTile(304);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
