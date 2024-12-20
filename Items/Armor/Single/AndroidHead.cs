using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Single
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class AndroidHead : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Android Head");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 22;
			base.item.value = 5000;
			base.item.rare = 5;
			base.item.defense = 10;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Mk1Plating", 3);
			modRecipe.AddIngredient(null, "ArtificalMuscle", 2);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}
	}
}
