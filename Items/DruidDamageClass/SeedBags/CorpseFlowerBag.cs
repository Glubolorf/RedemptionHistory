using System;
using Terraria;
using Terraria.ID;

namespace Redemption.Items.DruidDamageClass.SeedBags
{
	public class CorpseFlowerBag : DruidSeedBag
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corpse Flower Seedbag");
			base.Tooltip.SetDefault("Throws a seed that grows into a stinky Corpse Flower");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 7;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 45;
			base.item.useAnimation = 45;
			base.item.useStyle = 1;
			base.item.mana = 8;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 0, 60, 0);
			base.item.rare = 2;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.shoot = base.mod.ProjectileType("Seed11");
			base.item.shootSpeed = 17f;
			this.nativeText = "Blood Moon";
		}
	}
}
