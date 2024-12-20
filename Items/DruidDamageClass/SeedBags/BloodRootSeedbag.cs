using System;
using Redemption.Projectiles.DruidProjectiles.Plants;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.SeedBags
{
	public class BloodRootSeedbag : DruidSeedBag
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bloodroot Seedbag");
			base.Tooltip.SetDefault("Throws a seed that sprouts into Bloodroot Thorns");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 16;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 26;
			base.item.useAnimation = 26;
			base.item.useStyle = 1;
			base.item.mana = 5;
			base.item.crit = 8;
			base.item.knockBack = 0f;
			base.item.value = Item.buyPrice(0, 1, 0, 0);
			base.item.rare = 5;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.shoot = ModContent.ProjectileType<Seed32>();
			base.item.shootSpeed = 25f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LeatherPouch", 1);
			modRecipe.AddIngredient(2, 5);
			modRecipe.AddIngredient(86, 5);
			modRecipe.AddIngredient(1114, 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "LeatherPouch", 1);
			modRecipe2.AddIngredient(2, 5);
			modRecipe2.AddIngredient(1329, 5);
			modRecipe2.AddIngredient(1114, 1);
			modRecipe2.AddTile(null, "DruidicAltarTile");
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
