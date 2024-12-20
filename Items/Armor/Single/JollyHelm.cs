using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Single
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class JollyHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sunset Helm");
			base.Tooltip.SetDefault("'Comes from an ashen world...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 30;
			base.item.value = 7500;
			base.item.rare = -1;
			base.item.defense = 4;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}
	}
}
