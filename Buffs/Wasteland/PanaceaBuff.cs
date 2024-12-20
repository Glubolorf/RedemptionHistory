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
			player.ClearBuff(base.mod.BuffType("HeadacheDebuff"));
			player.ClearBuff(base.mod.BuffType("NauseaDebuff"));
			player.ClearBuff(base.mod.BuffType("FatigueDebuff"));
			player.ClearBuff(base.mod.BuffType("FeverDebuff"));
			player.ClearBuff(base.mod.BuffType("HairLossDebuff"));
			player.ClearBuff(base.mod.BuffType("SkinBurnDebuff"));
			player.ClearBuff(base.mod.BuffType("RadiationDebuff"));
		}
	}
}
