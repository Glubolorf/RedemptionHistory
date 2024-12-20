using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class OmegaCaller : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hovercopter Radar");
			base.Tooltip.SetDefault("Calls upon a Girus Attack Hovercopter\nOnly usable at night");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(5, 4));
			ItemID.Sets.SortingPriorityBossSpawns[base.item.type] = 13;
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 22;
			base.item.maxStack = 1;
			base.item.rare = 10;
			base.item.value = Item.sellPrice(0, 2, 0, 0);
			base.item.useAnimation = 45;
			base.item.useTime = 45;
			base.item.useStyle = 4;
			base.item.noUseGraphic = true;
			base.item.UseSound = SoundID.Item44;
			base.item.consumable = true;
		}

		public override bool CanUseItem(Player player)
		{
			return !Main.dayTime && !NPC.AnyNPCs(base.mod.NPCType("CorruptedCopter1"));
		}

		public override bool UseItem(Player player)
		{
			Redemption.SpawnBoss(player, "CorruptedCopter1", false, 0, 0, "", false);
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "OblitBrain", 1);
			modRecipe.AddIngredient(3467, 5);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
