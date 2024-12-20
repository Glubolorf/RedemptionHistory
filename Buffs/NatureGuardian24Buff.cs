using System;
using Redemption.Projectiles.Druid.Stave.Guardians;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian24Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Spore Pixie");
			base.Description.SetDefault("\"A Spore Pixie is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player);
			player.GetModPlayer<RedePlayer>().staveSpeed += 0.35f;
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<NatureGuardian24>()] > 0)
			{
				modPlayer2.natureGuardian24 = true;
			}
			if (!modPlayer2.natureGuardian24)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
