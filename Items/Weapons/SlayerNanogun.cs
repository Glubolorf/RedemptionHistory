using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class SlayerNanogun : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nanobot Launcher");
			base.Tooltip.SetDefault("'Nanomachines son'\nFires a barrage of Nanobots that latch onto enemies");
		}

		public override void SetDefaults()
		{
			base.item.damage = 43;
			base.item.magic = true;
			base.item.mana = 5;
			base.item.width = 60;
			base.item.height = 36;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 1f;
			base.item.value = Item.sellPrice(0, 15, 0, 0);
			base.item.rare = 9;
			base.item.UseSound = SoundID.Item40;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("Nanobot1");
			base.item.shootSpeed = 10f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int num = 8 + Main.rand.Next(4);
			for (int i = 0; i < num; i++)
			{
				Vector2 vector = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(10f));
				float num2 = 1f - Utils.NextFloat(Main.rand) * 0.4f;
				vector *= num2;
				Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
			}
			return false;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-5f, 0f));
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CyberPlating", 8);
			modRecipe.AddIngredient(null, "Mk2Capacitator", 1);
			modRecipe.AddIngredient(null, "AIChip", 2);
			modRecipe.AddIngredient(null, "KingCore", 1);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
