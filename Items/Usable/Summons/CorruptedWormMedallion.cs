using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Accessories.PreHM;
using Redemption.NPCs.Bosses;
using SubworldLibrary;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Usable.Summons
{
	public class CorruptedWormMedallion : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corrupted Worm Medallion");
			base.Tooltip.SetDefault("Summons one of Vlitch's Overlords\n'Mechanical shrieks beneath the ground, be wary of the deadly sound'\nOnly usable at night\nNot consumable");
			ItemID.Sets.SortingPriorityBossSpawns[base.item.type] = 13;
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 38;
			base.item.maxStack = 1;
			base.item.rare = 10;
			base.item.value = Item.sellPrice(0, 50, 0, 0);
			base.item.useAnimation = 45;
			base.item.useTime = 45;
			base.item.useStyle = 4;
			base.item.UseSound = SoundID.Item44;
			base.item.consumable = false;
		}

		public override bool CanUseItem(Player player)
		{
			return !Main.dayTime && !NPC.AnyNPCs(ModContent.NPCType<VlitchWormHead>()) && !SLWorld.subworld;
		}

		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<VlitchWormHead>());
			Main.PlaySound(15, player.position, 0);
			if (!RedeConfigClient.Instance.NoLoreElements)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().omegaPower)
				{
					Main.NewText("Why does this puny android summon me?", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
				}
				else if (BasePlayer.HasAccessory(player, ModContent.ItemType<CrownOfTheKing>(), true, true))
				{
					Main.NewText("Pah, a PUNY chicken challenges ME!?", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
				}
				else
				{
					Main.NewText("You fool, no weapon can pierce through me...", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
				}
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(548, 50);
			modRecipe.AddIngredient(null, "GirusChip", 1);
			modRecipe.AddIngredient(null, "VlitchBattery", 1);
			modRecipe.AddIngredient(null, "CorruptedXenomite", 10);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
