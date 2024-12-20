using System;
using Terraria;
using Terraria.ID;

namespace Redemption.Items.DruidDamageClass.SeedBags
{
	public class ThornSeedBag : DruidSeedBag
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Thorn Seedbag");
			base.Tooltip.SetDefault("Throws a seed that grows into thorn-spitting brambles");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 13;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 40;
			base.item.useAnimation = 40;
			base.item.useStyle = 1;
			base.item.mana = 5;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 0, 30, 0);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.shoot = base.mod.ProjectileType("Seed33");
			base.item.shootSpeed = 18f;
		}
	}
}
