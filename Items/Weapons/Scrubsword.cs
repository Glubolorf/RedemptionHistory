using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class Scrubsword : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Scrubsword");
			base.Tooltip.SetDefault("Shoots a swarm of dirt balls\n'Oh my...'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 15;
			base.item.melee = true;
			base.item.width = 52;
			base.item.height = 52;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.useStyle = 1;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 0, 6, 9);
			base.item.rare = 10;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("DirtPro");
			base.item.shootSpeed = 17f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DirtGreatsword", 1);
			modRecipe.AddIngredient(2, 500);
			modRecipe.AddIngredient(176, 500);
			modRecipe.AddIngredient(3467, 15);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int num = 60 + Main.rand.Next(20);
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
