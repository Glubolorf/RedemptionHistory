using System;
using Redemption.Projectiles.Druid.Seedbag;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Druid.Seedbags
{
	public class BlinkrootBushBag : DruidSeedBag
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Blinkroot Seedbag");
			base.Tooltip.SetDefault("Throws a seed that grows into a light-emitting blinkroot");
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 70;
			base.item.useAnimation = 70;
			base.item.mana = 5;
			base.item.useStyle = 1;
			base.item.value = Item.buyPrice(0, 0, 35, 0);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.shoot = ModContent.ProjectileType<Seed3>();
			base.item.shootSpeed = 17f;
			this.NativeTerrainIDs = TileLists.DirtTiles;
			this.nativeText = "Dirt";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LeatherPouch", 1);
			modRecipe.AddIngredient(2, 5);
			modRecipe.AddIngredient(315, 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
