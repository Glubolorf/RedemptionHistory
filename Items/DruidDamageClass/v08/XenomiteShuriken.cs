using System;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.v08
{
	public class XenomiteShuriken : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenomite Shuriken");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nBounces when hitting an enemy");
		}

		public override void SafeSetDefaults()
		{
			base.item.shootSpeed = 23f;
			base.item.crit = 4;
			base.item.damage = 51;
			base.item.knockBack = 5f;
			base.item.useStyle = 1;
			base.item.useAnimation = 14;
			base.item.useTime = 14;
			base.item.width = 46;
			base.item.height = 42;
			base.item.maxStack = 1;
			base.item.rare = 7;
			base.item.consumable = false;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.UseSound = SoundID.Item1;
			base.item.value = Item.sellPrice(0, 2, 0, 0);
			base.item.shoot = base.mod.ProjectileType<XenomiteShurikenPro>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "XenomiteDruidKunai", 999);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DruidShuriken", 1);
			modRecipe.AddIngredient(null, "XenomiteShard", 50);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
