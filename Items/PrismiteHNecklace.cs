using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	[AutoloadEquip(new EquipType[]
	{
		11
	})]
	public class PrismiteHNecklace : ModItem
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
				array[array.Length - 1] = base.mod.GetTexture("Items/" + base.GetType().Name + "_Glow");
				PrismiteHNecklace.customGlowMask = (short)(array.Length - 1);
				Main.glowMaskTexture = array;
			}
			base.DisplayName.SetDefault("Prismite Heart Necklace");
			base.Tooltip.SetDefault("+600 max health\nDecreases defence by 999");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 34;
			base.item.value = Item.buyPrice(0, 4, 0, 0);
			base.item.rare = 5;
			base.item.accessory = true;
			base.item.glowMask = PrismiteHNecklace.customGlowMask;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(29, 1);
			modRecipe.AddIngredient(85, 6);
			modRecipe.AddIngredient(2310, 1);
			modRecipe.AddTile(114);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statDefense -= 999;
			player.statLifeMax2 += 600;
		}

		public static short customGlowMask;
	}
}
