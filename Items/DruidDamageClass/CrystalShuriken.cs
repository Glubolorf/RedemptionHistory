using System;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class CrystalShuriken : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Crystal Shuriken");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nShurikens split in two after a small distance\nNot consumable, but deals less damage to compensate");
		}

		public override void SafeSetDefaults()
		{
			base.item.shootSpeed = 21f;
			base.item.crit = 4;
			base.item.damage = 37;
			base.item.knockBack = 5f;
			base.item.useStyle = 1;
			base.item.useAnimation = 22;
			base.item.useTime = 22;
			base.item.width = 14;
			base.item.height = 56;
			base.item.maxStack = 1;
			base.item.rare = 4;
			base.item.consumable = false;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.UseSound = SoundID.Item9;
			base.item.value = Item.sellPrice(0, 0, 1, 5);
			base.item.shoot = base.mod.ProjectileType<CrystalShurikenPro>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CrystalDagger", 999);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DruidShuriken", 1);
			modRecipe.AddIngredient(502, 10);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CrystalDaggerBall", 1);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
