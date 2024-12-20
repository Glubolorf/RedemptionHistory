using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Ranged
{
	public class AncientWoodBow : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Wood Bow");
		}

		public override void SetDefaults()
		{
			base.item.damage = 19;
			base.item.noMelee = true;
			base.item.ranged = true;
			base.item.width = 18;
			base.item.height = 48;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.useStyle = 5;
			base.item.shoot = 1;
			base.item.useAmmo = AmmoID.Arrow;
			base.item.knockBack = 0f;
			base.item.value = Item.sellPrice(0, 0, 0, 50);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item5;
			base.item.autoReuse = false;
			base.item.shootSpeed = 5.5f;
			base.item.crit = 0;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-2f, 0f));
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientWood", 10);
			modRecipe.AddTile(null, "AncientWoodWorkbenchTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
