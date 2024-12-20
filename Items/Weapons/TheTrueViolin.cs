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
			base.item.damage = 80;
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
			float num = 3f;
			float num2 = MathHelper.ToRadians(35f);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			int num3 = 0;
			while ((float)num3 < num)
			{
				Vector2 vector = Utils.RotatedBy(new Vector2(speedX, speedY), (double)MathHelper.Lerp(-num2, num2, (float)num3 / (num - 1f)), default(Vector2)) * 0.4f;
				Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				num3++;
			}
			return false;
		}
	}
}
