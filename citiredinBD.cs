namespace SubiectTutoring
{
    public class AngajatRepository
    {
        private string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Denisa\\Documents\\tutoring.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True";

        public List<Angajat> GetAngajat()
        {
            var list = new List<Angajat>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT Cod, Nume, Salariu, CodManager FROM Angajati", connection))
                {
                    var reader = command.ExecuteReader();
                    var angajat = new Angajat();
                    while (reader.Read())
                    {
                        angajat.Cod = reader.GetInt32(reader.GetOrdinal("Cod"));
                        angajat.Nume = reader.GetString(reader.GetOrdinal("Nume"));
                        angajat.Salariu = reader.GetDouble(reader.GetOrdinal("Salariu"));

                        int index = reader.GetOrdinal("CodManager");

                        if (!reader.IsDBNull(index))
                        {
                            angajat.CodManager = reader.GetInt32(index);
                        }
                        list.Add(angajat);
                    }

                }
                return list;
            }

        }
    }
}
