using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class VlitchBlade : ModItem
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
				VlitchBlade.customGlowMask = (short)(array.Length - 1);
				Main.glowMaskTexture = array;
			}
			base.item.glowMask = VlitchBlade.customGlowMask;
			base.DisplayName.SetDefault("Vlitch Blade");
			base.Tooltip.SetDefault("'One of the smaller variations'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 300;
			base.item.melee = true;
			base.item.knockBack = 5f;
			base.item.autoReuse = true;
			base.item.useTurn = false;
			base.item.width = 62;
			base.item.height = 62;
			base.item.useTime = 25;
			base.item.useAnimation = 25;
			base.item.useStyle = 1;
			base.item.UseSound = SoundID.Item7;
			base.item.value = 800000;
			base.item.rare = 10;
			base.item.glowMask = VlitchBlade.customGlowMask;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CorruptedXenomite", 20);
			modRecipe.AddIngredient(null, "CorruptedStarliteBar", 10);
			modRecipe.AddIngredient(null, "VlitchBattery", 2);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(1) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, base.mod.DustType("VlitchFlame"), 0f, 0f, 0, default(Color), 1f);
			}
		}

		public static short customGlowMask;
	}
}
