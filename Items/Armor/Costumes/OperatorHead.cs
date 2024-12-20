using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Costumes
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class OperatorHead : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Operator Head");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 22;
			base.item.rare = 1;
			base.item.value = Item.buyPrice(0, 3, 0, 0);
			base.item.vanity = true;
		}

		public override bool DrawHead()
		{
			return false;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}
	}
}
