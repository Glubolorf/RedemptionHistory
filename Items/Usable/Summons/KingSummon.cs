using System;
using Microsoft.Xna.Framework;
using Redemption.NPCs.Bosses.KSIII;
using Redemption.NPCs.Bosses.KSIII.Clone;
using SubworldLibrary;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Usable.Summons
{
	public class KingSummon : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cyber Tech");
			base.Tooltip.SetDefault("Summons King Slayer III\nOnly usable at day\nNot consumable");
			ItemID.Sets.SortingPriorityBossSpawns[base.item.type] = 13;
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 42;
			base.item.maxStack = 1;
			base.item.rare = 9;
			base.item.value = Item.sellPrice(0, 25, 0, 0);
			base.item.useAnimation = 45;
			base.item.useTime = 45;
			base.item.useStyle = 4;
			base.item.UseSound = SoundID.Item44;
			base.item.consumable = false;
		}

		public override bool CanUseItem(Player player)
		{
			return Main.dayTime && !NPC.AnyNPCs(ModContent.NPCType<KS3_Body>()) && !NPC.AnyNPCs(ModContent.NPCType<KS3_Body_Clone>()) && !NPC.AnyNPCs(ModContent.NPCType<ScannerDrone>()) && !NPC.AnyNPCs(ModContent.NPCType<KSStart>()) && !SLWorld.subworld;
		}

		public override bool UseItem(Player player)
		{
			if (RedeWorld.girusTalk3)
			{
				Main.NewText("King Slayer III emerges... ?", Color.MediumPurple.R, Color.MediumPurple.G, Color.MediumPurple.B, false);
				Redemption.SpawnBoss(player, "KS3_Body_Clone", false, new Vector2(player.position.X + 200f, player.position.Y - 80f), " ", false);
			}
			else
			{
				Redemption.SpawnBoss(player, "KSStart", false, new Vector2(player.position.X + 200f, player.position.Y - 80f), " ", false);
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AIChip", 1);
			modRecipe.AddIngredient(null, "Mk2Plating", 4);
			modRecipe.AddIngredient(null, "Mk2Capacitator", 2);
			modRecipe.AddIngredient(549, 5);
			modRecipe.AddIngredient(548, 5);
			modRecipe.AddIngredient(547, 5);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
