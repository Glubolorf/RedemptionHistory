using System;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class FissionStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Fission Staff");
			base.Tooltip.SetDefault("Casts a destructive atom that explodes and splits");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 1111;
			base.item.magic = true;
			base.item.mana = 50;
			base.item.width = 66;
			base.item.height = 66;
			base.item.useTime = 32;
			base.item.useAnimation = 32;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 11f;
			base.item.value = Item.buyPrice(5, 0, 0, 0);
			base.item.UseSound = SoundID.Item20;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<AtomNucleus>();
			base.item.shootSpeed = 10f;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 4;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Corium", 14);
			modRecipe.AddRecipeGroup("Redemption:Plating", 4);
			modRecipe.AddRecipeGroup("Redemption:Capacitators", 1);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
