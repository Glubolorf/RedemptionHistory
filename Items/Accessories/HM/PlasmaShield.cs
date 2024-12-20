using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.HM
{
	[AutoloadEquip(new EquipType[]
	{
		10
	})]
	public class PlasmaShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Holoshield");
			base.Tooltip.SetDefault("6% damage reduction\nDouble tap a direction to dash\nDashing into projectiles will reflect them\nCan't reflect projectiles exceeding 100 damage");
		}

		public override void SetDefaults()
		{
			base.item.damage = 20;
			base.item.knockBack = 8f;
			base.item.melee = true;
			base.item.width = 22;
			base.item.height = 26;
			base.item.value = Item.sellPrice(0, 5, 0, 0);
			base.item.rare = 7;
			base.item.accessory = true;
			base.item.defense = 2;
		}

		public override bool CanEquipAccessory(Player player, int slot)
		{
			if (slot < 10)
			{
				int maxAccessoryIndex = 5 + player.extraAccessorySlots;
				for (int i = 3; i < 3 + maxAccessoryIndex; i++)
				{
					if (slot != i && player.armor[i].type == ModContent.ItemType<InfectionShield>())
					{
						return false;
					}
				}
			}
			return true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<DashPlayer>().plasmaShield = true;
			player.endurance += 0.06f;
		}
	}
}
