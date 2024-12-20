using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Melee
{
	public class KeepersKnife : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Keeper's Knife");
			base.Tooltip.SetDefault("Shoots Dark Souls");
		}

		public override void SetDefaults()
		{
			base.item.damage = 12;
			base.item.melee = true;
			base.item.width = 58;
			base.item.height = 58;
			base.item.useTime = 15;
			base.item.useAnimation = 15;
			base.item.useStyle = 3;
			base.item.knockBack = 4f;
			base.item.value = Item.sellPrice(0, 2, 0, 0);
			base.item.rare = 3;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<DarkSoulPro1>();
			base.item.shootSpeed = 16f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DarkShard", 1);
			modRecipe.AddIngredient(null, "SmallLostSoul", 4);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(10f));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true;
		}
	}
}
