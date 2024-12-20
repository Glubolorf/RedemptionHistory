using System;
using Redemption.Items.Armor.Vanity;
using Redemption.Items.Materials.PostML;
using Redemption.Items.Weapons.PostML.Druid.Staves;
using Redemption.Items.Weapons.PostML.Magic;
using Redemption.Items.Weapons.PostML.Summon;
using Redemption.NPCs.Bosses.EaglecrestGolem;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Usable
{
	public class UkkoBag : ModItem
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
				return ModContent.NPCType<Ukko>();
			}
		}

		public override void OpenBossBag(Player player)
		{
			if (Main.rand.Next(7) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<UkkoMask>(), 1);
			}
			int num = Main.rand.Next(3);
			if (num == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<StonePuppet>(), 1);
			}
			if (num == 1)
			{
				player.QuickSpawnItem(ModContent.ItemType<EaglecrestGlove>(), 1);
			}
			if (num == 2)
			{
				player.QuickSpawnItem(ModContent.ItemType<AncientPowerStave>(), 1);
			}
			player.QuickSpawnItem(ModContent.ItemType<ViisaanKantele>(), 1);
			player.QuickSpawnItem(ModContent.ItemType<UkonRuno>(), 1);
			player.QuickSpawnItem(ModContent.ItemType<AncientPowerCore>(), Main.rand.Next(9, 18));
		}
	}
}
