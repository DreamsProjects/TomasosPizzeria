using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows_programmering_RGB.Models;

namespace Windows_programmering_RGB.DAL
{
    public class DataReader
    {
        private SqlConnection conn = null;
        private const string Colors = "ColorCode";

        public DataReader()
        {
            var connectionString = Properties.Settings.Default.AppConnection;
            conn = new SqlConnection(connectionString);
        }

        public BindingList<ColorModel> GetColors(string CommandText, CommandType cmdType, SqlParameter[] param)
        {
            var colors = new BindingList<ColorModel>();

            using (conn)
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(CommandText, conn);
                cmd.CommandType = cmdType;

                if (param != null)
                {
                    cmd.Parameters.AddRange(param);
                }

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    colors.Add(new ColorModel
                    {
                        //RNumber = Convert.ToInt32(reader[Colors]),
                        //BNumber = Convert.ToInt32(reader[Colors]),
                        //GNumber = Convert.ToInt32(reader[Colors]),
                        StringCode = reader[Colors].ToString()
                        //ColorCode = reader[Colors].ToString(),
                    });
                }

                conn.Close();
            }

            return colors;
        }
    }

}
