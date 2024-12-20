using System;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.SeedBags
{
	public class GasrootSeedbag : DruidSeedBag
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Gasroot Seedbag");
			base.Tooltip.SetDefault("Throws gas-filled Radroot that expands and pops, spraying gas");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 32;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 22;
			base.item.useAnimation = 22;
			base.item.useStyle = 1;
			base.item.mana = 5;
			base.item.crit = 4;
			base.item.knockBack = 5f;
			base.item.value = Item.buyPrice(0, 2, 0, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.shoot = ModContent.ProjectileType<GasrootPro>();
			base.item.shootSpeed = 17f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LeatherPouch", 1);
			modRecipe.AddIngredient(2, 5);
			modRecipe.AddIngredient(null, "Bile", 5);
			modRecipe.AddIngredient(null, "RadRoot", 2);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "LeatherPouch", 1);
			modRecipe2.AddIngredient(2, 5);
			modRecipe2.AddIngredient(null, "Bioweapon", 5);
			modRecipe2.AddIngredient(null, "RadRoot", 2);
			modRecipe2.AddTile(null, "DruidicAltarTile");
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
