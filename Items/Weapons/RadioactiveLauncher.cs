using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class RadioactiveLauncher : ModItem
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
				RadioactiveLauncher.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.item.glowMask = RadioactiveLauncher.customGlowMask;
			base.DisplayName.SetDefault("Radioactive Launcher");
			base.Tooltip.SetDefault("Shoots green gloop that releases radioactive gas");
		}

		public override void SetDefaults()
		{
			base.item.damage = 42;
			base.item.ranged = true;
			base.item.width = 70;
			base.item.height = 38;
			base.item.useTime = 40;
			base.item.useAnimation = 40;
			base.item.useStyle = 5;
			base.item.knockBack = 9f;
			base.item.UseSound = SoundID.Item11;
			base.item.value = Item.buyPrice(0, 2, 0, 0);
			base.item.rare = 7;
			base.item.shoot = base.mod.ProjectileType("GreenGloopPro");
			base.item.shootSpeed = 16f;
			base.item.autoReuse = true;
			base.item.noMelee = true;
			base.item.glowMask = RadioactiveLauncher.customGlowMask;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Xenomite", 8);
			modRecipe.AddIngredient(null, "DeadRock", 10);
			modRecipe.AddIngredient(null, "StarliteBar", 20);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-12f, 0f));
		}

		public static short customGlowMask;
	}
}
