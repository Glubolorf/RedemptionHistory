using System;
using Terraria;
using Terraria.Graphics.Shaders;

namespace Redemption.Backgrounds.Skies
{
	public class StarGodSkyData2 : ScreenShaderData
	{
		public StarGodSkyData2(string passName) : base(passName)
		{
		}

		private void UpdateStarGodIndex2()
		{
			int StarGodType2 = Redemption.Inst.NPCType("NebP2");
			if (this.StarGodIndex2 >= 0 && Main.npc[this.StarGodIndex2].active && Main.npc[this.StarGodIndex2].type == StarGodType2)
			{
				return;
			}
			this.StarGodIndex2 = -1;
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].active && Main.npc[i].type == StarGodType2)
				{
					this.StarGodIndex2 = i;
					return;
				}
			}
		}

		public override void Apply()
		{
			this.UpdateStarGodIndex2();
			if (this.StarGodIndex2 != -1)
			{
				base.UseTargetPosition(Main.npc[this.StarGodIndex2].Center);
			}
			base.Apply();
		}

		private int StarGodIndex2;
	}
}
