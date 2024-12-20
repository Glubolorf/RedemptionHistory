using System;
using Redemption.Projectiles.DruidProjectiles.Plants;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.SeedBags
{
	public class IcarsFlowerBag : DruidSeedBag
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Icar's Flower Seedbag");
			base.Tooltip.SetDefault("Throws a gravity-defying seed that grows into an upside-down flower");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 29;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 48;
			base.item.useAnimation = 48;
			base.item.useStyle = 1;
			base.item.mana = 10;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 1, 45, 0);
			base.item.rare = 7;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.shoot = ModContent.ProjectileType<Seed7>();
			base.item.shootSpeed = 4.5f;
			this.NativeTerrainIDs = TileLists.ForestTiles;
			this.nativeText = "Forest (Ceiling)";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LeatherPouch", 1);
			modRecipe.AddIngredient(751, 5);
			modRecipe.AddIngredient(null, "IcarsFlower", 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
