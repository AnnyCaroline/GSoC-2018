using System;
using System.Text.RegularExpressions;
using System.Management;

namespace PortTest
{
	class Program
	{
		// Helper function to handle regex search
		static string regex(string pattern, string text)
		{
			Regex re = new Regex(pattern);
			Match m = re.Match(text);
			if (m.Success)
			{
				return m.Value;
			}
			else
			{
				return null;
			}
		}

		static void Main(string[] args)
		{			
			
			string port = "", vid = "", pid = "", sn = "";

			// Use WMI to get info
			ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2",
				"SELECT * FROM Win32_PnPEntity WHERE ClassGuid=\"{4d36e978-e325-11ce-bfc1-08002be10318}\"");

			// Search all serial ports
			foreach(ManagementObject queryObj in searcher.Get())
			{
				// Parse the data
				if (null != queryObj["Name"])
				{
					//https://stackoverflow.com/questions/24135006/regex-that-match-any-character-inside-a-parenthesis
					port = regex("(?<=" + Regex.Escape("(") + ")[^)]*(?=" + Regex.Escape(")") + ")", queryObj["Name"].ToString());
				}

				if (null != queryObj["PNPDeviceID"])
				{
					//https://stackoverflow.com/questions/3926451/how-to-match-but-not-capture-part-of-a-regex?rq=1
					vid = regex("(?<=VID_)([0-9a-fA-F]+)", queryObj["PNPDeviceID"].ToString());
					pid = regex("(?<=PID_)([0-9a-fA-F]+)", queryObj["PNPDeviceID"].ToString());
					sn  = regex("([0-9a-fA-F]{5,})", queryObj["PNPDeviceID"].ToString());						

					if (!string.IsNullOrEmpty(port) && !string.IsNullOrEmpty(vid) && !string.IsNullOrEmpty(pid) && !string.IsNullOrEmpty(sn)){

						string text = System.IO.File.ReadAllText(@"C:\Users\AnnyC\Desktop\ceu-maker\ceu-maker\arduino-1.8.3\hardware\arduino\avr\boards.txt");

						//Pega as duas linhas correspondentes ao VID e PID
						string a  = "";
						a = regex("[a-zA-Z]+" + Regex.Escape(".") + "vid" + Regex.Escape(".") + "[0-9]=0x" + vid + "(\r)(\n)[a-zA-Z]+" + Regex.Escape(".") + "pid" + Regex.Escape(".") + "[0-9]=0x" + pid, text.ToString());

						//Pega a primeira palabra de [a]
						string b = "";
						b = regex("[a-zA-Z]+(?=" + Regex.Escape(".") + "vid)", a.ToString());

						//Pesquisa b.build.board
						string board = "";
						board = regex("(?<=" + b + Regex.Escape(".") + "build" + Regex.Escape(".") + "board=)(\\w)+", text.ToString());
						
						//Pesquisa b.name
						string name = "";
						name = regex("(?<=" + b + Regex.Escape(".") + "name=).+", text.ToString());
						
						//Prints
						Console.WriteLine("\n---------------------------------------------------");
						Console.WriteLine("Port = " + port);
						Console.WriteLine("VID = " + vid);
						Console.WriteLine("PID = " + pid);
						Console.WriteLine("SN  = " + sn );

						Console.WriteLine("");
						
						Console.WriteLine(name);
						Console.WriteLine(a);
						Console.WriteLine(b+".build.board = " + board);
						Console.WriteLine("");
						Console.WriteLine("set ARD_BOARD=\"--board arduino:avr:"+b+"\"");
						Console.WriteLine("set ARD_PORT=\"--port " + port + "\"");
						Console.WriteLine("---------------------------------------------------\n");
					}

				}

			}
			Console.WriteLine("Done");			

			int c = Console.Read();
		}
	}
}