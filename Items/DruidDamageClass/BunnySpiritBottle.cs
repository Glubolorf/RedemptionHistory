using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class BunnySpiritBottle : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Spirit Bunny in a Bottle");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nReleases a spirit bunny\nGets buffed from soul-related armoury");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 7;
			base.item.width = 20;
			base.item.height = 26;
			base.item.useTime = 16;
			base.item.useAnimation = 16;
			base.item.useStyle = 4;
			base.item.mana = 3;
			base.item.crit = 4;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 0, 1, 75);
			base.item.rare = 1;
			base.item.UseSound = SoundID.NPCDeath6.WithVolume(0.5f);
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("SpiritBunnyPro");
			base.item.shootSpeed = 11f;
		}

		public override bool CanUseItem(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).wanderingSoulSet)
			{
				base.item.damage = 37;
			}
			else if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).lostSoulSet)
			{
				base.item.damage = 14;
			}
			else
			{
				base.item.damage = 7;
			}
			return true;
		}

		public override float UseTimeMultiplier(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).fasterSpirits)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).wanderingSoulSet)
				{
					return 1.45f;
				}
				return 1.15f;
			}
			else
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).fasterSpirits)
				{
					return 1.35f;
				}
				return 1f;
			}
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).moreSpirits)
			{
				int num = 3;
				for (int i = 0; i < num; i++)
				{
					Vector2 vector = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(25f));
					float num2 = 1f - Utils.NextFloat(Main.rand) * 0.3f;
					vector *= num2;
					Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				}
				return false;
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(31, 1);
			modRecipe.AddIngredient(2019, 1);
			modRecipe.AddIngredient(null, "SmallLostSoul", 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
