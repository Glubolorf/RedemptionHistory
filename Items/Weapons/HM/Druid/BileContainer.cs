using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Druid;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Druid
{
	public class BileContainer : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bile Container");
			base.Tooltip.SetDefault("Sprays a shower of burning acid\nDecreases target's defense and drains life\nRight-clicking yeets the container");
		}

		public override void SafeSetDefaults()
		{
			base.item.crit = 4;
			base.item.damage = 28;
			base.item.knockBack = 4f;
			base.item.useStyle = 5;
			base.item.useAnimation = 6;
			base.item.useTime = 18;
			base.item.width = 24;
			base.item.height = 40;
			base.item.maxStack = 1;
			base.item.rare = 7;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.noUseGraphic = false;
			base.item.mana = 7;
			base.item.UseSound = SoundID.Item1;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.shoot = ModContent.ProjectileType<BilePro>();
			base.item.shootSpeed = 10f;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				base.item.useAnimation = 32;
				base.item.useTime = 32;
				base.item.knockBack = 7f;
				base.item.useStyle = 1;
				base.item.noUseGraphic = true;
				base.item.mana = 0;
				base.item.shoot = ModContent.ProjectileType<BileContainerPro>();
			}
			else
			{
				base.item.useAnimation = 6;
				base.item.useTime = 18;
				base.item.knockBack = 4f;
				base.item.useStyle = 5;
				base.item.noUseGraphic = false;
				base.item.mana = 7;
				base.item.shoot = ModContent.ProjectileType<BilePro>();
			}
			return base.CanUseItem(player);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.altFunctionUse == 2)
			{
				Projectile.NewProjectile(player.Center, new Vector2(speedX, speedY), base.item.shoot, base.item.damage * 2, base.item.knockBack, Main.myPlayer, 0f, 0f);
				return false;
			}
			Projectile.NewProjectile(position, new Vector2(speedX, speedY), base.item.shoot, base.item.damage, base.item.knockBack, Main.myPlayer, 0f, 0f);
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Bile", 10);
			modRecipe.AddIngredient(null, "Biohazard", 5);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
