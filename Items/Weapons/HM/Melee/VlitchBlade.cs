using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Melee
{
	public class VlitchBlade : ModItem
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Weapons/HM/Melee/" + base.GetType().Name + "_Glow");
				VlitchBlade.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
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
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(1) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<VlitchFlame>(), 0f, 0f, 0, default(Color), 1f);
			}
		}

		public static short customGlowMask;
	}
}
