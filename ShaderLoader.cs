using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace Redemption
{
	public sealed class ShaderLoader : ILoadable
	{
		public float Priority
		{
			get
			{
				return 1f;
			}
		}

		public bool LoadOnDedServer
		{
			get
			{
				return false;
			}
		}

		public void Load()
		{
			foreach (TmodFile.FileEntry fileEntry in Enumerable.Where<TmodFile.FileEntry>((TmodFile)typeof(Mod).GetProperty("File", BindingFlags.Instance | BindingFlags.NonPublic).GetGetMethod(true).Invoke(Redemption.Inst, null), (TmodFile.FileEntry x) => x.Name.Contains("Effects/") && x.Name.EndsWith(".xnb")))
			{
				string shaderPath = fileEntry.Name.Replace(".xnb", string.Empty);
				ShaderLoader.LoadShader(Path.GetFileName(shaderPath), shaderPath);
			}
		}

		public void Unload()
		{
		}

		internal static void LoadShader(string shaderName, string shaderPath)
		{
			Ref<Effect> shaderRef = new Ref<Effect>(Redemption.Inst.GetEffect(shaderPath));
			(Filters.Scene["MoR:" + shaderName] = new Filter(new ScreenShaderData(shaderRef, shaderName), 3)).Load();
		}
	}
}
