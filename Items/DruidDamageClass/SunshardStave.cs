using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Projectiles.DruidProjectiles.Stave;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class SunshardStave : DruidStave
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/DruidDamageClass/" + base.GetType().Name + "_Glow");
				SunshardStave.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Sunshard Greatstave");
			base.Tooltip.SetDefault("Rapidly shoots Redemptive Sparks");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 78;
			base.item.height = 116;
			base.item.width = 116;
			base.item.useTime = 11;
			base.item.useAnimation = 11;
			base.item.mana = 2;
			base.item.crit = 18;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 30, 0, 0);
			base.item.rare = 8;
			base.item.UseSound = SoundID.Item125;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<SunshardSpark>();
			base.item.shootSpeed = 18f;
			base.item.glowMask = SunshardStave.customGlowMask;
			this.defaultShoot = ModContent.ProjectileType<SunshardSpark>();
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 122.2f;
		}

		protected override bool SpecialShootPattern(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(15f));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "MoonflareStave", 1);
			modRecipe.AddIngredient(null, "FDruidStave", 1);
			modRecipe.AddIngredient(null, "CrystalStave", 1);
			modRecipe.AddIngredient(null, "HallowedStave", 1);
			modRecipe.AddIngredient(549, 10);
			modRecipe.AddIngredient(548, 10);
			modRecipe.AddIngredient(547, 10);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public static short customGlowMask;
	}
}
