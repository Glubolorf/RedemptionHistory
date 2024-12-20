using System;
using Microsoft.Xna.Framework;
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
			return Main.dayTime && !NPC.AnyNPCs(base.mod.NPCType("KingChicken"));
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
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "ChickenEgg", 10);
			modRecipe.AddIngredient(715, 1);
			modRecipe.AddTile(26);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "ChickenEgg", 20);
			modRecipe.AddIngredient(null, "BlankGoldCrown", 1);
			modRecipe.AddTile(26);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "ChickenEgg", 20);
			modRecipe.AddIngredient(null, "BlankPlatCrown", 1);
			modRecipe.AddTile(26);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
