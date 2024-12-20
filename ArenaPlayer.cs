using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using Redemption.WorldGeneration.Soulless;
using SubworldLibrary;
using Terraria.ModLoader;

namespace Redemption
{
	public class ArenaPlayer : ModPlayer
	{
		public override void PostUpdate()
		{
			if (Subworld.IsActive<SoullessSub>() && base.player.Hitbox.Intersects(ArenaPlayer.wardenArena) && !RedeWorld.downedTheWarden && base.player.active && !base.player.dead)
			{
				base.player.AddBuff(ModContent.BuffType<WardenHex>(), 10, true);
			}
			if (ArenaWorld.arenaActive && ArenaWorld.arenaTopLeft != new Vector2(-1f, -1f))
			{
				if (base.player.Center.X > ArenaWorld.arenaTopLeft.X + ArenaWorld.arenaSize.X)
				{
					Vector2 newPos = base.player.Center;
					newPos.X = ArenaWorld.arenaTopLeft.X + ArenaWorld.arenaSize.X;
					base.player.Center = newPos;
					base.player.velocity.X = 0f;
				}
				if (base.player.Center.X < ArenaWorld.arenaTopLeft.X)
				{
					Vector2 newPos2 = base.player.Center;
					newPos2.X = ArenaWorld.arenaTopLeft.X;
					base.player.Center = newPos2;
					base.player.velocity.X = 0f;
				}
				if (base.player.Center.Y > ArenaWorld.arenaTopLeft.Y + ArenaWorld.arenaSize.Y)
				{
					Vector2 newPos3 = base.player.Center;
					newPos3.Y = ArenaWorld.arenaTopLeft.Y + ArenaWorld.arenaSize.Y;
					base.player.Center = newPos3;
					base.player.velocity.Y = 0f;
				}
				if (base.player.Center.Y < ArenaWorld.arenaTopLeft.Y)
				{
					Vector2 newPos4 = base.player.Center;
					newPos4.Y = ArenaWorld.arenaTopLeft.Y;
					base.player.Center = newPos4;
					base.player.velocity.Y = 0f;
				}
			}
		}

		public static Rectangle wardenArena = new Rectangle(16064, 18672, 3104, 2592);
	}
}
