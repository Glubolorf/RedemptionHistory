using System;
using Redemption.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class CrystalDagger : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Crystal Dagger");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nDaggers split in two after a small distance");
		}

		public override void SafeSetDefaults()
		{
			base.item.shootSpeed = 19f;
			base.item.crit = 4;
			base.item.damage = 44;
			base.item.knockBack = 5f;
			base.item.useStyle = 1;
			base.item.useAnimation = 23;
			base.item.useTime = 23;
			base.item.width = 14;
			base.item.height = 56;
			base.item.maxStack = 999;
			base.item.rare = 4;
			base.item.consumable = true;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.UseSound = SoundID.Item9;
			base.item.value = Item.sellPrice(0, 0, 1, 5);
			base.item.shoot = base.mod.ProjectileType<CrystalDaggerPro>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DruidDagger", 100);
			modRecipe.AddIngredient(502, 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 100);
			modRecipe.AddRecipe();
		}
	}
}
