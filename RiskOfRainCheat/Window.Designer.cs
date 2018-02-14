namespace RiskOfRainCheat {
	partial class Window {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window));
			this.UIConsole = new System.Windows.Forms.TextBox();
			this.Health = new System.Windows.Forms.CheckBox();
			this.AttackSpeed = new System.Windows.Forms.CheckBox();
			this.AttackPower = new System.Windows.Forms.CheckBox();
			this.JmpHeight = new System.Windows.Forms.CheckBox();
			this.JmpDouble = new System.Windows.Forms.CheckBox();
			this.RunSpeed = new System.Windows.Forms.CheckBox();
			this.SkillCoolDown = new System.Windows.Forms.CheckBox();
			this.ItemCoolDown = new System.Windows.Forms.CheckBox();
			this.SubmitLog = new System.Windows.Forms.Button();
			this.CheatInit = new System.Windows.Forms.Button();
			this.Gold = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// UIConsole
			// 
			this.UIConsole.BackColor = System.Drawing.SystemColors.Window;
			this.UIConsole.Location = new System.Drawing.Point(159, 9);
			this.UIConsole.Multiline = true;
			this.UIConsole.Name = "UIConsole";
			this.UIConsole.ReadOnly = true;
			this.UIConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.UIConsole.Size = new System.Drawing.Size(751, 250);
			this.UIConsole.TabIndex = 12;
			// 
			// Health
			// 
			this.Health.AutoSize = true;
			this.Health.Location = new System.Drawing.Point(13, 12);
			this.Health.Name = "Health";
			this.Health.Size = new System.Drawing.Size(57, 17);
			this.Health.TabIndex = 0;
			this.Health.Text = "Health";
			this.Health.UseVisualStyleBackColor = true;
			this.Health.CheckedChanged += new System.EventHandler(this.Health_CheckedChanged);
			// 
			// AttackSpeed
			// 
			this.AttackSpeed.AutoSize = true;
			this.AttackSpeed.Location = new System.Drawing.Point(13, 36);
			this.AttackSpeed.Name = "AttackSpeed";
			this.AttackSpeed.Size = new System.Drawing.Size(91, 17);
			this.AttackSpeed.TabIndex = 2;
			this.AttackSpeed.Text = "Attack Speed";
			this.AttackSpeed.UseVisualStyleBackColor = true;
			this.AttackSpeed.CheckedChanged += new System.EventHandler(this.AttackSpeed_CheckedChanged);
			// 
			// AttackPower
			// 
			this.AttackPower.AutoSize = true;
			this.AttackPower.Location = new System.Drawing.Point(13, 60);
			this.AttackPower.Name = "AttackPower";
			this.AttackPower.Size = new System.Drawing.Size(90, 17);
			this.AttackPower.TabIndex = 4;
			this.AttackPower.Text = "Attack Power";
			this.AttackPower.UseVisualStyleBackColor = true;
			this.AttackPower.CheckedChanged += new System.EventHandler(this.AttackPower_CheckedChanged);
			// 
			// JmpHeight
			// 
			this.JmpHeight.AutoSize = true;
			this.JmpHeight.Location = new System.Drawing.Point(13, 84);
			this.JmpHeight.Name = "JmpHeight";
			this.JmpHeight.Size = new System.Drawing.Size(76, 17);
			this.JmpHeight.TabIndex = 6;
			this.JmpHeight.Text = "JmpHeight";
			this.JmpHeight.UseVisualStyleBackColor = true;
			this.JmpHeight.CheckedChanged += new System.EventHandler(this.JmpHeight_CheckedChanged);
			// 
			// JmpDouble
			// 
			this.JmpDouble.AutoSize = true;
			this.JmpDouble.Location = new System.Drawing.Point(13, 108);
			this.JmpDouble.Name = "JmpDouble";
			this.JmpDouble.Size = new System.Drawing.Size(79, 17);
			this.JmpDouble.TabIndex = 7;
			this.JmpDouble.Text = "JmpDouble";
			this.JmpDouble.UseVisualStyleBackColor = true;
			this.JmpDouble.CheckedChanged += new System.EventHandler(this.JmpDouble_CheckedChanged);
			// 
			// RunSpeed
			// 
			this.RunSpeed.AutoSize = true;
			this.RunSpeed.Location = new System.Drawing.Point(13, 132);
			this.RunSpeed.Name = "RunSpeed";
			this.RunSpeed.Size = new System.Drawing.Size(77, 17);
			this.RunSpeed.TabIndex = 8;
			this.RunSpeed.Text = "RunSpeed";
			this.RunSpeed.UseVisualStyleBackColor = true;
			this.RunSpeed.CheckedChanged += new System.EventHandler(this.RunSpeed_CheckedChanged);
			// 
			// SkillCoolDown
			// 
			this.SkillCoolDown.AutoSize = true;
			this.SkillCoolDown.Location = new System.Drawing.Point(13, 156);
			this.SkillCoolDown.Name = "SkillCoolDown";
			this.SkillCoolDown.Size = new System.Drawing.Size(94, 17);
			this.SkillCoolDown.TabIndex = 9;
			this.SkillCoolDown.Text = "SkillCoolDown";
			this.SkillCoolDown.UseVisualStyleBackColor = true;
			this.SkillCoolDown.CheckedChanged += new System.EventHandler(this.SkillCoolDown_CheckedChanged);
			// 
			// ItemCoolDown
			// 
			this.ItemCoolDown.AutoSize = true;
			this.ItemCoolDown.Location = new System.Drawing.Point(13, 180);
			this.ItemCoolDown.Name = "ItemCoolDown";
			this.ItemCoolDown.Size = new System.Drawing.Size(95, 17);
			this.ItemCoolDown.TabIndex = 10;
			this.ItemCoolDown.Text = "ItemCoolDown";
			this.ItemCoolDown.UseVisualStyleBackColor = true;
			this.ItemCoolDown.CheckedChanged += new System.EventHandler(this.ItemCoolDown_CheckedChanged);
			// 
			// SubmitLog
			// 
			this.SubmitLog.Location = new System.Drawing.Point(835, 265);
			this.SubmitLog.Name = "SubmitLog";
			this.SubmitLog.Size = new System.Drawing.Size(75, 23);
			this.SubmitLog.TabIndex = 13;
			this.SubmitLog.Text = "Copy Log";
			this.SubmitLog.UseVisualStyleBackColor = true;
			this.SubmitLog.Click += new System.EventHandler(this.SubmitLog_Click);
			// 
			// CheatInit
			// 
			this.CheatInit.Location = new System.Drawing.Point(159, 265);
			this.CheatInit.Name = "CheatInit";
			this.CheatInit.Size = new System.Drawing.Size(75, 23);
			this.CheatInit.TabIndex = 14;
			this.CheatInit.Text = "Init Cheats";
			this.CheatInit.UseVisualStyleBackColor = true;
			this.CheatInit.Click += new System.EventHandler(this.CheatInit_Click);
			// 
			// Gold
			// 
			this.Gold.AutoSize = true;
			this.Gold.Location = new System.Drawing.Point(12, 204);
			this.Gold.Name = "Gold";
			this.Gold.Size = new System.Drawing.Size(48, 17);
			this.Gold.TabIndex = 16;
			this.Gold.Text = "Gold";
			this.Gold.UseVisualStyleBackColor = true;
			this.Gold.CheckedChanged += new System.EventHandler(this.Gold_CheckedChanged);
			// 
			// Window
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(922, 309);
			this.Controls.Add(this.Gold);
			this.Controls.Add(this.CheatInit);
			this.Controls.Add(this.SubmitLog);
			this.Controls.Add(this.UIConsole);
			this.Controls.Add(this.ItemCoolDown);
			this.Controls.Add(this.SkillCoolDown);
			this.Controls.Add(this.RunSpeed);
			this.Controls.Add(this.JmpDouble);
			this.Controls.Add(this.JmpHeight);
			this.Controls.Add(this.AttackPower);
			this.Controls.Add(this.AttackSpeed);
			this.Controls.Add(this.Health);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Window";
			this.Text = "Risk of Rain Cheat";
			this.Load += new System.EventHandler(this.Window_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox Health;
		private System.Windows.Forms.CheckBox AttackSpeed;
		private System.Windows.Forms.CheckBox AttackPower;
		private System.Windows.Forms.CheckBox JmpHeight;
		private System.Windows.Forms.CheckBox JmpDouble;
		private System.Windows.Forms.CheckBox RunSpeed;
		private System.Windows.Forms.CheckBox SkillCoolDown;
		private System.Windows.Forms.CheckBox ItemCoolDown;
		private System.Windows.Forms.TextBox UIConsole;
		private System.Windows.Forms.Button SubmitLog;
		private System.Windows.Forms.Button CheatInit;
		private System.Windows.Forms.CheckBox Gold;
	}
}

