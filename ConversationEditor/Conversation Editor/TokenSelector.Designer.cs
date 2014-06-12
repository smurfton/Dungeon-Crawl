namespace Conversation_Editor
{
    partial class TokenSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TokenSelector));
            this.add = new System.Windows.Forms.Button();
            this.possibleTokens = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // add
            // 
            this.add.Image = global::Conversation_Editor.Properties.Resources.Plus;
            this.add.Location = new System.Drawing.Point(12, 199);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(260, 51);
            this.add.TabIndex = 0;
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // possibleTokens
            // 
            this.possibleTokens.FormattingEnabled = true;
            this.possibleTokens.Items.AddRange(new object[] {
            "First Name",
            "Last Name",
            "Full Name",
            "He/She",
            "he/she",
            "Him/Her",
            "him/her",
            "Male/Female",
            "male/female",
            "Boy/Girl",
            "boy/girl",
            "Man/Woman",
            "man/woman",
            "Age",
            "Race",
            "race",
            "Class",
            "class"});
            this.possibleTokens.Location = new System.Drawing.Point(12, 12);
            this.possibleTokens.Name = "possibleTokens";
            this.possibleTokens.ScrollAlwaysVisible = true;
            this.possibleTokens.Size = new System.Drawing.Size(260, 173);
            this.possibleTokens.TabIndex = 1;
            // 
            // TokenSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.possibleTokens);
            this.Controls.Add(this.add);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TokenSelector";
            this.ShowIcon = false;
            this.Text = "Token Selector";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button add;
        private System.Windows.Forms.ListBox possibleTokens;
    }
}