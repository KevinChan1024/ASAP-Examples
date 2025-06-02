using System;
using System.Windows.Forms;
using Kernel; 
using DevComponents.Editors;
using DevComponents.DotNetBar;

// Define a class that has at minimum a Main function (must be static)
public class Script
{
    public static void Main(IKernel kernel)
    {
        // Declare an input form
        MyForm myForm    = new MyForm();
        DialogResult ret = myForm.ShowDialog();
        
        if (ret == DialogResult.OK)
        {
            // Create an array of lenses
            int lensNum      = 0;
            int gridX        = myForm.columnInput.Value;
            int gridY        = myForm.rowInput.Value;
            double spacing   = myForm.spacingInput.Value;
            double curvature = myForm.curvatureInput.Value;
            double aperture  = myForm.apertureInput.Value;

            // Initialize system
            kernel.kSystem("New");
            kernel.kReset();
            kernel.kUnits("MM","Lumens");
            kernel.kWavelengths(0.55,"UM");
            
            // Loop through rows and columns
            for (int col = 0; col < gridX; col++)
            {
              for (int row = 0; row < gridY; row++)
              {
                lensNum++;
                kernel.kSurface ();
                kernel.kOptical ("Z", 0, curvature, "ELLIP", aperture);
                kernel.kShift (spacing*col, spacing*row, 0);

                // Create the name of each lens before making the surface an object
                string ObjectName = string.Format("'Lens{0}.{1}.{2}'", lensNum, col, row);
                kernel.kObject (ObjectName); 
              }
            }

            kernel.kWindow("Y","Z");
            kernel.kPlot("FACETS");
            kernel.k_View();
        }

    }
    
    // Define our input form
    public class MyForm : Form
    {        
        private LabelX label1;
        private LabelX label2;
        private LabelX label3;
        private LabelX label4;
        private LabelX label5;
        private ButtonX okButton;
        private ButtonX cancelButton;
        
        // Declare some public controls we can access
        public DoubleInput spacingInput;
        public DoubleInput curvatureInput;
        public DoubleInput apertureInput;
        public IntegerInput columnInput;
        public IntegerInput rowInput;

        public MyForm()
        {
            InitializeComponent();
        } 
        
        private void InitializeComponent()
        {
            this.label1         = new LabelX();
            this.label2         = new LabelX();
            this.label3         = new LabelX();
            this.label4         = new LabelX();
            this.label5         = new LabelX();
            this.spacingInput   = new DoubleInput();
            this.curvatureInput = new DoubleInput();
            this.apertureInput  = new DoubleInput();
            this.columnInput    = new IntegerInput();
            this.rowInput       = new IntegerInput();
            this.okButton       = new ButtonX();
            this.cancelButton   = new ButtonX();
            this.SuspendLayout();
            
            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 17);
            this.label1.Name     = "label1";
            this.label1.Size     = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 0;
            this.label1.Text     = "Grid columns";
            
            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 43);
            this.label2.Name     = "label2";
            this.label2.Size     = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 1;
            this.label2.Text     = "Grid rows";
            
            // label3
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 69);
            this.label3.Name     = "label3";
            this.label3.Size     = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 2;
            this.label3.Text     = "Lens spacing";
            
            // label4
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 95);
            this.label4.Name     = "label4";
            this.label4.Size     = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 3;
            this.label4.Text     = "Lens curvature";
            
            // label5
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 121);
            this.label5.Name     = "label5";
            this.label5.Size     = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 4;
            this.label5.Text     = "Lens aperture";
            
            // spacingInput
            this.spacingInput.BackgroundStyle.Class      = "DateTimeInputBackground";
            this.spacingInput.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.spacingInput.ButtonFreeText.Shortcut    = DevComponents.DotNetBar.eShortcut.F2;
            this.spacingInput.Increment                  = 1D;
            this.spacingInput.Location                   = new System.Drawing.Point(108, 65);
            this.spacingInput.Name                       = "spacingInput";
            this.spacingInput.ShowUpDown                 = true;
            this.spacingInput.Size                       = new System.Drawing.Size(80, 20);
            this.spacingInput.TabIndex                   = 7;
            
            // curvatureInput
            this.curvatureInput.BackgroundStyle.Class      = "DateTimeInputBackground";
            this.curvatureInput.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.curvatureInput.ButtonFreeText.Shortcut    = DevComponents.DotNetBar.eShortcut.F2;
            this.curvatureInput.Increment                  = 1D;
            this.curvatureInput.Location                   = new System.Drawing.Point(108, 91);
            this.curvatureInput.Name                       = "curvatureInput";
            this.curvatureInput.ShowUpDown                 = true;
            this.curvatureInput.Size                       = new System.Drawing.Size(80, 20);
            this.curvatureInput.TabIndex                   = 8;
            
            // apertureInput
            this.apertureInput.BackgroundStyle.Class      = "DateTimeInputBackground";
            this.apertureInput.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.apertureInput.ButtonFreeText.Shortcut    = DevComponents.DotNetBar.eShortcut.F2;
            this.apertureInput.Increment                  = 1D;
            this.apertureInput.Location                   = new System.Drawing.Point(108, 117);
            this.apertureInput.Name                       = "apertureInput";
            this.apertureInput.ShowUpDown                 = true;
            this.apertureInput.Size                       = new System.Drawing.Size(80, 20);
            this.apertureInput.TabIndex                   = 9;
            
            // columnInput
            this.columnInput.BackgroundStyle.Class      = "DateTimeInputBackground";
            this.columnInput.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.columnInput.ButtonFreeText.Shortcut    = DevComponents.DotNetBar.eShortcut.F2;
            this.columnInput.Location                   = new System.Drawing.Point(108, 13);
            this.columnInput.Name                       = "columnInput";
            this.columnInput.ShowUpDown                 = true;
            this.columnInput.Size                       = new System.Drawing.Size(80, 20);
            this.columnInput.TabIndex                   = 10;
            
            // rowInput
            this.rowInput.BackgroundStyle.Class      = "DateTimeInputBackground";
            this.rowInput.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rowInput.ButtonFreeText.Shortcut    = DevComponents.DotNetBar.eShortcut.F2;
            this.rowInput.Location                   = new System.Drawing.Point(108, 39);
            this.rowInput.Name                       = "rowInput";
            this.rowInput.ShowUpDown                 = true;
            this.rowInput.Size                       = new System.Drawing.Size(80, 20);
            this.rowInput.TabIndex                   = 11;
            
            // okButton
            this.okButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.okButton.ColorTable     = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.okButton.Location       = new System.Drawing.Point(23, 155);
            this.okButton.Name           = "okButton";
            this.okButton.Size           = new System.Drawing.Size(75, 23);
            this.okButton.Style          = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.okButton.TabIndex       = 12;
            this.okButton.Text           = "Ok";
            this.okButton.Click += new System.EventHandler(this.onOk);
            
            // cancelButton
            this.cancelButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cancelButton.ColorTable     = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cancelButton.DialogResult   = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location       = new System.Drawing.Point(113, 155);
            this.cancelButton.Name           = "cancelButton";
            this.cancelButton.Size           = new System.Drawing.Size(75, 23);
            this.cancelButton.Style          = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cancelButton.TabIndex       = 13;
            this.cancelButton.Text           = "Cancel";
            this.cancelButton.Click += new System.EventHandler(this.onCancel);
            
            // MyForm
            this.AcceptButton        = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton        = this.cancelButton;
            this.ClientSize          = new System.Drawing.Size(212, 190);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.rowInput);
            this.Controls.Add(this.columnInput);
            this.Controls.Add(this.apertureInput);
            this.Controls.Add(this.curvatureInput);
            this.Controls.Add(this.spacingInput);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name        = "MyForm";
            this.Text        = "Nested Loop Example";
            this.Load += new System.EventHandler(this.onLoadForm);
            this.ResumeLayout(false);
            this.PerformLayout();
        }  
                                
        // This is a callback from when the form appears
        private void onLoadForm(object sender, EventArgs e)
        {                 
            // Initialize default values
            columnInput.Value = 5;
            rowInput.Value = 5;
            spacingInput.Value = 20;
            apertureInput.Value = 6;
            curvatureInput.Value = 30;
        }

        // Callback from hitting OK button
        private void onOk(object sender, EventArgs e)
        {   
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // Callback from hitting cancel button or pressing escape key
        private void onCancel(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


    }
}

        
