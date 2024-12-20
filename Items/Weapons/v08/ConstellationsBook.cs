using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class ConstellationsBook : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Constellations");
			base.Tooltip.SetDefault("'We own the stars, We own the sky...'\nSummons stars around the user");
		}

		public override void SetDefaults()
		{
			base.item.damage = 1300;
			base.item.magic = true;
			base.item.width = 28;
			base.item.height = 24;
			base.item.mana = 15;
			base.item.useAnimation = 30;
			base.item.useTime = 30;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(1, 0, 0, 0);
			base.item.UseSound = base.mod.GetLegacySoundSlot(2, "Sounds/Item/WorldTree1");
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("ConstellationsStar");
			base.item.shootSpeed = 0f;
		}

		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine line2 in list)
			{
				if (line2.mod == "Terraria" && line2.Name == "ItemName")
				{
					line2.overrideColor = new Color?(RedeColor.NebColour);
				}
			}
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(new Vector2(player.position.X + (float)Main.rand.Next(-600, 600), player.position.Y + (float)Main.rand.Next(-600, 600)), new Vector2(speedX, speedY), base.mod.ProjectileType("ConstellationsStar"), damage, knockBack, player.whoAmI, 0f, 0f);
			Projectile.NewProjectile(new Vector2(player.position.X + (float)Main.rand.Next(-600, 600), player.position.Y + (float)Main.rand.Next(-600, 600)), new Vector2(speedX, speedY), base.mod.ProjectileType("ConstellationsStar"), damage, knockBack, player.whoAmI, 0f, 0f);
			Projectile.NewProjectile(new Vector2(player.position.X + (float)Main.rand.Next(-600, 600), player.position.Y + (float)Main.rand.Next(-600, 600)), new Vector2(speedX, speedY), base.mod.ProjectileType("ConstellationsStar"), damage, knockBack, player.whoAmI, 0f, 0f);
			return false;
		}
	}
}
