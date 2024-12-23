﻿using System;
using Redemption.ChickenArmy;
using SubworldLibrary;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Usable.Summons
{
	public class ChickenContract : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Strange Contract");
			base.Tooltip.SetDefault("It reads - [c/ff4c4c:'You fool...']\nSummons King Chicken's Royal Army\nOnly usable at day");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 22;
			base.item.maxStack = 30;
			base.item.value = 0;
			base.item.rare = 1;
			base.item.useStyle = 4;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.reuseDelay = 10;
			base.item.noMelee = true;
			base.item.consumable = true;
			base.item.autoReuse = false;
			base.item.UseSound = SoundID.Item43;
		}

		public override bool CanUseItem(Player player)
		{
			return !player.ZoneTowerSolar && !player.ZoneTowerVortex && !player.ZoneTowerNebula && !player.ZoneTowerStardust && !Main.pumpkinMoon && !Main.snowMoon && !ChickWorld.chickArmy && Main.dayTime && !SLWorld.subworld;
		}

		public override bool UseItem(Player player)
		{
			if (ChickWorld.chickArmy)
			{
				return false;
			}
			if (Main.netMode == 1 && player.whoAmI == Main.myPlayer)
			{
				Redemption.WriteToPacket(Redemption.Inst.GetPacket(256), 4, new object[0]).Send(-1, -1);
			}
			else
			{
				ChickWorld.chickArmy = true;
			}
			return true;
		}
	}
}
