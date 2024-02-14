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
                cmd.CommandText = "SELECT * FROM Prenotazionicinema";

                // eseguo il comando e ottengo un dataset 

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // int idcliente = Convert.ToInt32(reader.GetString(0));
                    //string nome = reader.GetString(1);
                    //string bigliettiVenduti = reader.GetString(2);

                    int IDcliente = Convert.ToInt32(reader["IDcliente"]);
                    string nome = reader.GetString(1);
                    string cognome = reader.GetString(2);
                    string tiposala = reader.GetString(3);
                    bool ridotto = reader.GetBoolean(4);


                    clientiAcquistati.InnerHtml += $" AcquistoNum: {IDcliente} - Nome: {nome} - cognome: {cognome} - tipoSala: {tiposala} - prezzoRidotto: {ridotto} ";
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
            }
        }
    }
}