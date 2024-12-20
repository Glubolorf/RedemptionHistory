using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class DirtGreatsword : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Dirt Greatsword");
			base.Tooltip.SetDefault("Shoots too many balls of dirt\n'I think you should stop now...'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 12;
			base.item.melee = true;
			base.item.width = 44;
			base.item.height = 44;
			base.item.useTime = 11;
			base.item.useAnimation = 11;
			base.item.useStyle = 1;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 0, 0, 53);
			base.item.rare = 8;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("DirtPro");
			base.item.shootSpeed = 15f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DirtLongsword", 1);
			modRecipe.AddIngredient(2, 250);
			modRecipe.AddIngredient(176, 250);
			modRecipe.AddIngredient(3458, 2);
			modRecipe.AddIngredient(3457, 2);
			modRecipe.AddIngredient(3459, 2);
			modRecipe.AddIngredient(3456, 2);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int num = 22 + Main.rand.Next(6);
			for (int i = 0; i < num; i++)
			{
				Vector2 vector = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(30f));
				float num2 = 1f - Utils.NextFloat(Main.rand) * 0.3f;
				vector *= num2;
				Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
			}
			return false;
		}
	}
}
