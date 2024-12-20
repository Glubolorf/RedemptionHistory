using System;
using Redemption.Projectiles.Druid.Stave.Guardians;
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
			player.GetModPlayer<RedePlayer>().staveQuadShot = true;
			player.statDefense += 26;
			player.thorns = 0.5f;
			player.endurance += 0.08f;
			player.manaRegen += 6;
			player.statManaMax2 += 50;
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<NatureGuardian20>()] > 0)
			{
				modPlayer2.natureGuardian20 = true;
			}
			if (!modPlayer2.natureGuardian20)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
