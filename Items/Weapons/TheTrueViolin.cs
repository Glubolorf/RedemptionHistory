using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class TheTrueViolin : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The True Violin");
			base.Tooltip.SetDefault("'Music will destroy all...'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 70;
			base.item.melee = true;
			base.item.width = 66;
			base.item.height = 66;
			base.item.useTime = 14;
			base.item.useAnimation = 14;
			base.item.useStyle = 1;
			base.item.knockBack = 6f;
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.rare = 7;
			base.item.UseSound = base.mod.GetLegacySoundSlot(2, "Sounds/Item/TheViolinSound");
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("TheTrueViolin");
			base.item.shootSpeed = 10f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "TheViolin", 1);
			modRecipe.AddIngredient(null, "ViolinString", 1);
			modRecipe.AddIngredient(1570, 1);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float numberProjectiles = 3f;
			float rotation = MathHelper.ToRadians(35f);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			int i = 0;
			while ((float)i < numberProjectiles)
			{
				Vector2 perturbedSpeed = Utils.RotatedBy(new Vector2(speedX, speedY), (double)MathHelper.Lerp(-rotation, rotation, (float)i / (numberProjectiles - 1f)), default(Vector2)) * 0.4f;
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				i++;
			}
			return false;
		}
	}
}
