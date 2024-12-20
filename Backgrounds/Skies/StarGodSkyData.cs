using System;
using Terraria;
using Terraria.Graphics.Shaders;

namespace Redemption.Backgrounds.Skies
{
	public class StarGodSkyData : ScreenShaderData
	{
		public StarGodSkyData(string passName) : base(passName)
		{
		}

		private void UpdateStarGodIndex()
		{
			int StarGodType = Redemption.Inst.NPCType("NebP1");
			if (this.StarGodIndex >= 0 && Main.npc[this.StarGodIndex].active && Main.npc[this.StarGodIndex].type == StarGodType)
			{
				return;
			}
			this.StarGodIndex = -1;
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].active && Main.npc[i].type == StarGodType)
				{
					this.StarGodIndex = i;
					return;
				}
			}
		}

		public override void Apply()
		{
			this.UpdateStarGodIndex();
			if (this.StarGodIndex != -1)
			{
				base.UseTargetPosition(Main.npc[this.StarGodIndex].Center);
			}
			base.Apply();
		}

		private int StarGodIndex;
	}
}
