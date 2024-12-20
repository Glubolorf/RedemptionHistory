using System;
using Redemption.Projectiles.DruidProjectiles.Plants;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.SeedBags
{
	public class ViciousShroomBag : DruidSeedBag
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ichor Shroom Seedbag");
			base.Tooltip.SetDefault("Throws a capsule that grows a spore-releasing Ichor Shroom\nThe spores inflict Ichor for a long duration");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 1;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 32;
			base.item.useAnimation = 32;
			base.item.useStyle = 1;
			base.item.mana = 8;
			base.item.crit = 4;
			base.item.knockBack = 0f;
			base.item.value = Item.buyPrice(0, 6, 0, 0);
			base.item.rare = 5;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.shoot = ModContent.ProjectileType<Seed30>();
			base.item.shootSpeed = 18f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LeatherPouch", 1);
			modRecipe.AddIngredient(2, 5);
			modRecipe.AddIngredient(1332, 10);
			modRecipe.AddIngredient(2887, 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
