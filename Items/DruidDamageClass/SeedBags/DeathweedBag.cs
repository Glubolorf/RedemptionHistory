using System;
using Redemption.Projectiles.DruidProjectiles.Plants;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.SeedBags
{
	public class DeathweedBag : DruidSeedBag
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Deathweed Seedbag");
			base.Tooltip.SetDefault("Throws a seed that grows into a Deathweed");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 10;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 44;
			base.item.useAnimation = 44;
			base.item.useStyle = 1;
			base.item.mana = 6;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 0, 45, 0);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.shoot = ModContent.ProjectileType<Seed10>();
			base.item.shootSpeed = 18f;
			this.NativeTerrainIDs = TileLists.EvilTiles;
			this.nativeText = "Corruption/Crimson";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LeatherPouch", 1);
			modRecipe.AddIngredient(2, 5);
			modRecipe.AddIngredient(316, 1);
			modRecipe.AddIngredient(57, 5);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "LeatherPouch", 1);
			modRecipe2.AddIngredient(2, 5);
			modRecipe2.AddIngredient(316, 1);
			modRecipe2.AddIngredient(1257, 5);
			modRecipe2.AddTile(18);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
