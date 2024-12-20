using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class Violin : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Violin");
			base.Tooltip.SetDefault("'Music will destroy all...'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 60;
			base.item.magic = true;
			base.item.mana = 6;
			base.item.width = 38;
			base.item.height = 40;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.useStyle = 5;
			base.item.knockBack = 6f;
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.rare = 7;
			base.item.UseSound = base.mod.GetLegacySoundSlot(2, "Sounds/Item/TheViolinSound");
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("ViolinProj1");
			base.item.shootSpeed = 14f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, base.mod.ProjectileType("TheTrueViolin"), damage, knockBack, player.whoAmI, 0f, 0f);
			return true;
		}
	}
}
