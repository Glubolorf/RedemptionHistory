using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class Brynildra : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Brynildra");
			base.Tooltip.SetDefault("Casts a dark bolt\nIf the bolt comes in contact with an enemy, a leafless tree sprouts out of the ground beneath");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 48;
			base.item.width = 20;
			base.item.height = 26;
			base.item.useTime = 17;
			base.item.useAnimation = 17;
			base.item.useStyle = 5;
			base.item.mana = 13;
			base.item.crit = 24;
			base.item.knockBack = 5f;
			base.item.value = Item.buyPrice(0, 0, 1, 75);
			base.item.rare = 5;
			base.item.UseSound = SoundID.Item8;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("DarkBoltPro");
			base.item.shootSpeed = 20f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(521, 20);
			modRecipe.AddIngredient(272, 1);
			modRecipe.AddIngredient(27, 25);
			modRecipe.AddIngredient(3779, 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
