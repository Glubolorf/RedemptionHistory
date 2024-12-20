using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class MoltenScrap : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Molten Scrap");
			base.Tooltip.SetDefault("'Careful! It's hot'");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(5, 4));
		}

		public override void SetDefaults()
		{
			base.item.width = 44;
			base.item.height = 28;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 50, 0);
			base.item.rare = 11;
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(67, 60, true);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AIChip", 1);
			modRecipe.AddTile(null, "LabForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "CyberPlating", 1);
			modRecipe2.AddTile(null, "LabForgeTile");
			modRecipe2.SetResult(this, 2);
			modRecipe2.AddRecipe();
			ModRecipe modRecipe3 = new ModRecipe(base.mod);
			modRecipe3.AddIngredient(null, "Mk1Capacitator", 1);
			modRecipe3.AddTile(null, "LabForgeTile");
			modRecipe3.SetResult(this, 1);
			modRecipe3.AddRecipe();
			ModRecipe modRecipe4 = new ModRecipe(base.mod);
			modRecipe4.AddIngredient(null, "Mk1Plating", 1);
			modRecipe4.AddTile(null, "LabForgeTile");
			modRecipe4.SetResult(this, 1);
			modRecipe4.AddRecipe();
			ModRecipe modRecipe5 = new ModRecipe(base.mod);
			modRecipe5.AddIngredient(null, "Mk2Capacitator", 1);
			modRecipe5.AddTile(null, "LabForgeTile");
			modRecipe5.SetResult(this, 1);
			modRecipe5.AddRecipe();
			ModRecipe modRecipe6 = new ModRecipe(base.mod);
			modRecipe6.AddIngredient(null, "Mk2Plating", 1);
			modRecipe6.AddTile(null, "LabForgeTile");
			modRecipe6.SetResult(this, 1);
			modRecipe6.AddRecipe();
			ModRecipe modRecipe7 = new ModRecipe(base.mod);
			modRecipe7.AddIngredient(null, "Mk3Capacitator", 1);
			modRecipe7.AddTile(null, "LabForgeTile");
			modRecipe7.SetResult(this, 1);
			modRecipe7.AddRecipe();
			ModRecipe modRecipe8 = new ModRecipe(base.mod);
			modRecipe8.AddIngredient(null, "Mk3Plating", 1);
			modRecipe8.AddTile(null, "LabForgeTile");
			modRecipe8.SetResult(this, 1);
			modRecipe8.AddRecipe();
			ModRecipe modRecipe9 = new ModRecipe(base.mod);
			modRecipe9.AddIngredient(null, "ScrapMetal", 1);
			modRecipe9.AddTile(null, "LabForgeTile");
			modRecipe9.SetResult(this, 6);
			modRecipe9.AddRecipe();
		}
	}
}
