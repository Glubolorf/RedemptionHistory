using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption
{
	public class BaseShaderCompiler
	{
		public static Effect CompileShader(Mod mod, string fileName, GraphicsDevice device = null)
		{
			Effect result;
			try
			{
				if (device == null)
				{
					device = Main.instance.GraphicsDevice;
				}
				if (BaseShaderCompiler.appDir == null)
				{
					BaseShaderCompiler.appDir = Path.GetDirectoryName(Application.ExecutablePath) + "\\Content\\ModShaders";
					if (!Directory.Exists(BaseShaderCompiler.appDir))
					{
						Directory.CreateDirectory(BaseShaderCompiler.appDir);
					}
				}
				string path = string.Concat(new string[]
				{
					BaseShaderCompiler.appDir,
					"\\",
					mod.Name,
					"_",
					fileName,
					".xnb"
				});
				if (File.Exists(path))
				{
					File.Delete(path);
				}
				using (FileStream fileStream = File.Create(path))
				{
					byte[] file = mod.File.GetFile(fileName + ".xnb");
					fileStream.Write(file, 0, file.Length);
				}
				if (BaseShaderCompiler.manager == null)
				{
					BaseShaderCompiler.manager = new ContentManager(Main.instance.Content.ServiceProvider, "Content\\ModShaders");
				}
				Effect effect = BaseShaderCompiler.manager.Load<Effect>(mod.Name + "_" + fileName);
				if (File.Exists(path))
				{
					File.Delete(path);
				}
				result = effect;
			}
			catch (Exception ex)
			{
				ErrorLogger.Log("SHADER ERROR: " + ex.Message);
				ErrorLogger.Log(ex.StackTrace);
				ErrorLogger.Log("--------");
				result = null;
			}
			return result;
		}

		public static string appDir;

		public static ContentManager manager;
	}
}
