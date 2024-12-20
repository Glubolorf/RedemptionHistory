using System;
using Redemption.Projectiles.DruidProjectiles.Plants;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.SeedBags
{
	public class WaterleafBag : DruidSeedBag
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Waterleaf Seedbag");
			base.Tooltip.SetDefault("Throws a seed that grows into a moist Waterleaf");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 4;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.useStyle = 1;
			base.item.mana = 4;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 0, 5, 0);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.shoot = ModContent.ProjectileType<Seed9>();
			base.item.shootSpeed = 18f;
			this.NativeTerrainIDs = TileLists.DesertTiles;
			this.nativeText = "Desert";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LeatherPouch", 1);
			modRecipe.AddIngredient(169, 5);
			modRecipe.AddIngredient(317, 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
