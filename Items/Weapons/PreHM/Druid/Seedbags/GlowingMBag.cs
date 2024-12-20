using System;
using Redemption.Projectiles.Druid.Seedbag;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Druid.Seedbags
{
	public class GlowingMBag : DruidSeedBag
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Glowing Mushroom Bag");
			base.Tooltip.SetDefault("Throws a capsule that grows spore-releasing Glowing Mushrooms\nThe spore clouds give a minor increase to all stats");
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
			base.item.value = Item.buyPrice(0, 0, 35, 0);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.shoot = ModContent.ProjectileType<GlowingMCapsule1>();
			base.item.shootSpeed = 18f;
			this.NativeTerrainIDs = TileLists.GlowingMushTiles;
			this.nativeText = "Glowing Mushroom";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LeatherPouch", 1);
			modRecipe.AddIngredient(176, 5);
			modRecipe.AddIngredient(183, 3);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
