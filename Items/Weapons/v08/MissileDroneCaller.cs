using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Minions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class MissileDroneCaller : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Missile Drone Remote");
			base.Tooltip.SetDefault("Summons one of Slayer's missile drones from the sky\nFires 4 homing missiles at the closest enemies then returns back to the ship\nDoesn't use minion slots\nUse sparingly");
		}

		public override void SetDefaults()
		{
			base.item.damage = 500;
			base.item.summon = true;
			base.item.width = 30;
			base.item.height = 26;
			base.item.useTime = 60;
			base.item.useAnimation = 60;
			base.item.useStyle = 1;
			base.item.maxStack = 20;
			base.item.noMelee = true;
			base.item.rare = 9;
			base.item.noUseGraphic = true;
			base.item.knockBack = 9f;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.UseSound = SoundID.Item44;
			base.item.shoot = ModContent.ProjectileType<SlayerMissileDroneMinion>();
			base.item.shootSpeed = 0f;
			base.item.consumable = true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y - 800f, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, 0f);
			return false;
		}
	}
}
