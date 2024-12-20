using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class CreationFragment : ModItem
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/DruidDamageClass/" + base.GetType().Name);
				CreationFragment.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Creation Fragment");
			base.Tooltip.SetDefault("'The blessing of life resides within this fragment'");
			ItemID.Sets.ItemNoGravity[base.item.type] = true;
			ItemID.Sets.ItemIconPulse[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 24;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 20, 0);
			base.item.rare = 9;
			base.item.glowMask = CreationFragment.customGlowMask;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void PostUpdate()
		{
			Lighting.AddLight(base.item.Center, Color.LightGreen.ToVector3() * 0.55f * Main.essScale);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(3457, 1);
			modRecipe.AddIngredient(3458, 1);
			modRecipe.AddIngredient(3459, 1);
			modRecipe.AddIngredient(3456, 1);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 2);
			modRecipe.AddRecipe();
		}

		public static short customGlowMask;
	}
}
