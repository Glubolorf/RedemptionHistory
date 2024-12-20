using System;
using Terraria;
using Terraria.ID;

namespace Redemption.Items.DruidDamageClass
{
	public class Petridish : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mitosis");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nThrow a Petridish filled with bacteria");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 125;
			base.item.width = 24;
			base.item.height = 20;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.useStyle = 1;
			base.item.mana = 18;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.rare = 11;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.noUseGraphic = true;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("PetridishPro");
			base.item.shootSpeed = 14f;
		}
	}
}
