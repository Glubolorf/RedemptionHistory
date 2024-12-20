using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class AncientCommanderStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Commander Staff");
			base.Tooltip.SetDefault("Summons Ancient Arrowheads, Daggers, or Stalactite");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 470;
			base.item.summon = true;
			base.item.mana = 15;
			base.item.width = 42;
			base.item.height = 44;
			base.item.useTime = 8;
			base.item.useAnimation = 8;
			base.item.useStyle = 5;
			base.item.knockBack = 5f;
			base.item.value = Item.sellPrice(0, 45, 0, 0);
			base.item.UseSound = SoundID.Item20;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = base.mod.ProjectileType("AncientArrowHeadPro");
			base.item.shootSpeed = 16f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int num = Main.rand.Next(3);
			if (num == 0)
			{
				Projectile.NewProjectile(new Vector2(player.position.X + (float)Main.rand.Next(-40, 40), player.position.Y + (float)Main.rand.Next(-40, 40)), new Vector2(speedX, speedY), base.mod.ProjectileType("AncientArrowHeadPro"), damage, knockBack, player.whoAmI, 0f, 0f);
			}
			if (num == 1)
			{
				Projectile.NewProjectile(new Vector2(player.position.X + (float)Main.rand.Next(-40, 40), player.position.Y + (float)Main.rand.Next(-40, 40)), new Vector2(speedX, speedY), base.mod.ProjectileType("AncientDaggerPro"), damage, knockBack, player.whoAmI, 0f, 0f);
			}
			if (num == 2)
			{
				Projectile.NewProjectile(new Vector2(player.position.X + (float)Main.rand.Next(-40, 40), player.position.Y + (float)Main.rand.Next(-40, 40)), new Vector2(speedX, speedY), base.mod.ProjectileType("AncientStalacmitePro"), damage, knockBack, player.whoAmI, 0f, 0f);
			}
			int num2 = Main.rand.Next(3);
			if (num2 == 0)
			{
				Projectile.NewProjectile(new Vector2(player.position.X + (float)Main.rand.Next(-40, 40), player.position.Y + (float)Main.rand.Next(-40, 40)), new Vector2(speedX, speedY), base.mod.ProjectileType("AncientArrowHeadPro"), damage, knockBack, player.whoAmI, 0f, 0f);
			}
			if (num2 == 1)
			{
				Projectile.NewProjectile(new Vector2(player.position.X + (float)Main.rand.Next(-40, 40), player.position.Y + (float)Main.rand.Next(-40, 40)), new Vector2(speedX, speedY), base.mod.ProjectileType("AncientDaggerPro"), damage, knockBack, player.whoAmI, 0f, 0f);
			}
			if (num2 == 2)
			{
				Projectile.NewProjectile(new Vector2(player.position.X + (float)Main.rand.Next(-40, 40), player.position.Y + (float)Main.rand.Next(-40, 40)), new Vector2(speedX, speedY), base.mod.ProjectileType("AncientStalacmitePro"), damage, knockBack, player.whoAmI, 0f, 0f);
			}
			return false;
		}

		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine line2 in list)
			{
				if (line2.mod == "Terraria" && line2.Name == "ItemName")
				{
					line2.overrideColor = new Color?(new Color(0, 255, 200));
				}
			}
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientPowerCore", 17);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
