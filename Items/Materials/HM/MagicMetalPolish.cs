using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.HM
{
	public class MagicMetalPolish : ModItem
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Materials/HM/" + base.GetType().Name + "_Glow");
				MagicMetalPolish.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Magic Metal Polish");
			base.Tooltip.SetDefault("Makes things shiny with the power of magic!");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 26;
			base.item.maxStack = 1;
			base.item.value = 10000;
			base.item.rare = 4;
			base.item.glowMask = MagicMetalPolish.customGlowMask;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(1225, 5);
			modRecipe.AddIngredient(549, 2);
			modRecipe.AddIngredient(548, 2);
			modRecipe.AddIngredient(547, 2);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public static short customGlowMask;
	}
}
