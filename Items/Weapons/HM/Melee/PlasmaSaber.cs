﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Melee
{
	public class PlasmaSaber : ModItem
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Weapons/HM/Melee/" + base.GetType().Name + "_Glow");
				PlasmaSaber.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.item.glowMask = PlasmaSaber.customGlowMask;
			base.DisplayName.SetDefault("Plasma Saber");
		}

		public override void SetDefaults()
		{
			base.item.damage = 44;
			base.item.melee = true;
			base.item.width = 46;
			base.item.height = 60;
			base.item.useTime = 12;
			base.item.useAnimation = 12;
			base.item.useStyle = 1;
			base.item.knockBack = 5f;
			base.item.value = Item.sellPrice(0, 5, 0, 0);
			base.item.rare = 7;
			base.item.UseSound = SoundID.Item15;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.glowMask = PlasmaSaber.customGlowMask;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<XenoDust>(), 0f, 0f, 0, default(Color), 1f);
			}
		}

		public static short customGlowMask;
	}
}
