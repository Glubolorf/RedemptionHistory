using System;
using Microsoft.Xna.Framework;
using Redemption.NPCs.Bosses.VCleaver;
using SubworldLibrary;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Usable.Summons
{
	public class CorruptedHeroSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corrupted Hero Sword");
			base.Tooltip.SetDefault("Summons one of Vlitch's Overlords\n'The corrupted blade draws near the power, thus beginning the final hour'\nOnly usable at night\nNot consumable");
			ItemID.Sets.SortingPriorityBossSpawns[base.item.type] = 13;
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 38;
			base.item.maxStack = 1;
			base.item.value = Item.sellPrice(0, 50, 0, 0);
			base.item.rare = 10;
			base.item.useAnimation = 30;
			base.item.useTime = 30;
			base.item.useStyle = 4;
			base.item.consumable = false;
		}

		public override bool CanUseItem(Player player)
		{
			return !Main.dayTime && !NPC.AnyNPCs(ModContent.NPCType<VlitchCleaver>()) && !NPC.AnyNPCs(ModContent.NPCType<Wielder>()) && !SLWorld.subworld;
		}

		public override bool UseItem(Player player)
		{
			Redemption.SpawnBoss(player, "Wielder", true, new Vector2(player.position.X + 200f, player.position.Y + 500f), "Wielder Bot", false);
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(1570, 1);
			modRecipe.AddIngredient(null, "GirusChip", 1);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(1570, 1);
			modRecipe2.AddIngredient(1508, 10);
			modRecipe2.AddTile(null, "CorruptorTile");
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
