using System;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class TeslaCoilStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Tesla Coil Staff");
			base.Tooltip.SetDefault("Focuses a tesla beam");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 190;
			base.item.magic = true;
			base.item.mana = 15;
			base.item.width = 64;
			base.item.height = 64;
			base.item.useTime = 10;
			base.item.useAnimation = 10;
			base.item.reuseDelay = 5;
			base.item.useStyle = 5;
			base.item.UseSound = SoundID.Item13;
			base.item.noMelee = true;
			base.item.noUseGraphic = true;
			base.item.channel = true;
			base.item.knockBack = 0f;
			base.item.value = Item.buyPrice(1, 50, 0, 0);
			base.item.shoot = ModContent.ProjectileType<TeslaCoilStaffPro>();
			base.item.shootSpeed = 30f;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "BluePrints", 1);
			modRecipe.AddIngredient(3541, 1);
			modRecipe.AddTile(null, "XenoTank1");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
