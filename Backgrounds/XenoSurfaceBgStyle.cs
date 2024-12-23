﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Backgrounds
{
	public class XenoSurfaceBgStyle : ModSurfaceBgStyle
	{
		public override bool ChooseBgStyle()
		{
			return !Main.gameMenu && (Main.LocalPlayer.GetModPlayer<RedePlayer>().ZoneXeno || Main.LocalPlayer.GetModPlayer<RedePlayer>().ZoneEvilXeno || Main.LocalPlayer.GetModPlayer<RedePlayer>().ZoneEvilXeno2);
		}

		public override void ModifyFarFades(float[] fades, float transitionSpeed)
		{
			for (int i = 0; i < fades.Length; i++)
			{
				if (i == base.Slot)
				{
					fades[i] += transitionSpeed;
					if (fades[i] > 1f)
					{
						fades[i] = 1f;
					}
				}
				else
				{
					fades[i] -= transitionSpeed;
					if (fades[i] < 0f)
					{
						fades[i] = 0f;
					}
				}
			}
		}

		public override int ChooseFarTexture()
		{
			return base.mod.GetBackgroundSlot("Backgrounds/XenoBiomeSurfaceFar");
		}

		public override int ChooseMiddleTexture()
		{
			return base.mod.GetBackgroundSlot("Backgrounds/XenoBiomeSurfaceMid");
		}

		public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
		{
			return base.mod.GetBackgroundSlot("Backgrounds/XenoBiomeSurfaceClose");
		}
	}
}
