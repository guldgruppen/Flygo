using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FlyGoWebService.Controllers
{
    public class ViewsController : ApiController
    {
        public string connectionString ="data source=flygodbserver.database.windows.net;initial catalog=FlyGoDB;persist security info=True;user id=Guldgruppen;password=Lort1234!;multipleactiveresultsets=True;application name=EntityFramework";


        public int GetAntalFejlSamlet()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM AntalFejlSamlet", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                using (reader)
                {
                    if (reader.Read())
                    {
                       return reader.GetInt32(0);
                    }
                }

            }
            return 0;
        }
        public int GetBaggerFejl()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM BaggersFejl", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                using (reader)
                {
                    if(reader.Read())
                    {
                       return reader.GetInt32(0);
                    }
                }

            }
            return 0;
        }
        public int GetBaggersForsinket()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM BaggersForsinket", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                using (reader)
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }

            }
            return 0;
        }
        public int GetBaggersKlargøringer()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM BaggersKlargøringer", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                using (reader)
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }

            }
            return 0;
        }
        public int GetCatersFejl()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM CatersFejl", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                using (reader)
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }

            }
            return 0;
        }
        public int GetCatersForsinket()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM CatersForsinket", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                using (reader)
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }

            }
            return 0;
        }
        public int GetCatersKlargøringer()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM CatersKlargøringer", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                using (reader)
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }

            }
            return 0;
        }
        public int GetCrewFejl()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM CrewFejl", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                using (reader)
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }

            }
            return 0;
        }
        public int GetCrewForsinket()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM CrewForsinket", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                using (reader)
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }

            }
            return 0;
        }
        public int GetCrewKlargøringer()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM CrewKlargøringer", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                using (reader)
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }

            }
            return 0;
        }
        public int GetFuelersFejl()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM FuelersFejl", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                using (reader)
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }

            }
            return 0;
        }
        public int GetFuelersForsinket()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM FuelersForsinket", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                using (reader)
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }

            }
            return 0;
        }
        public int GetFuelersKlargøringer()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM FuelsKlargøringer", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                using (reader)
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }

            }
            return 0;
        }
        public int GetKlargøringerIAlt()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM KlargøringerIAlt", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                using (reader)
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }

            }
            return 0;
        }
        public int GetMekanikerFejl()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM MekanikkerFejl", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                using (reader)
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }

            }
            return 0;
        }
        public int GetMekanikerForsinket()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM MekanikkerForsinket", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                using (reader)
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }

            }
            return 0;
        }
        public int GetmekanikerKlargøringer()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM MekanikkerKlargøringer", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                using (reader)
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }

            }
            return 0;
        }
        public int GetRedcapFejl()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM RedcapFejl", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                using (reader)
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }

            }
            return 0;
        }
        public int GetRedcapForsinket()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM RedcapForsinket", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                using (reader)
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }

            }
            return 0;
        }
        public int GetRedcapKlargøringer()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM RedcapKlargøringer", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                using (reader)
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }

            }
            return 0;
        }
        public int GetRengøringFejl()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM RengøringFejl", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                using (reader)
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }

            }
            return 0;
        }
        public int GetRengøringForsinket()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM RengøringForsinket", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                using (reader)
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }

            }
            return 0;
        }
        public int GetRengøringKlargøringer()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM RengøringKlargøringer", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                using (reader)
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }

            }
            return 0;
        }
        public int GetSamletForsinkelser()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM SamletForsinkelser", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                using (reader)
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }

            }
            return 0;
        }

    }
}
