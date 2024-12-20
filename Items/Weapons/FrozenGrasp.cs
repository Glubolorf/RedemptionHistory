using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class FrozenGrasp : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Frozen Grasp");
			base.Tooltip.SetDefault("Casts an icy grasp that inflicts Frostburn");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(10, 3));
		}

		public override void SetDefaults()
		{
			base.item.damage = 42;
			base.item.magic = true;
			base.item.mana = 5;
			base.item.width = 32;
			base.item.height = 30;
			base.item.useTime = 2;
			base.item.reuseDelay = 10;
			base.item.useAnimation = 10;
			base.item.useStyle = 5;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.knockBack = 5.5f;
			base.item.value = 25000;
			base.item.rare = 5;
			base.item.UseSound = SoundID.Item103;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("IceTentacle");
			base.item.shootSpeed = 30f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			float num = (float)Main.mouseX + Main.screenPosition.X - vector.X;
			float num2 = (float)Main.mouseY + Main.screenPosition.Y - vector.Y;
			Vector2 vector2;
			vector2..ctor(num, num2);
			vector2.Normalize();
			Vector2 vector3;
			vector3..ctor((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
			vector3.Normalize();
			vector2 = vector2 * 6f + vector3;
			vector2.Normalize();
			vector2 *= base.item.shootSpeed;
			float num3 = (float)Main.rand.Next(10, 50) * 0.001f;
			if (Main.rand.Next(2) == 0)
			{
				num3 *= -1f;
			}
			float num4 = (float)Main.rand.Next(10, 50) * 0.001f;
			if (Main.rand.Next(2) == 0)
			{
				num4 *= -1f;
			}
			Projectile.NewProjectile(vector.X, vector.Y, vector2.X, vector2.Y, type, damage, knockBack, player.whoAmI, num4, num3);
			return false;
		}
	}
}
