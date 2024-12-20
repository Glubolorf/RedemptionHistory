using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class DanceOfBlades : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Final Art: Dance of Blades");
			base.Tooltip.SetDefault("'This is the only path. I have no regrets.'\nFires 4 swords from portals behind the player");
		}

		public override void SetDefaults()
		{
			base.item.damage = 52;
			base.item.magic = true;
			base.item.mana = 15;
			base.item.width = 56;
			base.item.height = 56;
			base.item.useTime = 11;
			base.item.useAnimation = 11;
			base.item.useStyle = 1;
			base.item.crit = 20;
			base.item.knockBack = 5f;
			base.item.value = Item.sellPrice(0, 16, 0, 0);
			base.item.rare = 8;
			base.item.UseSound = SoundID.Item71;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = base.mod.ProjectileType("DanceOfBladesPro1");
			base.item.shootSpeed = 16f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(new Vector2(player.position.X + (float)Main.rand.Next(-44, 45), player.position.Y + (float)Main.rand.Next(-44, 45)), new Vector2(speedX, speedY), base.mod.ProjectileType("DanceOfBladesPro1"), base.item.damage, 5f, player.whoAmI, 0f, 0f);
			Projectile.NewProjectile(new Vector2(player.position.X + (float)Main.rand.Next(-44, 45), player.position.Y + (float)Main.rand.Next(-44, 45)), new Vector2(speedX, speedY), base.mod.ProjectileType("DanceOfBladesPro2"), base.item.damage, 5f, player.whoAmI, 0f, 0f);
			Projectile.NewProjectile(new Vector2(player.position.X + (float)Main.rand.Next(-44, 45), player.position.Y + (float)Main.rand.Next(-44, 45)), new Vector2(speedX, speedY), base.mod.ProjectileType("DanceOfBladesPro2"), base.item.damage, 5f, player.whoAmI, 0f, 0f);
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(3051, 1);
			modRecipe.AddIngredient(3787, 1);
			modRecipe.AddIngredient(109, 5);
			modRecipe.AddIngredient(528, 2);
			modRecipe.AddIngredient(527, 2);
			modRecipe.AddIngredient(3261, 12);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
