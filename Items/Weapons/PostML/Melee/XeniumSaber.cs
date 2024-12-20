using System;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Melee
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Weapons/PostML/Melee/" + base.GetType().Name + "_Glow");
				XeniumSaber.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Xenium Saber");
		}

		public override void SetDefaults()
		{
			base.item.damage = 190;
			base.item.melee = true;
			base.item.rare = 0;
			base.item.width = 78;
			base.item.height = 78;
			base.item.useTime = 10;
			base.item.useAnimation = 10;
			base.item.knockBack = 3f;
			base.item.useStyle = 5;
			base.item.value = Item.sellPrice(2, 50, 0, 0);
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.noUseGraphic = true;
			base.item.channel = true;
			base.item.shoot = ModContent.ProjectileType<XeniumSaberSlash>();
			base.item.shootSpeed = 12f;
			base.item.glowMask = XeniumSaber.customGlowMask;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override bool CanUseItem(Player player)
		{
			return true;
		}

		public static short customGlowMask;
	}
}
