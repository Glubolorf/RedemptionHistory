using System;
using Redemption.Items.Materials.PreHM;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Donator.Arche
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	internal class ArchePatreonVanityBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Iridescent Outfit");
			base.Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 22;
			base.item.rare = 1;
			base.item.value = Item.buyPrice(0, 0, 5, 0);
			base.item.vanity = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 9;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(ModContent.ItemType<Archcloth>(), 7);
			modRecipe.AddIngredient(ModContent.ItemType<MoonflareFragment>(), 4);
			modRecipe.AddTile(86);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
