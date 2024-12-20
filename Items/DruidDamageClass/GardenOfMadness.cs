using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class GardenOfMadness : DruidDamageItem
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
				GardenOfMadness.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Garden of Madness");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n'Breathe, with the blood of the fallen'\nRight-Clicks counters with powerful slashes");
		}

		public override void SafeSetDefaults()
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
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.noMelee = true;
			base.item.noUseGraphic = true;
			base.item.shoot = base.mod.ProjectileType("MadnessSlash");
			base.item.glowMask = GardenOfMadness.customGlowMask;
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
				base.item.shoot = base.mod.ProjectileType("MadnessSlash");
			}
			return base.CanUseItem(player);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddRecipeGroup("Redemption:Phasesabers", 1);
			modRecipe.AddIngredient(null, "CorruptedXenomite", 10);
			modRecipe.AddIngredient(548, 10);
			modRecipe.AddIngredient(null, "SoulOfBloom", 10);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public static short customGlowMask;
	}
}
