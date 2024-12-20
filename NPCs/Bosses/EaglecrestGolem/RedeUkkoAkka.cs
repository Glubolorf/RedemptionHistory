using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Redemption.NPCs.Bosses.EaglecrestGolem
{
	public class RedeUkkoAkka : ModWorld
	{
		public override void Initialize()
		{
			RedeUkkoAkka.begin = false;
			RedeUkkoAkka.TAbubbles = false;
			RedeUkkoAkka.TAearthProtection = false;
		}

		public override TagCompound Save()
		{
			bool beginB = false;
			bool TA = false;
			bool TA2 = false;
			if (RedeUkkoAkka.begin)
			{
				beginB = true;
			}
			if (RedeUkkoAkka.TAbubbles)
			{
				TA = true;
			}
			if (RedeUkkoAkka.TAearthProtection)
			{
				TA2 = true;
			}
			TagCompound tagCompound = new TagCompound();
			tagCompound.Add("beginT", beginB);
			tagCompound.Add("team1", TA);
			tagCompound.Add("team2", TA2);
			return tagCompound;
		}

		public override void Load(TagCompound tag)
		{
			RedeUkkoAkka.begin = tag.GetBool("beginT");
			RedeUkkoAkka.TAbubbles = tag.GetBool("team1");
			RedeUkkoAkka.TAearthProtection = tag.GetBool("team2");
		}

		public override void LoadLegacy(BinaryReader reader)
		{
			int loadVersion = reader.ReadInt32();
			if (loadVersion == 0)
			{
				BitsByte godFlags = reader.ReadByte();
				RedeUkkoAkka.begin = godFlags[0];
				RedeUkkoAkka.TAbubbles = godFlags[1];
				RedeUkkoAkka.TAearthProtection = godFlags[2];
				return;
			}
			base.mod.Logger.Debug("Redemption: Unknown loadVersion: " + loadVersion);
		}

		public override void NetSend(BinaryWriter writer)
		{
			BitsByte godFlags = default(BitsByte);
			godFlags[0] = RedeUkkoAkka.begin;
			godFlags[1] = RedeUkkoAkka.TAbubbles;
			godFlags[2] = RedeUkkoAkka.TAearthProtection;
			writer.Write(godFlags);
		}

		public override void NetReceive(BinaryReader reader)
		{
			BitsByte godFlags = reader.ReadByte();
			RedeUkkoAkka.begin = godFlags[0];
			RedeUkkoAkka.TAbubbles = godFlags[1];
			RedeUkkoAkka.TAearthProtection = godFlags[2];
		}

		public static bool begin;

		public static bool TAbubbles;

		public static bool TAearthProtection;
	}
}
