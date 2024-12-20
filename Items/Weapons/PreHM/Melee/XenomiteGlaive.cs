using System;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Projectiles.Melee;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Melee
{
	public class XenomiteGlaive : ModItem
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Weapons/PreHM/Melee/" + base.GetType().Name + "_Glow");
				XenomiteGlaive.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.item.glowMask = XenomiteGlaive.customGlowMask;
			base.DisplayName.SetDefault("Xenomite Glaive");
		}

		public override void SetDefaults()
		{
			base.item.damage = 14;
			base.item.melee = true;
			base.item.width = 90;
			base.item.height = 90;
			base.item.useTime = 10;
			base.item.useAnimation = 10;
			base.item.channel = true;
			base.item.useStyle = 100;
			base.item.knockBack = 6f;
			base.item.value = Item.buyPrice(0, 1, 0, 0);
			base.item.rare = 3;
			base.item.shoot = ModContent.ProjectileType<XenomiteGlaivePro>();
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.glowMask = XenomiteGlaive.customGlowMask;
		}

		public override bool UseItemFrame(Player player)
		{
			player.bodyFrame.Y = 3 * player.bodyFrame.Height;
			return true;
		}

		public static short customGlowMask;
	}
}
