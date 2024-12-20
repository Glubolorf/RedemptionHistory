using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class CreationHamaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Creation Hamaxe");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]");
		}

		public override void SetDefaults()
		{
			base.item.damage = 60;
			base.item.width = 54;
			base.item.height = 52;
			base.item.crit = 4;
			base.item.useTime = 7;
			base.item.useAnimation = 27;
			base.item.axe = 37;
			base.item.hammer = 100;
			base.item.useStyle = 1;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 5, 0, 0);
			base.item.rare = 10;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CreationFragment", 14);
			modRecipe.AddIngredient(3467, 12);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
