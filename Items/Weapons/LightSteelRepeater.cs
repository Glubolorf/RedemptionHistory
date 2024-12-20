using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class LightSteelRepeater : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hikarite Repeater");
			base.Tooltip.SetDefault("'Uses arrows like a minigun...'\nA lot of ammo is needed!");
		}

		public override void SetDefaults()
		{
			base.item.damage = 36;
			base.item.noMelee = true;
			base.item.ranged = true;
			base.item.width = 56;
			base.item.height = 36;
			base.item.useTime = 5;
			base.item.useAnimation = 5;
			base.item.useStyle = 5;
			base.item.shoot = 1;
			base.item.useAmmo = AmmoID.Arrow;
			base.item.knockBack = 0f;
			base.item.value = Item.buyPrice(0, 40, 0, 0);
			base.item.rare = 8;
			base.item.UseSound = SoundID.Item5;
			base.item.autoReuse = true;
			base.item.shootSpeed = 60f;
			base.item.crit = 0;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-4f, 0f));
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LightSteel", 28);
			modRecipe.AddIngredient(182, 4);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
