using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Magic
{
	public class SongOfTheAbyss : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Song of the Abyss");
			base.Tooltip.SetDefault("'Cry a requiem for sunlight'\nPlays a sorrowful tune");
		}

		public override void SetDefaults()
		{
			base.item.damage = 300;
			base.item.magic = true;
			base.item.mana = 20;
			base.item.width = 42;
			base.item.height = 42;
			base.item.useTime = 17;
			base.item.useAnimation = 17;
			base.item.useStyle = 5;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 20, 0, 0);
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<ShadeTreble>();
			base.item.shootSpeed = 19f;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-4f, 0f));
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (!Main.dedServ)
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
				Main.PlaySound(2, (int)player.Center.X, (int)player.Center.Y, base.mod.GetSoundSlot(2, "Sounds/Item/Lyre1"), 1f, cursorPosFromPlayer);
			}
			int num = this.shot;
			if (num != 0)
			{
				if (num == 1)
				{
					Projectile.NewProjectile(position.X, position.Y, speedX + (float)Main.rand.Next(-4, 5), speedY + (float)Main.rand.Next(-4, 5), ModContent.ProjectileType<ShadeTreble>(), damage, knockBack, Main.myPlayer, 1f, 0f);
					this.shot = 0;
				}
			}
			else
			{
				Projectile.NewProjectile(position.X, position.Y, speedX + (float)Main.rand.Next(-4, 5), speedY + (float)Main.rand.Next(-4, 5), ModContent.ProjectileType<ShadeTreble>(), damage, knockBack, Main.myPlayer, 0f, 0f);
				this.shot = 1;
			}
			return false;
		}

		public int shot;
	}
}
