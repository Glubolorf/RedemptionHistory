﻿using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class SigilOfThorns : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sigil of Thorns");
			base.Tooltip.SetDefault("Summons empowered Eaglecrest Golem and empowered Thorn, Bane of the Forest\nOnly useable after Thorn, Bane of the Forest is defeated\nOnly usable at day\nRight-click to seperate the item\nNot consumable");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(4, 4));
			ItemID.Sets.SortingPriorityBossSpawns[base.item.type] = 13;
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 42;
			base.item.maxStack = 1;
			base.item.noUseGraphic = true;
			base.item.value = Item.sellPrice(0, 20, 0, 0);
			base.item.useAnimation = 45;
			base.item.useTime = 45;
			base.item.useStyle = 4;
			base.item.UseSound = SoundID.Item44;
			base.item.consumable = false;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
			base.item.consumable = true;
			player.QuickSpawnItem(base.mod.ItemType("LifeFruitOfThorns"), 1);
			player.QuickSpawnItem(base.mod.ItemType("AncientSigil"), 1);
		}

		public override bool CanUseItem(Player player)
		{
			return Main.dayTime && !NPC.AnyNPCs(base.mod.NPCType("EaglecrestGolemPZ")) && !NPC.AnyNPCs(base.mod.NPCType("EaglecrestGolem")) && RedeWorld.downedThorn && !NPC.AnyNPCs(base.mod.NPCType("Thorn")) && !NPC.AnyNPCs(base.mod.NPCType("ThornPZ")) && !NPC.AnyNPCs(base.mod.NPCType("Ukko")) && !NPC.AnyNPCs(base.mod.NPCType("Akka"));
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LifeFruitOfThorns", 1);
			modRecipe.AddIngredient(null, "AncientSigil", 1);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, base.mod.NPCType("EaglecrestGolemPZ"));
			NPC.SpawnOnPlayer(player.whoAmI, base.mod.NPCType("ThornPZ"));
			Main.PlaySound(15, player.position, 0);
			return true;
		}
	}
}