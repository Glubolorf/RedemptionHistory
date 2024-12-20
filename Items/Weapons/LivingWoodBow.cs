using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class LivingWoodBow : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Living Wood Bow");
		}

		public override void SetDefaults()
		{
			base.item.damage = 5;
			base.item.noMelee = true;
			base.item.ranged = true;
			base.item.width = 16;
			base.item.height = 32;
			base.item.useTime = 22;
			base.item.useAnimation = 22;
			base.item.useStyle = 5;
			base.item.shoot = 1;
			base.item.useAmmo = AmmoID.Arrow;
			base.item.knockBack = 0f;
			base.item.value = Item.sellPrice(0, 0, 0, 0);
			base.item.rare = 0;
			base.item.UseSound = SoundID.Item5;
			base.item.autoReuse = false;
			base.item.shootSpeed = 6.2f;
			base.item.crit = 0;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-2f, 0f));
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LivingTwig", 16);
			modRecipe.AddTile(304);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
