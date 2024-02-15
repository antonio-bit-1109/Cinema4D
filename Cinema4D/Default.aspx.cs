using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace Cinema4D
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string StringaDiConnessione = ConfigurationManager.ConnectionStrings["STRING_CONNESSIONEDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(StringaDiConnessione);

            try
            {
                // apri connessione
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                //query da inviare al server 

                //cmd.CommandText = $"SELECT nome , count(*) from Prenotazionicinema group by tiposala , nome , idcliente";
                cmd.CommandText = "select tiposala , count(*) as spettacoloprenotato from PrenotazioniCinema group by tiposala";

                // eseguo il comando e ottengo un dataset 

                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {

                    string tiposala = reader.GetString(0);
                    int spettacoloPrenotato = reader.GetInt32(1);

                    contenitore.InnerHtml += $" <div>\r\n<p class='m-0'> Sala: {tiposala}</p> \r\n <a href='AcquirentiBiglietto.aspx?tipoSala={tiposala}' ><p class='m-0'> BigliettiAcquistati: {spettacoloPrenotato}</p> </a> \r\n  <p> Posti Rimanenti: {150 - spettacoloPrenotato} </p> </div>";

                    /*
                    int IDcliente = Convert.ToInt32(reader["IDcliente"]);
                    string nome = reader.GetString(1);
                    string cognome = reader.GetString(2);
                    string tiposala = reader.GetString(3);
                    bool ridotto = reader.GetBoolean(4);

                    string recordInfo = $"AcquistoNum: {IDcliente} - Nome: {nome} - Cognome: {cognome} - TipoSala: {tiposala} - PrezzoRidotto: {ridotto} <br>";

                    clientiAcquistati.InnerHtml += recordInfo;
                    */

                }


            }
            catch (Exception ex)
            {
                Response.Write("Errore");
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        protected void ConnettiAldb(object sender, EventArgs e)
        {
            string StringaDiConnessione = ConfigurationManager.ConnectionStrings["STRING_CONNESSIONEDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(StringaDiConnessione);

            try
            {
                conn.Open();
                //inserisci la query 
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = $"INSERT INTO PrenotazioniCinema (Nome , Cognome , TipoSala , Ridotto ) VALUES (@Nome, @Cognome, @TipoSala , @ridotto)";

                cmd.Parameters.AddWithValue("@Nome", nome.Text);
                cmd.Parameters.AddWithValue("@Cognome", cognome.Text);
                cmd.Parameters.AddWithValue("@TipoSala", DropDownList1.SelectedValue);
                cmd.Parameters.AddWithValue("@Ridotto", CheckBox1.Checked);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Response.Write("Errore");
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
                nome.Text = "";
                cognome.Text = "";
                CheckBox1.Checked = false;
                Response.Redirect("Default.aspx");
            }
        }
    }
}