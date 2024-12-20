using System;
using Redemption.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class BonfireDagger : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bonfire Dagger");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nDaggers split into flames after a small distance");
		}

		public override void SafeSetDefaults()
		{
			base.item.shootSpeed = 21f;
			base.item.crit = 4;
			base.item.damage = 14;
			base.item.knockBack = 5f;
			base.item.useStyle = 1;
			base.item.useAnimation = 25;
			base.item.useTime = 25;
			base.item.width = 22;
			base.item.height = 52;
			base.item.maxStack = 999;
			base.item.rare = 3;
			base.item.consumable = true;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.UseSound = SoundID.Item1;
			base.item.value = Item.sellPrice(0, 0, 1, 0);
			base.item.shoot = base.mod.ProjectileType<BonfireDaggerPro>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DruidDagger", 100);
			modRecipe.AddIngredient(174, 5);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 100);
			modRecipe.AddRecipe();
		}
	}
}
