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
		9
	})]
	public class RainPatreonWings : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Wings of a Living Weapon");
			base.Tooltip.SetDefault("'Best hold back, you wouldn't want to hurt yourself'");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 32;
			base.item.value = Item.sellPrice(0, 0, 5, 0);
			base.item.rare = 1;
			base.item.accessory = true;
			base.item.vanity = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 9;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(ModContent.ItemType<ArtificalMuscle>(), 4);
			modRecipe.AddRecipeGroup("Redemption:BioweaponBile", 6);
			modRecipe.AddIngredient(ModContent.ItemType<XeniumBar>(), 12);
			modRecipe.AddTile(ModContent.TileType<XenoTank1>());
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
