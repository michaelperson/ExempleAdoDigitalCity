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
    }
}
