using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class Omega : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Omega Android");
			base.Description.SetDefault("You are an Omega Android!");
			Main.debuff[base.Type] = true;
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
			this.canBeCleared = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer p = player.GetModPlayer<RedePlayer>();
			if (p.omegaAccessoryPrevious)
			{
				p.omegaPower = true;
				return;
			}
			player.DelBuff(buffIndex);
			buffIndex--;
		}
	}
}
