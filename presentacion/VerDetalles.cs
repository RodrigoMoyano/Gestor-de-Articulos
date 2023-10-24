using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace presentacion
{
    public partial class VerDetalles : Form
    {
        public VerDetalles(Articulos seleccionado)
        {
            InitializeComponent();
           

            lblVerCodigo.Text = seleccionado.Codigo;
            lblVerNombre.Text = seleccionado.Nombre;
            lblVerDescripcion.Text = seleccionado.Descripcion;
            lblVerPrecio.Text = seleccionado.Precio.ToString();
            lblVerMarca.Text = seleccionado.Marca.Descripcion;
            lblVerTipo.Text = seleccionado.Tipo.Descripcion;
            cargarImagen(seleccionado.ImagenUrl);
            



        }
        



        private void btnDetallesAceptar_Click(object sender, EventArgs e)
        {
            Close();
        }
         private void cargarImagen(string imagen)
        {

             try
             {
                pbxDetallesImagen.Load(imagen);
             }
             catch (Exception)
            {

                pbxDetallesImagen.Load("https://commercial.bunn.com/img/image-not-available.png");

            }
         }

       
    }
}
