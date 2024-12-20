using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class WyvernSpiritBottle : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Spirit Wyvern in a Bottle");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nReleases a spirit wyvern");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 51;
			base.item.width = 20;
			base.item.height = 26;
			base.item.useTime = 23;
			base.item.useAnimation = 23;
			base.item.useStyle = 4;
			base.item.mana = 7;
			base.item.crit = 4;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 2, 0, 0);
			base.item.rare = 5;
			base.item.UseSound = SoundID.NPCDeath6;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("SpiritWyvernPro");
			base.item.shootSpeed = 12f;
		}

		public override bool CanUseItem(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).fasterSpirits)
			{
				base.item.useTime = 19;
				base.item.useAnimation = 19;
			}
			else
			{
				base.item.useTime = 23;
				base.item.useAnimation = 23;
			}
			return true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 vector = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
			if (Collision.CanHit(position, 0, 0, position + vector, 0, 0))
			{
				position += vector;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).moreSpirits)
			{
				int num = 3;
				for (int i = 0; i < num; i++)
				{
					Vector2 vector2 = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(25f));
					float num2 = 1f - Utils.NextFloat(Main.rand) * 0.3f;
					vector2 *= num2;
					Projectile.NewProjectile(position.X, position.Y, vector2.X, vector2.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				}
				return false;
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(31, 1);
			modRecipe.AddIngredient(575, 15);
			modRecipe.AddIngredient(null, "LostSoul", 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
