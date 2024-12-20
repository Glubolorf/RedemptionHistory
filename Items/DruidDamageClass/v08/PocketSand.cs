using System;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.v08
{
	public class PocketSand : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Pocket Sand");
			base.Tooltip.SetDefault("'Zombie you don't like? Just throw sand in their eyes!'\nThrows a dust cloud that slightly reduces defense");
		}

		public override void SafeSetDefaults()
		{
			base.item.shootSpeed = 5f;
			base.item.crit = 4;
			base.item.damage = 3;
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
			base.item.value = Item.sellPrice(0, 0, 0, 10);
			base.item.shoot = ModContent.ProjectileType<SandDustPro>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LeatherPouch", 1);
			modRecipe.AddIngredient(169, 5);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 4);
			modRecipe.AddRecipe();
		}
	}
}
