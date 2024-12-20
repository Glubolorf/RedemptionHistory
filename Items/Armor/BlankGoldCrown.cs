using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class BlankGoldCrown : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bland Gold Crown");
			base.Tooltip.SetDefault("'For when you can't be bothered to find a single ruby'\nMaterial cost of Slime Crown and Egg Crown are doubled");
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 12;
			base.item.rare = 0;
			base.item.vanity = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(19, 7);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override bool DrawHead()
		{
			return true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
		}
	}
}
