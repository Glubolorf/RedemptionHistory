using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Wasteland
{
	public class BileFlaskBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Weapon Imbue: Bile");
			base.Description.SetDefault("Melee attacks inflict Burning Acid");
			Main.persistentBuff[base.Type] = true;
			Main.meleeBuff[base.Type] = true;
			this.canBeCleared = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (player.dead || !player.active)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
