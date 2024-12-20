using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Vanity
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class TBotEyes_Masc : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("T-Bot Head");
			base.Tooltip.SetDefault("Eyes\nMasculine");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 22;
			base.item.rare = 1;
			base.item.value = Item.buyPrice(0, 3, 0, 0);
			base.item.vanity = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "TBotVanityEyes", 1);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "TBotEyes_Femi", 1);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}

		public override bool DrawHead()
		{
			return false;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}
	}
}
