using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian20Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Sapphiron Spirit");
			base.Description.SetDefault("\"A Sapphiron Spirit is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player);
			player.GetModPlayer<RedePlayer>();
			player.statDefense += 8;
			player.thorns = 0.5f;
			player.endurance += 0.06f;
			player.manaRegen += 5;
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("NatureGuardian20")] > 0)
			{
				modPlayer.natureGuardian20 = true;
			}
			if (!modPlayer.natureGuardian20)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
