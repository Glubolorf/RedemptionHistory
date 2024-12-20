using System;
using Redemption.Items.Armor.Vanity;
using Redemption.Items.Materials.PostML;
using Redemption.Items.Weapons.PostML.Druid.Staves;
using Redemption.Items.Weapons.PostML.Melee;
using Redemption.NPCs.Bosses.Warden;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Usable
{
	public class TheWardenBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Treasure Bag");
			base.Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			base.item.maxStack = 999;
			base.item.consumable = true;
			base.item.width = 24;
			base.item.height = 24;
			base.item.rare = 9;
			base.item.expert = true;
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override int BossBagNPC
		{
			get
			{
				return ModContent.NPCType<WardenIdle>();
			}
		}

		public override void OpenBossBag(Player player)
		{
			if (Main.rand.Next(7) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<WardenMask>(), 1);
			}
			int num = Main.rand.Next(2);
			if (num != 0)
			{
				if (num == 1)
				{
					player.QuickSpawnItem(ModContent.ItemType<ShadeCandleSpear>(), 1);
				}
			}
			else
			{
				player.QuickSpawnItem(ModContent.ItemType<ShadeStave>(), 1);
			}
			player.QuickSpawnItem(ModContent.ItemType<VesselFrag>(), Main.rand.Next(13, 24));
			player.QuickSpawnItem(ModContent.ItemType<WardensKey>(), 1);
		}
	}
}
