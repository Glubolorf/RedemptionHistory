using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Melee;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Melee
{
	public class ShadeCandleSpear : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Light of the Abyss");
			base.Tooltip.SetDefault("'The abyss hungers...'");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(5, 3));
		}

		public override void SetDefaults()
		{
			base.item.damage = 690;
			base.item.width = 66;
			base.item.height = 70;
			base.item.maxStack = 1;
			base.item.value = Item.sellPrice(0, 35, 0, 0);
			base.item.useStyle = 5;
			base.item.useAnimation = 25;
			base.item.useTime = 25;
			base.item.UseSound = SoundID.Item1;
			base.item.knockBack = 6f;
			base.item.melee = true;
			base.item.autoReuse = true;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.shoot = ModContent.ProjectileType<ShadeCandleSpearPro1>();
			base.item.shootSpeed = 4f;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[base.item.shoot] < 1;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<CandleLightPro>(), damage / 2, knockBack, player.whoAmI, 0f, 0f);
			Projectile.NewProjectile(position.X, position.Y, speedX * 1.5f, speedY * 1.5f, ModContent.ProjectileType<CandleLightPro>(), damage / 2, knockBack, player.whoAmI, 0f, 0f);
			Projectile.NewProjectile(position.X, position.Y, speedX * 2f, speedY * 2f, ModContent.ProjectileType<CandleLightPro>(), damage / 2, knockBack, player.whoAmI, 0f, 0f);
			Projectile.NewProjectile(position.X, position.Y, speedX * 2.5f, speedY * 2.5f, ModContent.ProjectileType<CandleLightPro>(), damage / 2, knockBack, player.whoAmI, 0f, 0f);
			Projectile.NewProjectile(position.X, position.Y, speedX * 3f, speedY * 3f, ModContent.ProjectileType<CandleLightPro>(), damage / 2, knockBack, player.whoAmI, 0f, 0f);
			Projectile.NewProjectile(position.X, position.Y, speedX * 3.5f, speedY * 3.5f, ModContent.ProjectileType<CandleLightPro>(), damage / 2, knockBack, player.whoAmI, 0f, 0f);
			return true;
		}
	}
}
