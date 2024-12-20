using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles;
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
			base.item.damage = 25;
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
			base.item.shoot = ModContent.ProjectileType<IceTentacle>();
			base.item.shootSpeed = 30f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
			float num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
			float num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
			Vector2 value2 = new Vector2(num78, num79);
			value2.Normalize();
			Vector2 value3 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
			value3.Normalize();
			value2 = value2 * 6f + value3;
			value2.Normalize();
			value2 *= base.item.shootSpeed;
			float num80 = (float)Main.rand.Next(10, 50) * 0.001f;
			if (Main.rand.Next(2) == 0)
			{
				num80 *= -1f;
			}
			float num81 = (float)Main.rand.Next(10, 50) * 0.001f;
			if (Main.rand.Next(2) == 0)
			{
				num81 *= -1f;
			}
			Projectile.NewProjectile(vector2.X, vector2.Y, value2.X, value2.Y, type, damage, knockBack, player.whoAmI, num81, num80);
			return false;
		}
	}
}
