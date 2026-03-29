using System.Collections.Generic;

[System.Serializable]
public class Pedido
{
    public TipoPlato plato;

    public Bebida bebida;
    public List<Crema> cremas;
}
