using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Wasteland
{
	public class PanaceaBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Panacea");
			base.Description.SetDefault("\"You feel great\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<RedePlayer>().irradiatedEffect = 0;
			player.GetModPlayer<RedePlayer>().irradiatedLevel = 0;
			player.GetModPlayer<RedePlayer>().irradiatedTimer = 0;
			player.ClearBuff(ModContent.BuffType<HeadacheDebuff>());
			player.ClearBuff(ModContent.BuffType<NauseaDebuff>());
			player.ClearBuff(ModContent.BuffType<FatigueDebuff>());
			player.ClearBuff(ModContent.BuffType<FeverDebuff>());
			player.ClearBuff(ModContent.BuffType<HairLossDebuff>());
			player.ClearBuff(ModContent.BuffType<SkinBurnDebuff>());
			player.ClearBuff(ModContent.BuffType<RadiationDebuff>());
		}
	}
}
