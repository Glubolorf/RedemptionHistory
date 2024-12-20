using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Costumes
{
	public class HEVSuit : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("HEV Suit");
			base.Tooltip.SetDefault("Grants immunity to the Abandoned Lab and Wasteland water\nGreatly extends underwater breathing\nGrants immunity to Radioactive Fallout and all infection debuffs\nGrants protection against up to mid-level radiation");
		}

		public override void SetDefaults()
		{
			base.item.width = 32;
			base.item.height = 30;
			base.item.value = Item.buyPrice(1, 0, 0, 0);
			base.item.rare = 11;
			base.item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			RedePlayer p = player.GetModPlayer<RedePlayer>();
			player.buffImmune[base.mod.BuffType("HeavyRadiationDebuff")] = true;
			player.buffImmune[base.mod.BuffType("RadioactiveFalloutDebuff")] = true;
			player.buffImmune[base.mod.BuffType("XenomiteDebuff")] = true;
			player.buffImmune[base.mod.BuffType("XenomiteDebuff2")] = true;
			player.buffImmune[base.mod.BuffType("BInfectionDebuff")] = true;
			p.HEVAccessory = true;
			if (hideVisual)
			{
				p.HEVHideVanity = true;
			}
			p.labWaterImmune = true;
			player.accDivingHelm = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "HazmatSuit", 1);
			modRecipe.AddIngredient(null, "GasMask", 1);
			modRecipe.AddIngredient(null, "AntiXenomiteApplier", 4);
			modRecipe.AddIngredient(null, "RawXenium", 8);
			modRecipe.AddTile(114);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
