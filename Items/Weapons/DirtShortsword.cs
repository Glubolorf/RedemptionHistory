using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class DirtShortsword : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Dirt Shortsword");
			base.Tooltip.SetDefault("Shoots balls of dirt\n'Why would anyone use this?'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 4;
			base.item.melee = true;
			base.item.width = 30;
			base.item.height = 30;
			base.item.useTime = 14;
			base.item.useAnimation = 14;
			base.item.useStyle = 3;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 0, 0, 50);
			base.item.rare = 0;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = false;
			base.item.shoot = base.mod.ProjectileType("DirtPro");
			base.item.shootSpeed = 8f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(3507, 1);
			modRecipe.AddIngredient(2, 10);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(20f));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true;
		}
	}
}
