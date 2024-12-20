using System;
using Redemption.Items.Accessories.HM;
using Redemption.Items.Materials.HM;
using Redemption.Items.Usable;
using Redemption.Items.Weapons.HM.Druid.Staves;
using Redemption.Items.Weapons.PostML.Druid.Staves;
using Redemption.Items.Weapons.PreHM.Druid.Seedbags;
using Terraria;
using Terraria.ModLoader;

namespace Redemption
{
	public class BossBags : GlobalItem
	{
		public override void OpenVanillaBag(string context, Player player, int arg)
		{
			if (context == "bossBag" && arg == 3319 && Main.rand.Next(3) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<EyeStalkBag>(), 1);
			}
			if (context == "bossBag" && arg == 3329 && Main.rand.Next(8) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<LihzWorldStave>(), 1);
			}
			if (context == "bossBag" && arg == 3329 && Main.rand.Next(8) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<LihzWorldStave>(), 1);
			}
			if (context == "bossBag" && arg == 3328)
			{
				player.QuickSpawnItem(ModContent.ItemType<SoulOfBloom>(), Main.rand.Next(120, 180));
				if (Main.rand.Next(7) == 0)
				{
					player.QuickSpawnItem(ModContent.ItemType<PlanterasStave>(), 1);
				}
			}
			if (context == "bossBag" && arg == 3324)
			{
				if (Main.rand.Next(8) == 0)
				{
					player.QuickSpawnItem(ModContent.ItemType<DruidEmblem>(), 1);
				}
				if (Main.rand.Next(6) == 0)
				{
					player.QuickSpawnItem(ModContent.ItemType<WallsClaw>(), 1);
				}
			}
			if (context == "bossBag" && arg == 3332)
			{
				if (Main.rand.Next(7) == 0)
				{
					player.QuickSpawnItem(ModContent.ItemType<MoonlordStave>(), 1);
				}
				player.QuickSpawnItem(ModContent.ItemType<Keycard>(), 1);
			}
		}
	}
}
