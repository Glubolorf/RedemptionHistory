using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Druid;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Druid
{
	public class CriesOfGrief : DruidDamageSpirit
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cries of Grief");
			base.Tooltip.SetDefault("Releases cries of grief that have a chance to deal double damage\nGets buffed from spirit-related armoury");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(10, 3));
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 100;
			base.item.width = 20;
			base.item.height = 26;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.useStyle = 4;
			base.item.mana = 7;
			base.item.crit = 4;
			base.item.knockBack = 1f;
			base.item.value = Item.sellPrice(0, 55, 0, 0);
			base.item.UseSound = SoundID.NPCDeath52.WithVolume(0.5f);
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<CriesOfGriefPro2>();
			base.item.shootSpeed = 15f;
			this.spiritWeapon = false;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override bool CanUseItem(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().wanderingSoulSet)
			{
				base.item.damage = 120;
			}
			else if (Main.LocalPlayer.GetModPlayer<RedePlayer>().shadeSet)
			{
				base.item.damage = 170;
			}
			else if (Main.LocalPlayer.GetModPlayer<RedePlayer>().lostSoulSet)
			{
				base.item.damage = 105;
			}
			else
			{
				base.item.damage = 100;
			}
			return true;
		}

		public override float UseTimeMultiplier(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().fasterSpirits)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().wanderingSoulSet)
				{
					return 1.45f;
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().shadeSet)
				{
					return 1.65f;
				}
				return 1.15f;
			}
			else
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().fasterSpirits)
				{
					return 1.35f;
				}
				return 1f;
			}
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles;
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritExtras == 0)
			{
				numberProjectiles = 2;
			}
			else if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritExtras == 1)
			{
				numberProjectiles = 3;
			}
			else if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritExtras == 2)
			{
				numberProjectiles = 4;
			}
			else
			{
				numberProjectiles = 5;
			}
			for (int i = 0; i < numberProjectiles; i++)
			{
				if (Main.rand.Next(3) != 0)
				{
					Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(15f));
					float scale = 1f - Utils.NextFloat(Main.rand) * 0.3f;
					perturbedSpeed *= scale;
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				}
				else
				{
					Vector2 perturbedSpeed2 = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(15f));
					float scale2 = 1f - Utils.NextFloat(Main.rand) * 0.3f;
					perturbedSpeed2 *= scale2;
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed2.X, perturbedSpeed2.Y, ModContent.ProjectileType<CriesOfGriefPro1>(), damage * 2, knockBack, player.whoAmI, 0f, 0f);
				}
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(31, 1);
			modRecipe.AddIngredient(null, "Shadesoul", 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
