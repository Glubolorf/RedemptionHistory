using System;
using Redemption.Projectiles.v08;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class FlintAndSteel : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Flint and Steel");
			base.Tooltip.SetDefault("'Doesn't work on obsidian'\nReleases a tiny spark which lights enemies on fire");
		}

		public override void SetDefaults()
		{
			base.item.damage = 1;
			base.item.melee = true;
			base.item.width = 30;
			base.item.height = 26;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.useStyle = 1;
			base.item.noMelee = true;
			base.item.knockBack = 0f;
			base.item.value = 2800;
			base.item.rare = 0;
			base.item.UseSound = SoundID.Item17;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<FireSpark1>();
			base.item.shootSpeed = 6f;
		}
	}
}
