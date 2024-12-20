using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class DruidHat : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Druid's Hat");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n15% increased druidic damage");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.value = Item.sellPrice(0, 0, 10, 0);
			base.item.height = 24;
			base.item.rare = 2;
			base.item.defense = 2;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer.ModPlayer(player).druidDamage += 0.15f;
		}

		public override bool DrawHead()
		{
			return true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = true);
		}
	}
}
