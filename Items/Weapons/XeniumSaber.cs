using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class XeniumSaber : ModItem
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
				XeniumSaber.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Xenium Saber");
		}

		public override void SetDefaults()
		{
			base.item.CloneDefaults(3368);
			base.item.damage = 190;
			base.item.rare = 0;
			base.item.width = 78;
			base.item.height = 78;
			base.item.useTime = 4;
			base.item.useAnimation = 4;
			base.item.knockBack = 3f;
			base.item.useStyle = 1;
			base.item.value = Item.sellPrice(2, 50, 0, 0);
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.noMelee = true;
			base.item.noUseGraphic = true;
			base.item.shoot = base.mod.ProjectileType("XeniumSaberSlash");
			base.item.glowMask = XeniumSaber.customGlowMask;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public static short customGlowMask;
	}
}
