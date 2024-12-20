using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace Redemption.WorldGeneration
{
	public class SlayerShipClear : MicroBiome
	{
		public override bool Place(Point origin, StructureMap structures)
		{
			Mod inst = Redemption.inst;
			Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
			colorToTile[new Color(150, 150, 150)] = -2;
			colorToTile[Color.Black] = -1;
			BaseWorldGenTex.GetTexGenerator(inst.GetTexture("WorldGeneration/SlayerShipClear"), colorToTile, null, null, null, null).Generate(origin.X, origin.Y, true, true);
			return true;
		}
	}
}
