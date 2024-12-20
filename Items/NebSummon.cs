using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class NebSummon : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Galaxy Stone");
			base.Tooltip.SetDefault("Summons Nebuleus, Angel of the Cosmos\nOnly usable at night\nNot consumable");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(4, 8));
			ItemID.Sets.SortingPriorityBossSpawns[base.item.type] = 13;
		}

		public override void SetDefaults()
		{
			base.item.width = 38;
			base.item.height = 26;
			base.item.maxStack = 1;
			base.item.value = Item.sellPrice(0, 65, 0, 0);
			base.item.useAnimation = 30;
			base.item.useTime = 30;
			base.item.useStyle = 4;
			base.item.consumable = false;
			base.item.noUseGraphic = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override bool CanUseItem(Player player)
		{
			return !Main.dayTime && !NPC.AnyNPCs(base.mod.NPCType("Nebuleus")) && !NPC.AnyNPCs(base.mod.NPCType("BigNebuleus")) && !NPC.AnyNPCs(base.mod.NPCType("NebuleusClone")) && !NPC.AnyNPCs(base.mod.NPCType("BigNebuleusClone"));
		}

		public override bool UseItem(Player player)
		{
			if (!RedeWorld.downedTheKeeper && RedeWorld.redemptionPoints > 0)
			{
				string text = "I have yet a reason to fight you, Terrarian...";
				Color rarityPink = Colors.RarityPink;
				byte r = rarityPink.R;
				rarityPink = Colors.RarityPink;
				byte g = rarityPink.G;
				rarityPink = Colors.RarityPink;
				Main.NewText(text, r, g, rarityPink.B, false);
			}
			else if (!RedeWorld.downedNebuleus)
			{
				Redemption.SpawnBoss(player, "Nebuleus", true, new Vector2(player.position.X + (float)Main.rand.Next(200, 250), player.position.Y - 200f), "Nebuleus", false);
				Main.PlaySound(15, player.position, 0);
			}
			else
			{
				Main.NewText("Nebuleus is nowhere to be found...", Color.MediumPurple.R, Color.MediumPurple.G, Color.MediumPurple.B, false);
				Redemption.SpawnBoss(player, "NebuleusClone", false, new Vector2(player.position.X + (float)Main.rand.Next(200, 250), player.position.Y - 200f), " ", false);
				Main.PlaySound(15, player.position, 0);
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(3467, 20);
			modRecipe.AddIngredient(1225, 10);
			modRecipe.AddIngredient(3457, 40);
			modRecipe.AddIngredient(base.mod.ItemType("GildedStar"), 20);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
