using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class EmpoweredBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Empowered");
			base.Description.SetDefault("The feeling of wielding such a mighty weapon empowers you...");
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.meleeDamage *= 1.15f;
			player.magicDamage *= 1.15f;
			player.rangedDamage *= 1.15f;
			player.thrownDamage *= 1.15f;
			player.lifeRegen -= 5;
			player.moveSpeed *= 0.6f;
		}
	}
}
