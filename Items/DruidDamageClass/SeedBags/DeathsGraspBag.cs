using System;
using Terraria;
using Terraria.ID;

namespace Redemption.Items.DruidDamageClass.SeedBags
{
	public class DeathsGraspBag : DruidSeedBag
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Death's Grasp Seedbag");
			base.Tooltip.SetDefault("Throws a seed that grows into a spooky hand");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 11;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 42;
			base.item.useAnimation = 42;
			base.item.useStyle = 1;
			base.item.mana = 5;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 0, 30, 0);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.shoot = base.mod.ProjectileType("Seed26");
			base.item.shootSpeed = 18f;
		}
	}
}
