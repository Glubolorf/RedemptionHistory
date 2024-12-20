using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.PreHM
{
	public class HeartEmblem : ModItem
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Accessories/PreHM/" + base.GetType().Name + "_Glow");
				HeartEmblem.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Heart Insignia");
			base.Tooltip.SetDefault("You respawn with 75% of maximum health and a great regen boost after death");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 28;
			base.item.value = 50000;
			base.item.rare = 3;
			base.item.expert = true;
			base.item.accessory = true;
			base.item.glowMask = HeartEmblem.customGlowMask;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).heartEmblem = true;
		}

		public static short customGlowMask;
	}
}
