using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Minions;
using Redemption.Projectiles.Minions.StarSerpentMinion;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Summon
{
	public class StarSerpentsCollar : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Star Serpent's Collar");
			base.Tooltip.SetDefault("Summons a baby star serpent to fight for you");
		}

		public override void SetDefaults()
		{
			base.item.mana = 15;
			base.item.damage = 205;
			base.item.useStyle = 1;
			base.item.shootSpeed = 10f;
			base.item.shoot = ModContent.ProjectileType<StarSerpentMinionHead>();
			base.item.width = 24;
			base.item.height = 30;
			base.item.UseSound = SoundID.Item44;
			base.item.useAnimation = 24;
			base.item.useTime = 24;
			base.item.noMelee = true;
			base.item.knockBack = 2f;
			base.item.buffType = ModContent.BuffType<StarSerpentBuff>();
			base.item.summon = true;
			base.item.value = Item.sellPrice(1, 0, 0, 0);
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 3;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool UseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				player.MinionNPCTargetAim();
			}
			return base.UseItem(player);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.altFunctionUse == 2)
			{
				return false;
			}
			player.AddBuff(ModContent.BuffType<StarSerpentBuff>(), 2, true);
			int num184 = -1;
			int num185 = -1;
			int num186 = base.item.shoot;
			int num187 = damage;
			float num188 = base.item.knockBack;
			Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
			float num189 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
			float num190 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
			for (int num191 = 0; num191 < 1000; num191++)
			{
				if (Main.projectile[num191].active && Main.projectile[num191].owner == Main.myPlayer)
				{
					if (num184 == -1 && Main.projectile[num191].type == ModContent.ProjectileType<StarSerpentMinionHead>())
					{
						num184 = num191;
					}
					if (num185 == -1 && Main.projectile[num191].type == ModContent.ProjectileType<StarSerpentMinionTail>())
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
				num192 = Projectile.NewProjectile(vector2.X, vector2.Y, num189, num190, ModContent.ProjectileType<StarSerpentMinionNeck>(), num187, num188, Main.myPlayer, (float)num192, 0f);
				int num193 = num192;
				for (int z = 0; z < player.maxMinions; z++)
				{
					num192 = Projectile.NewProjectile(vector2.X, vector2.Y, num189, num190, ModContent.ProjectileType<StarSerpentMinionNeck>(), num187, num188, Main.myPlayer, (float)num192, 0f);
					Main.projectile[num193].localAI[1] = (float)num192;
					num193 = num192;
				}
				num192 = Projectile.NewProjectile(vector2.X, vector2.Y, num189, num190, ModContent.ProjectileType<StarSerpentMinionTail>(), num187, num188, Main.myPlayer, (float)num192, 0f);
				Main.projectile[num193].localAI[1] = (float)num192;
			}
			return false;
		}
	}
}
