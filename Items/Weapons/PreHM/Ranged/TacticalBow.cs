using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Ranged
{
	public class TacticalBow : ModItem
	{
		public override void SetStaticDefaults()
		{
			if (Main.netMode != 2)
			{
				Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					glowMasks[i] = Main.glowMaskTexture[i];
				}
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Weapons/PreHM/Ranged/" + base.GetType().Name + "_Glow");
				TacticalBow.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
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
			base.item.useTime = 60;
			base.item.useAnimation = 60;
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
				type = ModContent.ProjectileType<TacticalArrowPro>();
			}
			Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
			float num117 = 0.31415927f;
			int num118 = 6;
			Vector2 vector3 = new Vector2(speedX, speedY);
			vector3.Normalize();
			vector3 *= 40f;
			bool flag11 = Collision.CanHit(vector2, 0, 0, vector2 + vector3, 0, 0);
			for (int num119 = 0; num119 < num118; num119++)
			{
				float num120 = (float)num119 - ((float)num118 - 1f) / 2f;
				Vector2 value9 = Utils.RotatedBy(vector3, (double)(num117 * num120), default(Vector2));
				if (!flag11)
				{
					value9 -= vector3;
				}
				int num121 = Projectile.NewProjectile(vector2.X + value9.X, vector2.Y + value9.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, 0f);
				Main.projectile[num121].noDropItem = true;
			}
			Main.rand.Next(-25, 26);
			Main.rand.Next(-25, 26);
			return false;
		}

		public static short customGlowMask;
	}
}
