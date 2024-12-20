using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.Spirits
{
	public class AncientSoulCaller : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Worshipper's Talisman");
			base.Tooltip.SetDefault("[c/bdffff:---Druid Class---]\nSummons a swarm of skulls\nWhen max Spirit Level is reached, summons a Lunatic Vision dealing heavy damage to all nearby enemies [c/bee7c9:(10 Second Cooldown)]\n[c/c0bdff:Minimum Spirit Level: 5]\n[c/bdffe4:Maximum Spirit Level: 10]");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 104;
			base.item.width = 38;
			base.item.height = 42;
			base.item.useTime = 10;
			base.item.useAnimation = 10;
			base.item.useStyle = 4;
			base.item.mana = 5;
			base.item.crit = 4;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 2, 0, 0);
			base.item.rare = 8;
			base.item.UseSound = SoundID.NPCDeath6.WithVolume(0.5f);
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("SoulSkull");
			base.item.shootSpeed = 26f;
		}

		public override bool CanUseItem(Player player)
		{
			return !player.HasBuff(base.mod.BuffType("SoulCallerDebuff"));
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel < 5)
			{
				type = base.mod.ProjectileType("NoSpiritPro");
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 5 || Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 6)
			{
				type = base.mod.ProjectileType("SoulSkull");
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 7 || Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 8 || Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 9)
			{
				type = base.mod.ProjectileType("DragonSkull");
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel >= 10)
			{
				Main.PlaySound(SoundID.Item123, (int)player.position.X, (int)player.position.Y);
				player.AddBuff(base.mod.BuffType("SoulCallerDebuff"), 600, true);
				type = base.mod.ProjectileType("SpectralLord");
				Projectile.NewProjectile(position.X, position.Y, 0f, 0f, type, damage * 2, knockBack, player.whoAmI, 0f, 0f);
			}
			else
			{
				Vector2 vector = Main.MouseWorld;
				position += Vector2.Normalize(new Vector2(speedX, speedY)) * 70.5f;
				float speed = (float)(3.0 + (double)Utils.NextFloat(Main.rand) * 6.0);
				float speed2 = (float)(3.0 + (double)Utils.NextFloat(Main.rand) * 6.0);
				float speed3 = (float)(3.0 + (double)Utils.NextFloat(Main.rand) * 6.0);
				float speed4 = (float)(3.0 + (double)Utils.NextFloat(Main.rand) * 6.0);
				Vector2 start = Utils.RotatedByRandom(Vector2.UnitY, 6.32);
				Vector2 start2 = Utils.RotatedByRandom(Vector2.UnitY, 6.32);
				Vector2 start3 = Utils.RotatedByRandom(Vector2.UnitY, 6.32);
				Vector2 start4 = Utils.RotatedByRandom(Vector2.UnitY, 6.32);
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritExtras >= 0)
				{
					Projectile.NewProjectile(position.X, position.Y, start.X * speed, start.Y * speed, type, damage, knockBack, player.whoAmI, vector.X, vector.Y);
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritExtras >= 1)
				{
					Projectile.NewProjectile(position.X, position.Y, start2.X * speed2, start2.Y * speed2, type, damage, knockBack, player.whoAmI, vector.X, vector.Y);
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritExtras >= 2)
				{
					Projectile.NewProjectile(position.X, position.Y, start3.X * speed3, start3.Y * speed3, type, damage, knockBack, player.whoAmI, vector.X, vector.Y);
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritExtras >= 3)
				{
					Projectile.NewProjectile(position.X, position.Y, start4.X * speed4, start4.Y * speed4, type, damage, knockBack, player.whoAmI, vector.X, vector.Y);
				}
			}
			return false;
		}

		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel < 5)
			{
				flat -= 103f;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 6)
			{
				flat += 4f;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 7)
			{
				flat += 24f;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 8)
			{
				flat += 28f;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 9)
			{
				flat += 32f;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel >= 10)
			{
				flat += 50f;
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

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LostSoul", 5);
			modRecipe.AddIngredient(null, "SapphireBar", 5);
			modRecipe.AddIngredient(1508, 15);
			modRecipe.AddIngredient(182, 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "LostSoul", 5);
			modRecipe2.AddIngredient(null, "ScarletBar", 5);
			modRecipe2.AddIngredient(1508, 15);
			modRecipe2.AddIngredient(182, 1);
			modRecipe2.AddTile(null, "DruidicAltarTile");
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
