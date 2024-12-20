using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class CursedBook : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Storm");
			base.Tooltip.SetDefault("Rapidly shoots Cursed Crystals that stick to enemies, dealing rapid damage");
		}

		public override void SetDefaults()
		{
			base.item.damage = 220;
			base.item.magic = true;
			base.item.width = 32;
			base.item.height = 36;
			base.item.useAnimation = 10;
			base.item.useTime = 10;
			base.item.mana = 6;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 2f;
			base.item.value = Item.buyPrice(1, 0, 0, 0);
			base.item.UseSound = SoundID.Item101;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("CursedBookPro1");
			base.item.shootSpeed = 5f;
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
			int num = 2;
			int num2 = 2;
			float num3 = 0.3f;
			Vector2 vector = default(Vector2);
			for (int i = 0; i < num; i++)
			{
				float num4 = 8f * speedX + (float)Main.rand.Next(-num2, num2 + 1) * num3;
				float num5 = 8f * speedY + (float)Main.rand.Next(-num2, num2 + 1) * num3;
				float num6 = (float)Math.Atan((double)(num5 / num4));
				vector..ctor(position.X + 75f * (float)Math.Cos((double)num6), position.Y + 75f * (float)Math.Sin((double)num6));
				float num7 = (float)Main.mouseX + Main.screenPosition.X;
				if (num7 < player.position.X)
				{
					vector..ctor(position.X - 75f * (float)Math.Cos((double)num6), position.Y - 75f * (float)Math.Sin((double)num6));
				}
				Projectile.NewProjectile(vector.X, vector.Y, num4, num5, base.mod.ProjectileType("CursedBookPro1"), damage, knockBack, Main.myPlayer, 0f, 0f);
				Projectile.NewProjectile(vector.X, vector.Y, num4 * 0.6f, num5 * 0.6f, base.mod.ProjectileType("CursedBookPro1"), damage, knockBack, Main.myPlayer, 0f, 0f);
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "ShinkiteCursed", 7);
			modRecipe.AddIngredient(172, 15);
			modRecipe.AddIngredient(518, 1);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
