using System;
using Microsoft.Xna.Framework;
using Redemption.NPCs.Bosses;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class CorruptedHeroSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corrupted Hero Sword");
			base.Tooltip.SetDefault("Summons one of Vlitch's Overlords\n'The corrupted blade draws near the power, thus beginning the final hour'\nOnly usable at night");
			ItemID.Sets.SortingPriorityBossSpawns[base.item.type] = 13;
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
			return !Main.dayTime && !NPC.AnyNPCs(ModContent.NPCType<VlitchCleaver>());
		}

		public override bool UseItem(Player player)
		{
			Redemption.SpawnBoss(player, "VlitchCleaver", true, new Vector2(player.position.X + (float)Main.rand.Next(200, 300), player.position.Y - 100f), "The Vlitch Cleaver", false);
			Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0, 1f, 0f);
			if (!RedeConfigClient.Instance.NoBossText)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().omegaPower)
				{
					Main.NewText("... An Omega Android challenges me? Must be malfunctioning... I'll scrap it for now.", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
				}
				else if (Main.LocalPlayer.GetModPlayer<RedePlayer>().chickenPower)
				{
					Main.NewText("Let's se- wait... Is that a chicken!?", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
				}
				else
				{
					Main.NewText("Let's see how long you last...", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
				}
			}
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
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(1570, 1);
			modRecipe2.AddIngredient(1508, 10);
			modRecipe2.AddTile(null, "CorruptorTile");
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
