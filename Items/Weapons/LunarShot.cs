using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class LunarShot : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Lunar Shot");
			base.Tooltip.SetDefault("Replaces Wooden Arrows with Lunar Bolts");
		}

		public override void SetDefaults()
		{
			base.item.damage = 7;
			base.item.ranged = true;
			base.item.width = 22;
			base.item.height = 44;
			base.item.useTime = 19;
			base.item.useAnimation = 19;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 0f;
			base.item.value = Item.sellPrice(0, 0, 2, 0);
			base.item.rare = 0;
			base.item.UseSound = SoundID.Item5;
			base.item.autoReuse = true;
			base.item.shoot = 10;
			base.item.shootSpeed = 45f;
			base.item.useAmmo = AmmoID.Arrow;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-2f, 0f));
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == 1)
			{
				type = base.mod.ProjectileType("LunarShotPro");
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "MoonflareFragment", 12);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
