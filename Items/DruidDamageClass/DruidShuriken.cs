using System;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class DruidShuriken : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Druid Shuriken");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nAny enemy that gets hit is inflicted with a pollen cloud\nNot consumable, but deals less damage to compensate");
		}

		public override void SafeSetDefaults()
		{
			base.item.shootSpeed = 22f;
			base.item.crit = 4;
			base.item.damage = 7;
			base.item.knockBack = 5f;
			base.item.useStyle = 1;
			base.item.useAnimation = 25;
			base.item.useTime = 25;
			base.item.width = 30;
			base.item.height = 32;
			base.item.maxStack = 1;
			base.item.rare = 1;
			base.item.consumable = false;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.UseSound = SoundID.Item1;
			base.item.value = Item.sellPrice(0, 0, 0, 75);
			base.item.shoot = base.mod.ProjectileType<DruidShurikenPro>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DruidDagger", 999);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
