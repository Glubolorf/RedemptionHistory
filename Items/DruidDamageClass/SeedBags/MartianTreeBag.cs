using System;
using Redemption.Projectiles.DruidProjectiles.Plants;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.SeedBags
{
	public class MartianTreeBag : DruidSeedBag
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Martian Tree Seedbag");
			base.Tooltip.SetDefault("Throws a seed that grows into an otherworldly Martian Tree");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 103;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 50;
			base.item.useAnimation = 50;
			base.item.useStyle = 1;
			base.item.mana = 10;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 5, 0, 0);
			base.item.rare = 8;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<Seed20>();
			base.item.shootSpeed = 18f;
		}
	}
}
