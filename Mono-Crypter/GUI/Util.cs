using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using Gtk;

namespace GUI
{
	public class Util
	{	
		#region [Constant]
		public const char spliter = '█';
		#endregion
		
		public static string[] DecryptFile(string file)
		{
			DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
			
			//Seteo la key supersecreta
			DES.Key = StrToByteArray("comentar");
			//Seteo el vector
			DES.IV = StrToByteArray("comentar");
			
			//Crel el FileStream del archivo a desencriptar
			FileStream fsRead = new FileStream(file, FileMode.Open,	FileAccess.Read);
			
			//Creo el desencriptor basado en DES
			ICryptoTransform desdecrypt = DES.CreateDecryptor();
			
			//Seteo el CryptoStream con el archivo y el desencriptor DES
			CryptoStream cryptostreamDecr = new CryptoStream(fsRead, desdecrypt, CryptoStreamMode.Read);
			
			try
				{
				//Almaceno lo desencriptado
				string dataAux = new StreamReader(cryptostreamDecr).ReadToEnd();
				return dataAux.Split(spliter);
			}
			catch (Exception e)
			{
				throw e;
			}
			finally
			{
				fsRead.Flush(); fsRead.Close();	
			}
		} 
		
		public static void EncryptData(string data, string file) 
		{
			FileStream fsEncrypted = new FileStream(file, FileMode.Create, FileAccess.Write);
				
			DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
			DES.Key = StrToByteArray("comentar");
			DES.IV = StrToByteArray("comentar");
			
			ICryptoTransform desencrypt = DES.CreateEncryptor();
			
			CryptoStream cryptostream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write); 
		
			byte[] bytearrayinput = new byte[data.Length];
			bytearrayinput = StrToByteArray(data);
		 	
			try
			{
				//fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
			 	cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
			}
			catch (Exception e)
			{
				throw e;
			}
			finally
			{
			 	cryptostream.Close();
			 	fsEncrypted.Close();
			}
		}
		
	    public static bool NewFile(string path) 
	    {			
	        if (File.Exists(path))
	        {
	            return false;
	        }
			else using (StreamWriter sw = File.CreateText(path))
			{
				/*
				FileStream stream = new FileStream(path, FileMode.CreateNew, FileAccess.Write);
        		
				BinaryWriter writer = new BinaryWriter(stream);
				
				byte[] vals = { 0x3E, 0x17, 0x33, 0x5B, 0x0F, 0xC3, 0xBD, 0x44, 0x7A, 0x72,
								0x0A, 0xC2, 0x84, 0x57, 0xC3, 0x8B, 0x7A, 0XC2, 0xBA, 0x30, 0x0A};
				
				foreach (byte val in vals)
				{
					writer.Write(val);
				}
				stream.Flush(); stream.Close();
				writer.Flush(); writer.Close();
				//EncryptData("cadenana", path);
				*/
				return true;
	        }
		}
		
		public static string[] ReadFile(string file)
		{
			StreamReader rd = File.OpenText(file);
			string line;
			string[] data = new string[3];
			do
			{
				line = rd.ReadLine();
				if (line != null)
				{
					data = line.Split(spliter);
				}
			}
			while (line != null);
			
			rd.Close();
			
			return data;
		}
		
		public static bool ShowConfirmMessage(string titulo, string texto, Gtk.Window win)
		{
			MessageDialog md = new MessageDialog(win, DialogFlags.NoSeparator, MessageType.Question, ButtonsType.YesNo, texto);
			md.Modal = true;
			md.Title = titulo;
			md.Icon = null;			
			
			ResponseType result = (ResponseType)md.Run ();
			if (result == ResponseType.Yes)
			{
				md.Destroy();
				return true;
			}
			else
			{
				md.Destroy();
				return false;
			}
		}
		
		public static void ShowErrorMessage(string titulo, string texto, Gtk.Window win)
        {
			MessageDialog md = new MessageDialog(win, DialogFlags.NoSeparator, MessageType.Error, ButtonsType.Ok, texto);
			md.Modal = true;
			md.Title = titulo;
			md.Icon = null;			
			md.Run();
			md.Destroy();
        }
		
		public static bool esCampoVacio(string campo)
		{
			if (campo.Length == 0 || campo == null || campo == "")
				return true;
			else 
				return false;
		}
		
		public static byte[] StrToByteArray(string str)
		{
		    System.Text.UTF8Encoding  encoding=new System.Text.UTF8Encoding();
		    return encoding.GetBytes(str);
		}
	}
}
