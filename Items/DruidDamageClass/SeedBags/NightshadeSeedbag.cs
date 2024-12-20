using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.SeedBags
{
	public class NightshadeSeedbag : DruidSeedBag
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nightshade Seedbag");
			base.Tooltip.SetDefault("Throws a seed that grows into a Nightshade\nThe plant shoots nightshade skulls that form poison clouds");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 13;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 38;
			base.item.useAnimation = 38;
			base.item.useStyle = 1;
			base.item.mana = 8;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 1, 0, 0);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.shoot = base.mod.ProjectileType("Seed29");
			base.item.shootSpeed = 18f;
			this.NativeTerrainIDs = TileLists.ForestTiles;
			this.nativeText = "Forest (Night)";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LeatherPouch", 1);
			modRecipe.AddIngredient(2, 5);
			modRecipe.AddIngredient(null, "Nightshade", 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
