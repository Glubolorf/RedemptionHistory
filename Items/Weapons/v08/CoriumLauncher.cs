using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class CoriumLauncher : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corium Launcher");
			base.Tooltip.SetDefault("Shoots radiating corium that inflicts radiation poisoning");
		}

		public override void SetDefaults()
		{
			base.item.damage = 950;
			base.item.ranged = true;
			base.item.width = 112;
			base.item.height = 34;
			base.item.useTime = 50;
			base.item.useAnimation = 50;
			base.item.useStyle = 5;
			base.item.knockBack = 9f;
			base.item.UseSound = SoundID.Item11;
			base.item.value = Item.buyPrice(5, 0, 0, 0);
			base.item.shoot = base.mod.ProjectileType("CoriumBallPro");
			base.item.shootSpeed = 22f;
			base.item.autoReuse = true;
			base.item.noMelee = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 4;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Corium", 35);
			modRecipe.AddRecipeGroup("Redemption:Plating", 8);
			modRecipe.AddRecipeGroup("Redemption:Capacitators", 1);
			modRecipe.AddTile(null, "XenoTank1");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 6 + Main.rand.Next(3);
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(8f));
				float scale = 1f - Utils.NextFloat(Main.rand) * 0.3f;
				perturbedSpeed *= scale;
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
			}
			return false;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-40f, 0f));
		}
	}
}
