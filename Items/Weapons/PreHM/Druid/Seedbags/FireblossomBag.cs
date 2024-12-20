using System;
using Redemption.Projectiles.Druid.Seedbag;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Druid.Seedbags
{
	public class FireblossomBag : DruidSeedBag
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Fireblossom Seedbag");
			base.Tooltip.SetDefault("Throws a seed that grows into burning Fireblossom");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 31;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 44;
			base.item.useAnimation = 44;
			base.item.useStyle = 1;
			base.item.mana = 8;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 0, 65, 0);
			base.item.rare = 3;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.shoot = ModContent.ProjectileType<Seed5>();
			base.item.shootSpeed = 18f;
			this.NativeTerrainIDs = TileLists.HellTiles;
			this.nativeText = "Underworld";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LeatherPouch", 1);
			modRecipe.AddIngredient(172, 5);
			modRecipe.AddIngredient(318, 3);
			modRecipe.AddIngredient(175, 5);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
