using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Ranged
{
	public class KeepersBow : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bow of the Fallen");
			base.Tooltip.SetDefault("Replaces Wooden Arrows with Dark Souls");
		}

		public override void SetDefaults()
		{
			base.item.damage = 19;
			base.item.noMelee = true;
			base.item.ranged = true;
			base.item.width = 28;
			base.item.height = 66;
			base.item.useTime = 19;
			base.item.useAnimation = 19;
			base.item.useStyle = 5;
			base.item.shoot = 1;
			base.item.useAmmo = AmmoID.Arrow;
			base.item.knockBack = 0f;
			base.item.value = Item.sellPrice(0, 2, 0, 0);
			base.item.rare = 3;
			base.item.UseSound = SoundID.Item5;
			base.item.autoReuse = true;
			base.item.shootSpeed = 16f;
			base.item.crit = 0;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-2f, 0f));
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DarkShard", 1);
			modRecipe.AddIngredient(null, "SmallLostSoul", 4);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == 1)
			{
				type = ModContent.ProjectileType<DarkSoulPro1>();
			}
			return true;
		}
	}
}
