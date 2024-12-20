using System;
using Redemption.Items.Materials.PreHM;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Donator.Arche
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class ArchePatreonVanityHead : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Iridescent Hat");
			base.Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			base.item.width = 28;
			base.item.height = 16;
			base.item.rare = 1;
			base.item.value = Item.buyPrice(0, 0, 5, 0);
			base.item.vanity = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 9;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
			drawHair = false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(ModContent.ItemType<Archcloth>(), 5);
			modRecipe.AddIngredient(ModContent.ItemType<MoonflareFragment>(), 2);
			modRecipe.AddTile(86);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
