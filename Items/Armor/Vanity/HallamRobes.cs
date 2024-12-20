using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Vanity
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	internal class HallamRobes : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hallam's Fantasy Robe");
			base.Tooltip.SetDefault("'Great for impersonating devs!'");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 28;
			base.item.rare = 9;
			base.item.expert = true;
			base.item.vanity = true;
		}

		public override void SetMatch(bool male, ref int equipSlot, ref bool robes)
		{
			robes = true;
			equipSlot = base.mod.GetEquipSlot("HallamRobes_Legs", 2);
		}

		public override void DrawHands(ref bool drawHands, ref bool drawArms)
		{
			drawHands = true;
		}
	}
}
