using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class CreationFragment : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Creation Fragment");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n'The blessing of life resides within this fragment'");
			ItemID.Sets.ItemNoGravity[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 24;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 20, 0);
			base.item.rare = 9;
		}

		public override void PostUpdate()
		{
			Lighting.AddLight(base.item.Center, Color.LightGreen.ToVector3() * 0.55f * Main.essScale);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(3457, 1);
			modRecipe.AddIngredient(3458, 1);
			modRecipe.AddIngredient(3459, 1);
			modRecipe.AddIngredient(3456, 1);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 2);
			modRecipe.AddRecipe();
		}
	}
}
