using System;
using Redemption.Projectiles.Druid.Seedbag;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Druid.Seedbags
{
	public class DBLiquidFlames : DruidSeedBag
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Duality Seedbag - Liquid Flames");
			base.Tooltip.SetDefault("Tosses a fused seed of waterleaf and fireblossom with greater power than they are individually");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 57;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 38;
			base.item.useAnimation = 38;
			base.item.useStyle = 1;
			base.item.mana = 12;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 4, 0, 0);
			base.item.rare = 6;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<Seed15>();
			base.item.shootSpeed = 18f;
			this.NativeTerrainIDs = TileLists.HotTiles;
			this.nativeText = "Desert/Underworld";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "WaterleafBag", 1);
			modRecipe.AddIngredient(null, "FireblossomBag", 1);
			modRecipe.AddIngredient(null, "SoulOfBloom", 15);
			modRecipe.AddTile(null, "BotanistStationTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
