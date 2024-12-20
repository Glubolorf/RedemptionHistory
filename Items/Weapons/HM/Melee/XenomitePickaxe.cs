using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Melee
{
	public class XenomitePickaxe : ModItem
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
				XenomitePickaxe.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.item.glowMask = XenomitePickaxe.customGlowMask;
			base.DisplayName.SetDefault("Xenomite Pickaxe");
		}

		public override void SetDefaults()
		{
			base.item.damage = 40;
			base.item.melee = true;
			base.item.width = 50;
			base.item.height = 52;
			base.item.useTime = 8;
			base.item.useAnimation = 15;
			base.item.pick = 195;
			base.item.useStyle = 1;
			base.item.knockBack = 4f;
			base.item.value = 550000;
			base.item.rare = 7;
			base.item.UseSound = SoundID.Item15;
			base.item.autoReuse = true;
			base.item.glowMask = XenomitePickaxe.customGlowMask;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Xenomite", 20);
			modRecipe.AddIngredient(null, "StarliteBar", 5);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(5) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<XenoDust>(), 0f, 0f, 0, default(Color), 1f);
			}
		}

		public static short customGlowMask;
	}
}
