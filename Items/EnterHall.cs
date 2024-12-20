using System;
using SubworldLibrary;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class EnterHall : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("...");
			base.Tooltip.SetDefault("Use at your own risk.");
		}

		public override void SetDefaults()
		{
			base.item.width = 10;
			base.item.height = 10;
			base.item.maxStack = 1;
			base.item.noUseGraphic = true;
			base.item.rare = 3;
			base.item.useAnimation = 45;
			base.item.useTime = 45;
			base.item.useStyle = 4;
			base.item.consumable = false;
		}

		public override bool UseItem(Player player)
		{
			if (Redemption.emptyHallActive)
			{
				SLWorld.ExitSubworld();
			}
			else
			{
				SLWorld.EnterSubworld("Redemption_EmptyHallsSub");
			}
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (!player.HasBuff(base.mod.BuffType("HKStatueBuff")))
			{
				player.AddBuff(base.mod.BuffType("HKStatueBuff"), 1800, true);
				return true;
			}
			return false;
		}
	}
}
