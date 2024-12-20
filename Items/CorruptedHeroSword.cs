using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class CorruptedHeroSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corrupted Hero Sword");
			base.Tooltip.SetDefault("Summons one of Vlitch's Overlords\n'The corrupted blade draws near the power, thus beginning the final hour'\nOnly usable at night");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 38;
			base.item.maxStack = 20;
			base.item.value = Item.sellPrice(0, 50, 0, 0);
			base.item.rare = 10;
			base.item.useAnimation = 30;
			base.item.useTime = 30;
			base.item.useStyle = 4;
			base.item.consumable = true;
		}

		public override bool CanUseItem(Player player)
		{
			return !Main.dayTime && !NPC.AnyNPCs(base.mod.NPCType("VlitchCleaver"));
		}

		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, base.mod.NPCType("VlitchCleaver"));
			Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0, 1f, 0f);
			Main.NewText("Let's see how long you last...", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(1570, 1);
			modRecipe.AddIngredient(null, "GirusChip", 1);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
