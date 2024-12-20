using System;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.v08
{
	public class LuminiteDruidShuriken : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Luminite Shuriken");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nA phantom shuriken returns to the nearest player upon hitting an enemy or tile");
		}

		public override void SafeSetDefaults()
		{
			base.item.shootSpeed = 26f;
			base.item.crit = 4;
			base.item.damage = 178;
			base.item.knockBack = 5f;
			base.item.useStyle = 1;
			base.item.useAnimation = 9;
			base.item.useTime = 9;
			base.item.width = 30;
			base.item.height = 50;
			base.item.maxStack = 1;
			base.item.rare = 9;
			base.item.consumable = false;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.UseSound = SoundID.Item1;
			base.item.value = Item.sellPrice(0, 2, 0, 0);
			base.item.shoot = base.mod.ProjectileType<LuminiteShurikenPro>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LuminiteDruidDagger", 999);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DruidShuriken", 1);
			modRecipe.AddIngredient(3460, 50);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
