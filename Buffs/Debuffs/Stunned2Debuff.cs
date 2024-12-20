using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Debuffs
{
	public class Stunned2Debuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Snared!");
			base.Description.SetDefault("\"You are stunned!\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.debuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.wingTimeMax = 0;
			player.wingTime = 0f;
			player.wings = 0;
			player.wingsLogic = 0;
			player.noFallDmg = true;
			player.noBuilding = true;
			player.controlJump = false;
			player.controlDown = false;
			player.controlLeft = false;
			player.controlRight = false;
			player.controlUp = false;
			player.controlUseItem = false;
			player.controlUseTile = false;
			player.controlThrow = false;
			player.gravDir = 1f;
			player.velocity *= 0f;
			player.position = player.oldPosition;
			player.sandStorm = false;
			player.dJumpEffectCloud = false;
			player.dJumpEffectSandstorm = false;
			player.dJumpEffectBlizzard = false;
			player.dJumpEffectFart = false;
			player.dJumpEffectSail = false;
			player.dJumpEffectUnicorn = false;
			if (player.mount.Active)
			{
				player.mount.Dismount(player);
			}
		}
	}
}
