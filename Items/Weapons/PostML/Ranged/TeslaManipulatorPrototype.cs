using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Ranged
{
	public class TeslaManipulatorPrototype : ModItem
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Weapons/PostML/Ranged/" + base.GetType().Name + "_Glow");
				TeslaManipulatorPrototype.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.item.glowMask = TeslaManipulatorPrototype.customGlowMask;
			base.DisplayName.SetDefault("Tesla Manipulator Prototype");
			base.Tooltip.SetDefault("Fires bursts of tesla blasts");
		}

		public override void SetDefaults()
		{
			base.item.damage = 100;
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
			int ShotAmt = 10;
			int spread = 2;
			float spreadMult = 0.3f;
			Vector2 vector2 = default(Vector2);
			for (int i = 0; i < ShotAmt; i++)
			{
				float vX = 8f * speedX + (float)Main.rand.Next(-spread, spread + 1) * spreadMult;
				float vY = 8f * speedY + (float)Main.rand.Next(-spread, spread + 1) * spreadMult;
				float angle = (float)Math.Atan((double)(vY / vX));
				vector2 = new Vector2(position.X + 75f * (float)Math.Cos((double)angle), position.Y + 75f * (float)Math.Sin((double)angle));
				if ((float)Main.mouseX + Main.screenPosition.X < player.position.X)
				{
					vector2 = new Vector2(position.X - 75f * (float)Math.Cos((double)angle), position.Y - 75f * (float)Math.Sin((double)angle));
				}
				int projectile = Projectile.NewProjectile(vector2.X, vector2.Y, vX, vY, 440, damage, knockBack, Main.myPlayer, 0f, 0f);
				Main.projectile[projectile].ranged = true;
				Main.projectile[projectile].magic = false;
			}
			return false;
		}

		public static short customGlowMask;
	}
}
