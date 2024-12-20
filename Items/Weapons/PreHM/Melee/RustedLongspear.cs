using System;
using Redemption.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Melee
{
	public class RustedLongspear : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Rusted Longspear");
			base.Tooltip.SetDefault("'A weapon wielded by Skeleton Wanderers. Used for thrusting attacks.'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 8;
			base.item.useStyle = 5;
			base.item.useAnimation = 18;
			base.item.useTime = 24;
			base.item.shootSpeed = 3.7f;
			base.item.knockBack = 5.5f;
			base.item.width = 48;
			base.item.height = 48;
			base.item.scale = 1f;
			base.item.rare = -1;
			base.item.UseSound = SoundID.Item1;
			base.item.shoot = ModContent.ProjectileType<RustedLongspearPro>();
			base.item.value = 350;
			base.item.noMelee = true;
			base.item.noUseGraphic = true;
			base.item.melee = true;
			base.item.autoReuse = false;
		}

		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[base.item.shoot] < 1;
		}
	}
}
