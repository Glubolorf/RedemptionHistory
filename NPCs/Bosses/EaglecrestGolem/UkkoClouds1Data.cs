using System;
using Terraria;
using Terraria.Graphics.Shaders;

namespace Redemption.NPCs.Bosses.EaglecrestGolem
{
	public class UkkoClouds1Data : ScreenShaderData
	{
		public UkkoClouds1Data(string passName) : base(passName)
		{
		}

		private void UpdateUkkoIndex()
		{
			int UkkoType = Redemption.inst.NPCType("Ukko");
			if (this.UkkoIndex >= 0 && Main.npc[this.UkkoIndex].active && Main.npc[this.UkkoIndex].type == UkkoType)
			{
				return;
			}
			this.UkkoIndex = -1;
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].active && Main.npc[i].type == UkkoType)
				{
					this.UkkoIndex = i;
					return;
				}
			}
		}

		public override void Apply()
		{
			this.UpdateUkkoIndex();
			if (this.UkkoIndex != -1)
			{
				base.UseTargetPosition(Main.npc[this.UkkoIndex].Center);
			}
			base.Apply();
		}

		private int UkkoIndex;
	}
}
