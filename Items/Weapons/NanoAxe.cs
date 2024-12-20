using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class NanoAxe : ModItem
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
				NanoAxe.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.item.glowMask = NanoAxe.customGlowMask;
			base.DisplayName.SetDefault("Nano Pickaxe");
			base.Tooltip.SetDefault("Can mine Black Hardened Sludge");
		}

		public override void SetDefaults()
		{
			base.item.damage = 150;
			base.item.melee = true;
			base.item.width = 52;
			base.item.height = 58;
			base.item.useTime = 4;
			base.item.useAnimation = 11;
			base.item.pick = 300;
			base.item.useStyle = 1;
			base.item.knockBack = 4f;
			base.item.value = Item.sellPrice(5, 0, 0, 0);
			base.item.rare = 11;
			base.item.UseSound = SoundID.Item15;
			base.item.autoReuse = true;
			base.item.tileBoost += 3;
			base.item.glowMask = NanoAxe.customGlowMask;
		}

		public static short customGlowMask;
	}
}
