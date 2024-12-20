using System;
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
			bool z = false;
			bool z2 = false;
			bool z3 = false;
			bool z4 = false;
			bool z5 = false;
			bool z6 = false;
			bool z7 = false;
			bool z8 = false;
			bool z9 = false;
			bool d = false;
			bool d2 = false;
			bool d3 = false;
			bool d4 = false;
			bool d5 = false;
			bool d6 = false;
			bool d7 = false;
			bool d8 = false;
			bool d9 = false;
			if (RedeQuests.ZchickenQuest)
			{
				z = true;
			}
			if (RedeQuests.ZskullQuest)
			{
				z2 = true;
			}
			if (RedeQuests.ZdishQuest)
			{
				z3 = true;
			}
			if (RedeQuests.ZvepdorQuest)
			{
				z4 = true;
			}
			if (RedeQuests.ZswordQuest)
			{
				z5 = true;
			}
			if (RedeQuests.ZsoulQuest)
			{
				z6 = true;
			}
			if (RedeQuests.ZsheathQuest)
			{
				z7 = true;
			}
			if (RedeQuests.ZzweiQuest)
			{
				z8 = true;
			}
			if (RedeQuests.ZpotionQuest)
			{
				z9 = true;
			}
			if (RedeQuests.DnecklaceQuest)
			{
				d = true;
			}
			if (RedeQuests.DbackpackQuest)
			{
				d2 = true;
			}
			if (RedeQuests.DkitQuest)
			{
				d3 = true;
			}
			if (RedeQuests.DpotionQuest)
			{
				d4 = true;
			}
			if (RedeQuests.DsoulQuest)
			{
				d5 = true;
			}
			if (RedeQuests.DcloakQuest)
			{
				d6 = true;
			}
			if (RedeQuests.DparthQuest)
			{
				d7 = true;
			}
			if (RedeQuests.DmapQuest)
			{
				d8 = true;
			}
			if (RedeQuests.DslimeQuest)
			{
				d9 = true;
			}
			TagCompound tagCompound = new TagCompound();
			tagCompound.Add("zepQuests", RedeQuests.zephosQuests);
			tagCompound.Add("daeQuests", RedeQuests.daerelQuests);
			tagCompound.Add("zep1", z);
			tagCompound.Add("zep2", z2);
			tagCompound.Add("zep3", z3);
			tagCompound.Add("zep4", z4);
			tagCompound.Add("zep5", z5);
			tagCompound.Add("zep6", z6);
			tagCompound.Add("zep7", z7);
			tagCompound.Add("zep8", z8);
			tagCompound.Add("zep9", z9);
			tagCompound.Add("dae1", d);
			tagCompound.Add("dae2", d2);
			tagCompound.Add("dae3", d3);
			tagCompound.Add("dae4", d4);
			tagCompound.Add("dae6", d5);
			tagCompound.Add("dae7", d6);
			tagCompound.Add("dae8", d7);
			tagCompound.Add("dae9", d8);
			tagCompound.Add("dae10", d9);
			return tagCompound;
		}

		public override void Load(TagCompound tag)
		{
			RedeQuests.zephosQuests = tag.GetInt("zepQuests");
			RedeQuests.daerelQuests = tag.GetInt("daeQuests");
			RedeQuests.ZchickenQuest = tag.GetBool("zep1");
			RedeQuests.ZskullQuest = tag.GetBool("zep2");
			RedeQuests.ZdishQuest = tag.GetBool("zep3");
			RedeQuests.ZvepdorQuest = tag.GetBool("zep4");
			RedeQuests.ZswordQuest = tag.GetBool("zep5");
			RedeQuests.ZsoulQuest = tag.GetBool("zep6");
			RedeQuests.ZsheathQuest = tag.GetBool("zep7");
			RedeQuests.ZzweiQuest = tag.GetBool("zep8");
			RedeQuests.ZpotionQuest = tag.GetBool("zep9");
			RedeQuests.DnecklaceQuest = tag.GetBool("dae1");
			RedeQuests.DbackpackQuest = tag.GetBool("dae2");
			RedeQuests.DkitQuest = tag.GetBool("dae3");
			RedeQuests.DpotionQuest = tag.GetBool("dae4");
			RedeQuests.DsoulQuest = tag.GetBool("dae6");
			RedeQuests.DcloakQuest = tag.GetBool("dae7");
			RedeQuests.DparthQuest = tag.GetBool("dae8");
			RedeQuests.DmapQuest = tag.GetBool("dae9");
			RedeQuests.DslimeQuest = tag.GetBool("dae10");
		}

		public override void LoadLegacy(BinaryReader reader)
		{
			int loadVersion = reader.ReadInt32();
			if (loadVersion == 0)
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
				return;
			}
			base.mod.Logger.Debug("Redemption: Unknown loadVersion: " + loadVersion);
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
