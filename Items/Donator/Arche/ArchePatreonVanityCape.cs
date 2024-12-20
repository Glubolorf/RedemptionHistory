using System;
using Redemption.Items.Materials.PreHM;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Donator.Arche
{
	[AutoloadEquip(new EquipType[]
	{
		5
	})]
	public class ArchePatreonVanityCape : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Iridescent Cape");
			base.Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 22;
			base.item.value = Item.sellPrice(0, 0, 5, 0);
			base.item.rare = 1;
			base.item.accessory = true;
			base.item.vanity = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 9;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(ModContent.ItemType<Archcloth>(), 5);
			modRecipe.AddIngredient(ModContent.ItemType<MoonflareFragment>(), 3);
			modRecipe.AddTile(86);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
