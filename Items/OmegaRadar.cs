using System;
using Microsoft.Xna.Framework;
using Redemption.NPCs.Bosses.OmegaOblit;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class OmegaRadar : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Omega Radar");
			base.Tooltip.SetDefault("Summons one of Vlitch's Overlords\n'Feel the sense of frustration, prepare for obliteration'\nOnly usable at night\nOnly usable after the first 2 Vlitch Overlords are defeated\nNot consumable");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(5, 4));
			ItemID.Sets.SortingPriorityBossSpawns[base.item.type] = 13;
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 30;
			base.item.maxStack = 1;
			base.item.rare = 10;
			base.item.value = Item.sellPrice(0, 50, 0, 0);
			base.item.useAnimation = 45;
			base.item.useTime = 45;
			base.item.useStyle = 4;
			base.item.noUseGraphic = true;
			base.item.UseSound = SoundID.Item44;
			base.item.consumable = false;
		}

		public override bool CanUseItem(Player player)
		{
			return !Main.dayTime && !NPC.AnyNPCs(ModContent.NPCType<OO>()) && RedeWorld.downedVlitch1 && RedeWorld.downedVlitch2;
		}

		public override bool UseItem(Player player)
		{
			Redemption.SpawnBoss(player, "OO", true, new Vector2(player.position.X + (float)Main.rand.Next(300, 400), player.position.Y - 800f), "Omega Obliterator", false);
			Main.PlaySound(15, player.position, 0);
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "VlitchScale", 10);
			modRecipe.AddIngredient(null, "VlitchBattery", 1);
			modRecipe.AddIngredient(null, "CorruptedXenomite", 15);
			modRecipe.AddIngredient(3467, 20);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
