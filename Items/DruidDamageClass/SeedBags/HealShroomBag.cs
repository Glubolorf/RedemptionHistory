using System;
using Redemption.Projectiles.DruidProjectiles.Plants;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.SeedBags
{
	public class HealShroomBag : DruidSeedBag
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Heal Shroom Bag");
			base.Tooltip.SetDefault("Throws a capsule that grows spore-releasing Heal Shrooms\nThe spore clouds give Health Regen to players");
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 45;
			base.item.useAnimation = 45;
			base.item.useStyle = 1;
			base.item.mana = 5;
			base.item.crit = 4;
			base.item.value = Item.buyPrice(0, 0, 15, 0);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.shoot = ModContent.ProjectileType<HealShroomCapsule1>();
			base.item.shootSpeed = 18f;
			this.NativeTerrainIDs = TileLists.ForestTiles;
			this.nativeText = "Forest";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LeatherPouch", 1);
			modRecipe.AddIngredient(2, 5);
			modRecipe.AddIngredient(5, 3);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
