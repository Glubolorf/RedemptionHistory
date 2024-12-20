using System;
using Terraria;
using Terraria.ID;

namespace Redemption.Items.DruidDamageClass
{
	public class PlanterasStave1 : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Plantera's Stave");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nShoots pink petals.");
			Item.staff[base.item.type] = true;
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 74;
			base.item.mana = 5;
			base.item.width = 64;
			base.item.height = 64;
			base.item.useTime = 11;
			base.item.useAnimation = 16;
			base.item.useStyle = 5;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 4, 50, 0);
			base.item.rare = 7;
			base.item.UseSound = SoundID.Item17;
			base.item.autoReuse = true;
			base.item.noMelee = true;
			base.item.shoot = base.mod.ProjectileType("MiniPlanteraSeed1");
			base.item.shootSpeed = 15f;
		}

		public override bool CanUseItem(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).fasterStaves)
			{
				base.item.useTime = 7;
				base.item.useAnimation = 12;
			}
			else
			{
				base.item.useTime = 11;
				base.item.useAnimation = 16;
			}
			return true;
		}
	}
}
