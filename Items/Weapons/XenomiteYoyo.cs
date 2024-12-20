using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class XenomiteYoyo : ModItem
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
				XenomiteYoyo.customGlowMask = (short)(array.Length - 1);
				Main.glowMaskTexture = array;
			}
			base.item.glowMask = XenomiteYoyo.customGlowMask;
			base.DisplayName.SetDefault("Xenomite Yoyo");
		}

		public override void SetDefaults()
		{
			base.item.damage = 15;
			base.item.melee = true;
			base.item.useTime = 24;
			base.item.useAnimation = 24;
			base.item.useStyle = 5;
			base.item.channel = true;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 0, 30, 0);
			base.item.rare = 3;
			base.item.autoReuse = false;
			base.item.shoot = base.mod.ProjectileType("XenomiteYoyoPro");
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.UseSound = SoundID.Item1;
			base.item.glowMask = XenomiteYoyo.customGlowMask;
		}

		public static short customGlowMask;
	}
}
