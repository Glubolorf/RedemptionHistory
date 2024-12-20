using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class CreationPickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Creation Pickaxe");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]");
		}

		public override void SetDefaults()
		{
			base.item.damage = 80;
			base.item.width = 38;
			base.item.height = 38;
			base.item.crit = 4;
			base.item.useTime = 6;
			base.item.useAnimation = 11;
			base.item.pick = 255;
			base.item.useStyle = 1;
			base.item.knockBack = 5.5f;
			base.item.value = Item.sellPrice(0, 5, 0, 0);
			base.item.rare = 10;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.tileBoost = 4;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(3467, 10);
			modRecipe.AddIngredient(null, "CreationFragment", 12);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
