using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Costumes
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class TBotVanityEyes : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("T-Bot Head");
			base.Tooltip.SetDefault("Eyes");
		}

		public override void SetDefaults()
		{
			base.item.width = 18;
			base.item.height = 18;
			base.item.rare = 1;
			base.item.value = Item.buyPrice(0, 3, 0, 0);
			base.item.vanity = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "TBotVanityGoggles", 1);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "TBotEyes_Masc", 1);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
			ModRecipe modRecipe3 = new ModRecipe(base.mod);
			modRecipe3.AddIngredient(null, "TBotEyes_Femi", 1);
			modRecipe3.SetResult(this, 1);
			modRecipe3.AddRecipe();
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
