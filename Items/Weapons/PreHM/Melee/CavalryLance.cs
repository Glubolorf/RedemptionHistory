using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Melee
{
	public class CavalryLance : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cavalry Lance");
			base.Tooltip.SetDefault("Increased damage if the user is riding a mount");
		}

		public override void SetDefaults()
		{
			base.item.damage = 8;
			base.item.useStyle = 5;
			base.item.useAnimation = 19;
			base.item.useTime = 19;
			base.item.shootSpeed = 3.6f;
			base.item.knockBack = 7f;
			base.item.width = 54;
			base.item.height = 54;
			base.item.scale = 1f;
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.shoot = ModContent.ProjectileType<CavalryLancePro>();
			base.item.value = Item.buyPrice(0, 0, 35, 0);
			base.item.noMelee = true;
			base.item.noUseGraphic = true;
			base.item.melee = true;
			base.item.autoReuse = false;
		}

		public override bool UseItem(Player player)
		{
			Vector2 MyVelocity = Utils.SafeNormalize(Main.MouseWorld - player.Center, -Vector2.UnitY) * 3f;
			player.velocity += MyVelocity;
			return true;
		}

		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			if (player.mount.Type != -1)
			{
				flat = 8f;
			}
		}

		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[base.item.shoot] < 1;
		}
	}
}
