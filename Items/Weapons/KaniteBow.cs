using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class KaniteBow : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Kanite Bow");
		}

		public override void SetDefaults()
		{
			base.item.damage = 7;
			base.item.noMelee = true;
			base.item.ranged = true;
			base.item.width = 16;
			base.item.height = 36;
			base.item.useTime = 26;
			base.item.useAnimation = 26;
			base.item.useStyle = 5;
			base.item.shoot = 1;
			base.item.useAmmo = AmmoID.Arrow;
			base.item.knockBack = 0f;
			base.item.value = Item.sellPrice(0, 0, 13, 50);
			base.item.rare = 0;
			base.item.UseSound = SoundID.Item5;
			base.item.autoReuse = false;
			base.item.shootSpeed = 6.6f;
			base.item.crit = 0;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-2f, 0f));
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "KaniteBar", 7);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
