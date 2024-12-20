using System;
using Redemption.Items.Materials.PreHM;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Donator.Arche
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class ArchePatreonVanityLegs : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Iridescent Leggings");
			base.Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			base.item.width = 18;
			base.item.height = 18;
			base.item.rare = 1;
			base.item.value = Item.buyPrice(0, 0, 5, 0);
			base.item.vanity = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 9;
		}

		public override void SetMatch(bool male, ref int equipSlot, ref bool robes)
		{
			if (!male)
			{
				equipSlot = base.mod.GetEquipSlot("ArchePatreonVanityLegs_FemaleLegs", 2);
			}
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(ModContent.ItemType<Archcloth>(), 6);
			modRecipe.AddIngredient(ModContent.ItemType<MoonflareFragment>(), 2);
			modRecipe.AddTile(86);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
