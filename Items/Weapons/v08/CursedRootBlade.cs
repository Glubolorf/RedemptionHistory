using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class CursedRootBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Root Blade");
			base.Tooltip.SetDefault("Shoots a spread of stingers and cursed thorns");
		}

		public override void SetDefaults()
		{
			base.item.damage = 700;
			base.item.melee = true;
			base.item.width = 58;
			base.item.height = 60;
			base.item.useTime = 18;
			base.item.useAnimation = 18;
			base.item.useStyle = 1;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 50, 0, 0);
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shootSpeed = 16f;
			base.item.shoot = 55;
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

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CursedThorns", 17);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int num = 4 + Main.rand.Next(2);
			for (int i = 0; i < num; i++)
			{
				Vector2 vector = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(30f));
				float num2 = 1f - Utils.NextFloat(Main.rand) * 0.4f;
				vector *= num2;
				int num3 = Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				Main.projectile[num3].hostile = false;
				Main.projectile[num3].friendly = true;
				Main.projectile[num3].timeLeft = 60;
			}
			int num4 = 1 + Main.rand.Next(1);
			for (int j = 0; j < num4; j++)
			{
				Vector2 vector2 = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(30f));
				float num5 = 1f - Utils.NextFloat(Main.rand) * 0.4f;
				vector2 *= num5;
				Projectile.NewProjectile(position.X, position.Y, vector2.X, vector2.Y, base.mod.ProjectileType("CursedThornPro5"), damage, knockBack, player.whoAmI, 0f, 0f);
			}
			return false;
		}
	}
}
