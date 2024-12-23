﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Melee
{
	public class XeniumSpear : ModItem
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
				XeniumSpear.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.item.glowMask = XeniumSpear.customGlowMask;
			base.DisplayName.SetDefault("Xenium Lance");
			base.Tooltip.SetDefault("Thrusting unleashes a heavy-hitting wave blast");
		}

		public override void SetDefaults()
		{
			base.item.damage = 250;
			base.item.melee = true;
			base.item.width = 96;
			base.item.height = 96;
			base.item.useTime = 15;
			base.item.useAnimation = 15;
			base.item.useStyle = 5;
			base.item.knockBack = 9f;
			base.item.UseSound = SoundID.Item1;
			base.item.value = Item.sellPrice(0, 40, 0, 0);
			base.item.rare = 11;
			base.item.shoot = ModContent.ProjectileType<XeniumSpearPro>();
			base.item.shootSpeed = 5.7f;
			base.item.noMelee = true;
			base.item.noUseGraphic = true;
			base.item.autoReuse = true;
			base.item.glowMask = XeniumSpear.customGlowMask;
		}

		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[base.item.shoot] < 1;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX * 2f, speedY * 2f, ModContent.ProjectileType<XeniumSpearPro2>(), damage * 2, knockBack, player.whoAmI, 0f, 0f);
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "XeniumBar", 21);
			modRecipe.AddTile(null, "XenoTank1");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public static short customGlowMask;
	}
}
