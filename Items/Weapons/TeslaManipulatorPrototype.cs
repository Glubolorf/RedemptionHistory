using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class TeslaManipulatorPrototype : ModItem
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
				TeslaManipulatorPrototype.customGlowMask = (short)(array.Length - 1);
				Main.glowMaskTexture = array;
			}
			base.item.glowMask = TeslaManipulatorPrototype.customGlowMask;
			base.DisplayName.SetDefault("Tesla Manipulator Prototype");
			base.Tooltip.SetDefault("Fires bursts of tesla blasts");
		}

		public override void SetDefaults()
		{
			base.item.damage = 150;
			base.item.ranged = true;
			base.item.width = 32;
			base.item.height = 30;
			base.item.useAnimation = 30;
			base.item.useTime = 30;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 15, 0, 0);
			base.item.rare = 11;
			base.item.UseSound = base.mod.GetLegacySoundSlot(2, "Sounds/Item/XenoEyeLaser1");
			base.item.autoReuse = true;
			base.item.shoot = 440;
			base.item.shootSpeed = 5f;
			base.item.glowMask = TeslaManipulatorPrototype.customGlowMask;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int num = 10;
			int num2 = 2;
			float num3 = 0.3f;
			Vector2 vector = default(Vector2);
			for (int i = 0; i < num; i++)
			{
				float num4 = 8f * speedX + (float)Main.rand.Next(-num2, num2 + 1) * num3;
				float num5 = 8f * speedY + (float)Main.rand.Next(-num2, num2 + 1) * num3;
				float num6 = (float)Math.Atan((double)(num5 / num4));
				vector..ctor(position.X + 75f * (float)Math.Cos((double)num6), position.Y + 75f * (float)Math.Sin((double)num6));
				float num7 = (float)Main.mouseX + Main.screenPosition.X;
				if (num7 < player.position.X)
				{
					vector..ctor(position.X - 75f * (float)Math.Cos((double)num6), position.Y - 75f * (float)Math.Sin((double)num6));
				}
				int num8 = Projectile.NewProjectile(vector.X, vector.Y, num4, num5, 440, damage, knockBack, Main.myPlayer, 0f, 0f);
				Main.projectile[num8].ranged = true;
				Main.projectile[num8].magic = false;
			}
			return false;
		}

		public static short customGlowMask;
	}
}
