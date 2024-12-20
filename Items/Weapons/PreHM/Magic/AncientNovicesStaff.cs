using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Magic
{
	public class AncientNovicesStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Novice's Staff");
			base.Tooltip.SetDefault("'A simple wooden staff with a white crystal at the top'\nCasts the Ember spell");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 9;
			base.item.magic = true;
			base.item.mana = 3;
			base.item.width = 36;
			base.item.height = 36;
			base.item.useTime = 28;
			base.item.useAnimation = 28;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 3f;
			base.item.value = 2500;
			base.item.rare = 0;
			base.item.UseSound = SoundID.Item20;
			base.item.autoReuse = true;
			base.item.shoot = 258;
			base.item.shootSpeed = 16f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int projectile = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, 0f);
			Main.projectile[projectile].magic = true;
			Main.projectile[projectile].hostile = false;
			Main.projectile[projectile].friendly = true;
			Main.projectile[projectile].penetrate = 1;
			Main.projectile[projectile].timeLeft = 120;
			return false;
		}
	}
}
