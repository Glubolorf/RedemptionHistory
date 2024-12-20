using System;
using Microsoft.Xna.Framework;
using Redemption.NPCs.Bosses;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class EggCrown : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Egg Crown");
			base.Tooltip.SetDefault("'Summons a legendary being...'\nOnly usable at day");
			ItemID.Sets.SortingPriorityBossSpawns[base.item.type] = 13;
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 22;
			base.item.maxStack = 20;
			base.item.rare = 1;
			base.item.value = Item.sellPrice(0, 0, 50, 0);
			base.item.useAnimation = 45;
			base.item.useTime = 45;
			base.item.useStyle = 4;
			base.item.UseSound = SoundID.Item44;
			base.item.consumable = true;
		}

		public override bool CanUseItem(Player player)
		{
			return Main.dayTime && !NPC.AnyNPCs(ModContent.NPCType<KingChicken>());
		}

		public override bool UseItem(Player player)
		{
			Redemption.SpawnBoss(player, "KingChicken", true, new Vector2(player.position.X + (float)Main.rand.Next(200, 300), player.position.Y - 50f), "The Mighty King Chicken", false);
			Main.PlaySound(15, player.position, 0);
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "ChickenEgg", 10);
			modRecipe.AddIngredient(264, 1);
			modRecipe.AddTile(26);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "ChickenEgg", 10);
			modRecipe2.AddIngredient(715, 1);
			modRecipe2.AddTile(26);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
			ModRecipe modRecipe3 = new ModRecipe(base.mod);
			modRecipe3.AddIngredient(null, "ChickenEgg", 20);
			modRecipe3.AddIngredient(null, "BlankGoldCrown", 1);
			modRecipe3.AddTile(26);
			modRecipe3.SetResult(this, 1);
			modRecipe3.AddRecipe();
			ModRecipe modRecipe4 = new ModRecipe(base.mod);
			modRecipe4.AddIngredient(null, "ChickenEgg", 20);
			modRecipe4.AddIngredient(null, "BlankPlatCrown", 1);
			modRecipe4.AddTile(26);
			modRecipe4.SetResult(this, 1);
			modRecipe4.AddRecipe();
		}
	}
}
