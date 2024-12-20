using System;
using Redemption.Items.Accessories.PostML;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.PreHM
{
	public class CircletOfBrambles : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Circlet of Brambles");
			base.Tooltip.SetDefault("Every 5th use of a weapon shoots a spread of stingers\nIncreased life regeneration while in the Jungle");
		}

		public override void SetDefaults()
		{
			base.item.width = 36;
			base.item.height = 28;
			base.item.value = Item.sellPrice(0, 0, 75, 0);
			base.item.expert = true;
			base.item.accessory = true;
		}

		public override bool CanEquipAccessory(Player player, int slot)
		{
			if (slot < 10)
			{
				int maxAccessoryIndex = 5 + player.extraAccessorySlots;
				for (int i = 3; i < 3 + maxAccessoryIndex; i++)
				{
					if (slot != i && player.armor[i].type == ModContent.ItemType<CrownOfThorns>())
					{
						return false;
					}
				}
			}
			return true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).thornCirclet = true;
			if (player.ZoneJungle)
			{
				player.lifeRegen += 2;
			}
		}
	}
}
