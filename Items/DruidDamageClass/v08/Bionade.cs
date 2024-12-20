using System;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.v08
{
	public class Bionade : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bionade");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nExplodes into radioactive gas");
		}

		public override void SafeSetDefaults()
		{
			base.item.shootSpeed = 17f;
			base.item.crit = 4;
			base.item.damage = 53;
			base.item.knockBack = 5f;
			base.item.useStyle = 1;
			base.item.useAnimation = 22;
			base.item.useTime = 22;
			base.item.width = 22;
			base.item.height = 34;
			base.item.maxStack = 1;
			base.item.rare = 7;
			base.item.consumable = false;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.UseSound = SoundID.Item1;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.shoot = ModContent.ProjectileType<BionadePro>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Bioweapon", 10);
			modRecipe.AddIngredient(null, "Biohazard", 5);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
