using dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using negocio;
using static System.Net.Mime.MediaTypeNames;
using System.Configuration;
using System.IO;


namespace presentacion
{
    public partial class frmAltaArticulos : Form

    {
        private Articulos articulos = null;
        private OpenFileDialog archivo = null;
        

        public frmAltaArticulos()
        {
            InitializeComponent();
        }

        public frmAltaArticulos(Articulos articulos )
        {
            InitializeComponent();
            this.articulos = articulos;
            Text = "Modificar Articulo";
        }
        



        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            
            
            ArticulosNegocio negocio = new ArticulosNegocio();

            

            try
            {
                if(articulos ==null)
                    articulos = new Articulos();

                articulos.Codigo = txtCodigo.Text;
                articulos.Nombre = txtNombre.Text;
                articulos.Descripcion = txtDescripcion.Text;
                articulos.ImagenUrl = txtImagenUrl.Text;
                articulos.Precio = decimal.Parse(txtPrecio.Text);
                articulos.Tipo = (Categorias)cboTipo.SelectedItem;
                articulos.Marca = (Marcas)cboMarca.SelectedItem;
                
                if(articulos.Id != 0)
                {
                    negocio.modificar(articulos);
                    MessageBox.Show("Modificado exitosamente");

                }
                else
                {
                    negocio.agregar(articulos);
                    MessageBox.Show("Agregado exitosamente");

                }

                if (archivo != null && !(txtImagenUrl.Text.ToUpper().Contains("HTTP")))
                    File.Copy(archivo.FileName, ConfigurationManager.AppSettings["images-folder"] + archivo.SafeFileName);



                Close();



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void frmAltaArticulos_Load(object sender, EventArgs e)
        {
            MarcasNegocio marcasNegocio = new MarcasNegocio();
            cboMarca.ValueMember = "Id";
            cboMarca.DisplayMember = "Descripcion";
            CategoriasNegocio categoriasNegocio = new CategoriasNegocio();
            cboTipo.ValueMember = "Id";
            cboTipo.DisplayMember = "Descripcion";






            try
            {
                cboTipo.DataSource = categoriasNegocio.listar();
                

                cboMarca.DataSource = marcasNegocio.listar();
                

                
                if(articulos != null)
                {
                    txtCodigo.Text = articulos.Codigo;
                    txtNombre.Text = articulos.Nombre;
                    txtDescripcion.Text = articulos.Descripcion;
                    txtImagenUrl.Text = articulos.ImagenUrl;
                    cargarImagen(articulos.ImagenUrl);
                    txtPrecio.Text = articulos.Precio.ToString();
                    cboTipo.SelectedValue = articulos.Tipo.Id;
                    cboMarca.SelectedValue = articulos.Marca.Id;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

            
        }


        private void txtImagenUrl_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtImagenUrl.Text);
        }
        
        private void cargarImagen(string imagen)
            {
                try
                {
                    pbxArticulo.Load(imagen);
                }
                catch (Exception ex)
                {

                    pbxArticulo.Load("https://commercial.bunn.com/img/image-not-available.png");
                    
                }
            }

        


        private void brtAgregarImagen_Click(object sender, EventArgs e)
        {
            archivo = new OpenFileDialog();
            archivo.Filter = "jpg|*.jpg|png|*.png";
            if(archivo.ShowDialog() == DialogResult.OK)
            {
                txtImagenUrl.Text = archivo.FileName;
                cargarImagen(archivo.FileName);

               
            }

        }

        
    }
}
