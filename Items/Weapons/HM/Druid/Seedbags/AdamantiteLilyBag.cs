using System;
using Redemption.Projectiles.Druid.Seedbag;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Druid.Seedbags
{
	public class AdamantiteLilyBag : DruidSeedBag
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Adamantite Lily Seedbag");
			base.Tooltip.SetDefault("Throws a seed that grows into a sparkling Adamantite Lily");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 21;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 47;
			base.item.useAnimation = 47;
			base.item.useStyle = 1;
			base.item.mana = 8;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.sellPrice(0, 3, 22, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<Seed23>();
			base.item.shootSpeed = 18f;
			this.NativeTerrainIDs = TileLists.OreTiles;
			this.nativeText = "Ores";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LeatherPouch", 1);
			modRecipe.AddIngredient(2, 5);
			modRecipe.AddRecipeGroup("Redemption:Plant", 1);
			modRecipe.AddIngredient(391, 5);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
