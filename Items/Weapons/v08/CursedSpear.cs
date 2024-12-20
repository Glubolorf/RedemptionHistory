using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class CursedSpear : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Warmonger");
			base.Tooltip.SetDefault("Strikes foes in an arc, then stabs in the direction of the cursor\nShoots out Cursed Orbs\nThe orbs inflict Cursed Flames, if one kills an enemy, it will multiply into 2 Cursed Orbs\nThe orbs created by the multiply won't multiply themselves");
		}

		public override void SetDefaults()
		{
			base.item.width = 68;
			base.item.height = 72;
			base.item.maxStack = 1;
			base.item.value = Item.buyPrice(1, 0, 0, 0);
			base.item.useStyle = 5;
			base.item.useAnimation = 30;
			base.item.useTime = 30;
			base.item.UseSound = SoundID.Item1;
			base.item.damage = 460;
			base.item.knockBack = 6f;
			base.item.melee = true;
			base.item.autoReuse = true;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.shoot = base.mod.ProjType("CursedSpearPro1");
			base.item.shootSpeed = 4f;
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
			int num = 2 + Main.rand.Next(2);
			for (int i = 0; i < num; i++)
			{
				Vector2 vector = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(5f));
				float num2 = 1f - Utils.NextFloat(Main.rand) * 0.4f;
				vector *= num2;
				Projectile.NewProjectile(position.X, position.Y, vector.X * 4f, vector.Y * 4f, base.mod.ProjectileType("CursedOrbPro1"), damage, knockBack, player.whoAmI, 0f, 0f);
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "ShinkiteCursed", 8);
			modRecipe.AddIngredient(172, 30);
			modRecipe.AddIngredient(550, 1);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
