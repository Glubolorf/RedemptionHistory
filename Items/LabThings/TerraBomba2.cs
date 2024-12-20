using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.LabThings
{
	public class TerraBomba2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Terraforma Bomba");
			base.Tooltip.SetDefault("[c/87d883:Purity]\n'Country roads, take me home...'\nWARNING: Turns a colossal radius into Purity");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 18;
			base.item.maxStack = 99;
			base.item.consumable = true;
			base.item.useStyle = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.useAnimation = 60;
			base.item.useTime = 60;
			base.item.value = Item.buyPrice(15, 0, 0, 0);
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.shoot = base.mod.ProjectileType("TerraBombaPro2");
			base.item.shootSpeed = 0f;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "TerraBombaPart1", 1);
			modRecipe.AddIngredient(null, "TerraBombaPart2", 1);
			modRecipe.AddIngredient(null, "TerraBombaPart3", 1);
			modRecipe.AddIngredient(780, 333);
			modRecipe.AddTile(null, "XenoTank1");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(new Vector2(player.position.X, player.position.Y + -1300f), new Vector2(speedX, speedY), base.mod.ProjectileType("TerraBombaPro2"), 500, 5f, player.whoAmI, 0f, 0f);
			return false;
		}
	}
}
