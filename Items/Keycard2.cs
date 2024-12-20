using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class Keycard2 : ModItem
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
				array[array.Length - 1] = base.mod.GetTexture("Items/" + base.GetType().Name + "_Glow");
				Keycard2.customGlowMask = (short)(array.Length - 1);
				Main.glowMaskTexture = array;
			}
			base.DisplayName.SetDefault("Keycard");
			base.Tooltip.SetDefault("'Unlocks a special Lab Chest'");
		}

		public override void SetDefaults()
		{
			base.item.width = 44;
			base.item.height = 30;
			base.item.rare = 11;
			base.item.maxStack = 1;
			base.item.value = 0;
			base.item.glowMask = Keycard2.customGlowMask;
		}

		public static short customGlowMask;
	}
}
