using System;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Runtime.CompilerServices;
using System.Net.Http;
using Newtonsoft.Json;

namespace RiskOfRainCheat {
	public partial class Window : Form {
		public Window() {
			InitializeComponent();
		}

		// Bools to ask threads to stop
		private class Stop {
			public static bool Health = false;
		}

		private void Window_Load(object sender, EventArgs e) {
			//Console.SetOut(new ControlWriter(UIConsole)); // Make it log to UIConsole
			Debug.Write("Starting...");
			Debug.Write(Environment.OSVersion.ToString());
			Debug.Write(Environment.Version.ToString());

			string path = Directory.GetCurrentDirectory() + "\\Newtonsoft.Json.dll";
			if (!File.Exists(path)) {
				Debug.Write("Looks like Newtonsoft.Json.dll doesn't exist, using [Copy Log] will crash the program");
			}
		}

		private void Health_CheckedChanged(object sender, EventArgs e) {
			Thread thread = new Thread(() => Cheat.Health(ref Stop.Health));
			if (Health.Checked) {
				thread.Start();
			}
			Stop.Health = !Health.Checked;
		}

		private void AttackSpeed_CheckedChanged(object sender, EventArgs e) {
			Cheat.AttackSpeed(AttackSpeed.Checked);
		}

		private void AttackPower_CheckedChanged(object sender, EventArgs e) {
			Cheat.AttackPower(AttackPower.Checked);
		}

		private void JmpHeight_CheckedChanged(object sender, EventArgs e) {
			Cheat.JmpHeight(JmpHeight.Checked);
		}

		private void JmpDouble_CheckedChanged(object sender, EventArgs e) {
			Cheat.JmpDouble(JmpDouble.Checked);
		}

		private void RunSpeed_CheckedChanged(object sender, EventArgs e) {
			Cheat.RunSpeed(RunSpeed.Checked);
		}

		private void SkillCoolDown_CheckedChanged(object sender, EventArgs e) {
			Cheat.SkillCoolDown(SkillCoolDown.Checked);
		}

		private void ItemCoolDown_CheckedChanged(object sender, EventArgs e) {
			Cheat.ItemCoolDown(ItemCoolDown.Checked);
		}

		private void Gold_CheckedChanged(object sender, EventArgs e) {
			Cheat.Gold(Gold.Checked);
		}

		private class FileIo {
			public bool Success { get; set; }
			public string Key { get; set; }
			public string Link { get; set; }
			public string Expiry { get; set; }
		}
		private async void SubmitLog_Click(object sender, EventArgs e) {
			// Submit the log off UIConsole
			// cheers https://www.file.io

			HttpClient httpClient = new HttpClient();
			MultipartFormDataContent form = new MultipartFormDataContent {
				{ new ByteArrayContent(Encoding.ASCII.GetBytes(UIConsole.Text), 0, UIConsole.Text.Length), "file", "log.txt" }
			};
			HttpResponseMessage response = await httpClient.PostAsync("https://file.io", form);

			response.EnsureSuccessStatusCode();
			httpClient.Dispose();
			string responseString = response.Content.ReadAsStringAsync().Result;

			var json = JsonConvert.DeserializeObject<FileIo>(responseString);

			Clipboard.SetText(json.Link);

			Debug.Write(json.Link + " Copied to clipboard");

			return;
		}

		// <3 https://stackoverflow.com/questions/18726852/redirecting-console-writeline-to-textbox
		// & https://stackoverflow.com/questions/142003/cross-thread-operation-not-valid-control-accessed-from-a-thread-other-than-the#142108
		public class ControlWriter : TextWriter {
			private TextBox textbox;
			public ControlWriter(TextBox textbox) {
				this.textbox = textbox;
			}

			public override void Write(char value) {
				if (textbox.InvokeRequired) {
					textbox.Invoke(new MethodInvoker(delegate { textbox.AppendText(value.ToString()); }));
				} else {
					textbox.AppendText(value.ToString());
				}
			}

			public override void Write(string value) {
				if (textbox.InvokeRequired) {
					textbox.Invoke(new MethodInvoker(delegate { textbox.AppendText(value.ToString()); }));
				} else {
					textbox.AppendText(value.ToString());
				}
			}

			public override Encoding Encoding {
				get { return Encoding.ASCII; }
			}
		}

		private void CheatInit_Click(object sender, EventArgs e) {
			// Give the box starter text
			Debug.Write("Initiating Cheats...");
			try {
				Cheat.Init();
			} catch (Exception exception)
			  {
				//MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Debug.Write(exception.ToString());
				Debug.Write("Cheats failed to Initiate");
				return;
			}
			Debug.Write("Cheats Initiated");
		}
	}

	// nice little func for writing out with func name ect
	public class Debug {
		public static void Write(string str, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null) {
			Console.WriteLine(caller + "(" + lineNumber + "): " + str);
			return;
		}
	}
}
