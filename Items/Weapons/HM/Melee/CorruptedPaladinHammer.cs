using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Projectiles.Melee;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Melee
{
	public class CorruptedPaladinHammer : ModItem
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
				CorruptedPaladinHammer.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.item.glowMask = CorruptedPaladinHammer.customGlowMask;
			base.DisplayName.SetDefault("Corrupted Paladin's Hammer");
		}

		public override void SetDefaults()
		{
			base.item.CloneDefaults(1513);
			base.item.shootSpeed *= 1.55f;
			base.item.damage = 135;
			base.item.rare = 10;
			base.item.glowMask = CorruptedPaladinHammer.customGlowMask;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(1513, 1);
			modRecipe.AddIngredient(null, "CorruptedXenomite", 15);
			modRecipe.AddIngredient(null, "VlitchBattery", 1);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			type = ModContent.ProjectileType<CorruptedPaladinHammerPro>();
			return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
		}

		public static short customGlowMask;
	}
}
