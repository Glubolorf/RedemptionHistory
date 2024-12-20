using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class CursedRootBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Root Blade");
			base.Tooltip.SetDefault("Shoots a spread of stingers and cursed thorns");
		}

		public override void SetDefaults()
		{
			base.item.damage = 700;
			base.item.melee = true;
			base.item.width = 58;
			base.item.height = 60;
			base.item.useTime = 18;
			base.item.useAnimation = 18;
			base.item.useStyle = 1;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 50, 0, 0);
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shootSpeed = 16f;
			base.item.shoot = 55;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().thornCrown)
			{
				flat += 50f;
			}
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CursedThorns", 17);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 4 + Main.rand.Next(2);
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(30f));
				float scale = 1f - Utils.NextFloat(Main.rand) * 0.4f;
				perturbedSpeed *= scale;
				int proj = Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				Main.projectile[proj].hostile = false;
				Main.projectile[proj].friendly = true;
				Main.projectile[proj].timeLeft = 60;
			}
			int numberProjectiles2 = 1 + Main.rand.Next(1);
			for (int j = 0; j < numberProjectiles2; j++)
			{
				Vector2 perturbedSpeed2 = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(30f));
				float scale2 = 1f - Utils.NextFloat(Main.rand) * 0.4f;
				perturbedSpeed2 *= scale2;
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed2.X, perturbedSpeed2.Y, ModContent.ProjectileType<CursedThornPro5>(), damage, knockBack, player.whoAmI, 0f, 0f);
			}
			return false;
		}
	}
}
