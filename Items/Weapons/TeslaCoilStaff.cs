using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
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
			base.Tooltip.SetDefault("Casts powerful lightning bolts randomly around the user");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 2000;
			base.item.magic = true;
			base.item.mana = 30;
			base.item.width = 64;
			base.item.height = 64;
			base.item.useTime = 15;
			base.item.useAnimation = 15;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 6f;
			base.item.value = Item.buyPrice(1, 50, 0, 0);
			base.item.UseSound = SoundID.Item117;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("UkkosLightning");
			base.item.shootSpeed = 0f;
		}

		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.overrideColor = new Color?(new Color(0, 255, 200));
				}
			}
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(new Vector2(player.position.X + (float)Main.rand.Next(-449, 450), player.position.Y + (float)Main.rand.Next(-449, 50)), new Vector2(speedX, speedY), type, base.item.damage, base.item.knockBack, player.whoAmI, 0f, 0f);
			Projectile.NewProjectile(new Vector2(player.position.X + (float)Main.rand.Next(-449, 450), player.position.Y + (float)Main.rand.Next(-49, 50)), new Vector2(speedX, speedY), type, base.item.damage, base.item.knockBack, player.whoAmI, 0f, 0f);
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "BluePrints", 1);
			modRecipe.AddIngredient(null, "TeslaCannon", 1);
			modRecipe.AddTile(null, "XenoTank1");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
