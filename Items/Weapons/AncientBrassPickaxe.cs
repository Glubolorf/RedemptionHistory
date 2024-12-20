using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class AncientBrassPickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Brass Pickaxe");
			base.Tooltip.SetDefault("'A pickaxe completely made of old brass'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 7;
			base.item.melee = true;
			base.item.width = 32;
			base.item.height = 32;
			base.item.useTime = 22;
			base.item.useAnimation = 22;
			base.item.pick = 50;
			base.item.useStyle = 1;
			base.item.knockBack = 2f;
			base.item.value = 1200;
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientBrassIngot", 14);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
