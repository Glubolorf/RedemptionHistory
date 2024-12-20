using System;
using Redemption.Projectiles.Druid.Seedbag;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Druid.Seedbags
{
	public class SkyflowerBag : DruidSeedBag
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Skyflower Seedbag");
			base.Tooltip.SetDefault("Throws a seed that grows into a floating flower");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 8;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 37;
			base.item.useAnimation = 37;
			base.item.useStyle = 1;
			base.item.mana = 10;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 0, 75, 0);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.shoot = ModContent.ProjectileType<Seed2>();
			base.item.shootSpeed = 15f;
			this.NativeTerrainIDs = TileLists.CloudTiles;
			this.nativeText = "Cloud";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LeatherPouch", 1);
			modRecipe.AddIngredient(751, 5);
			modRecipe.AddIngredient(1109, 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
