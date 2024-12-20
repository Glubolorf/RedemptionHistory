using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class ViisaanKantele : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Viisaan Kantele");
			base.Tooltip.SetDefault("'An ancient instrument once used by a wise bard'\nShoots wavy notes");
		}

		public override void SetDefaults()
		{
			base.item.damage = 550;
			base.item.magic = true;
			base.item.mana = 9;
			base.item.width = 46;
			base.item.height = 18;
			base.item.useTime = 15;
			base.item.useAnimation = 15;
			base.item.useStyle = 5;
			base.item.knockBack = 5f;
			base.item.value = Item.sellPrice(0, 20, 0, 0);
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<KanteleNote>();
			base.item.shootSpeed = 20f;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-12f, 0f));
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (!Main.dedServ)
			{
				if (player.altFunctionUse == 2)
				{
					float cursorPosFromPlayer = player.Distance(Main.MouseWorld) / (float)(Main.screenHeight / 2 / 24);
					if (cursorPosFromPlayer > 24f)
					{
						cursorPosFromPlayer = 1f;
					}
					else
					{
						cursorPosFromPlayer = cursorPosFromPlayer / 12f - 1f;
					}
					Main.PlaySound(2, (int)player.Center.X, (int)player.Center.Y, base.mod.GetSoundSlot(2, "Sounds/Item/Kantele2"), 1f, cursorPosFromPlayer);
				}
				else
				{
					float cursorPosFromPlayer2 = player.Distance(Main.MouseWorld) / (float)(Main.screenHeight / 2 / 24);
					if (cursorPosFromPlayer2 > 24f)
					{
						cursorPosFromPlayer2 = 1f;
					}
					else
					{
						cursorPosFromPlayer2 = cursorPosFromPlayer2 / 12f - 1f;
					}
					Main.PlaySound(2, (int)player.Center.X, (int)player.Center.Y, base.mod.GetSoundSlot(2, "Sounds/Item/Kantele1"), 1f, cursorPosFromPlayer2);
				}
			}
			int numberProjectiles = 1 + Main.rand.Next(2);
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(5f));
				float scale = 1f - Utils.NextFloat(Main.rand) * 0.4f;
				perturbedSpeed *= scale;
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
			}
			return true;
		}
	}
}
