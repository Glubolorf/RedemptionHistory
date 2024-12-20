using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Magic
{
	public class BronzeStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bronze Wand");
			base.Tooltip.SetDefault("'A bronze wand with a blue orb'\nCasts two unstable zig-zagging blue orbs");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 23;
			base.item.magic = true;
			base.item.mana = 5;
			base.item.width = 38;
			base.item.height = 38;
			base.item.useTime = 26;
			base.item.useAnimation = 26;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 4f;
			base.item.value = 9500;
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item21;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<BlueOrb1>();
			base.item.shootSpeed = 10f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<BlueOrb2>(), damage, knockBack, player.whoAmI, 0f, 0f);
			return true;
		}
	}
}
