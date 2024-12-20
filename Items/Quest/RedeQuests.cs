using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Redemption.Items.Quest
{
	public class RedeQuests : ModWorld
	{
		public override void Initialize()
		{
			RedeQuests.zephosQuests = 0;
			RedeQuests.daerelQuests = 0;
			RedeQuests.ZchickenQuest = false;
			RedeQuests.ZskullQuest = false;
			RedeQuests.ZdishQuest = false;
			RedeQuests.ZvepdorQuest = false;
			RedeQuests.ZswordQuest = false;
			RedeQuests.ZsoulQuest = false;
			RedeQuests.ZsheathQuest = false;
			RedeQuests.ZzweiQuest = false;
			RedeQuests.ZpotionQuest = false;
			RedeQuests.DnecklaceQuest = false;
			RedeQuests.DbackpackQuest = false;
			RedeQuests.DkitQuest = false;
			RedeQuests.DpotionQuest = false;
			RedeQuests.DsoulQuest = false;
			RedeQuests.DcloakQuest = false;
			RedeQuests.DparthQuest = false;
			RedeQuests.DmapQuest = false;
			RedeQuests.DslimeQuest = false;
		}

		public override TagCompound Save()
		{
			List<string> quests = new List<string>();
			if (RedeQuests.ZchickenQuest)
			{
				quests.Add("zep1");
			}
			if (RedeQuests.ZskullQuest)
			{
				quests.Add("zep2");
			}
			if (RedeQuests.ZdishQuest)
			{
				quests.Add("zep3");
			}
			if (RedeQuests.ZvepdorQuest)
			{
				quests.Add("zep4");
			}
			if (RedeQuests.ZswordQuest)
			{
				quests.Add("zep5");
			}
			if (RedeQuests.ZsoulQuest)
			{
				quests.Add("zep6");
			}
			if (RedeQuests.ZsheathQuest)
			{
				quests.Add("zep7");
			}
			if (RedeQuests.ZzweiQuest)
			{
				quests.Add("zep8");
			}
			if (RedeQuests.ZpotionQuest)
			{
				quests.Add("zep9");
			}
			if (RedeQuests.DnecklaceQuest)
			{
				quests.Add("dae1");
			}
			if (RedeQuests.DbackpackQuest)
			{
				quests.Add("dae2");
			}
			if (RedeQuests.DkitQuest)
			{
				quests.Add("dae3");
			}
			if (RedeQuests.DpotionQuest)
			{
				quests.Add("dae4");
			}
			if (RedeQuests.DsoulQuest)
			{
				quests.Add("dae6");
			}
			if (RedeQuests.DcloakQuest)
			{
				quests.Add("dae7");
			}
			if (RedeQuests.DparthQuest)
			{
				quests.Add("dae8");
			}
			if (RedeQuests.DmapQuest)
			{
				quests.Add("dae9");
			}
			if (RedeQuests.DslimeQuest)
			{
				quests.Add("dae10");
			}
			TagCompound tagCompound = new TagCompound();
			tagCompound.Add("quests", quests);
			tagCompound.Add("zepQuests", RedeQuests.zephosQuests);
			tagCompound.Add("daeQuests", RedeQuests.daerelQuests);
			return tagCompound;
		}

		public override void Load(TagCompound tag)
		{
			IList<string> list = tag.GetList<string>("quests");
			RedeQuests.zephosQuests = tag.GetInt("zepQuests");
			RedeQuests.daerelQuests = tag.GetInt("daeQuests");
			RedeQuests.ZchickenQuest = list.Contains("zep1");
			RedeQuests.ZskullQuest = list.Contains("zep2");
			RedeQuests.ZdishQuest = list.Contains("zep3");
			RedeQuests.ZvepdorQuest = list.Contains("zep4");
			RedeQuests.ZswordQuest = list.Contains("zep5");
			RedeQuests.ZsoulQuest = list.Contains("zep6");
			RedeQuests.ZsheathQuest = list.Contains("zep7");
			RedeQuests.ZzweiQuest = list.Contains("zep8");
			RedeQuests.ZpotionQuest = list.Contains("zep9");
			RedeQuests.DnecklaceQuest = list.Contains("dae1");
			RedeQuests.DbackpackQuest = list.Contains("dae2");
			RedeQuests.DkitQuest = list.Contains("dae3");
			RedeQuests.DpotionQuest = list.Contains("dae4");
			RedeQuests.DsoulQuest = list.Contains("dae6");
			RedeQuests.DcloakQuest = list.Contains("dae7");
			RedeQuests.DparthQuest = list.Contains("dae8");
			RedeQuests.DmapQuest = list.Contains("dae9");
			RedeQuests.DslimeQuest = list.Contains("dae10");
		}

		public override void NetSend(BinaryWriter writer)
		{
			BitsByte questFlags = default(BitsByte);
			questFlags[0] = RedeQuests.ZchickenQuest;
			questFlags[1] = RedeQuests.ZskullQuest;
			questFlags[2] = RedeQuests.ZdishQuest;
			questFlags[3] = RedeQuests.ZvepdorQuest;
			questFlags[4] = RedeQuests.ZswordQuest;
			questFlags[5] = RedeQuests.DnecklaceQuest;
			questFlags[6] = RedeQuests.DbackpackQuest;
			questFlags[7] = RedeQuests.DkitQuest;
			writer.Write(questFlags);
			BitsByte questFlags2 = default(BitsByte);
			questFlags2[0] = RedeQuests.DpotionQuest;
			questFlags2[1] = RedeQuests.ZsoulQuest;
			questFlags2[2] = RedeQuests.DsoulQuest;
			questFlags2[3] = RedeQuests.ZzweiQuest;
			questFlags2[4] = RedeQuests.DcloakQuest;
			questFlags2[5] = RedeQuests.DparthQuest;
			questFlags2[6] = RedeQuests.DmapQuest;
			questFlags2[7] = RedeQuests.DslimeQuest;
			writer.Write(questFlags2);
			BitsByte questFlags3 = default(BitsByte);
			questFlags3[0] = RedeQuests.ZsheathQuest;
			questFlags3[1] = RedeQuests.ZpotionQuest;
			writer.Write(questFlags3);
			writer.Write(RedeQuests.zephosQuests);
			writer.Write(RedeQuests.daerelQuests);
		}

		public override void NetReceive(BinaryReader reader)
		{
			BitsByte questFlags = reader.ReadByte();
			RedeQuests.ZchickenQuest = questFlags[0];
			RedeQuests.ZskullQuest = questFlags[1];
			RedeQuests.ZdishQuest = questFlags[2];
			RedeQuests.ZvepdorQuest = questFlags[3];
			RedeQuests.ZswordQuest = questFlags[4];
			RedeQuests.DnecklaceQuest = questFlags[5];
			RedeQuests.DbackpackQuest = questFlags[6];
			RedeQuests.DkitQuest = questFlags[7];
			BitsByte questFlags2 = reader.ReadByte();
			RedeQuests.DpotionQuest = questFlags2[0];
			RedeQuests.ZsoulQuest = questFlags2[1];
			RedeQuests.DsoulQuest = questFlags2[2];
			RedeQuests.ZzweiQuest = questFlags2[3];
			RedeQuests.DcloakQuest = questFlags2[4];
			RedeQuests.DparthQuest = questFlags2[5];
			RedeQuests.DmapQuest = questFlags2[6];
			RedeQuests.DslimeQuest = questFlags2[7];
			BitsByte questFlags3 = reader.ReadByte();
			RedeQuests.ZsheathQuest = questFlags3[0];
			RedeQuests.ZpotionQuest = questFlags3[1];
			RedeQuests.zephosQuests = reader.ReadInt32();
			RedeQuests.daerelQuests = reader.ReadInt32();
		}

		public static int zephosQuests;

		public static int daerelQuests;

		public static bool ZchickenQuest;

		public static bool ZskullQuest;

		public static bool ZdishQuest;

		public static bool ZvepdorQuest;

		public static bool ZswordQuest;

		public static bool ZsoulQuest;

		public static bool ZsheathQuest;

		public static bool ZzweiQuest;

		public static bool ZpotionQuest;

		public static bool DnecklaceQuest;

		public static bool DbackpackQuest;

		public static bool DkitQuest;

		public static bool DpotionQuest;

		public static bool DsoulQuest;

		public static bool DcloakQuest;

		public static bool DparthQuest;

		public static bool DmapQuest;

		public static bool DslimeQuest;
	}
}
