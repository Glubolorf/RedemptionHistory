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
		0
	})]
	public class RainPatreonVanityHead : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Horns of a Living Weapon");
			base.Tooltip.SetDefault("'Strange, you can't seem to smile'");
		}

		public override void SetDefaults()
		{
			base.item.width = 28;
			base.item.height = 40;
			base.item.rare = 1;
			base.item.value = Item.buyPrice(0, 0, 5, 0);
			base.item.vanity = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 9;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = false;
			drawHair = false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(ModContent.ItemType<ArtificalMuscle>(), 2);
			modRecipe.AddRecipeGroup("Redemption:BioweaponBile", 4);
			modRecipe.AddIngredient(ModContent.ItemType<XeniumBar>(), 4);
			modRecipe.AddTile(ModContent.TileType<XenoTank1>());
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
