using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.PostML
{
	public class MutagenSummon : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Summoner's Mutagen");
			base.Tooltip.SetDefault("15% increased minion damage\n+1 max minions");
		}

		public override void SetDefaults()
		{
			base.item.width = 28;
			base.item.height = 36;
			base.item.value = Item.buyPrice(0, 12, 0, 0);
			base.item.rare = 11;
			base.item.accessory = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "EmptyMutagen", 1);
			modRecipe.AddIngredient(1301, 1);
			modRecipe.AddIngredient(3459, 10);
			modRecipe.AddTile(null, "XenoTank1");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.minionDamage *= 1.15f;
			player.maxMinions++;
		}
	}
}
