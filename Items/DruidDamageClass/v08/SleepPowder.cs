using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.v08
{
	public class SleepPowder : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Poisonous Sleep Powder");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nThrows a poisonous cloud that tires normal enemies and reduces defense");
		}

		public override void SafeSetDefaults()
		{
			base.item.shootSpeed = 5f;
			base.item.crit = 4;
			base.item.damage = 4;
			base.item.knockBack = 0f;
			base.item.useStyle = 1;
			base.item.useAnimation = 22;
			base.item.useTime = 22;
			base.item.width = 26;
			base.item.height = 48;
			base.item.maxStack = 999;
			base.item.rare = 2;
			base.item.consumable = true;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.UseSound = SoundID.Item1;
			base.item.value = Item.sellPrice(0, 0, 0, 50);
			base.item.shoot = base.mod.ProjectileType("PoisonSleepPro");
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(67, 10);
			modRecipe.AddIngredient(209, 1);
			modRecipe.AddIngredient(86, 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 10);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(2886, 10);
			modRecipe.AddIngredient(209, 1);
			modRecipe.AddIngredient(1329, 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 10);
			modRecipe.AddRecipe();
		}
	}
}
