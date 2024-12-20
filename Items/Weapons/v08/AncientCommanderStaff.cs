using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class AncientCommanderStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Commander Staff");
			base.Tooltip.SetDefault("Summons a stationary portal that shoots Ancient Arrowheads, Daggers, or Stalactite");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 450;
			base.item.summon = true;
			base.item.sentry = true;
			base.item.mana = 30;
			base.item.width = 42;
			base.item.height = 44;
			base.item.useTime = 15;
			base.item.useAnimation = 15;
			base.item.useStyle = 5;
			base.item.knockBack = 5f;
			base.item.value = Item.sellPrice(0, 45, 0, 0);
			base.item.UseSound = SoundID.Item20;
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.useTurn = true;
			base.item.shoot = ModContent.ProjectileType<AncientPortal>();
			base.item.shootSpeed = 0f;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			position = Main.MouseWorld;
			if (player.ownedProjectileCounts[type] < player.maxTurrets)
			{
				Projectile.NewProjectile(position.X, position.Y, 0f, 0f, type, damage, knockBack, player.whoAmI, 0f, 0f);
			}
			if (player.ownedProjectileCounts[type] == player.maxTurrets)
			{
				for (int g = 0; g < 1000; g++)
				{
					if (Main.projectile[g].active && Main.projectile[g].type == type)
					{
						Main.projectile[g].Kill();
						break;
					}
				}
				Projectile.NewProjectile(position.X, position.Y, 0f, 0f, type, damage, knockBack, player.whoAmI, 0f, 0f);
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientPowerCore", 17);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
