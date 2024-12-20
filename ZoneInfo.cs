using System;
using Terraria;

namespace Redemption
{
	public interface ZoneInfo
	{
		bool InZone(Player p, string zoneName);
	}
}
