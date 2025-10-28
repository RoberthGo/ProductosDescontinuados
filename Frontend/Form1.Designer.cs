namespace ProductosDescontinuados
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblCodigoBarras = new System.Windows.Forms.Label();
            this.txtCodigoBarras = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.btnDescontinuar = new System.Windows.Forms.Button();
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.panelInferior = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.panelSuperior.SuspendLayout();
            this.panelInferior.SuspendLayout();
            this.SuspendLayout();
            
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(315, 30);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Gestión de Productos Descontinuados";
            
            this.lblCodigoBarras.AutoSize = true;
            this.lblCodigoBarras.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCodigoBarras.Location = new System.Drawing.Point(20, 60);
            this.lblCodigoBarras.Name = "lblCodigoBarras";
            this.lblCodigoBarras.Size = new System.Drawing.Size(127, 19);
            this.lblCodigoBarras.TabIndex = 1;
            this.lblCodigoBarras.Text = "Código de Barras:";
            
            this.txtCodigoBarras.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCodigoBarras.Location = new System.Drawing.Point(153, 57);
            this.txtCodigoBarras.Name = "txtCodigoBarras";
            this.txtCodigoBarras.Size = new System.Drawing.Size(300, 25);
            this.txtCodigoBarras.TabIndex = 2;
            this.txtCodigoBarras.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoBarras_KeyPress);
            this.txtCodigoBarras.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCodigoBarras_KeyUp);
            
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(470, 52);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(120, 35);
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProductos.BackgroundColor = System.Drawing.Color.White;
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProductos.Location = new System.Drawing.Point(0, 110);
            this.dgvProductos.MultiSelect = false;
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductos.Size = new System.Drawing.Size(1000, 390);
            this.dgvProductos.TabIndex = 4;
            
            this.btnDescontinuar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDescontinuar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDescontinuar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnDescontinuar.ForeColor = System.Drawing.Color.White;
            this.btnDescontinuar.Location = new System.Drawing.Point(20, 10);
            this.btnDescontinuar.Name = "btnDescontinuar";
            this.btnDescontinuar.Size = new System.Drawing.Size(200, 45);
            this.btnDescontinuar.TabIndex = 5;
            this.btnDescontinuar.Text = "DESCONTINUAR";
            this.btnDescontinuar.UseVisualStyleBackColor = false;
            this.btnDescontinuar.Click += new System.EventHandler(this.btnDescontinuar_Click);
            
            this.panelSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelSuperior.Controls.Add(this.lblTitulo);
            this.panelSuperior.Controls.Add(this.lblCodigoBarras);
            this.panelSuperior.Controls.Add(this.txtCodigoBarras);
            this.panelSuperior.Controls.Add(this.btnBuscar);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(1000, 110);
            this.panelSuperior.TabIndex = 6;
            
            this.panelInferior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelInferior.Controls.Add(this.btnDescontinuar);
            this.panelInferior.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelInferior.Location = new System.Drawing.Point(0, 500);
            this.panelInferior.Name = "panelInferior";
            this.panelInferior.Size = new System.Drawing.Size(1000, 65);
            this.panelInferior.TabIndex = 7;
            
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 565);
            this.Controls.Add(this.dgvProductos);
            this.Controls.Add(this.panelSuperior);
            this.Controls.Add(this.panelInferior);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Productos Descontinuados - Sistema de Gestión";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.panelInferior.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblCodigoBarras;
        private System.Windows.Forms.TextBox txtCodigoBarras;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.Button btnDescontinuar;
        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Panel panelInferior;
    }
}
