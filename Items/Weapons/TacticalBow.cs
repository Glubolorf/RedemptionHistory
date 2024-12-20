using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class TacticalBow : ModItem
	{
		public override void SetStaticDefaults()
		{
			if (Main.netMode != 2)
			{
				Texture2D[] array = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					array[i] = Main.glowMaskTexture[i];
				}
				array[array.Length - 1] = base.mod.GetTexture("Items/Weapons/" + base.GetType().Name + "_Glow");
				TacticalBow.customGlowMask = (short)(array.Length - 1);
				Main.glowMaskTexture = array;
			}
			base.item.glowMask = TacticalBow.customGlowMask;
			base.DisplayName.SetDefault("Zombie Slayer's Tactical Bow");
			base.Tooltip.SetDefault("'Wow, that's a lotta arrows!'\nReplaces Wooden Arrows with Tactical Arrows");
		}

		public override void SetDefaults()
		{
			base.item.damage = 7;
			base.item.ranged = true;
			base.item.width = 36;
			base.item.height = 60;
			base.item.useTime = 42;
			base.item.useAnimation = 42;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 0f;
			base.item.value = Item.buyPrice(0, 15, 0, 0);
			base.item.rare = 2;
			base.item.UseSound = SoundID.Item5;
			base.item.autoReuse = true;
			base.item.shoot = 10;
			base.item.shootSpeed = 50f;
			base.item.useAmmo = AmmoID.Arrow;
			base.item.glowMask = TacticalBow.customGlowMask;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-2f, 0f));
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == 1)
			{
				type = base.mod.ProjectileType("TacticalArrowPro");
			}
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			float num = 0.31415927f;
			int num2 = 6;
			Vector2 vector2;
			vector2..ctor(speedX, speedY);
			vector2.Normalize();
			vector2 *= 40f;
			bool flag = Collision.CanHit(vector, 0, 0, vector + vector2, 0, 0);
			for (int i = 0; i < num2; i++)
			{
				float num3 = (float)i - ((float)num2 - 1f) / 2f;
				Vector2 vector3 = Utils.RotatedBy(vector2, (double)(num * num3), default(Vector2));
				if (!flag)
				{
					vector3 -= vector2;
				}
				int num4 = Projectile.NewProjectile(vector.X + vector3.X, vector.Y + vector3.Y, speedX, speedY, type, (int)((double)damage), knockBack, player.whoAmI, 0f, 0f);
				Main.projectile[num4].noDropItem = true;
			}
			Main.rand.Next(-25, 26);
			Main.rand.Next(-25, 26);
			return false;
		}

		public static short customGlowMask;
	}
}
