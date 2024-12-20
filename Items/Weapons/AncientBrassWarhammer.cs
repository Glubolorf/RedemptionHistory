using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class AncientBrassWarhammer : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Brass Warhammer");
			base.Tooltip.SetDefault("'A warhammer completely made of old brass'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 21;
			base.item.melee = true;
			base.item.width = 34;
			base.item.height = 32;
			base.item.useTime = 22;
			base.item.useAnimation = 38;
			base.item.hammer = 50;
			base.item.useStyle = 1;
			base.item.knockBack = 7f;
			base.item.value = 1050;
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientBrassIngot", 16);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
