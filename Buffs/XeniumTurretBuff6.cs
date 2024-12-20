using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class XeniumTurretBuff6 : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Xenium Autoturret");
			base.Description.SetDefault("\"Pewpewpewpewpewpew\"");
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("XeniumTurretMinion6")] > 0)
			{
				modPlayer.xeniumMinion6 = true;
			}
			if (!modPlayer.xeniumMinion6)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}
