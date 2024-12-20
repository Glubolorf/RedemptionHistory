using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class DragonLeadGreatbow : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dragon Slayer's Greatbow");
			base.Tooltip.SetDefault("'See a dragon? Shoot it down. See a wyvern? Shoot it down. See a pig-fish? ... Shoot it down.'\nReplaces Wooden Arrows with Hellfire Arrows");
		}

		public override void SetDefaults()
		{
			base.item.damage = 42;
			base.item.noMelee = true;
			base.item.ranged = true;
			base.item.width = 36;
			base.item.height = 54;
			base.item.useTime = 33;
			base.item.useAnimation = 33;
			base.item.useStyle = 5;
			base.item.shoot = 1;
			base.item.useAmmo = AmmoID.Arrow;
			base.item.knockBack = 0f;
			base.item.value = Item.sellPrice(0, 8, 0, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item5;
			base.item.autoReuse = false;
			base.item.shootSpeed = 20f;
			base.item.crit = 0;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DragonLeadBar", 10);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-6f, 0f));
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == 1)
			{
				type = 41;
			}
			return true;
		}

		public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
		{
			if (target.type == 551)
			{
				damage *= 50;
			}
			if (target.type >= 87 && target.type <= 92)
			{
				damage *= 50;
			}
			if (target.type == 558)
			{
				damage *= 50;
			}
			if (target.type == 559)
			{
				damage *= 50;
			}
			if (target.type == 560)
			{
				damage *= 50;
			}
			if (target.type >= 454 && target.type <= 459)
			{
				damage *= 50;
			}
			if (target.type == 170)
			{
				damage *= 50;
			}
			if (target.type == 180)
			{
				damage *= 50;
			}
			if (target.type == 171)
			{
				damage *= 50;
			}
			if (target.type == 370)
			{
				damage *= 50;
			}
		}
	}
}
