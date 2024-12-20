using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class DruidsCharm : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Druid's Charm");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nSeed Bags that throw only one seed will throw 2-3 seeds instead\nStaves swing faster\nThrows seedbags faster\nSpirits shoot faster\nSpirit summoning weapons will summon 2 extra spirits\nSpirits pierce through more targets\nSpirits home in on enemies");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 42;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.rare = 6;
			base.item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.moreSeeds = true;
			redePlayer.fasterStaves = true;
			redePlayer.fasterSeedbags = true;
			redePlayer.fasterSpirits = true;
			redePlayer.moreSpirits = true;
			redePlayer.spiritHoming = true;
			redePlayer.spiritPierce = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LargeSeedPouch", 1);
			modRecipe.AddIngredient(null, "ForestCore", 100);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
