using System;
using Microsoft.Xna.Framework;
using Redemption.NPCs.Bosses.Warden;
using Terraria;
using Terraria.ModLoader;

namespace Redemption
{
	public class ArenaWorld : ModWorld
	{
		public override void PostUpdate()
		{
			if (!ArenaWorld.arenaActive)
			{
				return;
			}
			string a = ArenaWorld.arenaBoss;
			if (!(a == "Warden"))
			{
				this.DeactivateArena();
				return;
			}
			if (NPC.AnyNPCs(ModContent.NPCType<WardenIdle>()))
			{
				ArenaWorld.arenaTopLeft = new Vector2(16064f, 18672f);
				ArenaWorld.arenaSize = new Vector2(3104f, 2592f);
				return;
			}
			this.DeactivateArena();
		}

		private void DeactivateArena()
		{
			ArenaWorld.arenaActive = false;
			ArenaWorld.arenaBoss = "";
			ArenaWorld.arenaTopLeft = new Vector2(-1f, -1f);
			ArenaWorld.arenaSize = new Vector2(0f, 0f);
		}

		public static bool arenaActive;

		public static Vector2 arenaTopLeft = new Vector2(-1f, -1f);

		public static Vector2 arenaSize = Vector2.Zero;

		public static Rectangle arenaRect = new Rectangle((int)ArenaWorld.arenaTopLeft.X, (int)ArenaWorld.arenaTopLeft.Y, (int)ArenaWorld.arenaSize.X, (int)ArenaWorld.arenaSize.Y);

		public static string arenaBoss;
	}
}
