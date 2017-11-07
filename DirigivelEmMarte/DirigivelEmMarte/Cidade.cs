namespace DirigivelEmMarte
{
  public class Cidade : ListaSimples<Cidade>
  {
    int cod, coordX, coordY;
    string nomeCidade;
    
    public Cidade(int cod, string nome, int cX, int cY)
    {
     this.cod = cod;
     this.nomeCidade = nome;
     this.coordX = cX;
     this.coordY = cY;
    }
    
    public int Cod
    {
      get{ return cod; }
      set{ cod = value; }
    }
    
    public int CoordX
    {
      get{ return coordX; }
      set{ coordX = value; }
    }
    
    public int CoordY
    {
      get{ return coordY; }
      set{ coordY = value; }
    }
    
    public string NomeCidade
    {
      get{ return nomeCidade; }
      set{ nomeCidade = value; }
    }
    
  }
}
