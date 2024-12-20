using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class CorruptedWormMedallion : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corrupted Worm Medallion");
			base.Tooltip.SetDefault("Summons one of Vlitch's Overlords\n'Mechanical shrieks beneath the ground, be wary of the deadly sound'\nOnly usable at night");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 38;
			base.item.maxStack = 20;
			base.item.rare = 10;
			base.item.useAnimation = 45;
			base.item.useTime = 45;
			base.item.useStyle = 4;
			base.item.UseSound = SoundID.Item44;
			base.item.consumable = true;
		}

		public override bool CanUseItem(Player player)
		{
			return !Main.dayTime && !NPC.AnyNPCs(base.mod.NPCType("VlitchWormHead"));
		}

		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, base.mod.NPCType("VlitchWormHead"));
			Main.PlaySound(15, player.position, 0);
			Main.NewText("You fool, no weapon can pierce through me...", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(548, 50);
			modRecipe.AddIngredient(null, "GirusChip", 1);
			modRecipe.AddIngredient(null, "VlitchBattery", 1);
			modRecipe.AddIngredient(null, "CorruptedXenomite", 10);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
