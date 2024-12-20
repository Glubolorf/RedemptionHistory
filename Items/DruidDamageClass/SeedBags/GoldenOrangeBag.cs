using System;
using Redemption.Projectiles.DruidProjectiles.Plants;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.SeedBags
{
	public class GoldenOrangeBag : DruidSeedBag
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Golden Orange Tree Seedbag");
			base.Tooltip.SetDefault("Throws a seed that grows into a Golden Orange Tree\nTouching the oranges will consume them, giving the 'Well Fed' buff for a short time");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 57;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 59;
			base.item.useAnimation = 59;
			base.item.useStyle = 1;
			base.item.mana = 10;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.sellPrice(0, 4, 50, 50);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<Seed28>();
			base.item.shootSpeed = 18f;
		}
	}
}
