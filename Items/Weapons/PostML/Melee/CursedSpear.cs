using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Melee
{
	public class CursedSpear : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Warmonger");
			base.Tooltip.SetDefault("Strikes foes in an arc, then stabs in the direction of the cursor\nShoots out Cursed Orbs\nThe orbs inflict Cursed Flames, if one kills an enemy, it will multiply into 2 Cursed Orbs\nThe orbs created by the multiply won't multiply themselves");
		}

		public override void SetDefaults()
		{
			base.item.width = 68;
			base.item.height = 72;
			base.item.maxStack = 1;
			base.item.value = Item.buyPrice(1, 0, 0, 0);
			base.item.useStyle = 5;
			base.item.useAnimation = 30;
			base.item.useTime = 30;
			base.item.UseSound = SoundID.Item1;
			base.item.damage = 460;
			base.item.knockBack = 6f;
			base.item.melee = true;
			base.item.autoReuse = true;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.shoot = ModContent.ProjectileType<CursedSpearPro1>();
			base.item.shootSpeed = 4f;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[base.item.shoot] < 1;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 2 + Main.rand.Next(2);
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(5f));
				float scale = 1f - Utils.NextFloat(Main.rand) * 0.4f;
				perturbedSpeed *= scale;
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X * 4f, perturbedSpeed.Y * 4f, ModContent.ProjectileType<CursedOrbPro1>(), damage, knockBack, player.whoAmI, 0f, 0f);
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "ShinkiteCursed", 8);
			modRecipe.AddIngredient(172, 30);
			modRecipe.AddIngredient(550, 1);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
