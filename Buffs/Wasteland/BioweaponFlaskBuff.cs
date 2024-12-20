using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Wasteland
{
	public class BioweaponFlaskBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Weapon Imbue: Corrosion");
			base.Description.SetDefault("Melee attacks inflict Cellular Destruction");
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
