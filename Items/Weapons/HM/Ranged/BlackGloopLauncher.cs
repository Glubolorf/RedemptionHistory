using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Ranged
{
	public class BlackGloopLauncher : ModItem
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Weapons/HM/Ranged/" + base.GetType().Name + "_Glow");
				BlackGloopLauncher.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.item.glowMask = BlackGloopLauncher.customGlowMask;
			base.DisplayName.SetDefault("Gloop Launcher");
			base.Tooltip.SetDefault("Shoots gloop that explodes into smaller pieces of gloop\nUses gel as ammo");
		}

		public override void SetDefaults()
		{
			base.item.damage = 70;
			base.item.ranged = true;
			base.item.width = 58;
			base.item.height = 58;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.useStyle = 5;
			base.item.knockBack = 9f;
			base.item.UseSound = SoundID.Item11;
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.rare = 10;
			base.item.shoot = ModContent.ProjectileType<BlackGloopBall>();
			base.item.shootSpeed = 20f;
			base.item.autoReuse = true;
			base.item.noMelee = true;
			base.item.glowMask = BlackGloopLauncher.customGlowMask;
			base.item.useAmmo = AmmoID.Gel;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "BlackGloop", 12);
			modRecipe.AddTile(134);
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
