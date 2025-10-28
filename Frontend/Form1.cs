using System.Data;
using ProductosDescontinuados.Services;
using ProductosDescontinuados.Models;

namespace ProductosDescontinuados
{
    public partial class Form1 : Form
    {
        private DataTable dtProductos;
        private readonly ApiService _apiService;

        public Form1()
        {
            InitializeComponent();
            _apiService = new ApiService("http://localhost:5053");
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            InicializarDataTable();
            ConfigurarDataGridView();
            await VerificarConexionAPI();
            txtCodigoBarras.Focus();
        }

        private async Task VerificarConexionAPI()
        {
            try
            {
                bool conectado = await _apiService.VerificarConexionAsync();
                
                if (!conectado)
                {
                    MessageBox.Show(
                        "?? No se pudo conectar con el servidor.\n\n" +
                        "El sistema no podrá consultar ni actualizar productos.\n\n" +
                        "Por favor, contacte al administrador del sistema.",
                        "Advertencia - Servidor no disponible",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                else
                {
                    MessageBox.Show(
                        "? Conexión establecida correctamente.\n\n" +
                        "El sistema está listo para usar.\n\n" +
                        "Puede comenzar a escanear productos.",
                        "Conexión Exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "?? Error al verificar conexión con el servidor.\n\n" +
                    $"Detalles: {ex.Message}\n\n" +
                    "Por favor, contacte al administrador del sistema.",
                    "Error de Conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void InicializarDataTable()
        {
            dtProductos = new DataTable();
            dtProductos.Columns.Add("id_producto", typeof(int));
            dtProductos.Columns.Add("codigo_barras", typeof(string));
            dtProductos.Columns.Add("nombre", typeof(string));
            dtProductos.Columns.Add("descripcion", typeof(string));
            dtProductos.Columns.Add("precio_venta", typeof(decimal));
            dtProductos.Columns.Add("precio_compra", typeof(decimal));
            dtProductos.Columns.Add("existencia", typeof(int));
            dtProductos.Columns.Add("descontinuado", typeof(bool));
            dtProductos.Columns.Add("fecha_registro", typeof(DateTime));

            dgvProductos.DataSource = dtProductos;
        }

        private void ConfigurarDataGridView()
        {
            if (dgvProductos.Columns.Count > 0)
            {
                dgvProductos.Columns["id_producto"].HeaderText = "ID";
                dgvProductos.Columns["id_producto"].Width = 60;
                
                dgvProductos.Columns["codigo_barras"].HeaderText = "Código de Barras";
                dgvProductos.Columns["codigo_barras"].Width = 130;
                
                dgvProductos.Columns["nombre"].HeaderText = "Nombre";
                dgvProductos.Columns["nombre"].Width = 150;
                
                dgvProductos.Columns["descripcion"].HeaderText = "Descripción";
                dgvProductos.Columns["descripcion"].Width = 200;
                
                dgvProductos.Columns["precio_venta"].HeaderText = "Precio Venta";
                dgvProductos.Columns["precio_venta"].DefaultCellStyle.Format = "C2";
                dgvProductos.Columns["precio_venta"].Width = 100;
                
                dgvProductos.Columns["precio_compra"].HeaderText = "Precio Compra";
                dgvProductos.Columns["precio_compra"].DefaultCellStyle.Format = "C2";
                dgvProductos.Columns["precio_compra"].Width = 100;
                
                dgvProductos.Columns["existencia"].HeaderText = "Existencia";
                dgvProductos.Columns["existencia"].Width = 80;
                
                dgvProductos.Columns["descontinuado"].HeaderText = "Descontinuado";
                dgvProductos.Columns["descontinuado"].Width = 100;
                
                dgvProductos.Columns["fecha_registro"].HeaderText = "Fecha Registro";
                dgvProductos.Columns["fecha_registro"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvProductos.Columns["fecha_registro"].Width = 110;
            }

            dgvProductos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 122, 204);
            dgvProductos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProductos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvProductos.EnableHeadersVisualStyles = false;
            dgvProductos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
        }

        private void txtCodigoBarras_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                _ = BuscarYAgregarProductoAsync();
            }
        }

        private async Task BuscarYAgregarProductoAsync()
        {
            if (string.IsNullOrWhiteSpace(txtCodigoBarras.Text))
            {
                MessageBox.Show("Por favor, escanee un código de barras.", 
                    "Campo Requerido", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);
                txtCodigoBarras.Focus();
                return;
            }

            string codigoBarras = txtCodigoBarras.Text.Trim();

            bool productoExiste = false;
            foreach (DataRow row in dtProductos.Rows)
            {
                if (row["codigo_barras"].ToString() == codigoBarras)
                {
                    productoExiste = true;
                    break;
                }
            }

            if (productoExiste)
            {
                MessageBox.Show($"El producto con código {codigoBarras} ya está en la lista.", 
                    "Producto Duplicado", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
                
                txtCodigoBarras.Clear();
                txtCodigoBarras.Focus();
                return;
            }

            txtCodigoBarras.Enabled = false;
            btnBuscar.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                var producto = await _apiService.ConsultarProductoAsync(codigoBarras);

                if (producto == null)
                {
                    MessageBox.Show(
                        $"? Producto con código '{codigoBarras}' no encontrado.\n\n" +
                        "Verifica que:\n" +
                        "• El código sea correcto\n" +
                        "• El producto exista en la base de datos\n" +
                        "• El producto no esté descontinuado", 
                        "Producto No Encontrado", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Warning);
                }
                else
                {
                    AgregarProductoAlGrid(
                        producto.IdProducto,
                        producto.CodigoBarras,
                        producto.Nombre,
                        producto.Descripcion ?? "",
                        producto.PrecioVenta,
                        0,
                        producto.Existencia
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"?? Error al consultar el producto:\n\n{ex.Message}",
                    "Error de Conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                txtCodigoBarras.Enabled = true;
                btnBuscar.Enabled = true;
                this.Cursor = Cursors.Default;
                txtCodigoBarras.Clear();
                txtCodigoBarras.Focus();
            }
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            await BuscarYAgregarProductoAsync();
        }

        private void txtCodigoBarras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
            }
        }

        private async void btnDescontinuar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un producto de la lista.", 
                    "Selección Requerida", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow filaSeleccionada = dgvProductos.SelectedRows[0];
            string codigoBarras = filaSeleccionada.Cells["codigo_barras"].Value?.ToString() ?? "";
            string nombreProducto = filaSeleccionada.Cells["nombre"].Value?.ToString() ?? "";

            DialogResult resultado = MessageBox.Show(
                $"¿Está seguro de que desea descontinuar el producto?\n\n" +
                $"Código: {codigoBarras}\n" +
                $"Nombre: {nombreProducto}\n\n" +
                "?? Esta acción marcará el producto como descontinuado en el sistema.", 
                "Confirmar Descontinuación", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                btnDescontinuar.Enabled = false;
                dgvProductos.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                try
                {
                    bool exito = await _apiService.DescontinuarProductoAsync(codigoBarras);

                    if (exito)
                    {
                        MessageBox.Show(
                            $"? Producto descontinuado exitosamente.\n\n" +
                            $"Código: {codigoBarras}\n" +
                            $"Nombre: {nombreProducto}", 
                            "Éxito", 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Information);

                        filaSeleccionada.Cells["descontinuado"].Value = true;
                        filaSeleccionada.DefaultCellStyle.BackColor = Color.LightGray;
                        filaSeleccionada.DefaultCellStyle.ForeColor = Color.DarkGray;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"?? Error al descontinuar el producto:\n\n{ex.Message}",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
                finally
                {
                    btnDescontinuar.Enabled = true;
                    dgvProductos.Enabled = true;
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void AgregarProductoAlGrid(int idProducto, string codigoBarras, string nombre, 
            string descripcion, decimal precioVenta, decimal precioCompra, int existencia)
        {
            dtProductos.Rows.Add(
                idProducto, 
                codigoBarras, 
                nombre, 
                descripcion,
                precioVenta,
                precioCompra,
                existencia,
                false,
                DateTime.Now
            );

            MessageBox.Show(
                $"? Producto agregado:\n\n" +
                $"Nombre: {nombre}\n" +
                $"Código: {codigoBarras}\n" +
                $"Precio: {precioVenta:C2}\n" +
                $"Existencia: {existencia}", 
                "Producto Agregado", 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Information);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _apiService?.Dispose();
        }
    }
}
