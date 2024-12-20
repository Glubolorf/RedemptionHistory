using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian26Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Boreal Statuette");
			base.Description.SetDefault("\"A Boreal Statuette is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player);
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			modPlayer.rapidStave = true;
			modPlayer.iceShield = true;
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("NatureGuardian26")] > 0)
			{
				modPlayer2.natureGuardian24 = true;
			}
			if (!modPlayer2.natureGuardian24 || !modPlayer.iceShield)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
