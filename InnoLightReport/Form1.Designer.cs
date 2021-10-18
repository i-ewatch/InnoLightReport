
namespace SunnineReport
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.fluentDesignFormControl1 = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl();
            this.fluentFormDefaultManager1 = new DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager(this.components);
            this.BarpanelControl = new DevExpress.XtraEditors.PanelControl();
            this.accordionControl1 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.accordionControlElement1 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.LogopictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.ViewpanelControl = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentFormDefaultManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarpanelControl)).BeginInit();
            this.BarpanelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LogopictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewpanelControl)).BeginInit();
            this.SuspendLayout();
            // 
            // fluentDesignFormControl1
            // 
            this.fluentDesignFormControl1.FluentDesignForm = this;
            this.fluentDesignFormControl1.Location = new System.Drawing.Point(0, 0);
            this.fluentDesignFormControl1.Manager = this.fluentFormDefaultManager1;
            this.fluentDesignFormControl1.Margin = new System.Windows.Forms.Padding(2);
            this.fluentDesignFormControl1.Name = "fluentDesignFormControl1";
            this.fluentDesignFormControl1.Size = new System.Drawing.Size(1278, 31);
            this.fluentDesignFormControl1.TabIndex = 2;
            this.fluentDesignFormControl1.TabStop = false;
            // 
            // fluentFormDefaultManager1
            // 
            this.fluentFormDefaultManager1.DockingEnabled = false;
            this.fluentFormDefaultManager1.Form = this;
            // 
            // BarpanelControl
            // 
            this.BarpanelControl.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.BarpanelControl.Appearance.Options.UseBackColor = true;
            this.BarpanelControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.BarpanelControl.Controls.Add(this.accordionControl1);
            this.BarpanelControl.Controls.Add(this.LogopictureEdit);
            this.BarpanelControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.BarpanelControl.Location = new System.Drawing.Point(0, 31);
            this.BarpanelControl.Name = "BarpanelControl";
            this.BarpanelControl.Size = new System.Drawing.Size(202, 688);
            this.BarpanelControl.TabIndex = 3;
            // 
            // accordionControl1
            // 
            this.accordionControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement1});
            this.accordionControl1.Location = new System.Drawing.Point(0, 101);
            this.accordionControl1.Margin = new System.Windows.Forms.Padding(2);
            this.accordionControl1.Name = "accordionControl1";
            this.accordionControl1.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Touch;
            this.accordionControl1.Size = new System.Drawing.Size(202, 587);
            this.accordionControl1.TabIndex = 5;
            this.accordionControl1.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            // 
            // accordionControlElement1
            // 
            this.accordionControlElement1.Name = "accordionControlElement1";
            this.accordionControlElement1.Text = "Element1";
            // 
            // LogopictureEdit
            // 
            this.LogopictureEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.LogopictureEdit.Location = new System.Drawing.Point(0, 0);
            this.LogopictureEdit.MenuManager = this.fluentFormDefaultManager1;
            this.LogopictureEdit.Name = "LogopictureEdit";
            this.LogopictureEdit.Properties.AllowFocused = false;
            this.LogopictureEdit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.LogopictureEdit.Properties.Appearance.Options.UseBackColor = true;
            this.LogopictureEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.LogopictureEdit.Properties.NullText = " ";
            this.LogopictureEdit.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.LogopictureEdit.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.LogopictureEdit.Size = new System.Drawing.Size(202, 101);
            this.LogopictureEdit.TabIndex = 4;
            // 
            // ViewpanelControl
            // 
            this.ViewpanelControl.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ViewpanelControl.Appearance.Options.UseBackColor = true;
            this.ViewpanelControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.ViewpanelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ViewpanelControl.Location = new System.Drawing.Point(202, 31);
            this.ViewpanelControl.Name = "ViewpanelControl";
            this.ViewpanelControl.Size = new System.Drawing.Size(1076, 688);
            this.ViewpanelControl.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1278, 719);
            this.Controls.Add(this.ViewpanelControl);
            this.Controls.Add(this.BarpanelControl);
            this.Controls.Add(this.fluentDesignFormControl1);
            this.FluentDesignFormControl = this.fluentDesignFormControl1;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "InnoLightReport";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(215)))), ((int)(((byte)(216)))));
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentFormDefaultManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarpanelControl)).EndInit();
            this.BarpanelControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LogopictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewpanelControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl fluentDesignFormControl1;
        private DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager fluentFormDefaultManager1;
        private DevExpress.XtraEditors.PictureEdit LogopictureEdit;
        private DevExpress.XtraEditors.PanelControl BarpanelControl;
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement1;
        private DevExpress.XtraEditors.PanelControl ViewpanelControl;
    }
}

