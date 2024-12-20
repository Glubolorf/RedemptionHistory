using System;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Melee
{
	public class BeamKatana : ModItem
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
				BeamKatana.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Beam Katana");
			base.Tooltip.SetDefault("'Breathe, with the blood of the fallen'\nRight-Click to swing the weapon normally");
		}

		public override void SetDefaults()
		{
			base.item.CloneDefaults(3368);
			base.item.damage = 200;
			base.item.width = 48;
			base.item.height = 46;
			base.item.useTime = 4;
			base.item.useAnimation = 4;
			base.item.knockBack = 3f;
			base.item.useStyle = 1;
			base.item.crit = 4;
			base.item.value = Item.sellPrice(0, 12, 0, 0);
			base.item.rare = 10;
			base.item.UseSound = SoundID.Item15;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.noMelee = true;
			base.item.noUseGraphic = true;
			base.item.shoot = ModContent.ProjectileType<MadnessSlash>();
			base.item.glowMask = BeamKatana.customGlowMask;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				base.item.damage = 200;
				base.item.knockBack = 6f;
				base.item.useTime = 13;
				base.item.useAnimation = 13;
				base.item.noMelee = false;
				base.item.noUseGraphic = false;
				base.item.shoot = 0;
			}
			else
			{
				base.item.damage = 200;
				base.item.useTime = 4;
				base.item.knockBack = 3f;
				base.item.useAnimation = 4;
				base.item.noMelee = true;
				base.item.noUseGraphic = true;
				base.item.shoot = ModContent.ProjectileType<MadnessSlash>();
			}
			return base.CanUseItem(player);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddRecipeGroup("Redemption:Phasesabers", 1);
			modRecipe.AddIngredient(null, "CorruptedXenomite", 10);
			modRecipe.AddIngredient(548, 10);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public static short customGlowMask;
	}
}
