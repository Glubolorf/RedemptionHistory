using System;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Magic
{
	public class CorruptedXenomiteScepter : ModItem
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
				CorruptedXenomiteScepter.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.item.glowMask = CorruptedXenomiteScepter.customGlowMask;
			base.DisplayName.SetDefault("Corrupted Xenomite Scepter");
			base.Tooltip.SetDefault("Shoots a volley of red lasers");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 55;
			base.item.magic = true;
			base.item.mana = 8;
			base.item.width = 60;
			base.item.height = 60;
			base.item.useAnimation = 6;
			base.item.useTime = 2;
			base.item.reuseDelay = 9;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 9, 0, 0);
			base.item.rare = 10;
			base.item.UseSound = SoundID.Item15;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<CorruptedXenomiteLaser>();
			base.item.shootSpeed = 32f;
			base.item.glowMask = CorruptedXenomiteScepter.customGlowMask;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CorruptedXenomite", 25);
			modRecipe.AddIngredient(null, "CorruptedStarliteBar", 5);
			modRecipe.AddIngredient(null, "VlitchBattery", 1);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public static short customGlowMask;
	}
}
