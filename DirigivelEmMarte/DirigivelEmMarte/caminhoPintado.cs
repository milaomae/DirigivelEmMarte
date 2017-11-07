using System;
using System.Drawing;

namespace DirigivelEmMarte{
    
public class caminhoPintado : IComparable<caminhoPintado> {
    
    Color cor;
    Point p1, p2;
    
    public Point P1
    {
      get{ return p1; }
      set{ p1 = value; }
    }
    
    public Point P2
    {
      get{ return p2; }
      set{ p2 = value; }
    }
    
    public Color Cor
    {
      get{ return cor; }
      set{ cor = value; }
    }
    
    //construtor da classe
    public caminhoPintado(Color cor, Point p1, Point p2)
    {
      this.cor = cor;
      this.p1 = p1;
      this.p2 = p2;
    }
    
    public void DesenharCaminho(Color cor,Graphics g)
    {
      
      Pen pen = new Pen(cor);
      g.DrawLine(pen, p1, p2);
    
    }

        public int CompareTo(caminhoPintado other)
        {
            return 0;
        }
    }
}
