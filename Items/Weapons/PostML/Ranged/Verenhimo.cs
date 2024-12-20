using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Ranged
{
	public class Verenhimo : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Verenhimo");
			base.Tooltip.SetDefault("'A beautiful, elegant crossbow that lusts for blood'\nShoots Bloodthrist Arrows\nDrains 10 health to shoot a lifestealing arrow that returns 20 health on a successful hit");
		}

		public override void SetDefaults()
		{
			base.item.damage = 1600;
			base.item.ranged = true;
			base.item.width = 66;
			base.item.height = 22;
			base.item.useTime = 25;
			base.item.useAnimation = 25;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 20, 0, 0);
			base.item.UseSound = SoundID.Item89;
			base.item.autoReuse = true;
			base.item.shoot = 10;
			base.item.shootSpeed = 50f;
			base.item.useAmmo = AmmoID.Arrow;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-6f, 0f));
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			for (int index = 0; index < 10; index++)
			{
				Dust dust = Dust.NewDustDirect(new Vector2(player.position.X, player.position.Y), player.width, player.height, 235, 0f, 0f, 100, default(Color), 2f);
				dust.velocity = -player.DirectionTo(dust.position);
				dust.noGravity = true;
			}
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<BloodthirstArrow>(), damage, knockBack, player.whoAmI, 0f, 0f);
			return false;
		}
	}
}
