using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class BoneLeviathanFlail : ModItem
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
				BoneLeviathanFlail.customGlowMask = (short)(array.Length - 1);
				Main.glowMaskTexture = array;
			}
			base.item.glowMask = BoneLeviathanFlail.customGlowMask;
			base.DisplayName.SetDefault("Bone Leviathan Flail");
		}

		public override void SetDefaults()
		{
			base.item.width = 44;
			base.item.height = 46;
			base.item.value = Item.sellPrice(0, 5, 0, 0);
			base.item.rare = 5;
			base.item.noMelee = true;
			base.item.useStyle = 5;
			base.item.useAnimation = 40;
			base.item.useTime = 40;
			base.item.knockBack = 7.5f;
			base.item.damage = 40;
			base.item.scale = 2f;
			base.item.noUseGraphic = true;
			base.item.shoot = base.mod.ProjectileType("BoneFlailHeadPro");
			base.item.shootSpeed = 24.5f;
			base.item.UseSound = SoundID.Item1;
			base.item.melee = true;
			base.item.channel = true;
			base.item.glowMask = BoneLeviathanFlail.customGlowMask;
		}

		public static short customGlowMask;
	}
}
