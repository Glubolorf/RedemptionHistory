using System;
using Terraria.ModLoader.IO;

namespace Redemption.StructureHelper
{
	public struct TileSaveData : TagSerializable
	{
		public bool Active
		{
			get
			{
				return (this.SHeader & 32) == 32;
			}
		}

		public TileSaveData(string tile, string wall, short frameX, short frameY, byte bHeader1, byte bHeader2, byte bHeader3, ushort sHeader, string teType = "", TagCompound teData = null)
		{
			this.Tile = tile;
			this.Wall = wall;
			this.FrameX = frameX;
			this.FrameY = frameY;
			this.BHeader1 = bHeader1;
			this.BHeader2 = bHeader2;
			this.BHeader3 = bHeader3;
			this.SHeader = sHeader;
			this.TEType = teType;
			this.TEData = teData;
		}

		public static TileSaveData DeserializeData(TagCompound tag)
		{
			TileSaveData output = new TileSaveData(tag.GetString("Tile"), tag.GetString("Wall"), tag.GetShort("FrameX"), tag.GetShort("FrameY"), tag.GetByte("BHeader1"), tag.GetByte("BHeader2"), tag.GetByte("BHeader3"), (ushort)tag.GetShort("SHeader"), "", null);
			if (tag.ContainsKey("TEType"))
			{
				output.TEType = tag.GetString("TEType");
				output.TEData = tag.Get<TagCompound>("TEData");
			}
			return output;
		}

		public TagCompound SerializeData()
		{
			TagCompound tagCompound = new TagCompound();
			tagCompound["Tile"] = this.Tile;
			tagCompound["Wall"] = this.Wall;
			tagCompound["FrameX"] = this.FrameX;
			tagCompound["FrameY"] = this.FrameY;
			tagCompound["BHeader1"] = this.BHeader1;
			tagCompound["BHeader2"] = this.BHeader2;
			tagCompound["BHeader3"] = this.BHeader3;
			tagCompound["SHeader"] = this.SHeader;
			TagCompound tag = tagCompound;
			if (this.TEType != "")
			{
				tag.Add("TEType", this.TEType);
				tag.Add("TEData", this.TEData);
			}
			return tag;
		}

		public string Tile;

		public string Wall;

		public short FrameX;

		public short FrameY;

		public byte BHeader1;

		public byte BHeader2;

		public byte BHeader3;

		public ushort SHeader;

		public string TEType;

		public TagCompound TEData;

		public static Func<TagCompound, TileSaveData> DESERIALIZER = (TagCompound s) => TileSaveData.DeserializeData(s);
	}
}
