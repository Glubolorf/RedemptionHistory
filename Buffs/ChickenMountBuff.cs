using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	internal class ChickenMountBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Chicken Cavalry");
			base.Description.SetDefault("'The cavalry's here!'");
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.mount.SetMount(base.mod.MountType("ChickenMount"), player, false);
			player.buffTime[buffIndex] = 10;
		}
	}
}
