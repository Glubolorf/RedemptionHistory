using System;
using Terraria.ModLoader;

namespace Redemption.Items.Quest
{
	public class ShellNecklaceQuest : ModItem
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/Quest/ShellNecklace";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shell Necklace");
			base.Tooltip.SetDefault("'Pretty.'");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 26;
			base.item.questItem = true;
			base.item.maxStack = 1;
			base.item.rare = -11;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "TreeBugShell", 2);
			modRecipe.AddIngredient(null, "CoastScarabShell", 2);
			modRecipe.AddIngredient(85, 4);
			modRecipe.AddIngredient(21, 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "TreeBugShell", 2);
			modRecipe2.AddIngredient(null, "CoastScarabShell", 2);
			modRecipe2.AddIngredient(85, 4);
			modRecipe2.AddIngredient(705, 1);
			modRecipe2.AddTile(18);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
