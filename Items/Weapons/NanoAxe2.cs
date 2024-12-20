using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class NanoAxe2 : ModItem
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
				NanoAxe2.customGlowMask = (short)(array.Length - 1);
				Main.glowMaskTexture = array;
			}
			base.item.glowMask = NanoAxe2.customGlowMask;
			base.DisplayName.SetDefault("CREATIVE PICKAXE OF RECOLOURED DOOOOOOOM!!!!");
			base.Tooltip.SetDefault("Can mine lab stuff yay");
		}

		public override void SetDefaults()
		{
			base.item.damage = 555;
			base.item.melee = true;
			base.item.width = 52;
			base.item.height = 58;
			base.item.useTime = 2;
			base.item.useAnimation = 8;
			base.item.pick = 500;
			base.item.useStyle = 1;
			base.item.knockBack = 4f;
			base.item.value = Item.sellPrice(0, 0, 0, 0);
			base.item.rare = 11;
			base.item.UseSound = SoundID.Item15;
			base.item.tileBoost += 6;
			base.item.autoReuse = true;
			base.item.glowMask = NanoAxe2.customGlowMask;
		}

		public static short customGlowMask;
	}
}
