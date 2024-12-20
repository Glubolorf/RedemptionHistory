using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Redemption.Items.Materials.PostML;
using Redemption.NPCs.Bosses.Neb;
using Redemption.NPCs.Bosses.Neb.Clone;
using Redemption.NPCs.Bosses.Neb.Phase2;
using SubworldLibrary;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Usable.Summons
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
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override bool CanUseItem(Player player)
		{
			return !Main.dayTime && !NPC.AnyNPCs(ModContent.NPCType<NebP1>()) && !NPC.AnyNPCs(ModContent.NPCType<NebP2>()) && !NPC.AnyNPCs(ModContent.NPCType<NebP1_Clone>()) && !NPC.AnyNPCs(ModContent.NPCType<NebP2_Clone>()) && !SLWorld.subworld;
		}

		public override bool AltFunctionUse(Player player)
		{
			return RedeWorld.nebDeath > 5 || (RedeWorld.downedNebuleus && RedeConfigClient.Instance.NoLoreElements);
		}

		public override bool UseItem(Player player)
		{
			if ((RedeWorld.nebDeath > 5 || (RedeWorld.downedNebuleus && RedeConfigClient.Instance.NoLoreElements)) && player.altFunctionUse == 2)
			{
				if (RedeWorld.nebDeath < 7)
				{
					Redemption.SpawnBoss(player, "NebP2", true, new Vector2(player.position.X + (float)Main.rand.Next(200, 250), player.position.Y - 200f), "Nebuleus", false);
					Main.PlaySound(15, player.position, 0);
				}
				else
				{
					Main.NewText("Nebuleus is nowhere to be found...", Color.MediumPurple.R, Color.MediumPurple.G, Color.MediumPurple.B, false);
					Redemption.SpawnBoss(player, "NebP2_Clone", false, new Vector2(player.position.X + (float)Main.rand.Next(200, 250), player.position.Y - 200f), " ", false);
					Main.PlaySound(15, player.position, 0);
				}
			}
			else if (RedeWorld.nebDeath < 7)
			{
				Redemption.SpawnBoss(player, "NebP1", true, new Vector2(player.position.X + (float)Main.rand.Next(200, 250), player.position.Y - 200f), "Nebuleus", false);
				Main.PlaySound(15, player.position, 0);
			}
			else
			{
				Main.NewText("Nebuleus is nowhere to be found...", Color.MediumPurple.R, Color.MediumPurple.G, Color.MediumPurple.B, false);
				Redemption.SpawnBoss(player, "NebP1_Clone", false, new Vector2(player.position.X + (float)Main.rand.Next(200, 250), player.position.Y - 200f), " ", false);
				Main.PlaySound(15, player.position, 0);
			}
			return true;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			Player player = Main.player[Main.myPlayer];
			int tooltipLocation = tooltips.FindIndex((TooltipLine TooltipLine) => TooltipLine.Name.Equals("Tooltip0"));
			string text = "Right-click to summon Nebuleus' Final Form instantly";
			TooltipLine line = new TooltipLine(base.mod, "text1", text)
			{
				overrideColor = new Color?(Main.DiscoColor)
			};
			if (RedeWorld.nebDeath > 5 || (RedeWorld.downedNebuleus && RedeConfigClient.Instance.NoLoreElements))
			{
				tooltips.Insert(tooltipLocation, line);
			}
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(3467, 20);
			modRecipe.AddIngredient(1225, 10);
			modRecipe.AddIngredient(3457, 40);
			modRecipe.AddIngredient(ModContent.ItemType<GildedStar>(), 20);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
