using System;
using Redemption.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class CorpseSkullBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Corpse-Walker Skull");
			base.Description.SetDefault("\"A dried skull to fight for you!\"");
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<CorpseWalkerSkull>()] > 0)
			{
				modPlayer.corpseskullMinion = true;
			}
			if (!modPlayer.corpseskullMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}
