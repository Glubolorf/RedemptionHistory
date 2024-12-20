using System;
using Redemption.Items.Armor.Costumes;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Cores
{
	public class OmegaGift : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Omega's Gift");
			base.Tooltip.SetDefault("'Gift from my friends to you'\n{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			base.item.maxStack = 999;
			base.item.consumable = true;
			base.item.width = 30;
			base.item.height = 30;
			base.item.rare = 1;
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
			int num = Main.rand.Next(2);
			if (num != 0)
			{
				if (num == 1)
				{
					player.QuickSpawnItem(ModContent.ItemType<TBotVanityGoggles>(), 1);
				}
			}
			else
			{
				player.QuickSpawnItem(ModContent.ItemType<TBotVanityEyes>(), 1);
			}
			player.QuickSpawnItem(ModContent.ItemType<TBotVanityChestplate>(), 1);
			player.QuickSpawnItem(ModContent.ItemType<TBotVanityLegs>(), 1);
			player.QuickSpawnItem(ModContent.ItemType<OmegaCore>(), 1);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "EmptyCore", 1);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
