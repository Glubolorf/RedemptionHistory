using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.Spirits
{
	public class WyvernSpiritBottle : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Spirit Wyvern in a Bottle");
			base.Tooltip.SetDefault("[c/bdffff:---Druid Class---]\nReleases a stationary spirit wyvern at cursor point\nSpirit Level 5 and above releases a flying wyvern\n[c/c0bdff:Minimum Spirit Level: 3]\n[c/bdffe4:Maximum Spirit Level: 7]");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 50;
			base.item.width = 20;
			base.item.height = 26;
			base.item.useTime = 31;
			base.item.useAnimation = 31;
			base.item.useStyle = 4;
			base.item.mana = 7;
			base.item.crit = 4;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 2, 0, 0);
			base.item.rare = 5;
			base.item.UseSound = SoundID.NPCDeath6.WithVolume(0.5f);
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("SpiritWyvernPro");
			base.item.shootSpeed = 0f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel < 5 && Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel >= 3)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().corruptedTalisman)
				{
					type = base.mod.ProjectileType("SpiritWyvernProCorrupt");
				}
				else if (Main.LocalPlayer.GetModPlayer<RedePlayer>().bloodedTalisman)
				{
					type = base.mod.ProjectileType("SpiritWyvernProCrimson");
				}
				else
				{
					type = base.mod.ProjectileType("SpiritWyvernPro");
				}
				position = Main.MouseWorld;
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, 0f);
			}
			else
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel < 3)
				{
					type = base.mod.ProjectileType("NoSpiritPro");
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel >= 5 && Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel < 7)
				{
					player.AddBuff(base.mod.BuffType("SpiritWyvernBuff"), 1200, true);
					int num184 = -1;
					int num185 = -1;
					int num186 = base.mod.ProjectileType("SpiritWyvernHead");
					int num187 = damage;
					float num188 = base.item.knockBack;
					Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
					float num189 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
					float num190 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
					for (int num191 = 0; num191 < 1000; num191++)
					{
						if (Main.projectile[num191].active && Main.projectile[num191].owner == Main.myPlayer)
						{
							if (num184 == -1 && Main.projectile[num191].type == ModContent.ProjectileType<SpiritWyvernHead>())
							{
								num184 = num191;
							}
							if (num185 == -1 && Main.projectile[num191].type == ModContent.ProjectileType<SpiritWyvernTail>())
							{
								num185 = num191;
							}
							if (num184 != -1 && num185 != -1)
							{
								break;
							}
						}
					}
					if (num184 == -1 && num185 == -1)
					{
						num189 = 0f;
						num190 = 0f;
						vector2.X = (float)Main.mouseX + Main.screenPosition.X;
						vector2.Y = (float)Main.mouseY + Main.screenPosition.Y;
						int num192 = Projectile.NewProjectile(vector2.X, vector2.Y, num189, num190, num186, num187, num188, Main.myPlayer, 0f, 0f);
						num192 = Projectile.NewProjectile(vector2.X, vector2.Y, num189, num190, ModContent.ProjectileType<SpiritWyvernBody>(), num187, num188, Main.myPlayer, (float)num192, 0f);
						int num193 = num192;
						for (int z = 0; z < player.maxMinions + 2; z++)
						{
							num192 = Projectile.NewProjectile(vector2.X, vector2.Y, num189, num190, ModContent.ProjectileType<SpiritWyvernBody>(), num187, num188, Main.myPlayer, (float)num192, 0f);
							Main.projectile[num193].localAI[1] = (float)num192;
							num193 = num192;
						}
						num192 = Projectile.NewProjectile(vector2.X, vector2.Y, num189, num190, ModContent.ProjectileType<SpiritWyvernTail>(), num187, num188, Main.myPlayer, (float)num192, 0f);
						Main.projectile[num193].localAI[1] = (float)num192;
					}
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel >= 7)
				{
					player.AddBuff(base.mod.BuffType("SpiritDragonBuff"), 2400, true);
					int num194 = -1;
					int num195 = -1;
					int num196 = base.mod.ProjectileType("SpiritDragonHead");
					int num197 = damage;
					float num198 = base.item.knockBack;
					Vector2 vector3 = player.RotatedRelativePoint(player.MountedCenter, true);
					float num199 = (float)Main.mouseX + Main.screenPosition.X - vector3.X;
					float num200 = (float)Main.mouseY + Main.screenPosition.Y - vector3.Y;
					for (int num201 = 0; num201 < 1000; num201++)
					{
						if (Main.projectile[num201].active && Main.projectile[num201].owner == Main.myPlayer)
						{
							if (num194 == -1 && Main.projectile[num201].type == ModContent.ProjectileType<SpiritDragonHead>())
							{
								num194 = num201;
							}
							if (num195 == -1 && Main.projectile[num201].type == ModContent.ProjectileType<SpiritDragonTail>())
							{
								num195 = num201;
							}
							if (num194 != -1 && num195 != -1)
							{
								break;
							}
						}
					}
					if (num194 == -1 && num195 == -1)
					{
						num199 = 0f;
						num200 = 0f;
						vector3.X = (float)Main.mouseX + Main.screenPosition.X;
						vector3.Y = (float)Main.mouseY + Main.screenPosition.Y;
						int num202 = Projectile.NewProjectile(vector3.X, vector3.Y, num199, num200, num196, num197, num198, Main.myPlayer, 0f, 0f);
						num202 = Projectile.NewProjectile(vector3.X, vector3.Y, num199, num200, ModContent.ProjectileType<SpiritDragonBody>(), num197, num198, Main.myPlayer, (float)num202, 0f);
						int num203 = num202;
						for (int z2 = 0; z2 < player.maxMinions + 4; z2++)
						{
							num202 = Projectile.NewProjectile(vector3.X, vector3.Y, num199, num200, ModContent.ProjectileType<SpiritDragonBody>(), num197, num198, Main.myPlayer, (float)num202, 0f);
							Main.projectile[num203].localAI[1] = (float)num202;
							num203 = num202;
						}
						num202 = Projectile.NewProjectile(vector3.X, vector3.Y, num199, num200, ModContent.ProjectileType<SpiritDragonTail>(), num197, num198, Main.myPlayer, (float)num202, 0f);
						Main.projectile[num203].localAI[1] = (float)num202;
					}
				}
			}
			return false;
		}

		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel < 3)
			{
				flat -= 49f;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 4)
			{
				flat += 1f;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 5)
			{
				flat += 3f;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 6)
			{
				flat += 4f;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel >= 7)
			{
				flat += 8f;
			}
		}

		public override float UseTimeMultiplier(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().fasterSpirits)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 5)
				{
					return 1.35f;
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel >= 7)
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
			modRecipe.AddIngredient(31, 1);
			modRecipe.AddIngredient(575, 15);
			modRecipe.AddIngredient(null, "LostSoul", 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
