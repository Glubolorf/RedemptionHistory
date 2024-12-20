using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.PreHM
{
	[AutoloadEquip(new EquipType[]
	{
		11
	})]
	public class IronfurAmulet : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ironfur Amulet");
			base.Tooltip.SetDefault("Makes the user immune to most frost debuffs");
		}

		public override void SetDefaults()
		{
			base.item.width = 18;
			base.item.height = 20;
			base.item.value = Item.buyPrice(0, 5, 0, 0);
			base.item.rare = 2;
			base.item.accessory = true;
			base.item.defense = 2;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.buffImmune[46] = true;
			player.buffImmune[44] = true;
			player.buffImmune[47] = true;
		}
	}
}
