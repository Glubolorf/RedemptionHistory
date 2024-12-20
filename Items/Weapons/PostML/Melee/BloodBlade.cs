using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Melee
{
	public class BloodBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Crimson Witherbrand");
			base.Tooltip.SetDefault("Shoots a spread of Blood Orbs\nThe orbs drain life, but if one kills an enemy, it will multiply into 2 Blood Orbs\nThe orbs created by the multiply won't multiply themselves");
		}

		public override void SetDefaults()
		{
			base.item.damage = 540;
			base.item.melee = true;
			base.item.width = 60;
			base.item.height = 60;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.useStyle = 1;
			base.item.knockBack = 6f;
			base.item.value = Item.buyPrice(1, 0, 0, 0);
			base.item.UseSound = SoundID.Item71;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shootSpeed = 20f;
			base.item.shoot = ModContent.ProjectileType<BloodOrbPro1>();
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 2 + Main.rand.Next(2);
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(20f));
				float scale = 1f - Utils.NextFloat(Main.rand) * 0.4f;
				perturbedSpeed *= scale;
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "ShinkiteBlood", 12);
			modRecipe.AddIngredient(172, 20);
			modRecipe.AddIngredient(757, 1);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
