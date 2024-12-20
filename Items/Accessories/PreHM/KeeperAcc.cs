using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.PreHM
{
	public class KeeperAcc : ModItem
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
				KeeperAcc.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Keeper's Circlet");
			base.Tooltip.SetDefault("Most skeletons become friendly");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 26;
			base.item.value = Item.buyPrice(0, 5, 0, 0);
			base.item.rare = 4;
			base.item.accessory = true;
			base.item.glowMask = KeeperAcc.customGlowMask;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).skeletonFriendly = true;
		}

		public static short customGlowMask;
	}
}
