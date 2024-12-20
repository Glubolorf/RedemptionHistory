using System;
using Redemption.Items.Materials.HM;
using Redemption.Items.Materials.PostML;
using Redemption.Tiles.Furniture.Lab;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Donator.Rain
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	internal class RainPatreonVanityBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Form of a Living Weapon");
			base.Tooltip.SetDefault("'A body etched by agony and filled with strength'");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 24;
			base.item.rare = 1;
			base.item.value = Item.buyPrice(0, 0, 5, 0);
			base.item.vanity = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 9;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(ModContent.ItemType<ArtificalMuscle>(), 4);
			modRecipe.AddRecipeGroup("Redemption:BioweaponBile", 6);
			modRecipe.AddIngredient(ModContent.ItemType<XeniumBar>(), 8);
			modRecipe.AddTile(ModContent.TileType<XenoTank1>());
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
