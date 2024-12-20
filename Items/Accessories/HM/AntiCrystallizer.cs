using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.HM
{
	public class AntiCrystallizer : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Anti-Crystallizer Band");
			base.Tooltip.SetDefault("Makes you immune to all Xenomite infections");
		}

		public override void SetDefaults()
		{
			base.item.width = 28;
			base.item.height = 30;
			base.item.value = 10000;
			base.item.rare = 11;
			base.item.accessory = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AntiXenomiteApplier", 8);
			modRecipe.AddIngredient(null, "Xenomite", 4);
			modRecipe.AddIngredient(null, "StarliteBar", 6);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void PostUpdate()
		{
			Lighting.AddLight(base.item.Center, Color.Green.ToVector3() * 0.55f * Main.essScale);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.buffImmune[ModContent.BuffType<XenomiteDebuff>()] = true;
			player.buffImmune[ModContent.BuffType<XenomiteDebuff2>()] = true;
		}
	}
}
