using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class GeigerCounter : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Anomaly Detector");
			base.Tooltip.SetDefault("Summons a strange portal...\n[c/67ff3e:Begins the Infection]");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(4, 4));
			ItemID.Sets.SortingPriorityBossSpawns[base.item.type] = 13;
		}

		public override void SetDefaults()
		{
			base.item.width = 40;
			base.item.height = 42;
			base.item.maxStack = 20;
			base.item.rare = 2;
			base.item.noUseGraphic = true;
			base.item.value = Item.sellPrice(0, 5, 0, 0);
			base.item.useAnimation = 45;
			base.item.useTime = 45;
			base.item.useStyle = 4;
			base.item.UseSound = SoundID.Item44;
			base.item.consumable = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(21, 12);
			modRecipe.AddIngredient(117, 6);
			modRecipe.AddIngredient(173, 2);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(705, 12);
			modRecipe2.AddIngredient(117, 6);
			modRecipe2.AddIngredient(173, 2);
			modRecipe2.AddTile(16);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}

		public override void PostUpdate()
		{
			Lighting.AddLight(base.item.Center, Color.Green.ToVector3() * 0.55f * Main.essScale);
		}

		public override bool CanUseItem(Player player)
		{
			return !NPC.AnyNPCs(base.mod.NPCType("SoI")) && !NPC.AnyNPCs(base.mod.NPCType("StrangePortal"));
		}

		public override bool UseItem(Player player)
		{
			Redemption.SpawnBoss(player, "StrangePortal", true, new Vector2(player.position.X + (float)Main.rand.Next(250, 450), player.position.Y - 50f), "A Strange Portal", false);
			Main.PlaySound(15, player.position, 0);
			return true;
		}
	}
}
