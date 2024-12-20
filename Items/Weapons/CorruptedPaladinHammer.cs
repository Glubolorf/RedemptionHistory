﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class CorruptedPaladinHammer : ModItem
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Weapons/" + base.GetType().Name + "_Glow");
				CorruptedPaladinHammer.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.item.glowMask = CorruptedPaladinHammer.customGlowMask;
			base.DisplayName.SetDefault("Corrupted Paladin's Hammer");
		}

		public override void SetDefaults()
		{
			base.item.CloneDefaults(1513);
			base.item.shootSpeed *= 1.55f;
			base.item.damage = 135;
			base.item.rare = 10;
			base.item.glowMask = CorruptedPaladinHammer.customGlowMask;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			type = base.mod.ProjectileType("CorruptedPaladinHammerPro");
			return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
		}

		public static short customGlowMask;
	}
}
