using System;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class BonfireShuriken : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bonfire Shuriken");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nShurikens split into flames after a small distance\nNot consumable, but deals less damage to compensate");
		}

		public override void SafeSetDefaults()
		{
			base.item.shootSpeed = 23f;
			base.item.crit = 4;
			base.item.damage = 15;
			base.item.knockBack = 5f;
			base.item.useStyle = 1;
			base.item.useAnimation = 24;
			base.item.useTime = 24;
			base.item.width = 30;
			base.item.height = 30;
			base.item.maxStack = 1;
			base.item.rare = 3;
			base.item.consumable = false;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.UseSound = SoundID.Item1;
			base.item.value = Item.sellPrice(0, 0, 1, 0);
			base.item.shoot = base.mod.ProjectileType<BonfireShurikenPro>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "BonfireDagger", 999);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DruidShuriken", 1);
			modRecipe.AddIngredient(174, 50);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "BonfireDaggerBall", 1);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
