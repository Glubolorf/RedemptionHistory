using System;
using Redemption.Projectiles.DruidProjectiles.Plants;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.SeedBags
{
	public class DBSunAndMoon : DruidSeedBag
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Duality Seedbag - Sun and Moon");
			base.Tooltip.SetDefault("Tosses a seed that unleashes the power of Daybloom and Moonglow with great power");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 55;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 40;
			base.item.useAnimation = 40;
			base.item.useStyle = 1;
			base.item.mana = 10;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 4, 0, 0);
			base.item.rare = 6;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<Seed17>();
			base.item.shootSpeed = 18f;
			this.NativeTerrainIDs = TileLists.NatureTiles;
			this.nativeText = "Forest/Jungle";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DaybloomBag", 1);
			modRecipe.AddIngredient(null, "MoonglowBag", 1);
			modRecipe.AddIngredient(null, "SoulOfBloom", 15);
			modRecipe.AddTile(null, "BotanistStationTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
