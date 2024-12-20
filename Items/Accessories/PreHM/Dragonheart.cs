using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.PreHM
{
	public class Dragonheart : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dragonheart");
			base.Tooltip.SetDefault("Grants complete lava immunity\nInflicts fire damage on attack\nGrants stat boost while submerged in lava\nGrants immunity to all fire debuffs");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 34;
			base.item.value = Item.buyPrice(0, 4, 0, 0);
			base.item.rare = 4;
			base.item.accessory = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DragonLeadBar", 5);
			modRecipe.AddIngredient(906, 1);
			modRecipe.AddIngredient(1322, 1);
			modRecipe.AddIngredient(1323, 1);
			modRecipe.AddTile(114);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.buffImmune[24] = true;
			player.buffImmune[67] = true;
			player.buffImmune[153] = true;
			player.buffImmune[44] = true;
			player.buffImmune[39] = true;
			player.magmaStone = true;
			player.lavaImmune = true;
			if (player.lavaWet)
			{
				player.lifeRegen += 12;
				player.statDefense += 6;
				player.moveSpeed *= 400f;
			}
		}
	}
}
