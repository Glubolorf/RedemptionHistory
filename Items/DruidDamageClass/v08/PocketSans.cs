using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.v08
{
	public class PocketSans : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Pocket Sans");
			base.Tooltip.SetDefault("'Bad times all around'\nThrows a strange skull with unfathomable power");
		}

		public override void SafeSetDefaults()
		{
			base.item.shootSpeed = 5f;
			base.item.crit = 4;
			base.item.damage = 1;
			base.item.knockBack = 0f;
			base.item.useStyle = 1;
			base.item.useAnimation = 22;
			base.item.useTime = 22;
			base.item.width = 26;
			base.item.height = 48;
			base.item.rare = 9;
			base.item.consumable = true;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.UseSound = SoundID.Item1;
			base.item.value = Item.sellPrice(1, 1, 1, 1);
			base.item.shoot = base.mod.ProjectileType("badtimekid");
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "PocketSand", 999);
			modRecipe.AddIngredient(3459, 20);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
