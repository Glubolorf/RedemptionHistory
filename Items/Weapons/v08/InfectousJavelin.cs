using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class InfectousJavelin : ModItem
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Weapons/v08/" + base.GetType().Name + "_Glow");
				InfectousJavelin.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.item.glowMask = InfectousJavelin.customGlowMask;
			base.DisplayName.SetDefault("Infectious Javelin");
			base.Tooltip.SetDefault("Throws a javelin that drips sludge");
		}

		public override void SetDefaults()
		{
			base.item.damage = 46;
			base.item.melee = true;
			base.item.width = 66;
			base.item.height = 70;
			base.item.useTime = 24;
			base.item.useAnimation = 24;
			base.item.useStyle = 1;
			base.item.knockBack = 6f;
			base.item.value = Item.buyPrice(0, 2, 50, 0);
			base.item.rare = 7;
			base.item.shoot = base.mod.ProjectileType("InfectousJavelinPro");
			base.item.shootSpeed = 19f;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.glowMask = InfectousJavelin.customGlowMask;
		}

		public static short customGlowMask;
	}
}
