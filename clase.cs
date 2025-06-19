  public class Angajat
  {
      private string _nume;
      private int _oreLucrate;
      private float _salariuOrar;
      public const string _constanta = "A";
      private static int _nrAngajati = 0;
      private string _cod;

      public Angajat(string nume, int oreLucrate, float salariuOrar)
      {
          _nume = nume;
          _oreLucrate = oreLucrate;
          _salariuOrar = salariuOrar;

          _nrAngajati++;
          _cod = $"{_constanta}{_nrAngajati}";
      }

      public string Nume
      {
          get=> _nume;
          set => _nume = value;

      }

      public int OreLucrate
      {
          get => _oreLucrate;
          set => _oreLucrate = value;

      }

      public float SalariuOrar
      {
          get => _salariuOrar;
          set => _salariuOrar = value;
      }

      public string Cod
      {
          get => _cod;

      }

      public override string ToString()
      {
          return $" Cod: {_cod}, Nume: {_nume}, Ore lucrate: {_oreLucrate}, Salariu orar: {_salariuOrar}";
      }
  }
