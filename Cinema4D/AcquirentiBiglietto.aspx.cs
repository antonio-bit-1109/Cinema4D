using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Cinema4D
{
    public partial class AcquirentiBiglietto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string StringaDiConnessione = ConfigurationManager.ConnectionStrings["STRING_CONNESSIONEDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(StringaDiConnessione);

            // qui prendo il parametro passato dall'altra pagina sull'ancora "<a>"
            string parametroTipoSala = Request.QueryString["tipoSala"];

            contenitoreDettagli.InnerHtml = string.Empty;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = $"SELECT nome , cognome FROM prenotazioniCinema where tiposala = '{parametroTipoSala}'";

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string nome = reader.GetString(0);
                    string cognome = reader.GetString(1);

                    //inserisco il template literals
                    contenitoreDettagli.InnerHtml += $"<div class='d-flex my-3'>\r\n  <p>{nome} {cognome}</p>\r\n <button class='btn btn-danger'  CommandArgument='{nome}' AutoPostBack='true' OnCommand='Delete_Click'  > Cancella Utente </button> </div> ";
                }
            }
            catch (Exception ex)
            {
                Response.Write("Errore");
                Response.Write(ex.Message);
            }
            finally { conn.Close(); }
        }

        protected void Delete_Click(object sender, CommandEventArgs e)
        {
            string nome = e.CommandArgument.ToString();

            Response.Write($" il mio parametro è {nome}");
        }
    }
}