﻿using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class GhastlyCutlass : ModItem
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
				GhastlyCutlass.customGlowMask = (short)(array.Length - 1);
				Main.glowMaskTexture = array;
			}
			base.item.glowMask = GhastlyCutlass.customGlowMask;
			base.DisplayName.SetDefault("Ghastly Cutlass");
			base.Tooltip.SetDefault("'Summons the great spirits of this world'\nHitting an enemy has a chance to summon a Ghastly Spirit");
		}

		public override void SetDefaults()
		{
			base.item.damage = 91;
			base.item.melee = true;
			base.item.width = 66;
			base.item.height = 74;
			base.item.useTime = 27;
			base.item.useAnimation = 27;
			base.item.useStyle = 1;
			base.item.knockBack = 5f;
			base.item.value = Item.buyPrice(0, 10, 50, 50);
			base.item.rare = 9;
			base.item.UseSound = SoundID.Item71;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.alpha = 100;
			base.item.glowMask = GhastlyCutlass.customGlowMask;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "GhostCutlass", 1);
			modRecipe.AddIngredient(150, 30);
			modRecipe.AddIngredient(154, 20);
			modRecipe.AddIngredient(1508, 10);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			if (Main.rand.Next(3) == 0)
			{
				Projectile.NewProjectile(player.position.X + Utils.NextFloat(Main.rand, (float)player.width), player.position.Y + Utils.NextFloat(Main.rand, (float)player.height), (float)(-4 + Main.rand.Next(0, 8)), (float)(-4 + Main.rand.Next(0, 8)), base.mod.ProjectileType("SpiritGhast"), base.item.damage, base.item.knockBack, player.whoAmI, 0f, 1f);
			}
		}

		public static short customGlowMask;
	}
}