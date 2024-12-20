using System;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.v08
{
	public class LuminiteDruidDagger : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Luminite Dagger");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nA phantom dagger returns to the nearest player upon hitting an enemy or tile");
		}

		public override void SafeSetDefaults()
		{
			base.item.shootSpeed = 24f;
			base.item.crit = 4;
			base.item.damage = 250;
			base.item.knockBack = 5f;
			base.item.useStyle = 1;
			base.item.useAnimation = 10;
			base.item.useTime = 10;
			base.item.width = 30;
			base.item.height = 50;
			base.item.maxStack = 999;
			base.item.rare = 9;
			base.item.consumable = true;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.UseSound = SoundID.Item1;
			base.item.value = Item.sellPrice(0, 0, 4, 0);
			base.item.shoot = ModContent.ProjectileType<LuminiteDaggerPro>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DruidDagger", 100);
			modRecipe.AddIngredient(3460, 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 100);
			modRecipe.AddRecipe();
		}
	}
}
