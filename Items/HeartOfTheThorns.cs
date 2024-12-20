using System;
using Redemption.NPCs.Bosses.Thorn;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class HeartOfTheThorns : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Heart of Thorns");
			base.Tooltip.SetDefault("Summons Thorn, Bane of the Forest\nOnly usable at day");
			ItemID.Sets.SortingPriorityBossSpawns[base.item.type] = 13;
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 40;
			base.item.maxStack = 20;
			base.item.rare = 2;
			base.item.value = Item.sellPrice(0, 1, 50, 0);
			base.item.noUseGraphic = true;
			base.item.useAnimation = 45;
			base.item.useTime = 45;
			base.item.useStyle = 4;
			base.item.UseSound = SoundID.Item44;
			base.item.consumable = true;
		}

		public override bool CanUseItem(Player player)
		{
			return Main.dayTime && !NPC.AnyNPCs(ModContent.NPCType<Thorn>()) && !NPC.AnyNPCs(ModContent.NPCType<ThornPZ>());
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(209, 7);
			modRecipe.AddIngredient(210, 2);
			modRecipe.AddIngredient(29, 1);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<Thorn>());
			Main.PlaySound(15, player.position, 0);
			return true;
		}
	}
}
