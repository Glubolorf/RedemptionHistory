using System;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Magic
{
	public class XenomiteScepter : ModItem
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Weapons/HM/Magic/" + base.GetType().Name + "_Glow");
				XenomiteScepter.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.item.glowMask = XenomiteScepter.customGlowMask;
			base.DisplayName.SetDefault("Xenomite Scepter");
			base.Tooltip.SetDefault("Shoots a volley of green lasers");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 20;
			base.item.magic = true;
			base.item.mana = 8;
			base.item.width = 64;
			base.item.height = 64;
			base.item.useAnimation = 8;
			base.item.useTime = 3;
			base.item.reuseDelay = 14;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 8, 0, 0);
			base.item.rare = 7;
			base.item.UseSound = SoundID.Item15;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<XenomiteLaser>();
			base.item.shootSpeed = 30f;
			base.item.glowMask = XenomiteScepter.customGlowMask;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Xenomite", 25);
			modRecipe.AddIngredient(null, "StarliteBar", 5);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public static short customGlowMask;
	}
}
