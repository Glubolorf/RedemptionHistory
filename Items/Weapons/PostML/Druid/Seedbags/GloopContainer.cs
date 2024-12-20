using System;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Projectiles.Druid.Seedbag;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Druid.Seedbags
{
	public class GloopContainer : DruidSeedBag
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Weapons/PostML/Druid/Seedbags/" + base.GetType().Name + "_Glow");
				GloopContainer.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Gloop Container");
			base.Tooltip.SetDefault("Throw a container filled with gloop");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 200;
			base.item.width = 18;
			base.item.height = 34;
			base.item.useTime = 36;
			base.item.useAnimation = 36;
			base.item.useStyle = 1;
			base.item.mana = 12;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 20, 0, 0);
			base.item.rare = 10;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.noUseGraphic = true;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<GloopContainerPro>();
			base.item.shootSpeed = 18f;
			base.item.glowMask = GloopContainer.customGlowMask;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "OblitBrain", 1);
			modRecipe.AddIngredient(null, "CorruptedXenomite", 16);
			modRecipe.AddIngredient(null, "VlitchScale", 14);
			modRecipe.AddIngredient(null, "VlitchBattery", 2);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public static short customGlowMask;
	}
}
