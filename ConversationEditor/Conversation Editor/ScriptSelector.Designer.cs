namespace Conversation_Editor
{
    partial class ScriptSelector
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptSelector));
            this.scripts = new System.Windows.Forms.ListBox();
            this.Ok = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.addScript = new System.Windows.Forms.Button();
            this.deleteScript = new System.Windows.Forms.Button();
            this.description = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // scripts
            // 
            this.scripts.FormattingEnabled = true;
            this.scripts.Location = new System.Drawing.Point(12, 12);
            this.scripts.Name = "scripts";
            this.scripts.ScrollAlwaysVisible = true;
            this.scripts.Size = new System.Drawing.Size(216, 82);
            this.scripts.TabIndex = 0;
            this.scripts.SelectedIndexChanged += new System.EventHandler(this.scripts_SelectedIndexChanged);
            // 
            // Ok
            // 
            this.Ok.Location = new System.Drawing.Point(185, 168);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(87, 23);
            this.Ok.TabIndex = 2;
            this.Ok.Text = "Ok";
            this.Ok.UseVisualStyleBackColor = true;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(12, 167);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(87, 23);
            this.Cancel.TabIndex = 3;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // addScript
            // 
            this.addScript.Image = global::Conversation_Editor.Properties.Resources.Plus;
            this.addScript.Location = new System.Drawing.Point(234, 12);
            this.addScript.Name = "addScript";
            this.addScript.Size = new System.Drawing.Size(38, 38);
            this.addScript.TabIndex = 1;
            this.addScript.UseVisualStyleBackColor = true;
            this.addScript.Click += new System.EventHandler(this.addScript_Click);
            // 
            // deleteScript
            // 
            this.deleteScript.Image = global::Conversation_Editor.Properties.Resources.Minus;
            this.deleteScript.Location = new System.Drawing.Point(234, 56);
            this.deleteScript.Name = "deleteScript";
            this.deleteScript.Size = new System.Drawing.Size(38, 38);
            this.deleteScript.TabIndex = 4;
            this.deleteScript.UseVisualStyleBackColor = true;
            this.deleteScript.Click += new System.EventHandler(this.deleteScript_Click);
            // 
            // description
            // 
            this.description.Location = new System.Drawing.Point(12, 100);
            this.description.Multiline = true;
            this.description.Name = "description";
            this.description.ReadOnly = true;
            this.description.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.description.Size = new System.Drawing.Size(260, 61);
            this.description.TabIndex = 5;
            // 
            // ScriptSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 202);
            this.Controls.Add(this.description);
            this.Controls.Add(this.deleteScript);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Ok);
            this.Controls.Add(this.addScript);
            this.Controls.Add(this.scripts);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ScriptSelector";
            this.ShowIcon = false;
            this.Text = "Script Selector";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox scripts;
        private System.Windows.Forms.Button addScript;
        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button deleteScript;
        private System.Windows.Forms.TextBox description;
    }
}