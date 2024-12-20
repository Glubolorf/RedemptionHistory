using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Magic
{
	public class SpookStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Pumpkin Wand");
			base.Tooltip.SetDefault("'Spooky'\nCasts explosive Jack O' Lanterns");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 48;
			base.item.magic = true;
			base.item.mana = 8;
			base.item.width = 50;
			base.item.height = 50;
			base.item.useTime = 28;
			base.item.useAnimation = 28;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 4, 85, 0);
			base.item.rare = 5;
			base.item.UseSound = SoundID.Item20;
			base.item.autoReuse = true;
			base.item.shoot = 312;
			base.item.shootSpeed = 16f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(1225, 5);
			modRecipe.AddIngredient(1725, 15);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
