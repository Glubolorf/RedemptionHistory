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
		5
	})]
	public class RainPatreonVanityAcc : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Tail of a Living Weapon");
			base.Tooltip.SetDefault("'You can feel every twitch, even the slightest breeze'");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 12;
			base.item.value = Item.sellPrice(0, 0, 5, 0);
			base.item.rare = 1;
			base.item.accessory = true;
			base.item.vanity = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 9;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(ModContent.ItemType<ArtificalMuscle>(), 1);
			modRecipe.AddRecipeGroup("Redemption:BioweaponBile", 2);
			modRecipe.AddIngredient(ModContent.ItemType<XeniumBar>(), 4);
			modRecipe.AddTile(ModContent.TileType<XenoTank1>());
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
