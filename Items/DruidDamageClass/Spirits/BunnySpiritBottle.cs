using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.DruidProjectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.Spirits
{
	public class BunnySpiritBottle : DruidDamageSpirit
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Spirit Bunny in a Bottle");
			base.Tooltip.SetDefault("Releases a spirit bunny");
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
			base.item.shoot = ModContent.ProjectileType<SpiritBunnyPro>();
			base.item.shootSpeed = 11f;
			this.spiritWeapon = true;
			this.minSpiritLevel = 1;
			this.maxSpiritLevel = 5;
		}

		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 0)
			{
				flat -= 6f;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 2)
			{
				flat += 2f;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 3)
			{
				flat += 4f;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 4)
			{
				flat += 5f;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel >= 5)
			{
				flat += 7f;
			}
		}

		public override float UseTimeMultiplier(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().fasterSpirits)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 3)
				{
					return 1.35f;
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel >= 5)
				{
					return 1.55f;
				}
				return 1.15f;
			}
			else
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().fasterSpirits)
				{
					return 1.25f;
				}
				return 1f;
			}
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 0)
			{
				type = ModContent.ProjectileType<NoSpiritPro>();
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 1 || Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 2)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().corruptedTalisman)
				{
					type = ModContent.ProjectileType<SpiritBunnyCorruptPro>();
				}
				else if (Main.LocalPlayer.GetModPlayer<RedePlayer>().bloodedTalisman)
				{
					type = ModContent.ProjectileType<SpiritBunnyCrimsonPro>();
				}
				else
				{
					type = ModContent.ProjectileType<SpiritBunnyPro>();
				}
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 3 || Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 4)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().corruptedTalisman)
				{
					type = ModContent.ProjectileType<SpiritBunnyCorruptPro2>();
				}
				else if (Main.LocalPlayer.GetModPlayer<RedePlayer>().bloodedTalisman)
				{
					type = ModContent.ProjectileType<SpiritBunnyCrimsonPro2>();
				}
				else
				{
					type = ModContent.ProjectileType<SpiritBunnyPro2>();
				}
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel >= 5)
			{
				type = ModContent.ProjectileType<SpiritBunnyPro3>();
				speedX *= 1.4f;
				speedY *= 1.4f;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritExtras == 0)
			{
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, 0f);
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritExtras == 1)
			{
				int numberProjectiles = 2;
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(15f));
					float scale = 1f - Utils.NextFloat(Main.rand) * 0.3f;
					perturbedSpeed *= scale;
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				}
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritExtras == 2)
			{
				int numberProjectiles2 = 3;
				for (int j = 0; j < numberProjectiles2; j++)
				{
					Vector2 perturbedSpeed2 = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(25f));
					float scale2 = 1f - Utils.NextFloat(Main.rand) * 0.3f;
					perturbedSpeed2 *= scale2;
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed2.X, perturbedSpeed2.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				}
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritExtras >= 3)
			{
				int numberProjectiles3 = 4;
				for (int k = 0; k < numberProjectiles3; k++)
				{
					Vector2 perturbedSpeed3 = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(35f));
					float scale3 = 1f - Utils.NextFloat(Main.rand) * 0.3f;
					perturbedSpeed3 *= scale3;
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed3.X, perturbedSpeed3.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				}
			}
			return false;
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
