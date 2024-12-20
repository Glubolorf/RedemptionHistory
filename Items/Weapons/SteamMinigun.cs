using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class SteamMinigun : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Steam-Cog Minigun");
			base.Tooltip.SetDefault("Replaces normal bullets with High Velocity Bullets\n33% chance not to consume ammo");
		}

		public override void SetDefaults()
		{
			base.item.damage = 34;
			base.item.ranged = true;
			base.item.width = 94;
			base.item.height = 40;
			base.item.useTime = 4;
			base.item.useAnimation = 4;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 0f;
			base.item.value = Item.buyPrice(1, 50, 0, 0);
			base.item.rare = 8;
			base.item.UseSound = SoundID.Item40;
			base.item.autoReuse = true;
			base.item.shoot = 10;
			base.item.shootSpeed = 90f;
			base.item.useAmmo = AmmoID.Bullet;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			if (type == 14)
			{
				type = 242;
			}
			Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(5f));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-12f, 0f));
		}
	}
}
