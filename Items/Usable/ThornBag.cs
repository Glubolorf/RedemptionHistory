using System;
using Redemption.Items.Accessories.PreHM;
using Redemption.Items.Armor.Vanity;
using Redemption.Items.Weapons.PreHM.Druid.Seedbags;
using Redemption.Items.Weapons.PreHM.Melee;
using Redemption.Items.Weapons.PreHM.Ranged;
using Redemption.NPCs.Bosses.Thorn;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Usable
{
	public class ThornBag : ModItem
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
				return ModContent.NPCType<Thorn>();
			}
		}

		public override void OpenBossBag(Player player)
		{
			if (Main.rand.Next(7) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<ThornMask>(), 1);
			}
			int num = Main.rand.Next(4);
			if (num == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<CursedGrassSword>(), 1);
			}
			if (num == 1)
			{
				player.QuickSpawnItem(ModContent.ItemType<CursedThornBow>(), 1);
			}
			if (num == 2)
			{
				player.QuickSpawnItem(ModContent.ItemType<RootTendril>(), 1);
			}
			if (num == 3)
			{
				player.QuickSpawnItem(ModContent.ItemType<ThornSeedBag>(), 1);
			}
			player.QuickSpawnItem(ModContent.ItemType<CircletOfBrambles>(), 1);
		}
	}
}
