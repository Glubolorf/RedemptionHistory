using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class HandheldBastilla : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hand-held Ballista");
			base.Tooltip.SetDefault("Fires fast and powerful Ballista Bolts");
		}

		public override void SetDefaults()
		{
			base.item.damage = 2200;
			base.item.ranged = true;
			base.item.width = 60;
			base.item.height = 40;
			base.item.useTime = 60;
			base.item.useAnimation = 60;
			base.item.useStyle = 5;
			base.item.knockBack = 14f;
			base.item.UseSound = SoundID.Item89;
			base.item.value = Item.buyPrice(0, 65, 0, 0);
			base.item.shoot = 10;
			base.item.shootSpeed = 400f;
			base.item.useAmmo = AmmoID.Arrow;
			base.item.crit = 50;
			base.item.autoReuse = true;
			base.item.noMelee = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<BastillaBoltPro>(), damage, knockBack, player.whoAmI, 0f, 0f);
			return false;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-5f, 0f));
		}
	}
}
