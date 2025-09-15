using Microsoft.Data.SqlClient;
using Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExempleAdoDigitalCity
{
    public class DBManager
    {
        private string _connectionString;
        public DBManager(string connectionString)
        {
             _connectionString=connectionString;   
        }
        public bool InsertJokes(Jokes j)
        {
            //connexion
            DbConnection oCon = new SqlConnection(_connectionString);
            // ouvrir
            try
            {
                oCon.Open();
            }
            catch (SqlException sqlex)
            {
                return false;
            }
            //si je suis connecté
            if (oCon.State == System.Data.ConnectionState.Open)
            {
                
                //commande
                DbCommand insertCommand = oCon.CreateCommand();

                // requête
                string maRequete = @"INSERT INTO JOKES (Title, Body) VALUES (@monTitre, @body)";

                //Version Longue
                DbParameter param2 = new SqlParameter();
                param2.ParameterName = "body";
                param2.Value = j.Body;

                // des infos a inserer
                insertCommand.CommandText = maRequete;
                //Version rapide mais moins lisible
                insertCommand.Parameters.Add(new SqlParameter("monTitre", j.Title));
                insertCommand.Parameters.Add(param2);
                // executer
                insertCommand.ExecuteNonQuery();
            }
            else
            {
                return false;
            }


                //fermer
                try
                {
                    oCon.Close();
                }
                catch (Exception ex)
                {
                return false;
                }
            return true;
        }

        public List<Jokes> GetAllJokes()
        {
            DbConnection connexion = new SqlConnection(connectionString);
            try
            {

                //Appeler la méthode qui permettra de se connecter
                //et Vérifier qu'on est connecte
                connexion.Open();

            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erreru de connexion db : {ex.Message}");
            }


            if (connexion.State == System.Data.ConnectionState.Open)
            {

                //Exécuter une requête select
                //string sqlQuery = @"GetJokes";
                //DbCommand command = connexion.CreateCommand();
                //command.CommandText = sqlQuery;
                //command.CommandType = System.Data.CommandType.StoredProcedure;
                //command.Parameters.Add(new SqlParameter("motcle", "vache"));

                try
                {
                    string sqlQuery = @"Select Id, Title, Body 
                        From Jokes";
                    DbCommand command = connexion.CreateCommand();
                    command.CommandText = sqlQuery;
                    //Afficher le résultat de ma requête
                    DbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int? id = reader["Id"] == DBNull.Value ? null : (int?)reader["Id"];
                        string? titre = reader["Title"] == DBNull.Value ? "" : reader["Title"].ToString();
                        Jokes j = new Jokes();
                        j.Id = id.Value;
                        j.Title = titre;
                        j.Body = reader["Body"] == DBNull.Value ? "" : reader["Body"].ToString(); ;
                        Console.WriteLine(titre);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }



            }

            //je ferme
            try
            {


                connexion.Close();
            }
            catch (SqlException sqex)
            {
                Console.WriteLine(sqex.Message);
            }
        }
    }
}
