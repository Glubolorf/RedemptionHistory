using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class AkisClaws : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Aki's Claws");
			base.Tooltip.SetDefault("'Very sharp...'\n[c/1c4dff:Rare]");
		}

		public override void SetDefaults()
		{
			base.item.damage = 7;
			base.item.melee = true;
			base.item.width = 26;
			base.item.height = 22;
			base.item.useTime = 10;
			base.item.useAnimation = 10;
			base.item.useStyle = 1;
			base.item.knockBack = 1f;
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.rare = 9;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = ModContent.ProjectileType<AkiClawPro>();
			base.item.shootSpeed = 15f;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 5;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (Main.rand.Next(5) == 0)
			{
				Main.PlaySound(SoundID.Item71, player.position);
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<AkiClawPro>(), damage * 2, 7f, player.whoAmI, 0f, 0f);
			}
			return false;
		}
	}
}
