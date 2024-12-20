using System;
using Redemption.Projectiles.Druid.Seedbag;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Druid.Seedbags
{
	public class ShiverthornBag : DruidSeedBag
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shiverthorn Seedbag");
			base.Tooltip.SetDefault("Throws a seed that grows into a Shiverthorn");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 6;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 44;
			base.item.useAnimation = 44;
			base.item.useStyle = 1;
			base.item.mana = 7;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 0, 35, 0);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.shoot = ModContent.ProjectileType<Seed8>();
			base.item.shootSpeed = 18f;
			this.NativeTerrainIDs = TileLists.SnowTiles;
			this.nativeText = "Snow";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LeatherPouch", 1);
			modRecipe.AddIngredient(593, 5);
			modRecipe.AddIngredient(2358, 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
