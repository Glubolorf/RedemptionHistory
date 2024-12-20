using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Nebuleus
{
	public class StarGodSkyData2 : ScreenShaderData
	{
		public StarGodSkyData2(string passName) : base(passName)
		{
		}

		private void UpdateStarGodIndex2()
		{
			int num = ModLoader.GetMod("Redemption").NPCType("BigNebuleus");
			if (this.StarGodIndex2 >= 0 && Main.npc[this.StarGodIndex2].active && Main.npc[this.StarGodIndex2].type == num)
			{
				return;
			}
			this.StarGodIndex2 = -1;
			for (int i = 0; i < Main.npc.Length; i++)
			{
				if (Main.npc[i].active && Main.npc[i].type == num)
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
